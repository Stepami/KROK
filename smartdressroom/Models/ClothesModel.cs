using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        public ClothesModel(int code, int price, string size, string brand, string imgformat)
        {
            ID = Guid.NewGuid();// Формирование нового уникального идентификатора
            Code = code;
            Price = price;
            Size = size;
            Brand = brand;
            ImgPath = $"~/images/clothes/{brand}/{code}.{imgformat}";
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
        public string Size { get; set; }

        [JsonProperty("brand")]
        [Display(Name = "Бренд")]
        public string Brand { get; set; }

        [JsonProperty("imgformat")]
        [Display(Name = "Расширение картинки")]
        public string ImgFormat { get; set; }

        [JsonProperty("img")]
        public string ImgPath { get; set; }

        [JsonProperty("collectionid")]
        [Display(Name = "ID коллекции")]
        public Guid? CollectionID { get; set; }

        [JsonProperty("collection")]
        [Display(Name = "Коллекция")]
        public CollectionModel Collection { get; set; }
    }
}
