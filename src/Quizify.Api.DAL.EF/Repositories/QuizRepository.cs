using Quizify.Api.DAL.EF.Entities;
using Quizify.Api.DAL.EF.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Quizify.Api.DAL.EF.Repositories
{
    public class QuizRepository : RepositoryBase<QuizEntity>, IQuizRepository
    {

        private readonly IMapper _mapper;
        public QuizRepository(QuizifyDbContext dbContext,IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }
        
        public override QuizEntity? GetById(Guid id)
        {
            return dbContext.Set<QuizEntity>()
                .Include(quiz => quiz.ActiveQuestion)
                .Include(quiz => quiz.CreatedByUser)
                .Include(quiz => quiz.Questions)
                .Include(quiz => quiz.Users)
                    .ThenInclude(quiz => quiz.User)
                .SingleOrDefault(entity => entity.Id == id);
        }
        
        
        public override Guid? Update(QuizEntity quiz)
        {
            if (Exists(quiz.Id))
            {
                var existingQuiz= dbContext.Quizzes
                    .Include(quiz => quiz.ActiveQuestion)
                    .Include(quiz => quiz.CreatedByUser)
                    .Include(quiz => quiz.Questions)
                    .Include(r => r.Users)
                    .ThenInclude(r => r.User)
                    .Single(r => r.Id == quiz.Id);

                existingQuiz = _mapper.Map(quiz, existingQuiz);
                
                foreach (var user in existingQuiz.Users)
                {
                    dbContext.QuizUsers.Add(user);
                }
                
                foreach (var question in existingQuiz.Questions)
                {
                    if(dbContext.Questions.Count(a => a.Id == question.Id) == 0)
                        dbContext.Questions.Add(question);
                    else
                    { 
                        dbContext.Questions.Update(question);
                    }
                }
                
                
                foreach (var question in existingQuiz.Questions)
                {
                    if(dbContext.Questions.Count(a => a.Id == question.Id) == 0)
                        dbContext.Questions.Add(question);
                    else
                    { 
                        dbContext.Questions.Update(question);
                    }
                }
                dbContext.Update(existingQuiz);
                dbContext.SaveChanges();
                
                return existingQuiz.Id;
            }
            else
            {
                return null;
            }
        }
        
        public int CountGamePin(string gamePin)
        { 
           return _dbSet.Count(a => a.GamePin == gamePin);
        }
        
    }
}
