namespace Quizify.Common.Models
{
    public record UserDetailQuizModel : ModelBase
    {
        public int TotalPoints { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }

        public required QuizListModel Quiz { get; set; }
    }
}
