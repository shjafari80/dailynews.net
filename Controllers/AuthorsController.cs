using Microsoft.AspNetCore.Mvc;
using DailyNews.Models; // اگر Author رو از مدل‌ها می‌کشی

public class AuthorsController : Controller
{
    public IActionResult Index()
    {
        var authors = new List<Author>
        {
            new Author 
            { 
                Id = 1, 
                Name = "حسین جعفری شارمی", 
                Bio = "دانشجوی کامپیوتر ورودی ۱۴۰۰ - توسعه‌دهنده اصلی بک‌اند و فرانت‌اند پروژه", 
                ImageUrl = "/images/authors/hossein.jpg" // اگر عکس داری، مسیر بده
            },
            new Author 
            { 
                Id = 2, 
                Name = "معین آقائی کوهی", 
                Bio = "دانشجوی کامپیوتر ورودی ۱۳۹۸ - مسئول طراحی دیتابیس و مدیریت پروژه", 
                ImageUrl = "/images/authors/moein.jpg" 
            }
        };

        return View(authors);
    }
}