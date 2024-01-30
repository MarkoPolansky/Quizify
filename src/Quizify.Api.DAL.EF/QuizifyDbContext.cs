using Microsoft.EntityFrameworkCore;
using Quizify.Api.DAL.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quizify.Api.DAL.Common.Tests.Seeds;

namespace Quizify.Api.DAL.EF
{
    public class QuizifyDbContext : DbContext
    {

        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<QuizEntity> Quizzes { get; set; } = null!;
        public DbSet<QuizUserEntity> QuizUsers { get; set; } = null!;
        public DbSet<QuestionEntity> Questions { get; set; } = null!;
        public DbSet<AnswerEntity> Answers { get; set; } = null!;
        public DbSet<UserAnswerEntity> AnswerUsers { get; set; } = null!;
       
        
        public QuizifyDbContext(DbContextOptions<QuizifyDbContext> options)
          : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
           modelBuilder.Entity<UserEntity>()
               .HasMany(u => u.CreatedQuizzes)
               .WithOne(u => u.CreatedByUser)
               .HasForeignKey(u => u.CreatedByUserId)
               .OnDelete(DeleteBehavior.Cascade);
           
           modelBuilder.Entity<QuestionEntity>()
               .HasOne(q => q.ActiveInQuiz)
               .WithOne(q => q.ActiveQuestion)
               .HasForeignKey<QuestionEntity>(q => q.ActiveInQuizId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired(false);
          
           
           modelBuilder.Entity<QuizEntity>()
                           .HasIndex(u => u.GamePin).IsUnique();
           
           modelBuilder.Entity<QuizEntity>()
               .HasMany(a => a.Questions)
               .WithOne(a => a.Quiz)
               .OnDelete(DeleteBehavior.Cascade);
                      
           modelBuilder.Entity<QuestionEntity>()
               .HasMany(a => a.Answers)
               .WithOne(a => a.Question)
               .OnDelete(DeleteBehavior.Cascade);
                      
           modelBuilder.Entity<QuizEntity>()
               .HasMany(a => a.Users)
               .WithOne(d => d.Quiz)
               .OnDelete(DeleteBehavior.Restrict);
           
           modelBuilder.Entity<UserEntity>()
               .HasMany(a => a.Quizzes)
               .WithOne(d => d.User)
               .OnDelete(DeleteBehavior.Restrict);
           
           modelBuilder.Entity<UserEntity>()
               .HasMany(a => a.Answers)
               .WithOne(d => d.User)
               .OnDelete(DeleteBehavior.Restrict);
           
           modelBuilder.Entity<AnswerEntity>()
               .HasMany(a => a.Users)
               .WithOne(d => d.Answer)
               .OnDelete(DeleteBehavior.Restrict);

            
            
       
            
        }
  }
}


