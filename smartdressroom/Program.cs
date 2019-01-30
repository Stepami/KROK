using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;


namespace smartdressroom
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Проверка соединения с базой данных
            using (Storage.Database db = new Storage.Database())
            {
                // Проверка на существование БД, автоматическое создание при отсутствии
                // db.Database.EnsureCreated();

                // Проверка на существование БД, автоматическое создание при отсутствии, автоматическая миграция
                db.Database.Migrate();

                if (db.ClothesModels.Where(a => a.Code == 132).FirstOrDefault() == null)
                {
                    var m = new Models.ClothesModel(132, 1000, "L", "SABBAT CULT", "/images/clothes/sabbat_tshirt1.jpg");
                    db.ClothesModels.Add(m);
                }

                if (db.ClothesModels.Where(a => a.Code == 12).FirstOrDefault() == null)
                {
                    var m = new Models.ClothesModel(12, 1200, "L", "SELA", "/images/clothes/sela_jemper1.jpg");
                    db.ClothesModels.Add(m);
                }
                // Запись изменений в БД
                db.SaveChanges();
            }
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseConfiguration(config)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
