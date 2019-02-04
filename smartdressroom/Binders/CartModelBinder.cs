using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using smartdressroom.Models;

namespace smartdressroom.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string key = "cart";
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            CartModel cart = null;
            var session = bindingContext.HttpContext.Session;

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

            bindingContext.Result = ModelBindingResult.Success(cart);
            return Task.CompletedTask;
        }
    }
}
