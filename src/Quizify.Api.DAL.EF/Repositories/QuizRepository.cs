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
                    .Single(r => r.Id == quiz.Id);

                existingQuiz = _mapper.Map(quiz, existingQuiz);

                foreach (var quizUserEntity in existingQuiz.Users)
                {
                    dbContext.QuizUsers.Add(quizUserEntity);
                }

               
                dbContext.Quizzes.Update(existingQuiz);
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
