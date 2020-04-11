using System.Threading.Tasks;
using Ciceksepeti.Dto.ApiResponse;
using Ciceksepeti.Dto.Cart;

namespace Ciceksepeti.Business.BaseInterface
{
    public interface IUpdate
    {
        Task<ApiResponse> Update(CartRequestDto entity);
    }
}
