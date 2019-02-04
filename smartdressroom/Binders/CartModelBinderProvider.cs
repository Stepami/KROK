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
