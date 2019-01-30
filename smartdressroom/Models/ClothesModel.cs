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

        [Key()]
        [JsonProperty("id")]
        public Guid ID { get; set; }

        /// <summary>
        /// Штрих-код товара
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("img")]
        public string ImgPath { get; set; }
    }
}
