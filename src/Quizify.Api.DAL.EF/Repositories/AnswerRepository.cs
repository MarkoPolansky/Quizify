using AutoMapper;
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

                existingAnswer = _mapper.Map(Answer, existingAnswer);

                foreach (var user in existingAnswer.Users)
                {
                    dbContext.AnswerUsers.Add(user);
                }

                dbContext.Update(existingAnswer);
                dbContext.SaveChanges();

                return existingAnswer.Id;
            }
            else
            {
                return null;
            }
        }

    }
}
