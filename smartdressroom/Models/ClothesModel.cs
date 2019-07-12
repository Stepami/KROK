using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

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
        public ClothesModel(int code, int price, string size, string brand, string imgformat, Guid collectionID)
        {
            ID = Guid.NewGuid();// Формирование нового уникального идентификатора
            Code = code;
            Price = price;
            Size = size;
            Brand = brand;
            ImgFormat = imgformat;
            ImgPath = $"~/images/clothes/{brand}/{code}.{imgformat}";
            CollectionID = collectionID;
        }

        public ClothesModel(int code, int price)
        {
            Size = Brand = string.Empty;
            ImgPath = "/images/scan_error.png";
        }

        /// <summary>
        /// Уникальный идентификатор записи
        /// </summary>
        [Key()]
        [JsonProperty("id")]
        public Guid ID { get; set; }

        /// <summary>
        /// Штрих-код товара
        /// </summary>
        [JsonProperty("code")]
        [Display(Name ="Штрих-код товара")]
        public int Code { get; set; }

        [JsonProperty("price")]
        [Display(Name = "Цена")]
        public int Price { get; set; }

        [JsonProperty("size")]
        [Display(Name = "Размер")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Это поле не может быть пустым.")]
        public string Size { get; set; }

        [JsonProperty("brand")]
        [Display(Name = "Бренд")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Это поле не может быть пустым.")]
        public string Brand { get; set; }

        [JsonProperty("imgformat")]
        [Display(Name = "Расширение картинки")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Это поле не может быть пустым.")]
        public string ImgFormat { get; set; }

        [JsonProperty("img")]
        public string ImgPath { get; set; }

        [JsonProperty("collectionid")]
        [Display(Name = "ID коллекции")]
        public Guid CollectionID { get; set; }

        [JsonProperty("collection")]
        [Display(Name = "Коллекция")]
        public CollectionModel Collection { get; set; }
    }
}
