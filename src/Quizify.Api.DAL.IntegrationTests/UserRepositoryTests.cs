using AutoMapper;
using AutoMapper.Internal;
using Quizify.Api.BL.Installers;
using Quizify.Api.DAL.Common.Tests.Seeds;
using Quizify.Api.DAL.Common.Tests;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Api.DAL.EF.Repositories.Interfaces;
using Quizify.Api.DAL.EF.Repositories;
using Quizify.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Quizify.Api.DAL.IntegrationTests;

public class UserRepositoryTests : TestBase
{
    private readonly IUserRepository _repository;
    public UserRepositoryTests(ITestOutputHelper output) : base(output)
    {
        _repository = new UserRepository(DbContextFactory.CreateDbContext(),
            new Mapper(
                    new MapperConfiguration(configuration => {
                        configuration.Internal().MethodMappingEnabled = false;
                        configuration.AddMaps(typeof(EntityBase), typeof(ApiBLInstaller));
                    })));
    }


    // [Fact]
    // public void GetById_ReturnsWithIncludedProperties()
    // {
    //     var user = _repository.GetById(UserSeeds.user.Id);
    //     
    //     
    //     Assert.Equal(user.Quizzes.First().Id, QuizSeeds.quiz.Id);
    //     Assert.Equal(user.Answers?.Count, 1);
    // }
    
    
    [Fact]
    public void UpdateUser_UserUpdated()
    {
        var user = _repository.GetById(UserSeeds.user.Id);
        user.Name = "ASdadasd Updated";
        _repository.Update(user);

        var userFromRepository = _repository.GetById(UserSeeds.user.Id);
        Assert.Equal(user, userFromRepository);
    }


    [Fact]
    public void AddNewAnswer_Added()
    {
        var user = _repository.GetById(UserSeeds.user.Id);
        Guid id = Guid.NewGuid();
        var Answer = new UserAnswerEntity
        {
            UserId = UserSeeds.user.Id,
            AnswerId = AnswerSeeds.Answer.Id,
            Id = id
        };
        user.Answers.Add(Answer);
        _repository.Update(user);

        user = _repository.GetById(UserSeeds.user.Id);
        Assert.Equal(1, user.Answers.Count(a => a.Id == id));
    }

    [Fact]
    public void AddNewQuiz_Added()
    {
        var user = _repository.GetById(UserSeeds.user.Id);
        Guid id = Guid.NewGuid();
        var Quiz = new QuizUserEntity
        {
            UserId = UserSeeds.user.Id,
            QuizId = QuizSeeds.quiz.Id,
            Id = id
        };
        user.Quizzes.Add(Quiz);
        _repository.Update(user);

        user = _repository.GetById(UserSeeds.user.Id);
        Assert.Equal(1, user.Quizzes.Count(a => a.Id == id));
    }

    [Fact]
    public void AddNewCreatedQuiz_Added()
    {
        var user = _repository.GetById(UserSeeds.user.Id);
        Guid id = Guid.NewGuid();
        var Quiz = new QuizEntity
        {
            Title = "Very original quiz title",
            QuizState = QuizStateEnum.Creation,
            CreatedByUserId = UserSeeds.user.Id,
            Id = id
        };
        user.CreatedQuizzes.Add(Quiz);
        _repository.Update(user);

        user = _repository.GetById(UserSeeds.user.Id);
        Assert.Equal(1, user.CreatedQuizzes.Count(a => a.Id == id));
    }


    [Fact]
    public void CreateNewUser_UserCreated()
    {
        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = "New user"
        };
        _repository.Insert(user);
        var fromRepository = _repository.GetById(user.Id);

        DeepAssert.Equal(user, fromRepository);
    }
    
    
    
    [Fact]
    public void UpdateUserAddedAnswers_AnswersInserted()
    {
        var user = _repository.GetById(UserSeeds.user.Id);
        Guid id = Guid.NewGuid();
        var AnswerUser = new UserAnswerEntity
        {
            Id = id,
            UserId = UserSeeds.user.Id,
            AnswerId = AnswerSeeds.Answer.Id,
            UserInput = "Zelena"
        };
        user.Answers.Add(AnswerUser);

        _repository.Update(user);
        var fromRepository = _repository.GetById(UserSeeds.user.Id);
        
       DeepAssert.Equal(user, fromRepository);
    }

}

