using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Quizify.Api.DAL.Common.Tests.Seeds;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Api.DAL.EF.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizify.Api.DAL.EF.Repositories
{
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        private readonly IMapper _mapper;

        public UserRepository(QuizifyDbContext dbContext,IMapper mapper)
            : base(dbContext)
        {
            _mapper = mapper;
        }

        public override UserEntity? GetById(Guid id)
        {
            return dbContext.Set<UserEntity>()
                .Include(user => user.Answers)
                .ThenInclude(a => a.Answer)
                .Include(user => user.Quizzes)
                .ThenInclude(a => a.Quiz)
                .Include(user => user.CreatedQuizzes)
                    .ThenInclude(u => u.Questions)
                    .ThenInclude(u => u.Answers)
                    .ThenInclude(u => u.Users)
                .Include(u => u.CreatedQuizzes)
                    .ThenInclude(u => u.Users)
                .SingleOrDefault(entity => entity.Id == id);
        }

        public override Guid? Update(UserEntity user)
        {
            if (Exists(user.Id))
            {
                var existingUser = dbContext.Users
                    .Include(user => user.Answers)
                    .ThenInclude(a =>a.Answer)
                    .Include(user => user.Quizzes)
                    .ThenInclude(a =>a.Quiz)
                    .Include(user => user.CreatedQuizzes)
                    .Single(r => r.Id == user.Id);

                user =_mapper.Map(user, existingUser);
                
                
                dbContext.ChangeTracker.Clear();
                
                foreach (var answerUserEntity in user.Answers)
                {
                    if(dbContext.AnswerUsers.Count(a =>a.Id == answerUserEntity.Id) == 0)
                        dbContext.AnswerUsers.Add(answerUserEntity);
                    else
                    { 
                        dbContext.AnswerUsers.Update(answerUserEntity);
                    }
                }
                
                foreach (var quizUserEntity in user.Quizzes)
                {
                    if(dbContext.QuizUsers.Count(a =>a.Id == quizUserEntity.Id) == 0)
                        dbContext.QuizUsers.Add(quizUserEntity);
                    else
                    { 
                        dbContext.QuizUsers.Update(quizUserEntity);
                    }
                }
                
                foreach (var quiz in user.CreatedQuizzes)
                {
                    if(dbContext.Quizzes.Count(a =>a.Id == quiz.Id) == 0)
                     dbContext.Quizzes.Add(quiz);
                    else
                    { 
                     dbContext.Quizzes.Update(quiz);
                    }
                }
                
                
                dbContext.Update(user);
                dbContext.SaveChanges();

                return user.Id;
            }
            else
            {
                return null;
            }
        }

        public override void Remove(Guid id)
        {
            var user = GetById(id);
            
            foreach (var createdQuiz in user.CreatedQuizzes)
            {
                foreach (var userInQuiz in createdQuiz.Users)
                {
                    dbContext.QuizUsers.Remove(userInQuiz);
                }
                
                foreach (var question in createdQuiz.Questions)
                {
                    foreach (var answer in question.Answers)
                    {    
                        foreach (var answerUser in answer.Users)
                        {
                            dbContext.AnswerUsers.Remove(answerUser);
                        }
                      
                    }
                }
            }

            foreach (var quiz  in user.Quizzes)
            {
                dbContext.QuizUsers.Remove(quiz);
            }
            
            foreach (var answer in user.Answers)
            {
                dbContext.AnswerUsers.Remove(answer);
            }
            base.Remove(id);
        }
    }
}
