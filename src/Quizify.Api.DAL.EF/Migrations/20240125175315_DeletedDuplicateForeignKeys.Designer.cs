﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quizify.Api.DAL.EF;

#nullable disable

namespace Quizify.Api.DAL.EF.Migrations
{
    [DbContext(typeof(QuizifyDbContext))]
    [Migration("20240125175315_DeletedDuplicateForeignKeys")]
    partial class DeletedDuplicateForeignKeys
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Quizify.Api.DAL.EF.Entities.AnswerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Quizify.Api.DAL.EF.Entities.QuestionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ActiveInQuizId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<Guid>("QuizId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeLimit")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActiveInQuizId")
                        .IsUnique()
                        .HasFilter("[ActiveInQuizId] IS NOT NULL");

                    b.HasIndex("QuizId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Quizify.Api.DAL.EF.Entities.QuizEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ActiveQuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedByUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GamePin")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuizState")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("GamePin")
                        .IsUnique()
                        .HasFilter("[GamePin] IS NOT NULL");

                    b.ToTable("Quizzes");
                });

            modelBuilder.Entity("Quizify.Api.DAL.EF.Entities.QuizUserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("EndedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("QuizId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalPoints")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.HasIndex("UserId");

                    b.ToTable("QuizUsers");
                });

            modelBuilder.Entity("Quizify.Api.DAL.EF.Entities.UserAnswerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnswerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserInput")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("UserId");

                    b.ToTable("AnswerUsers");
                });

            modelBuilder.Entity("Quizify.Api.DAL.EF.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Quizify.Api.DAL.EF.Entities.AnswerEntity", b =>
                {
                    b.HasOne("Quizify.Api.DAL.EF.Entities.QuestionEntity", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Quizify.Api.DAL.EF.Entities.QuestionEntity", b =>
                {
                    b.HasOne("Quizify.Api.DAL.EF.Entities.QuizEntity", "ActiveInQuiz")
                        .WithOne("ActiveQuestion")
                        .HasForeignKey("Quizify.Api.DAL.EF.Entities.QuestionEntity", "ActiveInQuizId");

                    b.HasOne("Quizify.Api.DAL.EF.Entities.QuizEntity", "Quiz")
                        .WithMany("Questions")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActiveInQuiz");

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("Quizify.Api.DAL.EF.Entities.QuizEntity", b =>
                {
                    b.HasOne("Quizify.Api.DAL.EF.Entities.UserEntity", "CreatedByUser")
                        .WithMany("CreatedQuizzes")
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByUser");
                });

            modelBuilder.Entity("Quizify.Api.DAL.EF.Entities.QuizUserEntity", b =>
                {
                    b.HasOne("Quizify.Api.DAL.EF.Entities.QuizEntity", "Quiz")
                        .WithMany("Users")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Quizify.Api.DAL.EF.Entities.UserEntity", "User")
                        .WithMany("Quizzes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Quiz");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Quizify.Api.DAL.EF.Entities.UserAnswerEntity", b =>
                {
                    b.HasOne("Quizify.Api.DAL.EF.Entities.AnswerEntity", "Answer")
                        .WithMany("Users")
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Quizify.Api.DAL.EF.Entities.UserEntity", "User")
                        .WithMany("Answers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Answer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Quizify.Api.DAL.EF.Entities.AnswerEntity", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Quizify.Api.DAL.EF.Entities.QuestionEntity", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("Quizify.Api.DAL.EF.Entities.QuizEntity", b =>
                {
                    b.Navigation("ActiveQuestion");

                    b.Navigation("Questions");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Quizify.Api.DAL.EF.Entities.UserEntity", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("CreatedQuizzes");

                    b.Navigation("Quizzes");
                });
#pragma warning restore 612, 618
        }
    }
}
