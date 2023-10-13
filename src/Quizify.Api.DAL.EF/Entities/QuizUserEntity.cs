using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizify.Api.DAL.EF.Entities
{
    public record QuizUserEntity: EntityBase
    {

        public UserEntity? User { get; set; }
        public required Guid UserId { get; set; }
        public QuizEntity? Quiz { get; set; }
        public required Guid QuizId { get; set; }
        public int TotalPoints { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
    }
}
