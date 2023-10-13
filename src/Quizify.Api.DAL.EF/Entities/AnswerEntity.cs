using Quizify.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizify.Api.DAL.EF.Entities
{
    public record AnswerEntity : EntityBase
    {
        public required string Text { get; set; }
        public string? ImageUrl { get; set; }
        public required TypeEnum Type { get; set; }
        public required bool IsCorrect { get; set; }

        public  QuestionEntity? Question { get; set; }
        public required Guid QuestionId { get; set; }

        public ICollection<AnswerUserEntity> Users { get; set; } = new List<AnswerUserEntity>();


    }
}
