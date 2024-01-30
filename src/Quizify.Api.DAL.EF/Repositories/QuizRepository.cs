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
                .ThenInclude(q => q.Answers)
                .Include(quiz => quiz.CreatedByUser)
                .Include(quiz => quiz.Questions)
                .Include(quiz => quiz.Users)
                    .ThenInclude(quiz => quiz.User)
                    .ThenInclude(quiz => quiz.Answers)
                    .ThenInclude(quiz => quiz.Answer)
                .SingleOrDefault(entity => entity.Id == id);
        }
        
        
        public override Guid? Update(QuizEntity quiz)
        {
          
        
            if (Exists(quiz.Id))
            {
                var existingQuiz= dbContext.Set<QuizEntity>()
                    .Include(quiz => quiz.ActiveQuestion)
                    .Include(quiz => quiz.CreatedByUser)
                    .Include(quiz => quiz.Questions)
                    .Include(r => r.Users)
                    .ThenInclude(r => r.User)
                    .Single(r => r.Id == quiz.Id);

                var createdUserId = existingQuiz.CreatedByUserId;
                var activeQuestionId = quiz.ActiveQuestionId ?? existingQuiz.ActiveQuestionId;
               
                quiz = _mapper.Map(quiz, existingQuiz);
                
                quiz.CreatedByUserId = createdUserId;
                quiz.ActiveQuestionId = activeQuestionId;
                
                dbContext.ChangeTracker.Clear();
                
                foreach (var user in quiz.Users)
                {
                    
                    if(!dbContext.QuizUsers.Where(
                           a => a.UserId == user.UserId).Any(a => a.QuizId == existingQuiz.Id))
                        dbContext.QuizUsers.Add(user);
                    else
                    { 
                        dbContext.QuizUsers.Update(user);
                    }
                }
                
                foreach (var question in quiz.Questions)
                {
                    if(dbContext.Questions.Count(a => a.Id == question.Id) == 0)
                        dbContext.Questions.Add(question);
                    else
                    { 
                        dbContext.Questions.Update(question);
                    }
                }

        
                dbContext.Quizzes.Update(quiz);
                dbContext.SaveChanges();
                
                return quiz.Id;
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

        public void RemoveQuizUser(Guid quizUser)
        {
             var quiz = dbContext.Set<QuizEntity>()
                 .Include(quizEntity => quizEntity.Users)
                    .ThenInclude(a => a.User)
                    .ThenInclude(a => a.Answers)
                    .ThenInclude(userAnswerEntity => userAnswerEntity.Answer)
                 .Include(a => a.Questions)
                 .FirstOrDefault(a => a.Users.Any(a => a.Id == quizUser));

             foreach (var userQuiz in quiz.Users)
             {
                 dbContext.QuizUsers.Remove(userQuiz);
                 if (userQuiz.User != null)
                     foreach (var answerUser in userQuiz.User.Answers)
                     {
                         if (quiz.Questions.Any(a => a.Id == answerUser?.Answer?.QuestionId))
                         {
                             dbContext.AnswerUsers.Remove(answerUser);
                         }
                     }
             }
             dbContext.SaveChanges();

        }

        public override void Remove(Guid id)
        {
            var quiz = GetById(id);
            
            foreach (var userInQuiz in quiz.Users)
            {
                dbContext.QuizUsers.Remove(userInQuiz);

                if (userInQuiz.User != null)
                    foreach (var answerUser in userInQuiz.User.Answers)
                    {
                        if (quiz.Questions.Any(a => a.Id == answerUser?.Answer?.QuestionId))
                        {
                            dbContext.AnswerUsers.Remove(answerUser);
                        }
                    }
            }
            
            
            base.Remove(id);
        }
    }
}
