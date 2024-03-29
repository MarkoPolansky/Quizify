﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Api.DAL.EF.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizify.Api.DAL.EF.Repositories
{
    public class AnswerRepository : RepositoryBase<AnswerEntity>, IAnswerRepository
    {
        private readonly IMapper _mapper;
        public AnswerRepository(QuizifyDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        public override AnswerEntity? GetById(Guid id)
        {
            return dbContext.Set<AnswerEntity>()
                .Include(Answer => Answer.Users)
                    .ThenInclude(quiz => quiz.User)
                .Include(Answer => Answer.Question)
                .SingleOrDefault(entity => entity.Id == id);
        }


        public override Guid? Update(AnswerEntity Answer)
        {
            if (Exists(Answer.Id))
            {
                var existingAnswer = dbContext.Answers
                    .Include(Answer => Answer.Users)
                    .Include(Answer => Answer.Question)
                    .Single(r => r.Id == Answer.Id);

                Answer = _mapper.Map(Answer, existingAnswer);

                dbContext.ChangeTracker.Clear();
                foreach (var user in Answer.Users)
                {
                    if (dbContext.AnswerUsers.Count(a => a.Id == user.Id) == 0)
                        dbContext.AnswerUsers.Add(user);
                    else
                    {
                        dbContext.AnswerUsers.Update(user);
                    }
              
                }

                dbContext.Update(Answer);
                dbContext.SaveChanges();

                return Answer.Id;
            }
            else
            {
                return null;
            }
        }

        public override void Remove(Guid id)
        {
            var answer = GetById(id);
            foreach (var user  in answer.Users)
            {
                dbContext.AnswerUsers.Remove(user);
            }
            base.Remove(id);
        }
    }
}
