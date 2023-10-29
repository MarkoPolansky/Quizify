using Quizify.Api.DAL.EF.Entities;

namespace Quizify.Api.DAL.Common.Tests.Seeds;

public class QuizUserSeeds
{
    public static QuizUserEntity QuizUser = new()
    {
        Id = new Guid("9ed29fef-6425-43f8-88df-8ce0d76a7d3a"),
        QuizId = QuizSeeds.quiz.Id,
        UserId = UserSeeds.user.Id
    };

    public static List<QuizUserEntity> Seed()
    {
        return new List<QuizUserEntity>() { QuizUser };
    }
}