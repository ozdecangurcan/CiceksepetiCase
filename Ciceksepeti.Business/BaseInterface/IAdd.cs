using Ciceksepeti.Dto.ApiResponse;
using System.Threading.Tasks;
using Ciceksepeti.Dto.Cart;

namespace Ciceksepeti.Business.BaseInterface
{
    public interface IAdd
    {
        Task<ApiResponse> Add(CartRequestDto cartRequest);
    }
}
