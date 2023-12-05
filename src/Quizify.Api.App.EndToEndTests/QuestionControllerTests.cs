using Quizify.Api.DAL.Common.Tests;
using Quizify.Common.Enums;
using Quizify.Common.Models;
using Xunit;

namespace Quizify.Api.App.EndToEndTests
{
    public class QuestionControllerTests : BaseTest,IAsyncDisposable
    {
        private readonly QuizifyApiApplicationFactory application;
        private readonly Lazy<HttpClient> client;

        public QuestionControllerTests()
        {
            application = new QuizifyApiApplicationFactory();
            client = new Lazy<HttpClient>(application.CreateClient());
        }

        [Fact]
        public async Task GetAllQuestions_Returns_At_Least_One_Question()
        {
            var response = await client.Value.GetAsync("/api/question");

            response.EnsureSuccessStatusCode();

            var questions = await response.Content.ReadFromJsonAsync<ICollection<QuestionListModel>>();
            Assert.NotNull(questions);
            Assert.NotEmpty(questions);
        }

        [Fact]
        public async Task CreateAndDeleteQuestion_Test()
        {   
            // Arrange
            Guid questionId = Guid.NewGuid();
            Guid quizId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            string requestQuestionUri = string.Format("{0}/{1}", baseQuestionUrl, questionId);
            string requestQuizUri = string.Format("{0}/{1}", baseQuizUrl, quizId);
            string requestUserUri = string.Format("{0}/{1}", baseUserUrl, userId);
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

            response = await client.Value.PostAsJsonAsync(baseQuizUrl, quiz);
            response.EnsureSuccessStatusCode();

            var question = new QuestionDetailModel
            {
                Id = questionId,
                Points = 0,
                QuizId = quizId,
                Text = "Co bylo drive vejce nebo slepice?",
                Type = TypeEnum.SingleSelect
            };

            // Act 1 - Create
            response = await client.Value.PostAsJsonAsync(baseQuestionUrl, question);
            response.EnsureSuccessStatusCode();

            response = await client.Value.GetAsync(requestQuestionUri);
            response.EnsureSuccessStatusCode();

            var storedQuestion = await response.Content.ReadFromJsonAsync<QuestionDetailModel>();
            // Assert 1
            Assert.NotNull(storedQuestion);

            // Act 2 - Delete
            response = await client.Value.DeleteAsync(requestQuestionUri);
            response.EnsureSuccessStatusCode();

            response = await client.Value.GetAsync(baseQuestionUrl);
            response.EnsureSuccessStatusCode();

            var questions = await response.Content.ReadFromJsonAsync<ICollection<QuestionListModel>>();
            // Assert 2
            Assert.NotNull(questions);
            Assert.Empty(questions.Where(q => q.Id == questionId));

            // Cleaning
            response = await client.Value.DeleteAsync(requestQuizUri);
            response.EnsureSuccessStatusCode();
            response = await client.Value.DeleteAsync(requestUserUri);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task UpdateQuestion_Test()
        {   
            // Arrange
            Guid questionId = Guid.NewGuid();
            Guid quizId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            string requestQuestionUri = string.Format("{0}/{1}", baseQuestionUrl, questionId);
            string requestQuizUri = string.Format("{0}/{1}", baseQuizUrl, quizId);
            string requestUserUri = string.Format("{0}/{1}", baseUserUrl, userId);
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

            response = await client.Value.PostAsJsonAsync(baseQuizUrl, quiz);
            response.EnsureSuccessStatusCode();

            var question = new QuestionDetailModel
            {
                Id = questionId,
                Points = 0,
                QuizId = quizId,
                Text = "Co bylo drive vejce nebo slepice?",
                Type = TypeEnum.SingleSelect
            };

            var questionUpdated = new QuestionDetailModel
            {
                Id = questionId,
                Points = 0,
                QuizId = quizId,
                Text = "Co bylo drive slepice nebo ...?",
                Type = TypeEnum.SingleSelect
            };

            response = await client.Value.PostAsJsonAsync(baseQuestionUrl, question);
            response.EnsureSuccessStatusCode();
            // Act
            response = await client.Value.PutAsJsonAsync(baseQuestionUrl, questionUpdated);
            response.EnsureSuccessStatusCode();
            // Assert
            response = await client.Value.GetAsync(requestQuestionUri);
            response.EnsureSuccessStatusCode();

            QuestionDetailModel? storedQuestion = await response.Content.ReadFromJsonAsync<QuestionDetailModel>();
            Assert.NotNull(storedQuestion);
            DeepAssert.Equal(storedQuestion, questionUpdated);
            
            // Cleaning
            response = await client.Value.DeleteAsync(requestQuestionUri);
            response.EnsureSuccessStatusCode();
            response = await client.Value.DeleteAsync(requestQuizUri);
            response.EnsureSuccessStatusCode();
            response = await client.Value.DeleteAsync(requestUserUri);
            response.EnsureSuccessStatusCode();
        }

        public async ValueTask DisposeAsync()
        {
            await application.DisposeAsync();
        }
    }
}
