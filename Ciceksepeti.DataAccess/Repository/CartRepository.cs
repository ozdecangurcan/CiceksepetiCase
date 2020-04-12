using Ciceksepeti.DataAccess.Interface;
using Ciceksepeti.Dto.ApiResponse;
using Ciceksepeti.Dto.Cart;
using Ciceksepeti.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ciceksepeti.DataAccess.Service
{
    public class CartRepository : ICartRepository
    {
        private readonly CartContext _context;

        public CartRepository(CartContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> Add(Cart entity)
        {
            _context.Add(entity);
            var affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0 ? ApiResponse.ReturnAsSuccess(data: entity) : ApiResponse.ReturnAsFail();
        }

        public int CartSize(Guid userId)
        {
            
           return  _context.Carts.Where(x => x.UserId == userId).Count();
        }

        public ApiResponse Delete(Cart entity)
        {
            _context.Remove(entity);
            var affectedRows = _context.SaveChanges();
            return ApiResponse.ReturnAsSuccess(data: affectedRows);
        }

        public async Task<ApiResponse> GetAll(Guid userId)
        {
            var cartItems = await _context.Carts.Where(x => x.UserId == userId).ToListAsync();
            return !cartItems.Any() ? ApiResponse.ReturnAsNotFound() : ApiResponse.ReturnAsSuccess(cartItems);
        }

        public ApiResponse Update(Cart entity)
        {
            _context.Update(entity);
            var affectedRows = _context.SaveChanges();
            return ApiResponse.ReturnAsSuccess(data: affectedRows);
        }

        public ApiResponse UpdateCartUser(UpdateCartUserDto request)
        {
            var cart = _context.Carts.Where(x => x.UserId == request.SessionId).ToList();
            cart.ForEach(x => x.UserId = request.UserId);
            _context.UpdateRange(cart);
            var affectedRows = _context.SaveChanges();
            return  ApiResponse.ReturnAsSuccess(data: affectedRows);
        }
    }
}
