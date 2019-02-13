using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace smartdressroom
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Проверка соединения с базой данных
            using (Storage.ApplicationContext db = new Storage.ApplicationContext())
            {
                // Проверка на существование БД, автоматическое создание при отсутствии
                // db.ApplicationContext.EnsureCreated();

                // Проверка на существование БД, автоматическое создание при отсутствии, автоматическая миграция
                db.Database.Migrate();

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
