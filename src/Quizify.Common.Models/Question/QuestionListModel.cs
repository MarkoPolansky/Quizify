using Quizify.Common.Enums;

namespace Quizify.Common.Models
{
    public record QuestionListModel : ModelBase
    {
        public required string Text { get; set; }
        public required TypeEnum Type { get; set; }
        public required int Points { get; set; }
    }
}
