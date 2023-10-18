using Microsoft.Extensions.DependencyInjection;
using Quizify.Common.Installers;

namespace Quizify.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInstaller<TInstaller>(this IServiceCollection serviceCollection)
            where TInstaller : IInstaller, new()
        {
            var installer = new TInstaller();
            installer.Install(serviceCollection);
        }
    }
}
