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

                existingUser =_mapper.Map(user, existingUser);
                
                foreach (var answerUserEntity in existingUser.Answers)
                {
                    dbContext.AnswerUsers.Add(answerUserEntity);
                }
                
                foreach (var quizUserEntity in existingUser.Quizzes)
                {
                    dbContext.QuizUsers.Add(quizUserEntity);
                }
                
                foreach (var quiz in existingUser.CreatedQuizzes)
                {
                    if(dbContext.Quizzes.Count(a =>a.Id == quiz.Id) == 0)
                     dbContext.Quizzes.Add(quiz);
                    else
                    { 
                     dbContext.Quizzes.Update(quiz);
                    }
                }
                
                
                dbContext.Update(existingUser);
                dbContext.SaveChanges();

                return existingUser.Id;
            }
            else
            {
                return null;
            }
        }
    }
}
