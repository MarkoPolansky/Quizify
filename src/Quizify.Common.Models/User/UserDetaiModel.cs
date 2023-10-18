namespace Quizify.Common.Models
{
    public record UserDetaiModel : ModelBase
    {
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }

        public IList<QuizListModel> CreatedQuizzes { get; set; } = new List<QuizListModel>();
        public IList<QuizUserDetailModel> Quizzes { get; set; } = new List<QuizUserDetailModel>();
        public IList<UserAnswerDetailModel> Answers { get; set; } = new List<UserAnswerDetailModel>();
    }
}
