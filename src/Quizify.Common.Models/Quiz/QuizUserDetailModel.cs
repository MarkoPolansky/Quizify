namespace Quizify.Common.Models
{
    public record QuizUserDetailModel : ModelBase
    {
        public int TotalPoints { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }

        public required Guid UserId { get; set; }
        public required Guid QuizId { get; set; }
    }
}
