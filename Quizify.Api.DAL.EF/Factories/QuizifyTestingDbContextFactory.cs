using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizify.Api.DAL.EF.Factories
{
    public class QuizifyTestingDbContextFactory : IDbContextFactory<QuizifyDbContext>
    {

        private readonly string _dbName;

        public QuizifyTestingDbContextFactory(string dbName) { 
            _dbName = dbName;
        }
        public QuizifyDbContext CreateDbContext()
        {
            var configuration = new ConfigurationBuilder()
                 .AddUserSecrets<QuizifyDbContextFactory>(optional: true)
                 .Build();

            var optionsBuilder = new DbContextOptionsBuilder<QuizifyDbContext>();
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = " + _dbName + ";MultipleActiveResultSets = True;Integrated Security = True; ");

            return new QuizifyDbContext(optionsBuilder.Options);
        }
    }
}
