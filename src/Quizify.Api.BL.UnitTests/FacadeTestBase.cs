using AutoMapper;
using Quizify.Common.Extensions;
using Quizify.Api.BL.Facades.IFacades;
using Quizify.Api.BL.MapperProfiles;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Api.DAL.EF.Entities.Interfaces;
using Quizify.Api.DAL.EF.Repositories.Interfaces;
using Quizify.Common;
using Quizify.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quizify.Api.DAL.EF.Factories;
using Microsoft.EntityFrameworkCore;
using Quizify.Api.DAL.Common.Tests;
using Quizify.Api.DAL.EF.Repositories;
using Xunit.Abstractions;

namespace Quizify.Api.BL.UnitTests
{
    public class FacadeTestBase : IAsyncLifetime
    {
        protected IDbContextFactory<QuizifyTestingDbContext> DbContextFactory { get; }

        public FacadeTestBase(ITestOutputHelper output)
        {
            DbContextFactory = new QuizifyTestingDbContextFactory(GetType().FullName!,true);

        }

        async Task IAsyncLifetime.InitializeAsync()
        {
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            await dbx.Database.EnsureDeletedAsync();
            await dbx.Database.EnsureCreatedAsync();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            await dbx.Database.EnsureDeletedAsync();
        }
    }   
}
