using Ciceksepeti.Dto.Cart;
using FluentValidation;
using System;

namespace Ciceksepeti.Business.ValidationRules
{
    /// <summary>
    /// Cart ürün ekleme için validasyon kuralları
    /// </summary>
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

            RuleFor(x => x.UserId)
                .Custom((userId, context) => 
                { 
                    var parseGuidControl = Guid.TryParse(userId.ToString(), out var parseGuid); 
                    if (!parseGuidControl || parseGuid == Guid.Empty) 
                    { 
                        context.AddFailure("Hatalı Kullanıcı Numarası"); 
                    } 
                });
        }
    }
}
