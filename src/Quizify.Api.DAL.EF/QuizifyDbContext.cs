using Microsoft.EntityFrameworkCore;
using Quizify.Api.DAL.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // modelBuilder.Entity<QuizEntity>()
            // .HasOne(c => c.CreatedByUser)
            // .WithMany(d => d.CreatedQuizzes)
            // .HasForeignKey(c => c.CreatedByUserId)
            // .OnDelete(DeleteBehavior.Restrict);
            //
            // modelBuilder.Entity<QuizEntity>()
            //     .HasOne(e => e.ActiveQuestion)
            //     .WithOne(d => d.ActiveInQuiz)
            //     .HasForeignKey<QuizEntity>(c => c.ActiveQuestionId)
            //     .IsRequired(false);

            // modelBuilder.Entity<QuizEntity>()
            //     .HasOne(e => e.ActiveQuestion)
            //     .WithOne(a => a.ActiveInQuiz)
            //     .HasForeignKey<QuestionEntity>("ActiveInQuizId")
            //     .IsRequired(false);

           //  modelBuilder.Entity<QuestionEntity>()
           //      .HasOne(e => e.Quiz)
           //      .WithMany(e => e.Questions);
           //  
           //
           //  modelBuilder.Entity<QuizEntity>()
           //      .HasMany<QuizUserEntity>()
           //      .WithOne(d => d.Quiz)
           //      .OnDelete(DeleteBehavior.Restrict);
           //  
           //  
           //  modelBuilder.Entity<QuizEntity>()
           //    .HasMany<QuizUserEntity>()
           //    .WithOne(d => d.Quiz)
           //    .OnDelete(DeleteBehavior.Restrict);
           //
           //  modelBuilder.Entity<QuizEntity>()
           //      .HasIndex(u => u.GamePin).IsUnique();
           //
           //  modelBuilder.Entity<UserEntity>()
           //  .HasMany<QuizUserEntity>()
           //  .WithOne(d => d.User)
           //  .OnDelete(DeleteBehavior.Restrict);
           //
           //  modelBuilder.Entity<UserEntity>()
           // .HasMany<UserAnswerEntity>()
           // .WithOne(d => d.User)
           // .OnDelete(DeleteBehavior.Restrict);
           //
           //  modelBuilder.Entity<AnswerEntity>()
           // .HasMany<UserAnswerEntity>()
           // .WithOne(d => d.Answer)
           // .OnDelete(DeleteBehavior.Restrict);

           modelBuilder.Entity<UserEntity>()
               .HasMany(u => u.CreatedQuizzes)
               .WithOne(u => u.CreatedByUser)
               .HasForeignKey(u => u.CreatedByUserId);
           
           
           modelBuilder.Entity<QuestionEntity>()
               .HasOne(q => q.ActiveInQuiz)
               .WithOne(q => q.ActiveQuestion)
               .HasForeignKey<QuestionEntity>(q => q.ActiveInQuizId)
               .IsRequired(false);
           
            modelBuilder.Entity<QuizEntity>()
                .HasMany<QuizUserEntity>()
                .WithOne(d => d.Quiz)
                .OnDelete(DeleteBehavior.Restrict);
            
            
            modelBuilder.Entity<QuizEntity>()
              .HasMany<QuizUserEntity>()
              .WithOne(d => d.Quiz)
              .OnDelete(DeleteBehavior.Restrict);
           
            modelBuilder.Entity<QuizEntity>()
                .HasIndex(u => u.GamePin).IsUnique();
           
            modelBuilder.Entity<UserEntity>()
            .HasMany<QuizUserEntity>()
            .WithOne(d => d.User)
            .OnDelete(DeleteBehavior.Restrict);
           
            modelBuilder.Entity<UserEntity>()
           .HasMany<UserAnswerEntity>()
           .WithOne(d => d.User)
           .OnDelete(DeleteBehavior.Restrict);
           
            modelBuilder.Entity<AnswerEntity>()
           .HasMany<UserAnswerEntity>()
           .WithOne(d => d.Answer)
           .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<QuestionEntity>()
                .HasMany(a => a.Answers)
                .WithOne(a => a.Question)
                .OnDelete(DeleteBehavior.Cascade);
            
            
            modelBuilder.Entity<QuizEntity>()
                .HasMany(a => a.Questions)
                .WithOne(a => a.Quiz)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<UserEntity>()
                .HasMany(a => a.CreatedQuizzes)
                .WithOne(a => a.CreatedByUser)
                .OnDelete(DeleteBehavior.Cascade);
        }
  }
}


