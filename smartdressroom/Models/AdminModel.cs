using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace smartdressroom.Models
{
    public class AdminModel
    {
        public AdminModel() { }
        public AdminModel(string login, string password)
        {
            ID = Guid.NewGuid();
            Login = login;
            Password = password;
        }

        [Key()]
        [JsonProperty("ïd")]
        public Guid ID { get; set; }
        [JsonProperty("login")]
        public string Login { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
