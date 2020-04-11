using AutoMapper;
using Ciceksepeti.Dto.Cart;
using Ciceksepeti.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ciceksepeti.Business.AutoMapper
{
    public class ApiMapperProfile:Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<CartRequestDto, Cart>().ReverseMap();
            CreateMap<CartRequestDto, CartResponseDto>().ReverseMap();
        }
    }
}
