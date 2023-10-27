using Quizify.Api.DAL.EF.Entities;
using Quizify.Common.Enums;

namespace Quizify.Api.DAL.Common.Tests.Seeds;

public static class AnswerSeeds
{
    public static AnswerEntity Answer = new AnswerEntity
    {
        Id = new Guid("e59e2cee-20f3-4e11-9aa0-ac21a3522d08"),
        Text = "Červená",
        Type = TypeEnum.SingleSelect,
        IsCorrect = true,
        QuestionId = default
    };

    public static List<AnswerEntity> Seed()
    {
        return new List<AnswerEntity>() { Answer };
    }
}