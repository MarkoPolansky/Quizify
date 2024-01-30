using System.Net.Http.Headers;
using Quizify.Api.DAL.Common.Tests;
using Quizify.Common.Enums;
using Quizify.Common.Models;
using Xunit;

namespace Quizify.Api.App.EndToEndTests
{
    public class QuizControllerTests : BaseTest
    {
        public QuizControllerTests(): base(true){}

        [Fact]
        public async Task GetAllQuizes_Returns_At_Least_One_Quiz()
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
            // Act 1 - Create
            response = await client.Value.PostAsJsonAsync(baseQuizUrl, quiz);
            response.EnsureSuccessStatusCode();

            response = await client.Value.GetAsync(requestQuizUri);
            response.EnsureSuccessStatusCode();

            var storedQuiz = await response.Content.ReadFromJsonAsync<QuizDetailModel>();
            // Assert 1
            Assert.NotNull(storedQuiz);

            // Act 2 - Delete
            response = await client.Value.DeleteAsync(requestQuizUri);
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

        [Fact]
        public async Task UpdateQuiz_Test()
        {   
            // Arrange
            Guid quizId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
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
                Title = "Not updated Quiz"
            };

            var quizUpdated = new QuizDetailModel
            {
                Id = quizId,
                QuizState = QuizStateEnum.Creation,
                CreatedByUser = user,
                Title = "Updated Quiz"
            };

            response = await client.Value.PostAsJsonAsync(baseQuizUrl, quiz);
            response.EnsureSuccessStatusCode();
            // Act
            response = await client.Value.PutAsJsonAsync(baseQuizUrl, quizUpdated);
            response.EnsureSuccessStatusCode();
            // Assert
            response = await client.Value.GetAsync(requestQuizUri);
            response.EnsureSuccessStatusCode();

            QuizDetailModel? storedQuiz = await response.Content.ReadFromJsonAsync<QuizDetailModel>();
            Assert.NotNull(storedQuiz);
            DeepAssert.Equal(storedQuiz, quizUpdated);
            
            // Cleaning
            response = await client.Value.DeleteAsync(requestQuizUri);
            response.EnsureSuccessStatusCode();
            response = await client.Value.DeleteAsync(requestUserUri);
            response.EnsureSuccessStatusCode();
        }
        
        
        [Fact]
        public async Task PublishQuiz_QuizPublished()
        {
            // Arrange
            Guid quizId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            var user = new UserDetailModel
            {
                Id = userId ,
                Name = "meno",
                ImageUrl = null,
                CreatedQuizzes = new List<QuizListModel>(),
                Quizzes = new List<UserDetailQuizModel>(),
                Answers = new List<UserDetailAnswerModel>()
            };
            var quiz = new QuizDetailModel
            {
                Id = quizId,
                Title = "title",
                ImageUrl = null,
                GamePin = null,
                QuizState = QuizStateEnum.Creation,
                ActiveQuestion = null,
                CreatedByUser = _mapper.Map<UserListModel>(user),
                Questions = new List<QuestionListModel>(),
                Users = new List<QuizDetailUserModel>()
            };
          
            string publishQuizUri = baseQuizUrl + "/"+ quiz.Id +"/publish" ;
            
            //Act
            var response = await client.Value.PostAsJsonAsync(baseUserUrl,user);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.PostAsJsonAsync(baseQuizUrl,quiz);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.PostAsync(publishQuizUri,null);
            response.EnsureSuccessStatusCode();
         

            response = await client.Value.GetAsync(baseQuizUrl+"/"+quizId);
            response.EnsureSuccessStatusCode();
            var storedPublishedQuiz = await response.Content.ReadFromJsonAsync<QuizDetailModel>();
            
            //Assert

            Assert.NotNull(storedPublishedQuiz);
            quiz.GamePin = storedPublishedQuiz.GamePin;
            quiz.QuizState = QuizStateEnum.Published;
            
            DeepAssert.Equal(quiz,_mapper.Map<QuizDetailModel>(storedPublishedQuiz));
        }
        
        
        [Fact]
        public async Task PublishQuiz_JoinQuizViaGameCode_DeleteJoinedUser_JoinedUserAndQuizUserDeleted()
        {
            // Arrange
            Guid quizId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            Guid joiningUserId = Guid.NewGuid();
            var joiningUser = new UserDetailModel
            {
                Id = joiningUserId ,
                Name = "meno",
                ImageUrl = null,
                CreatedQuizzes = new List<QuizListModel>(),
                Quizzes = new List<UserDetailQuizModel>(),
                Answers = new List<UserDetailAnswerModel>()
            };
            var user = new UserDetailModel
            {
                Id = userId ,
                Name = "meno",
                ImageUrl = null,
                CreatedQuizzes = new List<QuizListModel>(),
                Quizzes = new List<UserDetailQuizModel>(),
                Answers = new List<UserDetailAnswerModel>()
            };
            var quiz = new QuizDetailModel
            {
                Id = quizId,
                Title = "title",
                ImageUrl = null,
                GamePin = null,
                QuizState = QuizStateEnum.Creation,
                ActiveQuestion = null,
                CreatedByUser = _mapper.Map<UserListModel>(user),
                Questions = new List<QuestionListModel>(),
                Users = new List<QuizDetailUserModel>()
            };
          
            string publishQuizUri = baseQuizUrl + "/"+ quiz.Id +"/publish" ;
            
            //Act
            var response = await client.Value.PostAsJsonAsync(baseUserUrl,user);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.PostAsJsonAsync(baseUserUrl,joiningUser);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.PostAsJsonAsync(baseQuizUrl,quiz);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.PostAsync(publishQuizUri,null);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.GetAsync(baseQuizUrl+"/"+quizId);
            response.EnsureSuccessStatusCode();
            var storedPublishedQuiz = await response.Content.ReadFromJsonAsync<QuizDetailModel>();


            client.Value.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(joiningUserId.ToString());
            response = await client.Value.PostAsync(baseQuizUrl+"/join?gamePin="+storedPublishedQuiz.GamePin,null);
            response.EnsureSuccessStatusCode();
            
            
            response = await client.Value.GetAsync(baseUserUrl+"/"+joiningUserId);
            response.EnsureSuccessStatusCode();
            var storedJoinedUser = await response.Content.ReadFromJsonAsync<UserDetailModel>();
            
            
            response = await client.Value.DeleteAsync(baseUserUrl+"/"+joiningUserId);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.GetAsync(baseQuizUrl+"/"+quizId);
            response.EnsureSuccessStatusCode();
            var storedPublishedQuizAfterJoinedUserDeleted = await response.Content.ReadFromJsonAsync<QuizDetailModel>();


            
            //Assert
            
            Assert.NotNull(storedJoinedUser);
            Assert.NotEmpty(storedJoinedUser.Quizzes);
            DeepAssert.Equal(storedJoinedUser.Quizzes.First().Quiz,_mapper.Map<QuizListModel>(storedPublishedQuiz));

            DeepAssert.Equal( storedPublishedQuiz,storedPublishedQuizAfterJoinedUserDeleted);
        }
        
        
         [Fact]
        public async Task DeleteQuizWithJoinedUser_Quiz_And_QuizUser_Deleted()
        {
            // Arrange
            Guid quizId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            Guid joiningUserId = Guid.NewGuid();
            var joiningUser = new UserDetailModel
            {
                Id = joiningUserId ,
                Name = "meno",
                ImageUrl = null,
                CreatedQuizzes = new List<QuizListModel>(),
                Quizzes = new List<UserDetailQuizModel>(),
                Answers = new List<UserDetailAnswerModel>()
            };
            var user = new UserDetailModel
            {
                Id = userId ,
                Name = "meno",
                ImageUrl = null,
                CreatedQuizzes = new List<QuizListModel>(),
                Quizzes = new List<UserDetailQuizModel>(),
                Answers = new List<UserDetailAnswerModel>()
            };
            var quiz = new QuizDetailModel
            {
                Id = quizId,
                Title = "title",
                ImageUrl = null,
                GamePin = null,
                QuizState = QuizStateEnum.Creation,
                ActiveQuestion = null,
                CreatedByUser = _mapper.Map<UserListModel>(user),
                Questions = new List<QuestionListModel>(),
                Users = new List<QuizDetailUserModel>()
            };
          
            string publishQuizUri = baseQuizUrl + "/"+ quiz.Id +"/publish" ;
            
            //Act
            var response = await client.Value.PostAsJsonAsync(baseUserUrl,user);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.PostAsJsonAsync(baseUserUrl,joiningUser);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.PostAsJsonAsync(baseQuizUrl,quiz);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.PostAsync(publishQuizUri,null);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.GetAsync(baseQuizUrl+"/"+quizId);
            response.EnsureSuccessStatusCode();
            var storedPublishedQuiz = await response.Content.ReadFromJsonAsync<QuizDetailModel>();


            client.Value.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(joiningUserId.ToString());
            response = await client.Value.PostAsync(baseQuizUrl+"/join?gamePin="+storedPublishedQuiz.GamePin,null);
            response.EnsureSuccessStatusCode();
            
            
            response = await client.Value.GetAsync(baseUserUrl+"/"+joiningUserId);
            response.EnsureSuccessStatusCode();
            
            
            response = await client.Value.DeleteAsync(baseQuizUrl+"/"+quizId);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.GetAsync(baseUserUrl+"/"+joiningUserId);
            response.EnsureSuccessStatusCode();
            var storedJoiedUserAfterQuizDeletion = await response.Content.ReadFromJsonAsync<UserDetailModel>();

            response = await client.Value.GetAsync(baseUserUrl+"/"+userId);
            response.EnsureSuccessStatusCode();
            var storedUserCreatorAfterQuizDeletion = await response.Content.ReadFromJsonAsync<UserDetailModel>();

            //Assert
            DeepAssert.Equal(joiningUser,storedJoiedUserAfterQuizDeletion);
            DeepAssert.Equal(user,storedUserCreatorAfterQuizDeletion);
        }
        
        
          [Fact]
        public async Task UpdateJoinedQuiz_QuizUpdated()
        {
            // Arrange
            Guid quizId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            Guid joiningUserId = Guid.NewGuid();
            var joiningUser = new UserDetailModel
            {
                Id = joiningUserId ,
                Name = "meno",
                ImageUrl = null,
                CreatedQuizzes = new List<QuizListModel>(),
                Quizzes = new List<UserDetailQuizModel>(),
                Answers = new List<UserDetailAnswerModel>()
            };
            var user = new UserDetailModel
            {
                Id = userId ,
                Name = "meno",
                ImageUrl = null,
                CreatedQuizzes = new List<QuizListModel>(),
                Quizzes = new List<UserDetailQuizModel>(),
                Answers = new List<UserDetailAnswerModel>()
            };
            var quiz = new QuizDetailModel
            {
                Id = quizId,
                Title = "title",
                ImageUrl = null,
                GamePin = null,
                QuizState = QuizStateEnum.Creation,
                ActiveQuestion = null,
                CreatedByUser = _mapper.Map<UserListModel>(user),
                Questions = new List<QuestionListModel>(),
                Users = new List<QuizDetailUserModel>()
            };
          
            string publishQuizUri = baseQuizUrl + "/"+ quiz.Id +"/publish" ;
            
            //Act
            var response = await client.Value.PostAsJsonAsync(baseUserUrl,user);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.PostAsJsonAsync(baseUserUrl,joiningUser);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.PostAsJsonAsync(baseQuizUrl,quiz);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.PostAsync(publishQuizUri,null);
            response.EnsureSuccessStatusCode();
            var publishedQuiz = await response.Content.ReadFromJsonAsync<QuizDetailModel>();
            
            client.Value.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(joiningUserId.ToString());
            response = await client.Value.PostAsync(baseQuizUrl+"/join?gamePin="+publishedQuiz.GamePin,null);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.GetAsync(baseQuizUrl+"/"+quizId);
            response.EnsureSuccessStatusCode();
            var storedPublishedQuiz = await response.Content.ReadFromJsonAsync<QuizDetailModel>();

         
            response = await client.Value.PutAsJsonAsync(baseQuizUrl,storedPublishedQuiz);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.GetAsync(baseQuizUrl+"/"+quizId);
            response.EnsureSuccessStatusCode();
            var updatedQuiz = await response.Content.ReadFromJsonAsync<QuizDetailModel>();

            
            //Assert
            DeepAssert.Equal(storedPublishedQuiz,updatedQuiz);
        }

  [Fact]
        public async Task DeleteQuizUser_QuizUser_Deleted()
        {
            // Arrange
            Guid quizId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            Guid joiningUserId = Guid.NewGuid();
            var joiningUser = new UserDetailModel
            {
                Id = joiningUserId ,
                Name = "meno",
                ImageUrl = null,
                CreatedQuizzes = new List<QuizListModel>(),
                Quizzes = new List<UserDetailQuizModel>(),
                Answers = new List<UserDetailAnswerModel>()
            };
            var user = new UserDetailModel
            {
                Id = userId ,
                Name = "meno",
                ImageUrl = null,
                CreatedQuizzes = new List<QuizListModel>(),
                Quizzes = new List<UserDetailQuizModel>(),
                Answers = new List<UserDetailAnswerModel>()
            };
            var quiz = new QuizDetailModel
            {
                Id = quizId,
                Title = "title",
                ImageUrl = null,
                GamePin = null,
                QuizState = QuizStateEnum.Creation,
                ActiveQuestion = null,
                CreatedByUser = _mapper.Map<UserListModel>(user),
                Questions = new List<QuestionListModel>(),
                Users = new List<QuizDetailUserModel>()
            };
          
            string publishQuizUri = baseQuizUrl + "/"+ quiz.Id +"/publish" ;
            
            //Act
            var response = await client.Value.PostAsJsonAsync(baseUserUrl,user);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.PostAsJsonAsync(baseUserUrl,joiningUser);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.PostAsJsonAsync(baseQuizUrl,quiz);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.PostAsync(publishQuizUri,null);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.GetAsync(baseQuizUrl+"/"+quizId);
            response.EnsureSuccessStatusCode();
            var storedPublishedQuiz = await response.Content.ReadFromJsonAsync<QuizDetailModel>();


            client.Value.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(joiningUserId.ToString());
            response = await client.Value.PostAsync(baseQuizUrl+"/join?gamePin="+storedPublishedQuiz.GamePin,null);
            response.EnsureSuccessStatusCode();
            
            
            response = await client.Value.GetAsync(baseUserUrl+"/"+joiningUserId);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.GetAsync(baseQuizUrl+"/"+quizId);
            response.EnsureSuccessStatusCode();
            storedPublishedQuiz = await response.Content.ReadFromJsonAsync<QuizDetailModel>();

            
            response = await client.Value.DeleteAsync(baseQuizUrl+"/quizUser/"+storedPublishedQuiz.Users.First().Id);
            response.EnsureSuccessStatusCode();
            
            response = await client.Value.GetAsync(baseUserUrl+"/"+joiningUserId);
            response.EnsureSuccessStatusCode();
            var storedJoiedUserAfterUserQuizDeletion = await response.Content.ReadFromJsonAsync<UserDetailModel>();
            
               
            response = await client.Value.GetAsync(baseQuizUrl+"/"+quizId);
            response.EnsureSuccessStatusCode();
            var storedQuizAfterUserQuizDeletion = await response.Content.ReadFromJsonAsync<QuizDetailModel>();

            //Assert
            Assert.NotNull(storedJoiedUserAfterUserQuizDeletion);
            Assert.Empty(storedJoiedUserAfterUserQuizDeletion.Quizzes);
            
                        
            Assert.NotNull(storedQuizAfterUserQuizDeletion);
            Assert.Empty(storedQuizAfterUserQuizDeletion.Users);
        
     
        }
        
     
        
        
    }
}
