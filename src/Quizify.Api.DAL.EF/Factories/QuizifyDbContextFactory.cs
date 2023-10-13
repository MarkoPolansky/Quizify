using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace Quizify.Api.DAL.EF.Factories
{
    public class QuizifyDbContextFactory : IDesignTimeDbContextFactory<QuizifyDbContext>
    {
        public QuizifyDbContext CreateDbContext(string[] args)
        { 
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<QuizifyDbContextFactory>(optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<QuizifyDbContext>();

            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = quizify; Integrated Security = True");
            return new QuizifyDbContext(optionsBuilder.Options);
        }
    
    }
}

