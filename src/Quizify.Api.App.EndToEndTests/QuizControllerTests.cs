using Quizify.Common.Models;
using Xunit;

namespace Quizify.Api.App.EndToEndTests
{
    public class QuizControllerTests : IAsyncDisposable
    {
        private readonly QuizifyApiApplicationFactory application;
        private readonly Lazy<HttpClient> client;

        public QuizControllerTests()
        {
            application = new QuizifyApiApplicationFactory();
            client = new Lazy<HttpClient>(application.CreateClient());
        }

        [Fact]
        public async Task GetAllAnswers_Returns_At_Least_One_User()
        {
            var response = await client.Value.GetAsync("/api/quiz");

            response.EnsureSuccessStatusCode();

            var quizes = await response.Content.ReadFromJsonAsync<ICollection<QuizListModel>>();
            Assert.NotNull(quizes);
            Assert.NotEmpty(quizes);
        }

        public async ValueTask DisposeAsync()
        {
            await application.DisposeAsync();
        }
    }
}
