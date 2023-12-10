using AutoMapper;
using Moq;
using Quizify.Api.BL.Facades;
using Quizify.Api.BL.Facades.IFacades;
using Quizify.Api.BL.Services;
using Quizify.Api.DAL.EF.Repositories.Interfaces;
using Quizify.Common.Models;

namespace Quizify.Api.BL.UnitTests;

public class QuizFacadeTests
{
    private static QuizFacade GetFacadeWithForbiddenDependencyCalls()
    {
        // var userFacade = new Mock<IUserFacade>(MockBehavior.Strict).Object;
        // var repository = new Mock<IQuizRepository>(MockBehavior.Strict).Object;
        // var mapper = new Mock<IMapper>(MockBehavior.Strict).Object;
        // var pinGenerationService = new PinGenerationService(new Random());
        // var facade = new QuizFacade(userFacade,repository, mapper, pinGenerationService);
        // return facade;
        return null;
    }
}