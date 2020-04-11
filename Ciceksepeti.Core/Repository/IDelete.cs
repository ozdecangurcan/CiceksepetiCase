using Ciceksepeti.Dto.ApiResponse;
using Ciceksepeti.Entity;
using System.Threading.Tasks;

namespace Ciceksepeti.Core.Repository
{
    public interface IDelete
    {
        Task<ApiResponse> Delete(Cart entity);
    }
}
