using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace smartdressroom.Models
{
    /*public class Cart
    {
        public CartModel() => lineList = new List<CartEntity>();
        private List<CartEntity> lineList;

        public IEnumerable<CartEntity> Lines => lineList;

        public void AddItem(CartEntity ce)
        {
            var temp = lineList.Find(x => x.Item.Code == ce.Item.Code);
            if (temp == null)
                lineList.Add(ce);
            else
                temp.Quantity += ce.Quantity;
        }

        public void RemoveLine(CartEntity ce) => lineList.RemoveAll(l => l.Item.Id == ce.Item.Id);

        public decimal ComputeTotalValue() => lineList.Sum(e => e.Item.Price * e.Quantity);

        public void Clear() => lineList.Clear();
    }
    */
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
