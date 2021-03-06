﻿using Microsoft.AspNetCore.Http;
using smartdressroom.Models;

namespace smartdressroom.Services
{
    public interface ICartService
    {
        CartModel GetCart(ISession session);
        void SetCart(CartModel cart, ISession session);
        void ClearCart(ISession session);
    }
}
