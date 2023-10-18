using AutoMapper;
using Quizify.Api.BL.Facades.IFacades;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Api.DAL.EF.Repositories;
using Quizify.Api.DAL.EF.Repositories.Interfaces;
using Quizify.Common.Models;

namespace Quizify.Api.BL.Facades
{
    public class AnswerFacade : BaseFacade<AnswerEntity, AnswerListModel, AnswerDetailModel>, IAnswerFacade
    {
        private readonly IApiRepository<AnswerEntity> answerRepository;
        private readonly IMapper _mapper;
        public AnswerFacade(
            IApiRepository<AnswerEntity> repository,
            IMapper mapper)
            : base(repository, mapper)
            {
            answerRepository = repository;
            _mapper = mapper;
            }

        public override Guid? Update(AnswerDetailModel answerModel)
        {
            var answerEntity = _mapper.Map<AnswerEntity>(answerModel);
            return answerRepository.Update(answerEntity);
        }
    }
}
