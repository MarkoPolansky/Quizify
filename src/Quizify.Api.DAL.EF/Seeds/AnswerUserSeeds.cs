using Quizify.Api.DAL.EF.Entities;

namespace Quizify.Api.DAL.Common.Tests.Seeds;

public class AnswerUserSeeds
{
    public static UserAnswerEntity UserAnswer = new UserAnswerEntity
    {
        Id = new Guid("e59e2cee-20f3-4e11-9aa0-ac21a3522d08"),
        UserId = UserSeeds.user.Id,
        AnswerId = AnswerSeeds.Answer.Id,
    };

    public static List<UserAnswerEntity> Seed()
    {
        return new List<UserAnswerEntity>() { UserAnswer };
    }
}