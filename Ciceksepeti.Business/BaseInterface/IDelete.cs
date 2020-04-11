using System;
using System.Threading.Tasks;
using Ciceksepeti.Dto.ApiResponse;

namespace Ciceksepeti.Business.BaseInterface
{
    public interface IDelete
    {
        Task<ApiResponse> Delete(Guid guid);
    }
}
