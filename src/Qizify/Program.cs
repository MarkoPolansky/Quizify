using System.Globalization;
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quizify.Api.App.Extensions;
using Quizify.Api.App.Preprocessors;
using Quizify.Api.BL.Facades.IFacades;
using Quizify.Api.BL.Installers;
using Quizify.Api.DAL.EF;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Api.DAL.EF.Extensions;
using Quizify.Api.DAL.EF.Installers;
using Quizify.Common.Extensions;
using Quizify.Common.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder();

ConfigureCors(builder.Services);
ConfigureLocalization(builder.Services);

ConfigureOpenApiDocuments(builder.Services);
ConfigureDependencies(builder.Services, builder.Configuration);
ConfigureAutoMapper(builder.Services);


var app = builder.Build();

ValidateAutoMapperConfiguration(app.Services);

UseDevelopmentSettings(app);
UseSecurityFeatures(app);
UseLocalization(app);
UseRouting(app);
UseEndpoints(app);
UseOpenApi(app);

app.Run();
void ConfigureCors(IServiceCollection serviceCollection)
{
    serviceCollection.AddCors(options =>
    {
        options.AddDefaultPolicy(o =>
            o.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
    });
}


void ConfigureLocalization(IServiceCollection serviceCollection)
{
    serviceCollection.AddLocalization(options => options.ResourcesPath = string.Empty);
}

void ConfigureOpenApiDocuments(IServiceCollection serviceCollection)
{
    serviceCollection.AddEndpointsApiExplorer();
    serviceCollection.AddOpenApiDocument(settings =>
        settings.OperationProcessors.Add(new RequestCultureOperationProcessor()));
}

void ConfigureDependencies(IServiceCollection serviceCollection, IConfiguration configuration)
{
   
    var connectionString = configuration.GetConnectionString("DefaultConnection")
      ?? throw new ArgumentException("The connection string is missing");
    serviceCollection.AddInstaller<ApiDALEFInstaller>(connectionString);
    serviceCollection.AddInstaller<ApiBLInstaller>();
}



void ConfigureAutoMapper(IServiceCollection serviceCollection)
{


    serviceCollection.AddAutoMapper(configuration =>
    {
        // This is a temporary fix - should be able to remove this when version 11.0.2 comes out
        // More information here: https://github.com/AutoMapper/AutoMapper/issues/3988
        configuration.Internal().MethodMappingEnabled = false;
    }, typeof(EntityBase), typeof(ApiBLInstaller));
}

void ValidateAutoMapperConfiguration(IServiceProvider serviceProvider)
{
    var mapper = serviceProvider.GetRequiredService<IMapper>();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
}

void UseEndpoints(WebApplication application)
{
    var endpointsBase = application.MapGroup("api")
        .WithOpenApi();

    UseUserEndpoints(endpointsBase);
    UseQuizEndpoints(endpointsBase);
    UseQuestionEndpoints(endpointsBase);
    UseAnswerEndpoints(endpointsBase);
}


void UseUserEndpoints(RouteGroupBuilder routeGroupBuilder)
{
    var userEndpoints = routeGroupBuilder.MapGroup("user")
        .WithTags("user");


    userEndpoints.MapGet("{id:guid}", Results<Ok<UserDetailModel>, NotFound<string>> (Guid id, IUserFacade userFacade)
        => userFacade.GetById(id) is { } user
            ? TypedResults.Ok(user)
            : TypedResults.NotFound("Error 404 Not Found"));


    userEndpoints.MapGet("/login", (string name,IUserFacade userFacade) => userFacade.Login(name));
    userEndpoints.MapGet("/me", (IUserFacade userFacade) => userFacade.Profile());

    
    userEndpoints.MapGet("/search", (string userName,IUserFacade userFacade) => userFacade.GetUsersByName(userName));
    
    userEndpoints.MapGet("", (IUserFacade userFacade) => userFacade.GetAll());
    userEndpoints.MapPost("", (UserDetailModel user, IUserFacade userFacade) => userFacade.Create(user));
    userEndpoints.MapPut("", (UserDetailModel user, IUserFacade userFacade) => userFacade.Update(user));
    userEndpoints.MapDelete("{id:guid}", (Guid id, IUserFacade userFacade) => userFacade.Delete(id));
}

void UseQuizEndpoints(RouteGroupBuilder routeGroupBuilder)
{
    var questionEndpoints = routeGroupBuilder.MapGroup("quiz")
        .WithTags("quiz");


    questionEndpoints.MapGet("{id:guid}", Results<Ok<QuizDetailModel>, NotFound<string>> (Guid id, IQuizFacade userFacade)
        => userFacade.GetById(id) is { } quiz
            ? TypedResults.Ok(quiz)
            : TypedResults.NotFound("Error 404 Not Found"));


    questionEndpoints.MapGet("", (IQuizFacade quizFacade) => quizFacade.GetAll());
    questionEndpoints.MapPost("", (QuizDetailModel quiz, IQuizFacade quizFacade) => quizFacade.Create(quiz));
    questionEndpoints.MapPut("", (QuizDetailModel quiz, IQuizFacade quizFacade) => quizFacade.Update(quiz));
    questionEndpoints.MapDelete("{id:guid}", (Guid id, IQuizFacade quizFacade) => quizFacade.Delete(id));
    questionEndpoints.MapPost("/join", (string gamePin,string userName, IQuizFacade quizFacade) => quizFacade.Join(gamePin,userName));
    questionEndpoints.MapPost("/{id:guid}/publish", (Guid id, IQuizFacade quizFacade) => quizFacade.Publish(id));
    questionEndpoints.MapPost("/{id:guid}/start", (Guid id, IQuizFacade quizFacade) => quizFacade.Start(id));
    questionEndpoints.MapPost("/{id:guid}/end", (Guid id, IQuizFacade quizFacade) => quizFacade.End(id));
}


void UseQuestionEndpoints(RouteGroupBuilder routeGroupBuilder)
{
    var questionEndpoints = routeGroupBuilder.MapGroup("question")
        .WithTags("question");


    questionEndpoints.MapGet("{id:guid}", Results<Ok<QuestionDetailModel>, NotFound<string>> (Guid id, IQuestionFacade userFacade)
        => userFacade.GetById(id) is { } question
            ? TypedResults.Ok(question)
            : TypedResults.NotFound("Error 404 Not Found"));

    questionEndpoints.MapGet("/search", (string text,IQuestionFacade questionFacade) => questionFacade.GetQuestionByText(text));

    
    questionEndpoints.MapGet("", (IQuestionFacade questionFacade) => questionFacade.GetAll());
    questionEndpoints.MapPost("", (QuestionDetailModel question, IQuestionFacade questionFacade) => questionFacade.Create(question));
    questionEndpoints.MapPut("", (QuestionDetailModel question, IQuestionFacade questionFacade) => questionFacade.Update(question));
    questionEndpoints.MapDelete("{id:guid}", (Guid id, IQuestionFacade questionFacade) => questionFacade.Delete(id));
}

void UseAnswerEndpoints(RouteGroupBuilder routeGroupBuilder)
{
    var answerEndpoints = routeGroupBuilder.MapGroup("answer")
        .WithTags("answer");


    answerEndpoints.MapGet("{id:guid}", Results<Ok<AnswerDetailModel>, NotFound<string>> (Guid id, IAnswerFacade answerFacade)
        => answerFacade.GetById(id) is { } answer
            ? TypedResults.Ok(answer)
            : TypedResults.NotFound("Error 404 Not Found"));

    answerEndpoints.MapGet("/search", (string text,IAnswerFacade answerFacade) => answerFacade.GetAnswersByText(text));


    answerEndpoints.MapGet("", (IAnswerFacade answerFacade) => answerFacade.GetAll());
    answerEndpoints.MapPost("", (AnswerDetailModel answer, IAnswerFacade answerFacade) => answerFacade.Create(answer));
    answerEndpoints.MapPut("", (AnswerDetailModel answer, IAnswerFacade answerFacade) => answerFacade.Update(answer));
    answerEndpoints.MapDelete("{id:guid}", (Guid id, IAnswerFacade answerFacade) => answerFacade.Delete(id));
}

void UseDevelopmentSettings(WebApplication application)
{
    var environment = application.Services.GetRequiredService<IWebHostEnvironment>();

    if (environment.IsDevelopment())
    {
        application.UseDeveloperExceptionPage();
    }
}

void UseSecurityFeatures(IApplicationBuilder application)
{
    application.UseCors();
    application.UseHttpsRedirection();
}

void UseLocalization(IApplicationBuilder application)
{
    application.UseRequestLocalization(new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture(new CultureInfo("en")),
        SupportedCultures = new List<CultureInfo> { new("en"), new("cs") }
    });

    application.UseRequestCulture();
}

void UseRouting(IApplicationBuilder application)
{
    application.UseRouting();
}

void UseOpenApi(IApplicationBuilder application)
{
    application.UseOpenApi();
    application.UseSwaggerUi3();
}


// Make the implicit Program class public so test projects can access it
public partial class Program
{}