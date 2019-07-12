using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace smartdressroom.Binders
{
    public class ClothesModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var codeValue = bindingContext.ValueProvider.GetValue("Code");
            var priceValue = bindingContext.ValueProvider.GetValue("Price");
            var sizeValue = bindingContext.ValueProvider.GetValue("Size");
            var brandValue = bindingContext.ValueProvider.GetValue("Brand");
            var formatValue = bindingContext.ValueProvider.GetValue("ImgFormat");
            var cIDValue = bindingContext.ValueProvider.GetValue("CollectionID");

            int code = int.Parse(codeValue.FirstValue);
            int price = int.Parse(priceValue.FirstValue);
            string size = sizeValue.FirstValue;
            string brand = brandValue.FirstValue;
            string format = formatValue.FirstValue;
            Guid collectionID = Guid.Parse(cIDValue.FirstValue);

            bindingContext.Result = ModelBindingResult.Success(
                new Models.ClothesModel(code, price, size, brand, format, collectionID));

            return Task.CompletedTask;
        }
    }
}
