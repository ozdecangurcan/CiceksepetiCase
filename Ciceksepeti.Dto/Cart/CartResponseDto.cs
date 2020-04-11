using System;

namespace Ciceksepeti.Dto.Cart
{
    public class CartResponseDto
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Guid UserId { get; set; }

        public int Quantity { get; set; }
    }
}
