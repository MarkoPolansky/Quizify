using Microsoft.EntityFrameworkCore;
using Quizify.Api.DAL.Common.Tests;
using Quizify.Api.DAL.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quizify.Common.Enums;
using Xunit.Abstractions;

namespace Quizify.Api.DAL.UnitTests
{
    public class QuizUserTests : TestBase
    {
        public QuizUserTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task Assign2UsersTo1Quiz_Assigned()
        {

            var user1 = new UserEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Janko"
            };

            var user2 = new UserEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Honza"
            };

            var quiz = new QuizEntity
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                CreatedByUserId = user1.Id,
                QuizState = QuizStateEnum.Creation,
            };
            var assign1 = new QuizUserEntity()
            {
                Id = Guid.NewGuid(),
                UserId = user1.Id,
                QuizId = quiz.Id,

            };
            var assign2 = new QuizUserEntity()
            {
                Id = Guid.NewGuid(),
                UserId = user2.Id,
                QuizId = quiz.Id,

            };

            QuizifyDbContextSUT.Users.Add(user1);
            QuizifyDbContextSUT.Users.Add(user2);
            QuizifyDbContextSUT.Quizzes.Add(quiz);
            QuizifyDbContextSUT.QuizUsers.Add(assign1);
            QuizifyDbContextSUT.QuizUsers.Add(assign2);
            QuizifyDbContextSUT.SaveChangesAsync();


            var dbContextText = await DbContextFactory.CreateDbContextAsync();

            List<QuizUserEntity> recordsFromDb = await dbContextText.QuizUsers
                .Include(u => u.User)
                .Include(u => u.Quiz)
                .ToListAsync();


            DeepAssert.Equal(new List<QuizUserEntity> { assign1 , assign2}, recordsFromDb);

        }
    }
}
