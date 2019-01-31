using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace smartdressroom.Storage
{
    /// <summary>
    /// База данных
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Каталог одежды
        /// </summary>
        public DbSet<Models.ClothesModel> ClothesModels { get; set; }

        /// <summary>
        /// Коллекции
        /// </summary>
        public DbSet<Models.CollectionModel> CollectionModels { get; set; }

        /// <summary>
        /// Настройка подключения к БД
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;UserId=stepan;Password=leftkrok;database=db;");
        }
    }
}
