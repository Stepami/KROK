using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace smartdressroom.Models
{
    /// <summary>
    /// Коллекция модных вещей
    /// </summary>
    public class CollectionModel
    {
        /// <summary>
        /// Конструктор по умолчанию для сериализации
        /// </summary>
        public CollectionModel()
        {
        }

        public CollectionModel(string name)
        {
            ID = Guid.NewGuid();
            Name = name;
            ClothesModels = new List<ClothesModel>();
        }

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
        [Display(Name = "Название коллекции")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Это поле не может быть пустым.")]
        public string Name { get; set; }

        [JsonProperty("clothesmodels")]
        [Display(Name = "Вещи в коллекции")]
        public ICollection<ClothesModel> ClothesModels { get; set; }
    }
}
