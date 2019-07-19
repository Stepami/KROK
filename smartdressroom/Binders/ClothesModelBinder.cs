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
            var vcodeValue = bindingContext.ValueProvider.GetValue("VendorCode");
            var priceValue = bindingContext.ValueProvider.GetValue("Price");
            var sizesstrValue = bindingContext.ValueProvider.GetValue("SizesString");
            var brandValue = bindingContext.ValueProvider.GetValue("Brand");
            var formatValue = bindingContext.ValueProvider.GetValue("ImgFormat");
            var countValue = bindingContext.ValueProvider.GetValue("ImagesCount");
            var cIDValue = bindingContext.ValueProvider.GetValue("CollectionID");

            Guid id = Guid.Empty;
            if (idValue.FirstValue != null)
                id = Guid.Parse(idValue.FirstValue);
            string vcode = vcodeValue.FirstValue;
            int price = int.Parse(priceValue.FirstValue);
            string sizesstr = sizesstrValue.FirstValue;
            string brand = brandValue.FirstValue;
            string format = formatValue.FirstValue;
            int count = int.Parse(countValue.FirstValue);
            Guid collectionID = Guid.Parse(cIDValue.FirstValue);

            Models.ClothesModel model = new Models.ClothesModel(vcode, price, sizesstr, brand, format, count, collectionID);
            using (var context = new Storage.ApplicationContext())
            {
                if (id != Guid.Empty)
                    if (context.ClothesModels.Find(id) != null)
                        model.ID = id;
            }

            bindingContext.Result = ModelBindingResult.Success(model);

            return Task.CompletedTask;
        }
    }
}
