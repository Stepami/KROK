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

            var idValue = bindingContext.ValueProvider.GetValue("ID");
            var codeValue = bindingContext.ValueProvider.GetValue("Code");
            var priceValue = bindingContext.ValueProvider.GetValue("Price");
            var sizeValue = bindingContext.ValueProvider.GetValue("Size");
            var brandValue = bindingContext.ValueProvider.GetValue("Brand");
            var formatValue = bindingContext.ValueProvider.GetValue("ImgFormat");
            var cIDValue = bindingContext.ValueProvider.GetValue("CollectionID");

            Guid id = Guid.Parse(idValue.FirstValue);
            int code = int.Parse(codeValue.FirstValue);
            int price = int.Parse(priceValue.FirstValue);
            string size = sizeValue.FirstValue;
            string brand = brandValue.FirstValue;
            string format = formatValue.FirstValue;
            Guid collectionID = Guid.Parse(cIDValue.FirstValue);

            Models.ClothesModel model = new Models.ClothesModel(code, price, size, brand, format, collectionID);
            using (var context = new Storage.ApplicationContext())
            {
                if (context.ClothesModels.Find(id) != null)
                    model.ID = id;
            }

            bindingContext.Result = ModelBindingResult.Success(model);

            return Task.CompletedTask;
        }
    }
}
