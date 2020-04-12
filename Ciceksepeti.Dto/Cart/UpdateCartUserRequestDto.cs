using System;
using System.Collections.Generic;
using System.Text;

namespace Ciceksepeti.Dto.Cart
{
    public class UpdateCartUserRequestDto
    {
        public Guid UserId { get; set; }

        public Guid SessionId { get; set; }
    }
}
