using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizify.Api.DAL.EF.Entities
{
    public record AnswerUserEntity: EntityBase
    {
        public UserEntity? User { get; set; }
        public required Guid UserId { get; set; }
        public AnswerEntity? Answer { get; set; }
        public required Guid AnswerId { get; set; }

        public string?  UserInput { get; set; }
    }
}
