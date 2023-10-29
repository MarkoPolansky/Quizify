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
    public void CreteUser_UserCreatedNoMock()
    {
    
        var DbContextFactory = new QuizifyTestingDbContextFactory(GetType().FullName!,true).CreateDbContext();
        DbContextFactory.Database.EnsureDeleted();
        DbContextFactory.Database.EnsureCreated();

        var _config = new MapperConfiguration(cfg => cfg.CreateMap<UserDetailModel, UserEntity>());
        var mapper = _config.CreateMapper();

        var repository = new UserRepository(DbContextFactory,mapper);

        var facade = new UserFacade(repository, mapper);

        //var userFromSeeds = facade.GetById(UserSeeds.user.Id);
        
        
        var userId = Guid.NewGuid();
        var user = new UserDetailModel
        {
            Id = userId,
            Name = "Janko",
        };

        facade.Create(user);

        Assert.Equal(user.Id, userId);


        DbContextFactory.Database.EnsureDeleted();
    }


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