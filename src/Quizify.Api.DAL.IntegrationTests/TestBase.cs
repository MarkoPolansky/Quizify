using Microsoft.EntityFrameworkCore;
using Quizify.Api.DAL.Common.Tests;
using Quizify.Api.DAL.EF;
using Quizify.Api.DAL.EF.Factories;
using Xunit.Abstractions;

namespace Quizify.Api.DAL.IntegrationTests;

public class TestBase : IAsyncLifetime
{
    protected IDbContextFactory<QuizifyTestingDbContext> DbContextFactory { get; }
    protected QuizifyTestingDbContext QuizifyDbContextSUT { get; }

    protected  TestBase(ITestOutputHelper output)   
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

     
          
        DbContextFactory = new QuizifyTestingDbContextFactory(GetType().FullName,true);
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