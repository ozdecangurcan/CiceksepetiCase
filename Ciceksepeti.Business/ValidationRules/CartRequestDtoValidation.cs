using Ciceksepeti.Dto.Cart;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ciceksepeti.Business.ValidationRules
{
    public class CartRequestDtoValidation : AbstractValidator<CartRequestDto>
    {
        public CartRequestDtoValidation()
        {
            RuleFor(x => x.Quantity).NotEmpty();
            RuleFor(x => x.ProductId)
                .Custom((productId, context) =>
                {
                    var parseGuidControl = Guid.TryParse(productId.ToString(), out var parseGuid);

                    if (!parseGuidControl || parseGuid == Guid.Empty)
                    {
                        context.AddFailure("Hatalı Ürün Numarası");
                    }
                });
        }
    }
}
