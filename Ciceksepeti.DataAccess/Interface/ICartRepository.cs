
using Ciceksepeti.Core.Repository;
using Ciceksepeti.Dto.ApiResponse;
using Ciceksepeti.Dto.Cart;
using System;

namespace Ciceksepeti.DataAccess.Interface
{
    /// <summary>
    /// Sepet Repo'suna ait custom işlemlerin tanımlandığı interface
    /// </summary>
    public interface ICartRepository : IAdd, IDelete, IUpdate, IGetAll
    {
        int CartSize(Guid userId);

        ApiResponse UpdateCartUser(UpdateCartUserRequestDto request);
    }
}
