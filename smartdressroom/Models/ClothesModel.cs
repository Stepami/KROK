using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        /// <param name="code"></param>
        /// <param name="price"></param>
        /// <param name="size"></param>
        /// <param name="brand"></param>
        /// <param name="path"></param>
        public ClothesModel(int code, int price, string size, string brand, string path)
        {
            // Формирование нового уникального идентификатора
            ID = Guid.NewGuid();
            Code = code;
            Price = price;
            Size = size;
            Brand = brand;
            ImgPath = path;
        }

        public ClothesModel(string id, int code, int price, string size, string brand, string path)
        {
            // Формирование нового уникального идентификатора
            ID = Guid.Parse(id);
            Code = code;
            Price = price;
            Size = size;
            Brand = brand;
            ImgPath = path;
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

        [JsonProperty("img")]
        [Display(Name = "Изображение")]
        public string ImgPath { get; set; }

        /// <summary>
        /// Коллекция
        /// </summary>      
        [JsonProperty("collection")]
        [Display(Name = "Коллекция")]
        public virtual CollectionModel Collection { get; set; }
    }
}
