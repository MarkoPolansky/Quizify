namespace Quizify.Common.Models
{
    public record UserDetailModel : ModelBase
    {
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }

        public IList<QuizListModel> CreatedQuizzes { get; set; } = new List<QuizListModel>();
        public IList<QuizDetailUserModel> Quizzes { get; set; } = new List<QuizDetailUserModel>();
        public IList<UserDetailAnswerModel> Answers { get; set; } = new List<UserDetailAnswerModel>();
    }
}
