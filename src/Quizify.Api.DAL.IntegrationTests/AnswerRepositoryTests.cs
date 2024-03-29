﻿using AutoMapper;
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

public class AnswerRepositoryTests : TestBase
{
    private readonly IAnswerRepository _repository;
    public AnswerRepositoryTests(ITestOutputHelper output) : base(output)
    {
        _repository = new AnswerRepository(DbContextFactory.CreateDbContext(),
            new Mapper(
                    new MapperConfiguration(configuration => {
                        configuration.Internal().MethodMappingEnabled = false;
                        configuration.AddMaps(typeof(EntityBase), typeof(ApiBLInstaller));
                    })));
    }


    [Fact]
    public void GetById_ReturnsWithIncludedProperties()
    {
        var answer = _repository.GetById(AnswerSeeds.Answer.Id);
        
        Assert.Equal(answer.Question?.Id, QuestionSeeds.Question.Id);
    }


    [Fact]
    public void AddNewAnswer_Added()
    {
        var answer = _repository.GetById(AnswerSeeds.Answer.Id);
        Guid id = Guid.NewGuid();
        var UserAnswer = new UserAnswerEntity
        {
            UserId = UserSeeds.user.Id,
            AnswerId = AnswerSeeds.Answer.Id,
            Id = id
        };
        answer.Users.Add(UserAnswer);
        _repository.Update(answer);

        answer = _repository.GetById(AnswerSeeds.Answer.Id);
        Assert.Equal(1, answer.Users.Count(a => a.Id == id));
    }
    

    [Fact]
    public void CreateNewAnswer_AnswerCreated()
    {
        var answer = new AnswerEntity
        {
            Id = Guid.NewGuid(),
            Text = "8",
            Type = TypeEnum.MultiSelect,
            IsCorrect = true,
            QuestionId = QuestionSeeds.Question.Id
            
        };
        _repository.Insert(answer);
        var fromRepository = _repository.GetById(answer.Id);

        DeepAssert.Equal(answer, fromRepository);
    }
    
    [Fact]
    public void DeleteAnswerWithAttachedUser_AnswerDeletedWithAttachedUser()
    {
     
        var answer = _repository.GetById(AnswerSeeds.answer2.Id);
        Guid id = Guid.NewGuid();
        var User = new UserAnswerEntity
        {
            UserId = UserSeeds.user2.Id,
            AnswerId = AnswerSeeds.answer2.Id,
            Id = id,
         
        };
        answer.Users.Add(User);
        _repository.Update(answer);
        _repository.Remove(answer.Id);
        answer = _repository.GetById(AnswerSeeds.answer2.Id);
   
        DeepAssert.Equal(null,answer);
    }

}

