using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using smartdressroom.Models;
using System;

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
        public DbSet<ClothesModel> ClothesModels { get; set; }

        /// <summary>
        /// Коллекции
        /// </summary>
        public DbSet<CollectionModel> CollectionModels { get; set; }

        /// <summary>
        /// Админы
        /// </summary>
        public DbSet<AdminModel> Admins { get; set; }

        /// <summary>
        /// Настройка подключения к БД
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=db;Trusted_Connection=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converter = new GuidToBytesConverter();

            modelBuilder.Entity<AdminModel>()
                .Property(am => am.ID)
                .HasConversion(converter);
            modelBuilder.Entity<ClothesModel>()
                .Property(am => am.ID)
                .HasConversion(converter);
            modelBuilder.Entity<CollectionModel>()
                .Property(am => am.ID)
                .HasConversion(converter);

            modelBuilder.Entity<ClothesModel>()
                .Property(cm => cm.Sizes)
                .HasConversion(
                    s => string.Join(',', s),
                    s => s.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<CollectionModel>()
                .HasMany(c => c.ClothesModels)
                .WithOne(cm => cm.Collection).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
