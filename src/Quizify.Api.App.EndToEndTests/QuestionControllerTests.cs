using Quizify.Common.Models;
using Xunit;

namespace Quizify.Api.App.EndToEndTests
{
    public class QuestionControllerTests : IAsyncDisposable
    {
        private readonly QuizifyApiApplicationFactory application;
        private readonly Lazy<HttpClient> client;

        public QuestionControllerTests()
        {
            application = new QuizifyApiApplicationFactory();
            client = new Lazy<HttpClient>(application.CreateClient());
        }

        [Fact]
        public async Task GetAllAnswers_Returns_At_Least_One_User()
        {
            var response = await client.Value.GetAsync("/api/question");

            response.EnsureSuccessStatusCode();

            var questions = await response.Content.ReadFromJsonAsync<ICollection<QuestionListModel>>();
            Assert.NotNull(questions);
            Assert.NotEmpty(questions);
        }

        public async ValueTask DisposeAsync()
        {
            await application.DisposeAsync();
        }
    }
}
