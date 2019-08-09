using Newtonsoft.Json;

namespace smartdressroom.HubModels
{
    public class Room
    {
        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("responsible")]
        public string Responsible { get; set; } = null;

        [JsonProperty("hubID")]
        public string HubID { get; set; }
    }
}