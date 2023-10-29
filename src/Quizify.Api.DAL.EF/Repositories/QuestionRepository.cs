using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Api.DAL.EF.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Quizify.Api.DAL.EF.Repositories
{
    public class QuestionRepository : RepositoryBase<QuestionEntity>, IQuestionRepository
    {
        private readonly IMapper _mapper;
        public QuestionRepository(QuizifyDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        public override QuestionEntity? GetById(Guid id)
        {
            return dbContext.Set<QuestionEntity>()
                .Include(question => question.ActiveInQuiz)
                .Include(question => question.Answers)
                .Include(question => question.Quiz)
                .SingleOrDefault(entity => entity.Id == id);
        }

        public override Guid? Update(QuestionEntity question)
        {
            if (Exists(question.Id))
            {
                var existingQuestion = dbContext.Questions
                    .Include(question => question.ActiveInQuiz)
                    .Include(question => question.Answers)
                    .Include(question => question.Quiz)
                    .Single(r => r.Id == question.Id);

                _mapper.Map(question, existingQuestion);

                // foreach (var AnswerEntity in existingQuestion.Answers)
                // {
                //     dbContext.Answers.Add(AnswerEntity);
                // }

                dbContext.Update(existingQuestion);
                //dbContext.SaveChanges();

                return existingQuestion.Id;
            }
            else
            {
                return null;
            }
        }

    }
}
