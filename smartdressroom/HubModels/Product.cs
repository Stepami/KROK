using Newtonsoft.Json;

namespace smartdressroom.HubModels
{
    public class Product
    {
        [JsonProperty("vendorCode")]
        public string VendorCode { get; set; } = string.Empty;

        [JsonProperty("selectedSize")]
        public string SelectedSize { get; set; } = string.Empty;

        [JsonProperty("imgUrl")]
        public string ImgUrl { get; set; } = "/images/scan_error.png";

        [JsonProperty("imgCount")]
        public int ImgCount { get; set; } = 0;
    }
}