using Quizify.Common.Enums;
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

        [Fact]
        public async Task CreateAndDeleteQuiz_Test()
        {   
            // Arrange
            Guid quizId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            string baseQuizUrl = "/api/quiz";
            string baseUserUrl = "/api/user";
            string requestUri = string.Format("{0}/{1}", baseQuizUrl, quizId);
            string requestUserUri = string.Format("{0}/{1}", baseQuizUrl, userId);
            var user = new UserListModel
            {
                Id = userId,
                Name = "CreatedQuiz"
            };

            var response = await client.Value.PostAsJsonAsync(baseUserUrl, user);
            response.EnsureSuccessStatusCode();

            var quiz = new QuizDetailModel
            {
                Id = quizId,
                QuizState = QuizStateEnum.Creation,
                CreatedByUser = user,
                Title = "First Quiz"
            };
            // Act 1 - Create
            response = await client.Value.PostAsJsonAsync(baseQuizUrl, quiz);
            response.EnsureSuccessStatusCode();

            response = await client.Value.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            var storedQuiz = await response.Content.ReadFromJsonAsync<QuizDetailModel>();
            // Assert 1
            Assert.NotNull(storedQuiz);

            // Act 2 - Delete
            response = await client.Value.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();

            response = await client.Value.GetAsync(baseQuizUrl);
            response.EnsureSuccessStatusCode();

            var quizes = await response.Content.ReadFromJsonAsync<ICollection<QuizListModel>>();
            // Assert 2
            Assert.NotNull(quizes);
            Assert.Empty(quizes.Where(q => q.Id == quizId));

            // Cleaning
            response = await client.Value.DeleteAsync(requestUserUri);
            response.EnsureSuccessStatusCode();
        }

        public async ValueTask DisposeAsync()
        {
            await application.DisposeAsync();
        }
    }
}
