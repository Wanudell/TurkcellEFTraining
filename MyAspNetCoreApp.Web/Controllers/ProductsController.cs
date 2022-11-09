using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private AppDbContext _dbContext;

        private readonly ProductRepository _productRepository;

        public ProductsController(AppDbContext dbContext)
        {
            _productRepository = new ProductRepository(); //Artık new keyword'unu kullanmaya gerek yok. Biz aşağıda dbContext'i EF Core sayesinde Dependency Injection yaptık.Yani
            // Bağımlılıkların yönetilmesini sağlayan bir design pattern'dır Dependency Injection. Bu class'ları almamıza imkan sağlayanlara da DI Container deniyor.
            // Herhangi bir class'ın ihtiyacı duyduğu nesneyi eğer constructor'dan alıyor ise Dependency Injection design pattern aracılığı ile almış olur.
            // Bu şekilde nesne almamızı sağlayan yapıya da DI Container diyoruz.
            _dbContext = dbContext;

            if (!_dbContext.Products.Any()) //Eğer db'de hiç veri yok ise çalışması için yazıldı.
            {
                _dbContext.Products.Add(new Product() { Name = "Kalem 1", Price = 100, Stock = 100, Color = "Red", Width = 10, Height = 20 });
                _dbContext.Products.Add(new Product() { Name = "Kalem 2", Price = 90, Stock = 120, Color = "Blue", Width = 20, Height = 35 });
                _dbContext.Products.Add(new Product() { Name = "Kalem 3", Price = 80, Stock = 130, Color = "Yellow", Width = 15, Height = 25 });

                _dbContext.SaveChanges();
            }
        }

        public IActionResult Index()
        {
            //var products = _productRepository.GetAll(); // Biz artık DbContext kullandığımız için artık buna ihtiyaç kalmadı.
            var products = _dbContext.Products.ToList();
            return View(products);
        }

        public IActionResult Remove(int id)
        {
            var product = _dbContext.Products.Find(id);
            //_productRepository.Remove(id); //Artık EntityFramework core kullandığımız için direkt dbContext üzerinden siliyoruz. Eskiden ProductRepository'de oluşturduğumuz methodla siliyorduk.
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet] //Bize sayfayı getirmeye ve veriyi getirmeye yarıyor.
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost] //Sayfadan gelen veriyi database'e göndermemizi sağlıyor.
        public IActionResult AddProduct()
        {
            //1. YÖNTEM = HTTPContext'in Request propertysi sayesinde HttpContext'te olan bütün request'leri alabiliriz.
            var name = HttpContext.Request.Form["Name"];
            var price = HttpContext.Request.Form["Price"];
            var stock = HttpContext.Request.Form["Stock"];
            var color = HttpContext.Request.Form["Color"];

            return View();
        }

        public IActionResult Update(int id)
        {
            return View();
        }
    }
}