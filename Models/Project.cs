namespace Velore_Studio.Models;

public class Project
{
    public int Id { get; set; }
    public  string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public  string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } =  DateTime.Now;
    public bool IsFeatured { get; set; } = false;
}