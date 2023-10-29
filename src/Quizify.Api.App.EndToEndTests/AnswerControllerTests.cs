using Quizify.Common.Models;
using Xunit;

namespace Quizify.Api.App.EndToEndTests
{
    public class AnswerControllerTests : IAsyncDisposable
    {
        private readonly QuizifyApiApplicationFactory application;
        private readonly Lazy<HttpClient> client;

        public AnswerControllerTests()
        {
            application = new QuizifyApiApplicationFactory();
            client = new Lazy<HttpClient>(application.CreateClient());
        }

        [Fact]
        public async Task GetAllAnswers_Returns_At_Least_One_User()
        {
            var response = await client.Value.GetAsync("/api/answer");

            response.EnsureSuccessStatusCode();

            var answers = await response.Content.ReadFromJsonAsync<ICollection<AnswerListModel>>();
            Assert.NotNull(answers);
            Assert.NotEmpty(answers);
        }

        public async ValueTask DisposeAsync()
        {
            await application.DisposeAsync();
        }
    }
}
