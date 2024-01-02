using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Quizify.Common.BL.Facades;
using Quizify.Common.Extensions;

namespace Quizify.Web.BL.Installers
{
    public class WebBLInstaller
    {
        public void Install(IServiceCollection serviceCollection, string apiBaseUrl,HttpClient clients)
        {
            serviceCollection.AddTransient<IUserApiClient, UserApiClient>(provider =>
            {
                var client = CreateApiHttpClient(provider, apiBaseUrl);
                return new UserApiClient(client, apiBaseUrl);
            });

            serviceCollection.AddTransient<IQuizApiClient, QuizApiClient>(provider =>
            {
                var client = CreateApiHttpClient(provider, apiBaseUrl);
                return new QuizApiClient(client, apiBaseUrl);
            });
            serviceCollection.AddTransient<IQuestionApiClient, QuestionApiClient>(provider =>
            {
                var client = CreateApiHttpClient(provider, apiBaseUrl);
                return new QuestionApiClient(client, apiBaseUrl);
            });
            serviceCollection.AddTransient<IAnswerApiClient, AnswerApiClient>(provider =>
            {
                var client = CreateApiHttpClient(provider, apiBaseUrl);
                return new AnswerApiClient(client, apiBaseUrl);
            });
            
            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<WebBLInstaller>()
                    .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime());
        }

        public HttpClient CreateApiHttpClient(IServiceProvider serviceProvider, string apiBaseUrl)
        {
            var client = new HttpClient() { BaseAddress = new Uri(apiBaseUrl) };
            client.BaseAddress = new Uri(apiBaseUrl);
            var _jsModule = serviceProvider.GetRequiredService<IJSRuntime>();
            string token = "";
            try
            {
                token = ((IJSInProcessRuntime)_jsModule).Invoke<string>("getToken");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (!String.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Add( "Authorization", token);
            }
            
            return client;
        }
        
    }
}
