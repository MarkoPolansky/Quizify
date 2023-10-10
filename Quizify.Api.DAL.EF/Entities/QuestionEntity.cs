using Quizify.Common.Enums;

namespace Quizify.Api.DAL.EF.Entities
{
    public record QuestionEntity: EntityBase
    {
        public required string Text { get; set; }
 
        public required TypeEnum Type { get; set; }

        public  QuizEntity? Quiz { get; set; }
        public required Guid QuizId { get; set; }
        public ICollection<AnswerEntity> Answers{ get; set; } = new List<AnswerEntity>();
   
    }
}
