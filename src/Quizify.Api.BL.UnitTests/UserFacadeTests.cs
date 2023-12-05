using AutoMapper;
using Moq;
using Quizify.Api.BL.Facades;
using Quizify.Api.DAL.Common.Tests.Seeds;
using Quizify.Api.DAL.EF;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Api.DAL.EF.Factories;
using Quizify.Api.DAL.EF.Repositories;
using Quizify.Api.DAL.EF.Repositories.Interfaces;
using Quizify.Common.Models;

namespace Quizify.Api.BL.UnitTests;

public class UserFacadeTests
{
    
    [Fact]
    public void CreteUser_UserCreatedMock()
    {

        var facade = GetFacadeWithForbiddenDependencyCalls();
        var userId = Guid.NewGuid();
        var user = new UserDetailModel
        {
            Id = userId,
            Name = "Janko",
        };

        

        // Assert.Equal(String.Empty, user.Name);

    }

    private static UserFacade GetFacadeWithForbiddenDependencyCalls()
    {
        

        var repository = new Mock<IUserRepository>(MockBehavior.Loose);
        var mapper = new Mock<IMapper>(MockBehavior.Loose).Object;
        var facade = new UserFacade(repository.Object, mapper);
        return facade;
    }
}