
using Ciceksepeti.Core.Repository;
using Ciceksepeti.Dto.ApiResponse;
using Ciceksepeti.Dto.Cart;
using System;

namespace Ciceksepeti.DataAccess.Interface
{
    public interface ICartRepository : IAdd, IDelete, IUpdate, IGetAll
    {
        //Custom Cart Methods

        int CartSize(Guid userId);

        ApiResponse UpdateCartUser(UpdateCartUserRequestDto request);
    }
}
