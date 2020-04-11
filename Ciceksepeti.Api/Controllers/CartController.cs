using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ciceksepeti.Business.Interface;
using Ciceksepeti.Dto.ApiResponse;
using Ciceksepeti.Dto.Cart;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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


        [HttpPut("UpdateItem")]
        public ApiResponse UpdateCartItem([FromBody]CartRequestDto request)
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


        [HttpDelete("DeleteItem")]
        public ApiResponse DeleteCartItem([FromBody]CartRequestDto request)
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

        [HttpPut("UpdateCartUser")]
        public ApiResponse UpdateCartUser([FromBody]UpdateCartUserDto request)
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
