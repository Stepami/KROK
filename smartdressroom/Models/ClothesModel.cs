using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartdressroom.Models
{
    public class ClothesModel
    {
        public ClothesModel(int id, int code, int price, string size, string brand)
        {
            Id = id;
            Code = code;
            Price = price;
            Size = size;
            Brand = brand;
        }

        public int Id { get; set; }
        public int Code { get; set; }
        public int Price { get; set; }
        public string Size { get; set; }
        public string Brand { get; set; }
    }
}
