using Quizify.Api.DAL.Common.Tests;
using Quizify.Common.Enums;
using Quizify.Common.Models;
using Xunit;

namespace Quizify.Api.App.EndToEndTests
{
    public class AnswerControllerTests : BaseTest,IAsyncDisposable
    {
      

        public AnswerControllerTests(): base(true){}

        [Fact]
        public async Task GetAllAnswers_Returns_At_Least_One_Answer()
        {
            var response = await client.Value.GetAsync("/api/answer");

            response.EnsureSuccessStatusCode();

            var answers = await response.Content.ReadFromJsonAsync<ICollection<AnswerListModel>>();
            Assert.NotNull(answers);
            Assert.NotEmpty(answers);
        }

        [Fact]
        public async Task CreateAndDeleteAnswer_Test()
        {   
            // Arrange
            Guid answerId = Guid.NewGuid();
            Guid questionId = Guid.NewGuid();
            Guid quizId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            string requestAnswerUri = string.Format("{0}/{1}", baseAnswerUrl, answerId);
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

            response = await client.Value.PostAsJsonAsync(baseQuestionUrl, question);
            response.EnsureSuccessStatusCode();

            var answer = new AnswerDetailModel
            {
                Id = answerId,
                IsCorrect = true,
                QuestionId = questionId,
                Text = "Vejce",
                Type = TypeEnum.SingleSelect
            };

            // Act 1 - Create
            response = await client.Value.PostAsJsonAsync(baseAnswerUrl, answer);
            response.EnsureSuccessStatusCode();

            response = await client.Value.GetAsync(requestAnswerUri);
            response.EnsureSuccessStatusCode();

            var storedAnswer = await response.Content.ReadFromJsonAsync<AnswerDetailModel>();
            // Assert 1
            Assert.NotNull(storedAnswer);

            // Act 2 - Delete
            response = await client.Value.DeleteAsync(requestAnswerUri);
            response.EnsureSuccessStatusCode();

            response = await client.Value.GetAsync(baseAnswerUrl);
            response.EnsureSuccessStatusCode();

            var answers = await response.Content.ReadFromJsonAsync<ICollection<AnswerListModel>>();
            // Assert 2
            Assert.NotNull(answers);
            Assert.Empty(answers.Where(q => q.Id == answerId));

            // Cleaning
            response = await client.Value.DeleteAsync(requestQuestionUri);
            response.EnsureSuccessStatusCode();
            response = await client.Value.DeleteAsync(requestQuizUri);
            response.EnsureSuccessStatusCode();
            response = await client.Value.DeleteAsync(requestUserUri);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task UpdateAnswer_Test()
        {   
            // Arrange
            Guid answerId = Guid.NewGuid();
            Guid questionId = Guid.NewGuid();
            Guid quizId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            string requestAnswerUri = string.Format("{0}/{1}", baseAnswerUrl, answerId);
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

            response = await client.Value.PostAsJsonAsync(baseQuestionUrl, question);
            response.EnsureSuccessStatusCode();

            var answer = new AnswerDetailModel
            {
                Id = answerId,
                IsCorrect = true,
                QuestionId = questionId,
                Text = "Vejce",
                Type = TypeEnum.SingleSelect
            };

            response = await client.Value.PostAsJsonAsync(baseAnswerUrl, answer);
            response.EnsureSuccessStatusCode();

            var answerUpdated = new AnswerDetailModel
            {
                Id = answerId,
                IsCorrect = true,
                QuestionId = questionId,
                Text = "...",
                Type = TypeEnum.SingleSelect
            };

            // Act
            response = await client.Value.PutAsJsonAsync(baseAnswerUrl, answerUpdated);
            response.EnsureSuccessStatusCode();
            // Assert
            response = await client.Value.GetAsync(requestAnswerUri);
            response.EnsureSuccessStatusCode();

            AnswerDetailModel? storedAnswer = await response.Content.ReadFromJsonAsync<AnswerDetailModel>();
            Assert.NotNull(storedAnswer);
            DeepAssert.Equal(storedAnswer, answerUpdated);
            
            // Cleaning
            response = await client.Value.DeleteAsync(requestAnswerUri);
            response.EnsureSuccessStatusCode();
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
