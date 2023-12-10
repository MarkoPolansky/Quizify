using System.ComponentModel.DataAnnotations;

namespace Quizify.Common.Models
{
    public record UserDetailModel : ModelBase
    {
       [Required]
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }

        public IList<QuizListModel> CreatedQuizzes { get; set; } = new List<QuizListModel>();
        public IList<UserDetailQuizModel> Quizzes { get; set; } = new List<UserDetailQuizModel>();
        public IList<UserDetailAnswerModel> Answers { get; set; } = new List<UserDetailAnswerModel>();
    }
}
