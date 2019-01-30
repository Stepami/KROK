using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace smartdressroom.Models
{
    /// <summary>
    /// Коллекция модный вещей
    /// </summary>
    public class CollectionModel
    {
        /// <summary>
        /// Уникальный идентификатор записи
        /// </summary>
        [Key()]
        [JsonProperty("id")]
        public Guid ID { get; set; }

        /// <summary>
        /// Название коллекции
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
