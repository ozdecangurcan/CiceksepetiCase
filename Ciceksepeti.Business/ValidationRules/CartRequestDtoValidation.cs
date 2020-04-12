﻿using Ciceksepeti.Dto.Cart;
using FluentValidation;
using System;

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

            RuleFor(x => x.UserId)
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
