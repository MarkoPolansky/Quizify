using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Quizify.Api.DAL.Common.Tests;
using Quizify.Api.DAL.EF;
using Quizify.Api.DAL.EF.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Quizify.Api.DAL.UnitTests
{
    public class TestBase : IAsyncLifetime
    {
        protected IDbContextFactory<QuizifyDbContext> DbContextFactory { get; }
        protected QuizifyDbContext QuizifyDbContextSUT { get; }

        protected  TestBase(ITestOutputHelper output)
        {
            XUnitTestOutputConverter converter = new(output);
            Console.SetOut(converter);

     
          
            DbContextFactory = new QuizifyTestingDbContextFactory(GetType().FullName);
            QuizifyDbContextSUT = DbContextFactory.CreateDbContext();
        }
        public async Task DisposeAsync()
        {
            await QuizifyDbContextSUT.Database.EnsureDeletedAsync();
            await QuizifyDbContextSUT.DisposeAsync();
        }

        public async Task InitializeAsync()
        {
            await QuizifyDbContextSUT.Database.EnsureDeletedAsync();
            await QuizifyDbContextSUT.Database.EnsureCreatedAsync();
        }
    }
}
