using Microsoft.EntityFrameworkCore;

namespace MyAspNetCoreApp.Web.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Bu options'ı program.cs'de dolduruyoruz ve bağlantımızı kuruyoruz.
        }

        //Ardından DbSet yani Tabloda karşılığı olacak table ismini yazıyoruz.

        public DbSet<Product> Products { get; set; } //Products değil de başka bir isim vermek ister isem Product Entity'sine gelip başına [Table("Products")] yazıp mapleyebiliriz.

        //Sonra Program.cs tarafına geliyoruz.
    }
}