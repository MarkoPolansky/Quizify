using Quizify.Common.Enums;

namespace Quizify.Common.Models
{
    public record AnswerDetailModel : ModelBase
    {
        public required string Text { get; set; }
        public string? ImageUrl { get; set; }
        public required TypeEnum Type { get; set; }
        public required bool IsCorrect { get; set; }

        public required Guid QuestionId { get; set; }
    }
}
