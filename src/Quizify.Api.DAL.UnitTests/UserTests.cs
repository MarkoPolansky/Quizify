using Microsoft.EntityFrameworkCore;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Api.DAL.EF.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Quizify.Api.DAL.UnitTests
{
    public class UserTests : TestBase
    {
        public UserTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task InserUser_UserInsertedAsync()
        {   
        
            var user = new UserEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Janko"
            };

            QuizifyDbContextSUT.Users.Add(user);
            QuizifyDbContextSUT.SaveChanges();


            var dbContextText = await DbContextFactory.CreateDbContextAsync();

            var userFromDb = await  dbContextText.Users.SingleAsync(u => u.Id == user.Id);

            Assert.Equal(user.Id, userFromDb.Id);

        }
    }
}
