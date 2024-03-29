﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quizify.Api.DAL.EF.Repositories.Interfaces;

namespace Quizify.Api.DAL.EF.Installers
{
    public class ApiDALEFInstaller
    {
        public void Install(IServiceCollection serviceCollection, string connectionString, DALType dalType)
        {
            serviceCollection.AddDbContext<QuizifyDbContext>(options =>
                {
                    switch(dalType)
                    {
                        case DALType.Memory:
                            options.UseInMemoryDatabase(connectionString.Length > 0 ? connectionString : "quizify");
                            break;
                        case DALType.EntityFramework:
                            options.UseSqlServer(connectionString);
                            break;
                    }
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
