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

public class QuestionRepositoryTests : TestBase
{
    private readonly IQuestionRepository _repository;
    public QuestionRepositoryTests(ITestOutputHelper output) : base(output)
    {
        _repository = new QuestionRepository(DbContextFactory.CreateDbContext(),
            new Mapper(
                    new MapperConfiguration(configuration => {
                        configuration.Internal().MethodMappingEnabled = false;
                        configuration.AddMaps(typeof(EntityBase), typeof(ApiBLInstaller));
                    })));
    }


    [Fact]
    public void GetById_ReturnsWithIncludedProperties()
    {
        var question = _repository.GetById(QuestionSeeds.Question.Id);

        _repository.Update(question);

        question = _repository.GetById(QuestionSeeds.Question.Id);
        Assert.Equal(question.Quiz?.Id, QuizSeeds.quiz.Id);
        Assert.Equal(question.Answers?.Count, 1); //TODO: No answers in seeds?
    }


    [Fact]
    public void AddNewAnswer_Added()
    {
        var question = _repository.GetById(QuestionSeeds.Question.Id);
        Guid id = Guid.NewGuid();
        var Answer = new AnswerEntity
        {
            Text = AnswerSeeds.Answer.Text,
            Type = AnswerSeeds.Answer.Type,
            IsCorrect = AnswerSeeds.Answer.IsCorrect,
            QuestionId = QuestionSeeds.Question.Id,
            Id = id
        };
        question.Answers.Add(Answer);
        _repository.Update(question);

        question = _repository.GetById(QuestionSeeds.Question.Id);
        Assert.Equal(1, question.Answers.Count(a => a.Id == id));
    }

    [Fact]
    public void AddNewActiveActiveInQuiz_ActiveActiveInQuiz()
    {
        var question = _repository.GetById(QuestionSeeds.Question.Id);


        question.ActiveInQuizId = QuizSeeds.quiz.Id;
        _repository.Update(question);

        question = _repository.GetById(QuestionSeeds.Question.Id);
        Assert.Equal(QuizSeeds.quiz.Id, question.ActiveInQuizId);
    }


    [Fact]
    public void CreateNewQuestion_QuestionCreated()
    {
        var question = new QuestionEntity
        {
            Id = Guid.NewGuid(),
            Text = "How many legs do spiders have",
            Type = TypeEnum.MultiSelect,
            Points = 5,
            QuizId = QuizSeeds.quiz.Id
        };
        _repository.Insert(question);
        var fromRepository = _repository.GetById(question.Id);

        DeepAssert.Equal(question, fromRepository);
    }

}

