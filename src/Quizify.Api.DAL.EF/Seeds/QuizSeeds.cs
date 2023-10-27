using Quizify.Api.DAL.EF.Entities;
using Quizify.Common.Enums;

namespace Quizify.Api.DAL.Common.Tests.Seeds;

public static class QuizSeeds
{
    public static QuizEntity quiz = new QuizEntity
    {
        Id = new Guid("e59e2cee-20f3-4e11-9aa0-ac21a3522d08"),
        Title = "New quiz about CI CD",
        QuizState = QuizStateEnum.Creation,
        CreatedByUserId = UserSeeds.user.Id
    };

    public static List<QuizEntity> Seed()
    {
        return new List<QuizEntity>() { quiz };
    }
}