﻿using Ciceksepeti.Dto.ApiResponse;
using Ciceksepeti.Dto.Cart;
using System;
using System.Threading.Tasks;

namespace Ciceksepeti.Business.Interface
{
    /// <summary>
    /// Cart işlemlerine ait CRUD operasyonları
    /// </summary>
    public interface ICartService
    {
        Task<ApiResponse> Add(CartRequestDto requet);

        ApiResponse Update(CartUpdateRequestDto request);

        Task<ApiResponse> GetAll(Guid userId);

        ApiResponse Delete(CartDeleteRequestDto request);

        ApiResponse UpdateCartUser(UpdateCartUserRequestDto request);
    }
}
