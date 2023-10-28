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
    public class QuizTests : TestBase
    {
        public QuizTests(ITestOutputHelper output) : base(output)
        {
        }

    
        [Fact]
        public async Task InserQuiz_QuizInsertedAsync()
        {

            var user = new UserEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Janko"
            };

            var quiz = new QuizEntity
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                CreatedByUserId = user.Id,
                QuizState = QuizStateEnum.Creation,
            };

            QuizifyDbContextSUT.Users.Add(user);
            QuizifyDbContextSUT.Quizzes.Add(quiz);
            QuizifyDbContextSUT.SaveChangesAsync();


            var dbContextText = await DbContextFactory.CreateDbContextAsync();

            var quizFromDb = await dbContextText.Quizzes.Where(u => u.Id == quiz.Id)
                .Include(u => u.CreatedByUser)
                .FirstAsync(); ;

            DeepAssert.Equal(quizFromDb, quiz);

        }



        [Fact]
        public async Task InserQuiz_QuizInsertedAsyncssdsd()
        {

            

        }


        [Fact]
        public async Task InserQuizWithGamePin_GamePinUnique_QuizInsertedAsync()
        {

            var user = new UserEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Janko"
            };

            var quiz = new QuizEntity
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                CreatedByUserId = user.Id,
                QuizState = QuizStateEnum.Creation
            };
            quiz.GamePin = "123456";

            QuizifyDbContextSUT.Users.Add(user);
            QuizifyDbContextSUT.Quizzes.Add(quiz);
            QuizifyDbContextSUT.SaveChangesAsync();


            var dbContextText = await DbContextFactory.CreateDbContextAsync();

            var quizFromDb = await dbContextText.Quizzes.Where(u => u.Id == quiz.Id)
                .Include(u => u.CreatedByUser)
                .FirstAsync(); ;

            DeepAssert.Equal(quizFromDb, quiz);

        }




        [Fact]
        public async Task InserQuizWithGamePin_GamePinNotUnique_NotInserted()
        {

            var user = new UserEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Janko"
            };

            var quiz = new QuizEntity
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                CreatedByUserId = user.Id,
                QuizState = QuizStateEnum.Creation
            };
            quiz.GamePin = "123456";

            var quiz2 = new QuizEntity
            {
                Id = Guid.NewGuid(),
                Title = "Titleasdasd",
                CreatedByUserId = user.Id,
                GamePin = "123456",
                QuizState = QuizStateEnum.Creation
            };

            QuizifyDbContextSUT.Users.Add(user);
            QuizifyDbContextSUT.Quizzes.Add(quiz);
            QuizifyDbContextSUT.SaveChangesAsync();

            QuizifyDbContextSUT.Quizzes.Add(quiz2);
            QuizifyDbContextSUT.SaveChangesAsync();


            var dbContextText = await DbContextFactory.CreateDbContextAsync();

            var quizCount = await dbContextText.Quizzes
                .Include(u => u.CreatedByUser)
                .CountAsync(); ;

            Assert.Equal(quizCount,1);
        

        }




    }
}
