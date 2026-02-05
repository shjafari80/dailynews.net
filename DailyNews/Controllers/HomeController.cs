using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DailyNews.Data;
using DailyNews.Models;

public class HomeController : Controller
{
    private readonly NewsDbContext _context;

    public HomeController(NewsDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var latestNews = await _context.News
            .Include(n => n.Category)
            .Include(n => n.Author)
            .OrderByDescending(n => n.PublishDate)
            .Take(6)
            .ToListAsync();

        return View(latestNews);
    }
    public IActionResult About()
    {
        return View();
    }
    public async Task<IActionResult> Details(int id)
{
    var news = await _context.News
        .Include(n => n.Category)
        .Include(n => n.Author)
        .FirstOrDefaultAsync(n => n.Id == id);

    if (news == null)
    {
        return NotFound();
    }

    return View(news);
}
}