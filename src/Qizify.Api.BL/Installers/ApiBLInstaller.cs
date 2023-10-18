using Quizify.Common.BL.Facades;
using Microsoft.Extensions.DependencyInjection;
using Quizify.Common.Installers;

namespace Quizify.Api.BL.Installers
{
    public class ApiBLInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<ApiBLInstaller>()
                        .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                        .AsSelfWithInterfaces()
                        .WithScopedLifetime());
        }
    }
}
