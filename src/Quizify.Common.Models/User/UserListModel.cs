namespace Quizify.Common.Models
{
    public record UserListModel : ModelBase
    {
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
    }
}
