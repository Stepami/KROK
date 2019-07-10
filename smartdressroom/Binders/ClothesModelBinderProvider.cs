using Microsoft.AspNetCore.Mvc.ModelBinding;
using smartdressroom.Models;

namespace smartdressroom.Binders
{
    public class ClothesModelBinderProvider : IModelBinderProvider
    {
        private readonly IModelBinder binder = new ClothesModelBinder();

        public IModelBinder GetBinder(ModelBinderProviderContext context) => context.Metadata.ModelType == typeof(ClothesModel) ? binder : null;
    }
}
