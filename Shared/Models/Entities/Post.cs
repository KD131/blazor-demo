namespace Shared.Models.Entities
{
    public class Post(int id, string title, string description, string content)
    {
        public int Id { get; set; } = id;
        public string Title { get; set; } = title;
        public string Description { get; set; } = description;
        public string Content { get; set; } = content;
    }
}
