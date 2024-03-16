using System.ComponentModel.DataAnnotations;

namespace Model.Entities;

public class Post(int id, string title, string description, string content)
{
    [Key]
    public int? Id { get; set; } = id;
    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
    public string Content { get; set; } = content;
}