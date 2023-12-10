using Microsoft.AspNetCore.Http;
using Quizify.Common.BL.Facades;
using Microsoft.Extensions.DependencyInjection;
using Quizify.Common.Installers;
using Quizify.Api.BL.Services.Interfaces;
using Quizify.Api.BL.Services;

namespace Quizify.Api.BL.Installers
{
    public class ApiBLInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<Random, Random>();
            serviceCollection.AddScoped<IPinGenerationService, PinGenerationService>();
            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<ApiBLInstaller>()
                        .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                        .AsSelfWithInterfaces()
                        .WithScopedLifetime());
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddScoped<IAuthService, AuthService>();
        }
    }
}
