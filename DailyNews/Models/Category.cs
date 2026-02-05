namespace DailyNews.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<News> News { get; set; } = new List<News>();
}