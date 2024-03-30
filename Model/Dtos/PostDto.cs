using System.ComponentModel.DataAnnotations;

namespace Model.Dtos;

public class PostDto()
{
    public int Id { get; set; }
    [StringLength(50)]
    public required string Title { get; set; }
    [StringLength(100)]
    public string? Description { get; set; }
    [StringLength(500)]
    public required string Content { get; set; }
}