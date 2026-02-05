using Microsoft.EntityFrameworkCore;
using DailyNews.Models;

namespace DailyNews.Data;

public class NewsDbContext : DbContext
{
    public NewsDbContext(DbContextOptions<NewsDbContext> options)
        : base(options)
    {
    }

    public DbSet<News> News { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Seed Categories
    modelBuilder.Entity<Category>().HasData(
        new Category { Id = 1, Name = "سیاسی" },
        new Category { Id = 2, Name = "اقتصادی" },
        new Category { Id = 3, Name = "ورزشی" },
        new Category { Id = 4, Name = "فرهنگی" }
    );

    // Seed Authors
    modelBuilder.Entity<Author>().HasData(
        new Author { Id = 1, Name = "حسین جعفری شارمی", Bio = "دانشجوی کامپیوتر ورودی ۱۴۰۰ - توسعه‌دهنده اصلی", ImageUrl = null },
        new Author { Id = 2, Name = "معین آقائی کوهی", Bio = "دانشجوی کامپیوتر ورودی ۱۳۹۸ - مدیر پروژه", ImageUrl = null }
    );

    // Seed News → تاریخ‌ها رو ثابت کن (نه DateTime.Now)
    modelBuilder.Entity<News>().HasData(
        new News 
        { 
            Id = 1, 
            Title = "تحولات جدید سیاسی کشور", 
            Content = "در تازه‌ترین اخبار سیاسی...", 
            PublishDate = new DateTime(2026, 2, 1, 10, 0, 0),  // تاریخ ثابت
            CategoryId = 1, 
            AuthorId = 1 
        },
        new News 
        { 
            Id = 2, 
            Title = "افزایش نرخ ارز در بازار", 
            Content = "دلار به رکورد جدیدی رسید...", 
            PublishDate = new DateTime(2026, 2, 3, 14, 30, 0),  // تاریخ ثابت
            CategoryId = 2, 
            AuthorId = 2 
        },
        new News 
        { 
            Id = 3, 
            Title = "پیروزی تیم ملی در بازی دوستانه", 
            Content = "بازیکنان درخشیدند...", 
            PublishDate = new DateTime(2026, 2, 5, 18, 0, 0),  // تاریخ ثابت
            CategoryId = 3, 
            AuthorId = 1 
        }
    );
}
}

