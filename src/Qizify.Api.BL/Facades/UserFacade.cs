using AutoMapper;
using Quizify.Api.BL.Facades.IFacades;
using Quizify.Api.BL.Services.Interfaces;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Api.DAL.EF.Entities.Interfaces;
using Quizify.Api.DAL.EF.Repositories.Interfaces;
using Quizify.Common.Enums;
using Quizify.Common.Models;

namespace Quizify.Api.BL.Facades
{
    public class UserFacade : BaseFacade<UserEntity, UserListModel, UserDetailModel>, IUserFacade
    {
        private readonly IUserRepository userRepository;
        private readonly IQuizFacade quizFacade;
        private readonly IQuestionFacade questionFacade;
        private readonly IMapper _mapper;
        private readonly IAuthService _auth;
        public UserFacade(
            IUserRepository repository,
            IQuizFacade QuizFacade,
            IQuestionFacade QuestionFacade,
            IMapper mapper,
            IAuthService auth)
            : base(repository, mapper)
            {
            userRepository = repository;
            quizFacade = QuizFacade;
            questionFacade = QuestionFacade;
            _mapper = mapper;
            _auth = auth;
            }
        

        public override Guid? Update(UserDetailModel userModel)
        {
            var userEntity = _mapper.Map<UserEntity>(userModel);
            userEntity.Answers = userModel.Answers.Select(t =>
                new UserAnswerEntity
                {
                    Id = t.Id,
                    UserId = userEntity.Id,
                    AnswerId = t.Answer.Id,
                    UserInput = t.UserInput,

                }).ToList();
            userEntity.Quizzes = userModel.Quizzes.Select(t =>
            new QuizUserEntity
            {
                Id = t.Id,
                UserId = userEntity.Id, 
                QuizId = t.Quiz.Id,
                TotalPoints = t.TotalPoints,
                EndedAt = t.EndedAt
            }).ToList();
            var result = userRepository.Update(userEntity);
            return result;
        }

        public List<UserListModel> GetUsersByName(string? userName)
        {
            var userQuery = userRepository.Get();
            
            if (userName != null) 
            {
                userQuery = userQuery.Where(usr => usr.Name.Contains(userName));    
            }

            List<UserEntity> userEntities = userQuery.ToList();

            var result = _mapper.Map<List<UserListModel>>(userEntities);

            return result;
        }

        public Guid Login(string? userName)
        {
            var user = new UserDetailModel
            {
                Name = userName,
                Id = Guid.NewGuid()
            };
            var id = Create(user);
            return id;
        }

        public UserDetailModel? Profile()
        {
            return _auth.GetUser() ?? new UserDetailModel
            {
                Name = null,
                Id = Guid.Empty
            };
        }

        public Guid? SubmitQuiz(UserDetailModel model,Guid quizId)
        {
            var quiz = quizFacade.GetById(quizId);
            if (quiz == null)
            {
                return null;
            }
            
            var questions = new List<QuestionDetailModel?>();

            
            var Result = new UserDetailQuizModel
            {
                Quiz = _mapper.Map<QuizListModel>(quiz),
                Id = Guid.NewGuid(),
                TotalPoints = 0,
                EndedAt = DateTime.Now
            };

            foreach (var question in quiz.Questions)
            {
                questions.Add(questionFacade.GetById(question.Id));
            }
            
            foreach (var question in questions)
            {
                var correctAnswers = question.Answers.Where(a => a.IsCorrect).ToHashSet();
                var userAnswersForQuestion = model.Answers.Where(q => q.Answer.QuestionId == question.Id).Select(a => a.Answer).ToHashSet();
                if (userAnswersForQuestion.SetEquals(correctAnswers))
                    Result.TotalPoints += question.Points;
            }
            model.Quizzes.Add(Result);
           return Update(model);
        }


        public override void Delete(Guid id)
        {
            var user = GetById(id);
            foreach (var quiz in user.CreatedQuizzes)
            {
                quizFacade.Delete(quiz.Id);
            }
            base.Delete(id);
        }
        
   
    }
}
