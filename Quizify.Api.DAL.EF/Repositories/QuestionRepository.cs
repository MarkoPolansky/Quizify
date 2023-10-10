﻿using Quizify.Api.DAL.EF.Entities;
using Quizify.Api.DAL.EF.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizify.Api.DAL.EF.Repositories
{
    public class QuestionRepository : RepositoryBase<QuestionEntity>, IApiRepository<QuestionEntity>
    {
        protected QuestionRepository(QuizifyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
