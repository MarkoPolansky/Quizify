using System.ComponentModel.DataAnnotations;
using Quizify.Common.Enums;

namespace Quizify.Common.Models
{
    public record QuizDetailModel : ModelBase
    {
        [Required(ErrorMessage = "Pole Title je povinne")]
        public required string Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? GamePin { get; set; }
      
        public required QuizStateEnum QuizState{ get; set; }
        
        public  QuestionDetailModel? ActiveQuestion { get; set; }
        public required UserListModel CreatedByUser { get; set; }
        public IList<QuestionListModel> Questions { get; set; } = new List<QuestionListModel>();
        public IList<QuizDetailUserModel> Users { get; set; } = new List<QuizDetailUserModel>();
    }
}
