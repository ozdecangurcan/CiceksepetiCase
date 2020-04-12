using Ciceksepeti.Dto.Cart;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ciceksepeti.Business.ValidationRules
{
    public class UpdateCartUserDtoValidation : AbstractValidator<UpdateCartUserRequestDto>
    {
        public UpdateCartUserDtoValidation()
        {
            RuleFor(x => x.SessionId)
                .Custom((productId, context) =>
                {
                    var parseGuidControl = Guid.TryParse(productId.ToString(), out var parseGuid);

                    if (!parseGuidControl || parseGuid == Guid.Empty)
                    {
                        context.AddFailure("Hatalı Ürün Numarası");
                    }
                });

            RuleFor(x => x.UserId)
                .Custom((productId, context) =>
                {
                    var parseGuidControl = Guid.TryParse(productId.ToString(), out var parseGuid);

                    if (!parseGuidControl || parseGuid == Guid.Empty)
                    {
                        context.AddFailure("Hatalı Kullanıcı Numarası");
                    }
                });
        }
    }
}
