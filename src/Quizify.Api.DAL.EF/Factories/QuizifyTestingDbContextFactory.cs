using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quizify.Api.DAL.Common.Tests;

namespace Quizify.Api.DAL.EF.Factories
{
    public class QuizifyTestingDbContextFactory : IDbContextFactory<QuizifyTestingDbContext>
    {

        private readonly string _dbName;
        private readonly bool _seedData;
        public QuizifyTestingDbContextFactory(string dbName, bool seedData = false) { 
            _dbName = dbName;
            _seedData = seedData;
        }
        
        public QuizifyTestingDbContext CreateDbContext()
        {
            var configuration = new ConfigurationBuilder()
                 .AddUserSecrets<QuizifyDbContextFactory>(optional: true)
                 .Build();
            
            var optionsBuilder = new DbContextOptionsBuilder<QuizifyDbContext>();
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = " + _dbName + ";MultipleActiveResultSets = True;Integrated Security = True; ");
            
            return new QuizifyTestingDbContext(optionsBuilder.Options, _seedData);
        }
    }
}
