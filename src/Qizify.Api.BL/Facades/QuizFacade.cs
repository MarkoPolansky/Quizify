using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Quizify.Api.BL.Facades.IFacades;
using Quizify.Api.BL.Services.Interfaces;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Api.DAL.EF.Repositories.Interfaces;
using Quizify.Common.Enums;
using Quizify.Common.Models;

namespace Quizify.Api.BL.Facades
{
    public class QuizFacade : BaseFacade<QuizEntity, QuizListModel, QuizDetailModel>, IQuizFacade
    {
        private readonly IQuestionFacade _questionFacade;
        private readonly IQuizRepository quizRepository;
        private readonly IMapper _mapper;
        private readonly IPinGenerationService _pinGenerationService;
        private readonly IAuthService _auth;
        public QuizFacade(
            IQuestionFacade questionFacade,
            IQuizRepository repository,
            IMapper mapper,
            IPinGenerationService pinGenerationService,
            IAuthService auth)
            : base(repository, mapper)
        {
            _questionFacade = questionFacade;
            quizRepository = repository;
            _mapper = mapper;
            _pinGenerationService = pinGenerationService;
            _auth = auth;
        }

        public override Guid? Update(QuizDetailModel quizModel)
        {
            var quizEntity = _mapper.Map<QuizEntity>(quizModel);
            quizEntity.Questions = quizModel.Questions.Select(t =>
                new QuestionEntity
                {
                    Id = t.Id,
                    Type = t.Type,
                    Text = t.Text,
                    QuizId = quizEntity.Id,
                    Points = t.Points,
                }).ToList();
            quizEntity.Users = quizModel.Users.Select(t =>
            new QuizUserEntity
            {
                Id = t.Id,
                UserId = t.User.Id,
                QuizId = quizEntity.Id,
            }).ToList();
            quizEntity.ActiveQuestionId = quizModel.ActiveQuestion?.Id;
            var result = quizRepository.Update(quizEntity);
            
            return result;
        }
        public Guid? Start(Guid modelId)
        {
            var model = GetById(modelId);
           
            if(model == null) return null;
            if (model.QuizState != QuizStateEnum.Published) return null;
            
            Guid? result = null;
            if (model != null)
            {
                var activeQuestion = new QuestionDetailModel
                {
                    Text = null,
                    Type = TypeEnum.SingleSelect,
                    Points = 0,
                    QuizId = default,
                    Id = model.Questions.First().Id
                };
                
                model.QuizState = QuizStateEnum.Running;
                model.ActiveQuestion = activeQuestion;
                result = Update(model);
            }
            return result;
        }
        public QuizDetailModel Publish(Guid modelId)
        {  
            var emptyModel = new QuizDetailModel
            {
                Title = null,
                QuizState = QuizStateEnum.Creation,
                CreatedByUser = null,
                Id = Guid.Empty
            };
            var model = GetById(modelId);
            if(model == null) return emptyModel;
            if (model.QuizState == QuizStateEnum.Published || model.QuizState == QuizStateEnum.Running)
                return emptyModel;
            
            model.GamePin = GenerateGamePin();
            model.QuizState = QuizStateEnum.Published;
            if (Update(model) != null)
            {
                return model;
            }

            return emptyModel;
        }
        
        public Guid? End(Guid modelId)
        {
            var model = GetById(modelId);
            if(model == null) return null;
            if (model.QuizState != QuizStateEnum.Running) return null;
            
            model.QuizState = QuizStateEnum.Ended;
            return Update(model);
        }

        
        public QuizDetailModel? GetByGamePin(string gamePin)
        {
            var quizEntity = quizRepository.Get().FirstOrDefault(q => q.GamePin == gamePin);
            if (quizEntity == null)
                return null;
            
            var quizDetailModel = _mapper.Map<QuizDetailModel>(quizEntity);
            
            if (quizDetailModel == null && quizDetailModel.QuizState != QuizStateEnum.Published)
            {
                return null;
            }

            return quizDetailModel;
        }
        
        private bool IsGamePinUnique(string gamePin)
        {
            return quizRepository.CountGamePin(gamePin) == 0;
        }

        private string GenerateGamePin()
        {

            string gamePin = _pinGenerationService.Generate();
            while (!IsGamePinUnique(gamePin))
            {
                gamePin = _pinGenerationService.Generate();
            }
            return gamePin;
        }
        
        public QuizDetailModel JoinQuiz(string gamePin)
        {
            var quizDetailModel = GetByGamePin(gamePin);
            var emptyModel = new QuizDetailModel
                {
                    Title = null,
                    QuizState = QuizStateEnum.Creation,
                    CreatedByUser = null,
                    Id = Guid.Empty
                }
                ;
            if (quizDetailModel == null || quizDetailModel.QuizState != QuizStateEnum.Published)
            {
                return emptyModel;
            }
            
            
            var userdetail = new QuizDetailUserModel
            {
                Id = Guid.NewGuid(),
                TotalPoints = 0,
                StartedAt = default,
                EndedAt = null,
                User = _mapper.Map<UserListModel>( _auth.GetUser())
            };
            
            quizDetailModel.Users.Add(userdetail);
            var quizId = Update(quizDetailModel);

            if (quizId == null)
            {
                return emptyModel;
            }
            
            return quizDetailModel;
        }
   
    }
}
