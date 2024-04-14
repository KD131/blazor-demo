using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels;

public class PostViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Title { get; set; } = null!;

    [StringLength(100)]
    public string? Description { get; set; }

    [Required]
    [StringLength(500)]
    public string Content { get; set; } = null!;
}