using AutoMapper;
using AutoMapper.Internal;
using Quizify.Api.BL.Installers;
using Quizify.Api.DAL.Common.Tests;
using Quizify.Api.DAL.Common.Tests.Seeds;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Api.DAL.EF.Repositories;
using Quizify.Api.DAL.EF.Repositories.Interfaces;
using Quizify.Common.Enums;
using Quizify.Common.Models;
using Xunit.Abstractions;

namespace Quizify.Api.DAL.IntegrationTests;

public class QuizRepositoryTests : TestBase
{
    private readonly IQuizRepository _repository;
    public QuizRepositoryTests(ITestOutputHelper output) : base(output)
    {
        _repository = new QuizRepository(DbContextFactory.CreateDbContext(), 
            new Mapper(
                    new MapperConfiguration(configuration => {
                        configuration.Internal().MethodMappingEnabled = false;
                        configuration.AddMaps(typeof(EntityBase), typeof(ApiBLInstaller));
                    })));
    }
    
    
    [Fact]
    public void  GetById_ReturnsWithIncludedProperties()
    {
        var quiz = _repository.GetById(QuizSeeds.quiz.Id);
        
        _repository.Update(quiz);

        quiz = _repository.GetById(QuizSeeds.quiz.Id);
        Assert.Equal(quiz.CreatedByUser?.Id,UserSeeds.user.Id);
        Assert.Equal(quiz.Questions?.Count,1);
    }
    
    
    [Fact]
    public void  AddNewUserQuiz_Added()
    {
        var quiz = _repository.GetById(QuizSeeds.quiz.Id);
        Guid id = Guid.NewGuid();
        var quizUser = new QuizUserEntity
        {
            UserId = UserSeeds.user.Id,
            QuizId = QuizSeeds.quiz.Id,
            Id = id
        };
        quiz.Users.Add(quizUser); 
        _repository.Update(quiz);

        quiz = _repository.GetById(QuizSeeds.quiz.Id);
        Assert.Equal(1,quiz.Users.Count(a => a.Id == id));
    }
    
    [Fact]
    public void  AddNewActiveQuestion_ActiveQuestionAdded()
    {
        var quiz = _repository.GetById(QuizSeeds.quiz.Id);


        quiz.ActiveQuestionId = QuestionSeeds.Question.Id;
        _repository.Update(quiz);

        quiz = _repository.GetById(QuizSeeds.quiz.Id);
        Assert.Equal(QuestionSeeds.Question.Id,quiz.ActiveQuestionId);
    }


    [Fact]
    public void CreateNewQuiz_QuizCreated()
    {
        var quiz = new QuizEntity
        {
            Id = Guid.NewGuid(),
            Title = "New Quiz about animals",
            QuizState = QuizStateEnum.Creation,
            CreatedByUserId = UserSeeds.user.Id
        };
        _repository.Insert(quiz);
        var fromRepository = _repository.GetById(quiz.Id);
       
        DeepAssert.Equal(quiz,fromRepository);
    }
    
    
    [Fact]
    public void CountQuizesByGameId_NotInDb_Returns0()
    {

        var count = _repository.CountGamePin("1234");
       
        DeepAssert.Equal(0,count);
    }
    
    [Fact]
    public void CountQuizesByGameId_OneInDb_Returns1()
    {
        var quiz = new QuizEntity
        {
            Title = "new Title",
            QuizState = QuizStateEnum.Creation,
            CreatedByUserId = UserSeeds.user.Id,
            Id = Guid.NewGuid(),
            GamePin = "1234"
        };
        _repository.Insert(quiz);
            
        var count = _repository.CountGamePin("1234");
       
        DeepAssert.Equal(1,count);
    }
}