using Quizify.Common.Enums;

namespace Quizify.Common.Models
{
    public record AnswerListModel : ModelBase
    {
        public required string Text { get; set; }
        public string? ImageUrl { get; set; }
        public required TypeEnum Type { get; set; }
    }
}
