using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace smartdressroom.Models
{
    /// <summary>
    /// Одежда
    /// </summary>
    public class ClothesModel
    {
        /// <summary>
        /// Конструктор по умолчанию для сериализации
        /// </summary>
        public ClothesModel()
        {
        }

        /// <summary>
        /// Конструктор по значениям
        /// </summary>
        public ClothesModel(string vcode, int price, string sizesstr, string brand, string imgformat, int count, Guid collectionID)
        {
            ID = Guid.NewGuid(); // Формирование нового уникального идентификатора
            VendorCode = vcode;
            Price = price;
            SizesString = sizesstr;
            Brand = brand;
            ImgFormat = imgformat;
            ImagesCount = count;
            ImgPath = $"~/images/clothes/{brand}/{vcode}/{{0}}.{imgformat}";
            CollectionID = collectionID;
        }

        public ClothesModel(string vcode, int price)
        {
            Sizes = new string[] { string.Empty };
            Brand = string.Empty;
            ImgPath = "/images/scan_error.png";
        }

        [Key()]
        [JsonProperty("id")]
        public Guid ID { get; set; }

        [JsonProperty("vendorCode")]
        [Display(Name ="Артикул товара")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Это поле не может быть пустым.")]
        public string VendorCode { get; set; }

        [JsonProperty("price")]
        [Display(Name = "Цена")]
        [Range(1, int.MaxValue)]
        public int Price { get; set; }

        [NotMapped]
        [Display(Name = "Строка размеров")]
        [RegularExpression(@"[A-Z]+(\,[A-Z]*)*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Это поле не может быть пустым.")]
        public string SizesString { get => string.Join(',', Sizes); set => Sizes = value.Split(',', StringSplitOptions.RemoveEmptyEntries); }

        [JsonProperty("sizes")]
        [Display(Name = "Размеры")]
        public string[] Sizes { get; set; }

        [NotMapped]
        [JsonProperty("selectedSize")]
        public string SelectedSize { get; set; }

        [JsonProperty("brand")]
        [Display(Name = "Бренд")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Это поле не может быть пустым.")]
        public string Brand { get; set; }

        [JsonProperty("imgFormat")]
        [Display(Name = "Расширение картинки")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Это поле не может быть пустым.")]
        public string ImgFormat { get; set; }

        [JsonProperty("imagesCount")]
        [Display(Name = "Количество картинок в базе")]
        [Range(1, int.MaxValue)]
        public int ImagesCount { get; set; }

        [JsonProperty("img")]
        public string ImgPath { get; set; }

        [JsonProperty("collectionId")]
        [Display(Name = "ID коллекции")]
        public Guid CollectionID { get; set; }

        [JsonProperty("collection")]
        [Display(Name = "Коллекция")]
        public CollectionModel Collection { get; set; }
    }
}
