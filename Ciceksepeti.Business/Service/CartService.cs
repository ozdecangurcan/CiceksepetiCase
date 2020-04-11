using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ciceksepeti.Business.Interface;
using Ciceksepeti.DataAccess;
using Ciceksepeti.DataAccess.Interface;
using Ciceksepeti.Dto.ApiResponse;
using Ciceksepeti.Dto.Cart;
using Ciceksepeti.Entity;

namespace Ciceksepeti.Business.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(CartContext context, ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<ApiResponse> Add(CartRequestDto request)
        {
            //Sepete boyutu aşıldı mı
            if (!IsCartSizeOver(request.UserId))
            {
                return ApiResponse.ReturnAsFail();
            }

            //Eklenen ürün stokda var mı
            if (!IsItemInStock(request.ProductId))
            {
                return ApiResponse.ReturnAsFail();
            }

            //Ürünü ait maksimum ekleme sınırı aşıldı mı
            if (!IsItemMaxAdded(request.ProductId, request.Quantity))
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

            var entity = request.MapTo<Cart>();
            var result = await _cartRepository.Add(entity);
            return result;
        }

        public ApiResponse Update(CartRequestDto request)
        {
            //Eklenen ürün stokda var mı
            if (!IsItemInStock(request.ProductId))
            {
                return ApiResponse.ReturnAsFail();
            }

            //Ürünü ait maksimum ekleme sınırı aşıldı mı
            if (!IsItemMaxAdded(request.ProductId, request.Quantity))
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

            var entity = request.MapTo<Cart>();
            var result = _cartRepository.Update(entity);

            return result;
        }

        public async Task<ApiResponse> GetAll(Guid userId)
        {
            var result = await _cartRepository.GetAll(userId);

            return result;
        }

        public async Task<ApiResponse> Delete(CartRequestDto request)
        {
            var entity = request.MapTo<Cart>();
            var result = await _cartRepository.Delete(entity);
            return result;
        }

        public ApiResponse UpdateCartUser(UpdateCartUserDto request)
        {
            var result = _cartRepository.UpdateCartUser(request);

            return result;
        }

        #region Private Methods

        private bool IsItemInStock(Guid productId)
        {
            return ProductService.GetProducts().FirstOrDefault(x => x.ProductId == productId).Quantity > 0;
        }

        private bool IsItemMaxAdded(Guid productId, int quantity)
        {
            return ProductService.GetProducts().FirstOrDefault(x => x.ProductId == productId).MaxCartQuantity <= quantity;
        }

        private bool IsCartSizeOver(Guid customerId)
        {
            return _cartRepository.CartSize(customerId) > 30;
        }

        #endregion
    }
}
