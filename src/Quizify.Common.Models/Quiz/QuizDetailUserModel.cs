namespace Quizify.Common.Models
{
    public record QuizDetailUserModel : ModelBase
    {
        public int TotalPoints { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }

        public required UserListModel User { get; set; }
    }
}
