using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quizify.Api.DAL.EF.Repositories.Interfaces;

namespace Quizify.Api.DAL.EF.Installers
{
    public class ApiDALEFInstaller
    {
        public void Install(IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<QuizifyDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                }
               );
            

        

            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<ApiDALEFInstaller>()
                    .AddClasses(classes => classes.AssignableTo(typeof(IApiRepository<>)))
                    .AsMatchingInterface()
                    .WithScopedLifetime());
        }
    }
}
