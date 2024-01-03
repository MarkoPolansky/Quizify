using Microsoft.Extensions.DependencyInjection;
using Quizify.Api.DAL.EF.Installers;

namespace Quizify.Api.DAL.EF.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInstaller<TInstaller>(this IServiceCollection serviceCollection, string connectionString, DALType dalType)
                where TInstaller : ApiDALEFInstaller, new()
        {
            var installer = new TInstaller();
            installer.Install(serviceCollection, connectionString, dalType);
        }
    }
}
