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

    public static List<QuestionEntity> Seed()
    {
        return new List<QuestionEntity>() { Question };
    }
}