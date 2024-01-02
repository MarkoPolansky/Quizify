using Quizify.Api.DAL.EF.Entities;
using Quizify.Common.Enums;

namespace Quizify.Api.DAL.Common.Tests.Seeds;

public static class AnswerSeeds
{
    public static AnswerEntity Answer = new AnswerEntity
    {
        Id = new Guid("3d36cd2d-7482-49f6-bc8f-b3beb4046f2c"),
        Text = "Červená",
        Type = TypeEnum.SingleSelect,
        IsCorrect = true,
        QuestionId = QuestionSeeds.Question.Id
    };
    
    public static AnswerEntity answer2 = new AnswerEntity
    {
        Id = new Guid("69edc381-3245-4b52-9a58-1579a906f9bd"),
        Text = "Červená",
        Type = TypeEnum.SingleSelect,
        IsCorrect = true,
        QuestionId = QuestionSeeds.Question.Id
    };

    public static List<AnswerEntity> Seed()
    {
        return new List<AnswerEntity>() { Answer,answer2 };
    }
}