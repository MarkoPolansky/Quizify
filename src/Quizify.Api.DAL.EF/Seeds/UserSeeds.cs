using Quizify.Api.DAL.EF.Entities;

namespace Quizify.Api.DAL.Common.Tests.Seeds;

public static class UserSeeds
{
    public static UserEntity user = new UserEntity
    {
        Name = "Gejza",
        Id = new Guid("b83a8ab0-a5dc-4d77-b778-ac30b7422576")
    };

    public static List<UserEntity> Seed()
    {
        return new List<UserEntity>() { user };
    }

}