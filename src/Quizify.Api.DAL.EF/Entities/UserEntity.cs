using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizify.Api.DAL.EF.Entities
{
    public record UserEntity : EntityBase
    {
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }


   
        public ICollection<QuizEntity> CreatedQuizzes { get; set; } = new List<QuizEntity>();

        public ICollection<QuizUserEntity> Quizzes { get; set; } = new List<QuizUserEntity>();

        public ICollection<AnswerUserEntity> Answers { get; set; } = new List<AnswerUserEntity>();
    }
}
