using Newtonsoft.Json;

namespace smartdressroom.Models
{
    public class CartItemModel
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        public CartItemModel(ClothesModel item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        /// <summary>
        /// Артикул
        /// </summary>
        [JsonProperty("item")]
        public ClothesModel Item { get; set; }
        /// <summary>
        /// Количество
        /// </summary>
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
