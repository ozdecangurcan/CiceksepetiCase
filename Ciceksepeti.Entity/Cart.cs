using System;
using System.ComponentModel.DataAnnotations;

namespace Ciceksepeti.Entity
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Guid UserId { get; set; }

        public int Quantity { get; set; }
    }
}
