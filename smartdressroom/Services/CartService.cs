using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using smartdressroom.Models;

namespace smartdressroom.Services
{
    public class CartService : ICartService
    {
        private const string key = "cart";

        public CartModel GetCart(ISession session)
        {
            CartModel cart = null;

            if (session != null)
            {
                if (session.Keys.Contains(key))
                {
                    string json = session.GetString(key);
                    cart = CartModel.FromJson(json);
                }
                else
                {
                    cart = new CartModel();
                    string json = cart.ToJson();
                    session.SetString(key, json);
                }
            }

            return cart;
        }

        public void SetCart(CartModel cart, ISession session) => session.SetString(key, cart.ToJson());

        public void ClearCart(ISession session)
        {
            var cart = GetCart(session);
            cart.Clear();
            SetCart(cart, session);
        }
    }
}
