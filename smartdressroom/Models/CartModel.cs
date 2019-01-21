using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartdressroom.Models
{
    public class CartModel
    {
        private List<CartEntity> lineList = new List<CartEntity>();

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

    public class CartEntity
    {
        public CartEntity(ClothesModel item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        public ClothesModel Item { get; set; }
        public int Quantity { get; set; }
    }
}
