
using System.Globalization;

using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Quizify.Api.App.Extensions;
using Quizify.Api.App.Preprocessors;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Api.DAL.EF.Installers;

var builder = WebApplication.CreateBuilder();

ConfigureCors(builder.Services);
ConfigureLocalization(builder.Services);

ConfigureOpenApiDocuments(builder.Services);
//ConfigureDependencies(builder.Services, builder.Configuration);
//ConfigureAutoMapper(builder.Services);

var app = builder.Build();

//ValidateAutoMapperConfiguration(app.Services);

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

//void ConfigureDependencies(IServiceCollection serviceCollection, IConfiguration configuration)
//{
   
//              var connectionString = configuration.GetConnectionString("DefaultConnection")
//                ?? throw new ArgumentException("The connection string is missing");
//            serviceCollection.AddInstaller<ApiDALEFInstaller>(connectionString);
       
//            serviceCollection.AddInstaller<ApiBLInstaller>();
//}

//void ConfigureAutoMapper(IServiceCollection serviceCollection)
//{
//    serviceCollection.AddAutoMapper(configuration =>
//    {
//        // This is a temporary fix - should be able to remove this when version 11.0.2 comes out
//        // More information here: https://github.com/AutoMapper/AutoMapper/issues/3988
//        configuration.Internal().MethodMappingEnabled = false;
//    }, typeof(EntityBase), typeof(ApiBLInstaller));
//}

//void ValidateAutoMapperConfiguration(IServiceProvider serviceProvider)
//{
//    var mapper = serviceProvider.GetRequiredService<IMapper>();
//    mapper.ConfigurationProvider.AssertConfigurationIsValid();
//}

void UseEndpoints(WebApplication application)
{
    var endpointsBase = application.MapGroup("api")
        .WithOpenApi();

    UseIngredientEndpoints(endpointsBase);
    UseRecipeEndpoints(endpointsBase);
}

void UseIngredientEndpoints(RouteGroupBuilder routeGroupBuilder)
{
}

void UseRecipeEndpoints(RouteGroupBuilder routeGroupBuilder)
{
  
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
{
}
