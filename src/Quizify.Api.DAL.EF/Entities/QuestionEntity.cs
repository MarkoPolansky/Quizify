using Quizify.Common.Enums;

namespace Quizify.Api.DAL.EF.Entities
{
    public record QuestionEntity: EntityBase
    {
        public required string Text { get; set; }
        public required TypeEnum Type { get; set; }
        public required int Points { get; set; }

        public DateTime TimeLimit { get; set; } = new DateTime().AddSeconds(15);
        public  QuizEntity? Quiz { get; set; }
        public required Guid QuizId { get; set; }
        
        public  QuizEntity? ActiveInQuiz { get; set; }
        public  Guid? ActiveInQuizId { get; set; }

        
        public DateTime CreatedAt { get; set; }

        public ICollection<AnswerEntity> Answers{ get; set; } = new List<AnswerEntity>();
   
    }
}
