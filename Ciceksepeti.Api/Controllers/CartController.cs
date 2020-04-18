using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ciceksepeti.Business.Interface;
using Ciceksepeti.Dto.ApiResponse;
using Ciceksepeti.Dto.Cart;
using Microsoft.AspNetCore.Mvc;

namespace Ciceksepeti.Api.Controllers
{
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Kullanıcıya ait sepeti listeler
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("CartList")]
        public async Task<ApiResponse> GetCartListByUserIdAsync(Guid userId)
        {
            try
            {
                return await _cartService.GetAll(userId);
            }
            catch (Exception ex)
            {

                return ApiResponse.ReturnAsFail(data: null, ex.Message);
            }
            
        }

        /// <summary>
        /// Sepete ürün ekler
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("AddItem")]
        public async Task<ApiResponse> AddItemToCardAsync([FromBody]CartRequestDto request)
        {
            try
            {
                return await _cartService.Add(request);
            }
            catch (Exception ex)
            {

                return ApiResponse.ReturnAsFail(data: null, ex.Message);
            }
        }

        /// <summary>
        /// Sepetten ürün günceller
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("UpdateItem")]
        public ApiResponse UpdateCartItem([FromBody]CartUpdateRequestDto request)
        {
            try
            {
                return _cartService.Update(request);
            }
            catch (Exception ex)
            {
                return ApiResponse.ReturnAsFail(data: null, ex.Message);
            }
        }

        /// <summary>
        /// Sepetten ürün siler
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("DeleteItem")]
        public ApiResponse DeleteCartItem([FromBody]CartDeleteRequestDto request)
        {
            try
            {
                return _cartService.Delete(request);
            }
            catch (Exception ex)
            {

                return ApiResponse.ReturnAsFail(data: null, ex.Message);
            }
        }

        /// <summary>
        /// Kullanıcı login olduktan sonra login olmadan önce sepetine eklediği ürünleri kullanıcının sepetine atar
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("UpdateCartUser")]
        public ApiResponse UpdateCartUser([FromBody]UpdateCartUserRequestDto request)
        {
            try
            {
                return _cartService.UpdateCartUser(request);
            }
            catch (Exception ex)
            {
                return ApiResponse.ReturnAsFail(data: null, ex.Message);
            }
        }
    }
}
