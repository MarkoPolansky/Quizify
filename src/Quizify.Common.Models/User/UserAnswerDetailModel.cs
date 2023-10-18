namespace Quizify.Common.Models
{
    public record UserAnswerDetailModel : ModelBase
    {        
        public string? UserInput { get; set; }

        public required Guid AnswerId { get; set; }
    }
}
