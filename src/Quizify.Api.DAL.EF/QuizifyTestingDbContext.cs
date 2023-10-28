using Microsoft.EntityFrameworkCore;
using Quizify.Api.DAL.Common.Tests.Seeds;
using Quizify.Api.DAL.EF;
using Quizify.Api.DAL.EF.Entities;

namespace Quizify.Api.DAL.Common.Tests;

public class QuizifyTestingDbContext : QuizifyDbContext
{
    
    private readonly bool _seedTestingData;
    public QuizifyTestingDbContext(DbContextOptions<QuizifyDbContext> options, bool seedData = false) : base(options)
    {
        _seedTestingData = seedData;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        
        if (_seedTestingData)
        {
            modelBuilder.Entity<UserEntity>().HasData(UserSeeds.Seed());
            modelBuilder.Entity<QuizEntity>().HasData(QuizSeeds.Seed());
            modelBuilder.Entity<QuestionEntity>().HasData(QuestionSeeds.Seed());
            // modelBuilder.Entity<AnswerEntity>().HasData(AnswerSeeds.Seed());
            // modelBuilder.Entity<UserAnswerEntity>().HasData(AnswerUserSeeds.Seed());
        }
    
        
    }
}