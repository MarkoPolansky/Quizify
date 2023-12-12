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
            optionsBuilder.UseSqlServer("Server=tcp:sqldb-iw5-2023-team-xletak00.database.windows.net,1433;Initial Catalog=db-iw5-2023-team-xletak00;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";");
            return new QuizifyDbContext(optionsBuilder.Options);
        }
    
    }
}

