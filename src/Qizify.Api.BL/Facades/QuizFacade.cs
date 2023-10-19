using AutoMapper;
using Quizify.Api.BL.Facades.IFacades;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Api.DAL.EF.Repositories.Interfaces;
using Quizify.Common.Models;

namespace Quizify.Api.BL.Facades
{
    public class QuizFacade : BaseFacade<QuizEntity, QuizListModel, QuizDetailModel>, IQuizFacade
    {
        private readonly IQuizRepository quizRepository;
        private readonly IMapper _mapper;
        public QuizFacade(
            IQuizRepository repository,
            IMapper mapper)
            : base(repository, mapper)
            {
            quizRepository = repository;
            _mapper = mapper;
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
    }
}
