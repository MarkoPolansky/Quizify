namespace Quizify.Common.Models
{
    public record QuizDetailModel : ModelBase
    {
        public required string Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? GamePin { get; set; }
        public TimeSpan? TimeLimit { get; set; }
        public bool IsStarted { get; set; }

        public required UserListModel CreatedByUser { get; set; }
        public IList<QuestionListModel> Questions { get; set; } = new List<QuestionListModel>();
        public IList<QuizDetailUserModel> Users { get; set; } = new List<QuizDetailUserModel>();
    }
}
