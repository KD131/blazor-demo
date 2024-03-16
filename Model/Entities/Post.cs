using System.ComponentModel.DataAnnotations;

namespace Model.Entities;

public class Post(int id, string title, string description, string content)
{
    [Key]
    public int Id { get; set; } = id;
    [StringLength(50)]
    public string Title { get; set; } = title;
    [StringLength(100)]
    public string Description { get; set; } = description;
    [StringLength(500)]
    public string Content { get; set; } = content;
}