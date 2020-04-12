using Ciceksepeti.Business.Interface;
using Ciceksepeti.DataAccess.Interface;
using Ciceksepeti.Dto.ApiResponse;
using Ciceksepeti.Dto.Cart;
using Ciceksepeti.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ciceksepeti.Business.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private const int CART_SIZE_LIMIT = 5;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        /// <summary>
        /// Sepete Ürün Ekler
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResponse> Add(CartRequestDto request)
        {
            //Sepet boyutu aşıldı mı
            if (!IsCartSizeOver(request.UserId))
            {
                return ApiResponse.ReturnAsFail(data: null, "Toplam sepet kapasitesini aştınız");
            }

            //Eklenen ürün stokda var mı
            if (!IsItemInStock(request.ProductId))
            {
                return ApiResponse.ReturnAsFail(data: null, "Eklenen Ürün Stokta Yoktur");
            }

            //Ürüne ait maksimum ekleme sınırı aşıldı mı
            if (IsItemMaxAdded(request.ProductId, request.Quantity))
            {
                var product = ProductService.GetProducts().FirstOrDefault(x => x.ProductId == request.ProductId);

                //Eklenen ürün adedi max eklenebilecekten büyükse bunu max eklenebilecek ürüne eşitle
                if (request.Quantity > product.MaxCartQuantity)
                {
                    request.Quantity = product.MaxCartQuantity;
                }

                //Eklenen ürün adedi stok adedinden büyükse eklenen ürün adedini stoğa eşitle
                if (request.Quantity > product.Quantity)
                {
                    request.Quantity = product.Quantity;
                }
            }

            var entity = new Cart
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                UserId = request.UserId
            };

            var result = await _cartRepository.Add(entity);

            return result;
        }

        /// <summary>
        /// Sepette mevcut olan ürünü günceller
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ApiResponse Update(CartUpdateRequestDto request)
        {
            //Eklenen ürün stokda var mı
            if (!IsItemInStock(request.ProductId))
            {
                return ApiResponse.ReturnAsFail();
            }

            //Ürüne ait maksimum ekleme sınırı aşıldı mı
            if (IsItemMaxAdded(request.ProductId, request.Quantity))
            {
                var product = ProductService.GetProducts().FirstOrDefault(x => x.ProductId == request.ProductId);

                //Eklenen ürün adedi max eklenebilecekten büyükse bunu max eklenebilecek ürüne eşitle
                if (request.Quantity > product.MaxCartQuantity)
                {
                    request.Quantity = product.MaxCartQuantity;
                }

                //Eklenen ürün adedi stok adedinden büyükse eklenen ürün adedini stoğa eşitle
                if (request.Quantity > product.Quantity)
                {
                    request.Quantity = product.Quantity;
                }
            }

            var entity = new Cart
            {
                Id = request.Id,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                UserId = request.UserId
            };

            var result = _cartRepository.Update(entity);

            return result;
        }

        /// <summary>
        /// Verilen kullanıcıya ait sepetteki ürünleri döner
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ApiResponse> GetAll(Guid userId)
        {
            if (!GuidIsValid(userId))
            {
                return ApiResponse.ReturnAsNotFound();
            }

            var result = await _cartRepository.GetAll(userId);

            return result;
        }

        /// <summary>
        /// Sepette mevcut olan ürünü siler
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ApiResponse Delete(CartDeleteRequestDto request)
        {
            var entity = new Cart
            {
                Id = request.Id
            };
            var result = _cartRepository.Delete(entity);
            return result;
        }

        /// <summary>
        /// Kullanıcının login olmadan önce eklediği ürünleri login olduktan sonra kullanıcnın ID'sine atar
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ApiResponse UpdateCartUser(UpdateCartUserRequestDto request)
        {
            var result = _cartRepository.UpdateCartUser(request);

            return result;
        }

        #region Private Methods

        /// <summary>
        /// Ürün stok kontrolü
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        private bool IsItemInStock(Guid productId)
        {
            return ProductService.GetProducts().FirstOrDefault(x => x.ProductId == productId).Quantity > 0;
        }

        /// <summary>
        /// Ürüne ait maximum sepete ekleme adedi kontrolü
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        private bool IsItemMaxAdded(Guid productId, int quantity)
        {
            return ProductService.GetProducts().FirstOrDefault(x => x.ProductId == productId).MaxCartQuantity <= quantity;
        }

        /// <summary>
        /// Sepet kapasitesi kontrolü
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private bool IsCartSizeOver(Guid customerId)
        {
            return _cartRepository.CartSize(customerId) < CART_SIZE_LIMIT;
        }

        /// <summary>
        /// Guid kontrolü
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        private bool GuidIsValid(Guid guid)
        {
            var parseGuidControl = Guid.TryParse(guid.ToString(), out var parseGuid);

            if (!parseGuidControl || parseGuid == Guid.Empty)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
