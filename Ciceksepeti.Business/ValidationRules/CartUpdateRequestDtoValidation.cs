using Ciceksepeti.Dto.Cart;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ciceksepeti.Business.ValidationRules
{
    /// <summary>
    /// Cart ürün güncelleme için validasyon kuralları
    /// </summary>
    public class CartUpdateRequestDtoValidation : AbstractValidator<CartUpdateRequestDto>
    {
        public CartUpdateRequestDtoValidation()
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

            RuleFor(x => x.UserId)
                .Custom((userId, context) =>
                {
                    var parseGuidControl = Guid.TryParse(userId.ToString(), out var parseGuid);

                    if (!parseGuidControl || parseGuid == Guid.Empty)
                    {
                        context.AddFailure("Hatalı Kullanıcı Numarası");
                    }
                });

            RuleFor(x => x.ProductId)
                .Custom((productId, context) =>
                {
                    var parseGuidControl = Guid.TryParse(productId.ToString(), out var parseGuid);

                    if (!parseGuidControl || parseGuid == Guid.Empty)
                    {
                        context.AddFailure("Hatalı Ürün Numarası");
                    }
                });

            RuleFor(x => x.Quantity).NotEmpty();
        }
    }
}
