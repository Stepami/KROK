using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;

namespace smartdressroom.Models
{
    public class CartModel
    {
        [JsonProperty("list")]
        public List<CartItemModel> List;

        /// <summary>
        /// Создание объекта из JSON-представления
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static CartModel FromJson(string json) => JsonConvert.DeserializeObject<CartModel>(json, Settings);

        public static CartModel FromArray(byte[] a) => FromJson(Encoding.Default.GetString(a));

        /// <summary>
        /// JSON-представление объекта
        /// </summary>
        /// <returns></returns>
        public string ToJson() => JsonConvert.SerializeObject(this);

        /// <summary>
        /// Бинарное представление объекта
        /// </summary>
        /// <returns></returns>
        public byte[] ToArray() => Encoding.Default.GetBytes(ToJson());

        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public CartModel()
        {
            List = new List<CartItemModel>();
        }
    }
}
