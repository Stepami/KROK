using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace smartdressroom.Models
{
    /// <summary>
    /// Коллекция модных вещей
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
