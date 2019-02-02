using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using smartdressroom.Models;

namespace smartdressroom.Binders
{
    public class CartModelBinderProvider : IModelBinderProvider
    {
        private readonly IModelBinder binder = new CartModelBinder();

        public IModelBinder GetBinder(ModelBinderProviderContext context) => context.Metadata.ModelType == typeof(CartModel) ? binder : null;
    }
}
