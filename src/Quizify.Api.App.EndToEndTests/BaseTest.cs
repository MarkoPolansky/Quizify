using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Quizify.Api.BL.Services;
using Quizify.Api.BL.Services.Interfaces;
using Quizify.Api.DAL.Common.Tests;
using Quizify.Api.DAL.Common.Tests.Seeds;
using Quizify.Api.DAL.EF;
using Xunit;

namespace Quizify.Api.App.EndToEndTests;

public abstract class BaseTest : IAsyncLifetime
{
    protected string baseQuestionUrl = "/api/question";
    protected string baseQuizUrl = "/api/quiz";
    protected string baseUserUrl = "/api/user";
    protected string baseAnswerUrl = "/api/Answer";
    
    
    protected  QuizifyApiApplicationFactory application;
    protected  Lazy<HttpClient> client;
    protected  IMapper _mapper;
   

    private bool ShouldSeedData { get; set; }


    public BaseTest(bool seed)
    {
        application = new QuizifyApiApplicationFactory();
        client = new Lazy<HttpClient>(application.CreateClient());
        _mapper = application.Services.GetRequiredService<IMapper>();
        ShouldSeedData = seed;
       
    }


    private async Task Seed()
    {
        foreach (var user in UserSeeds.Seed())
        {
            await client.Value.PostAsJsonAsync(baseUserUrl,user);
        }
        foreach (var quiz in QuizSeeds.Seed())
        {
            await client.Value.PostAsJsonAsync(baseQuizUrl,quiz);
        }
        foreach (var question in QuestionSeeds.Seed())
        {
            await client.Value.PostAsJsonAsync(baseQuestionUrl,question);
        }
        foreach (var answer in AnswerSeeds.Seed())
        {
            await client.Value.PostAsJsonAsync(baseAnswerUrl,answer);
        }
        
    }

    public async Task InitializeAsync()
    {
       await application.Services.GetRequiredService<QuizifyDbContext>().Database.EnsureDeletedAsync();
       await application.Services.GetRequiredService<QuizifyDbContext>().Database.EnsureCreatedAsync();
       if (ShouldSeedData)
           await Seed();
      
    }

    public async Task DisposeAsync()
    {
        await application.Services.GetRequiredService<QuizifyDbContext>().Database.EnsureDeletedAsync();
        await application.Services.GetRequiredService<QuizifyDbContext>().DisposeAsync();


    }

  
}