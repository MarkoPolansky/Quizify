namespace Quizify.Common.Models
{
    public record AnswerUserDetailModel : ModelBase
    {        
        public string? UserInput { get; set; }

        public required Guid AnswerId { get; set; }
    }
}
