namespace Quizify.Common.Models
{
    public record UserDetailAnswerModel : ModelBase
    {        
        public string? UserInput { get; set; }

        public required AnswerListModel Answer { get; set; }
    }
}
