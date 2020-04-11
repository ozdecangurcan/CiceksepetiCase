using Ciceksepeti.Dto.ApiResponse;
using Ciceksepeti.Dto.Cart;
using System;
using System.Threading.Tasks;

namespace Ciceksepeti.Business.Interface
{
    public interface ICartService
    {
        Task<ApiResponse> Add(CartRequestDto requet);

        ApiResponse Update(CartRequestDto request);

        Task<ApiResponse> GetAll(Guid userId);

        ApiResponse Delete(CartRequestDto request);

        ApiResponse UpdateCartUser(UpdateCartUserDto request);
    }
}
