namespace Quizify.Common.Models
{
    public record QuizListModel : ModelBase
    {
        public required string Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? GamePin { get; set; }

    }
}
