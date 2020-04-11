using Ciceksepeti.Dto.ApiResponse;
using System;
using System.Threading.Tasks;

namespace Ciceksepeti.Core.Repository
{
    public interface IGetAll
    {
        Task<ApiResponse> GetAll(Guid customerId);
    }
}
