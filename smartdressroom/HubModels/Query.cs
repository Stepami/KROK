using Newtonsoft.Json;
using System;

namespace smartdressroom.HubModels
{
    public class Query
    {
        [JsonProperty("id")]
        public string ID { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; } = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'");

        [JsonProperty("status")]
        public QueryStatus Status { get; set; }

        [JsonProperty("servedBy")]
        public string ServedBy { get; set; } = null;

        [JsonProperty("room")]
        public Room Room { get; set; }

        [JsonProperty("product")]
        public Product Product { get; set; }
    }
}