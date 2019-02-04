using Microsoft.EntityFrameworkCore;

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
        /// Админы
        /// </summary>
        public DbSet<Models.AdminModel> Admins { get; set; }

        /// <summary>
        /// Настройка подключения к БД
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySQL("server=localhost;UserId=stepan;Password=leftkrok;database=db;");
    }
}
