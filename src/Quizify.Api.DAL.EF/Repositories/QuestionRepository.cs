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
                .ThenInclude(q => q.Users)
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

                question = _mapper.Map(question, existingQuestion);
               
                dbContext.ChangeTracker.Clear();
                foreach (var answer in question.Answers)
                {
                    if (dbContext.Answers.Count(a => a.Id == answer.Id) == 0)
                        dbContext.Answers.Add(answer);
                    else
                    {
                        dbContext.Answers.Update(answer);
                    }
                }

              
                dbContext.Update(question);
                dbContext.ChangeTracker.DetectChanges();
                Console.WriteLine(dbContext.ChangeTracker.DebugView.LongView);
                dbContext.SaveChanges();


                return question.Id;
            }
            else
            {
                return null;
            }
        }
        public override void Remove(Guid id)
        {
            var question = GetById(id);

            foreach (var answer in question.Answers)
            {
                foreach (var answerUser in answer.Users)
                {
                    dbContext.AnswerUsers.Remove(answerUser);
                }
            }
            
            
            base.Remove(id);
        }
    }
}
