using System.ComponentModel.DataAnnotations;

namespace Model.Entities;

public class Post()
{
    [Key]
    public int Id { get; set; }
    [StringLength(50)]
    public required string Title { get; set; }
    [StringLength(100)]
    public required string Description { get; set; }
    [StringLength(500)]
    public required string Content { get; set; }
}