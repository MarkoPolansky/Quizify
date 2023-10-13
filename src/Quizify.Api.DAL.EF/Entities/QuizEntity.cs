using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizify.Api.DAL.EF.Entities
{
    public record QuizEntity : EntityBase
    {
        public required string Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? GamePin { get; set; }
        public TimeSpan? TimeLimit { get; set; }
        public bool IsStatred { get; set; } = false;


        public  UserEntity? CreatedByUser { get; set; }
        public required Guid CreatedByUserId { get; set; }
        public ICollection<QuestionEntity> Questions{ get; set; } = new List<QuestionEntity>();

        public ICollection<QuizUserEntity> Users { get; set; } = new List<QuizUserEntity>();

    }
}
