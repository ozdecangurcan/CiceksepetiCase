using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ciceksepeti.Dto.ApiResponse;
using Ciceksepeti.Entity;

namespace Ciceksepeti.Business.BaseInterface
{
    public interface IList
    {
        Task<ApiResponse> ListAsync(Expression<Func<Cart, bool>> filter=null);
    }
}
