using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartdressroom.HubModels
{
    public class Product
    {
        public string VendorCode { get; set; } = string.Empty;
        public string SelectedSize { get; set; } = string.Empty;
        public string ImgUrl { get; set; } = "/images/scan_error.png";
        public int ImgCount { get; set; } = 0;
    }
}
