using AutoMapper;
using Quizify.Api.BL.Facades.IFacades;
using Quizify.Api.BL.Services.Interfaces;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Api.DAL.EF.Repositories.Interfaces;
using Quizify.Common.Models;

namespace Quizify.Api.BL.Facades
{
    public class QuizFacade : BaseFacade<QuizEntity, QuizListModel, QuizDetailModel>, IQuizFacade
    {
        private readonly IQuizRepository quizRepository;
        private readonly IMapper _mapper;
        private readonly IPinGenerationService _pinGenerationService;
        public QuizFacade(
            IQuizRepository repository,
            IMapper mapper,
            IPinGenerationService pinGenerationService)
            : base(repository, mapper)
            {
            quizRepository = repository;
            _mapper = mapper;
            _pinGenerationService = pinGenerationService;
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
            var result = quizRepository.Update(quizEntity);
            return quizRepository.Update(quizEntity);
        }


        public Guid? Publish(QuizDetailModel model)
        {
            model.GamePin = GenerateGamePin();
            return Update(model);
            
        }

        public Guid? Start(QuizDetailModel model)
        {
            model.IsStarted = true;
            return Update(model);
        }



        private bool IsGamePinUnique(string gamePin)
        {
            return quizRepository.Get().Count(a => a.GamePin == gamePin) == 0;
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
    }
}
