using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace smartdressroom.Models
{
    public class ClothesModel
    {
        /// <summary>
        /// Конструктор по значениям
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <param name="price"></param>
        /// <param name="size"></param>
        /// <param name="brand"></param>
        /// <param name="path"></param>
        public ClothesModel(int id, int code, int price, string size, string brand, string path)
        {
            Id = id;
            Code = code;
            Price = price;
            Size = size;
            Brand = brand;
            ImgPath = path;
        }

        [JsonProperty("id")]
        public int Id { get; set; }

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
