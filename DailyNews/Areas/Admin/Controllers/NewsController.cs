using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DailyNews.Data;
using DailyNews.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

[Area("Admin")]
public class NewsController : Controller
{
    private readonly NewsDbContext _context;

    public NewsController(NewsDbContext context)
    {
        _context = context;
    }

    // GET: Admin/News
    public async Task<IActionResult> Index()
    {
        var news = await _context.News
            .Include(n => n.Category)
            .Include(n => n.Author)
            .ToListAsync();
        return View(news);
    }

    // GET: Admin/News/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var news = await _context.News
            .Include(n => n.Category)
            .Include(n => n.Author)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (news == null) return NotFound();

        return View(news);
    }

    // GET: Admin/News/Create
    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
        ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name");
        return View();
    }

    // POST: Admin/News/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(News news)
    {
        if (ModelState.IsValid)
        {
            _context.Add(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", news.CategoryId);
        ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", news.AuthorId);
        return View(news);
    }

    // GET: Admin/News/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var news = await _context.News.FindAsync(id);
        if (news == null) return NotFound();

        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", news.CategoryId);
        ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", news.AuthorId);
        return View(news);
    }

    // POST: Admin/News/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, News news)
    {
        if (id != news.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(news);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsExists(news.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", news.CategoryId);
        ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", news.AuthorId);
        return View(news);
    }

    // GET: Admin/News/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var news = await _context.News
            .Include(n => n.Category)
            .Include(n => n.Author)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (news == null) return NotFound();

        return View(news);
    }

    // POST: Admin/News/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var news = await _context.News.FindAsync(id);
        if (news != null)
        {
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool NewsExists(int id)
    {
        return _context.News.Any(e => e.Id == id);
    }
}