using System;

namespace Ciceksepeti.Dto.Cart
{
    public class CartRequestDto
    {
        public Guid ProductId { get; set; }

        public Guid UserId { get; set; }

        public int Quantity { get; set; }
    }
}
