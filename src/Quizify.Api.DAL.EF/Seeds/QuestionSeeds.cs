using Quizify.Api.DAL.EF.Entities;
using Quizify.Common.Enums;

namespace Quizify.Api.DAL.Common.Tests.Seeds;

public static class QuestionSeeds
{
    public static QuestionEntity Question = new()
    {
        Id = new Guid("9ed29fef-6425-43f8-88df-8ce0d76a7d3a"),
        Text = "Červená",
        Type = TypeEnum.SingleSelect,
        Points = 10,
        QuizId = QuizSeeds.quiz.Id,
    };
    public static QuestionEntity Question2 = new()
    {
        Id = new Guid("05b2d8a5-40e0-4728-b177-ecb5a9dc767f"),
        Text = "Červená",
        Type = TypeEnum.SingleSelect,
        Points = 10,
        QuizId = QuizSeeds.quiz.Id,
    };

    public static List<QuestionEntity> Seed()
    {
        return new List<QuestionEntity>() { Question,Question2 };
    }
}