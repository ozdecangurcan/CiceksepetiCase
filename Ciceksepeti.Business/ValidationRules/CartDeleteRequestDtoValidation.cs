using Ciceksepeti.Dto.Cart;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ciceksepeti.Business.ValidationRules
{
    public class CartDeleteRequestDtoValidation: AbstractValidator<CartDeleteRequestDto>
    {
        public CartDeleteRequestDtoValidation()
        {
            RuleFor(x => x.Id)
                .Custom((id, context) =>
                {
                    var parseGuidControl = Guid.TryParse(id.ToString(), out var parseGuid);

                    if (!parseGuidControl || parseGuid == Guid.Empty)
                    {
                        context.AddFailure("Hatalı Id");
                    }
                });
        }
    }
}
