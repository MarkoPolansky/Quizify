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
        private readonly IAnswerRepository answerRepository;
        private readonly IMapper _mapper;
        public AnswerFacade(
            IAnswerRepository repository,
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

        public List<AnswerListModel> GetAnswersByText(string? text)
        {
            var query = answerRepository.Get();

            if (text != null)
            {
                query = query.Where(x => x.Text.Contains(text));
            }

            List<AnswerEntity> answerEntities = query.ToList();

            var result = _mapper.Map<List<AnswerListModel>>(answerEntities);

            return result;
        }
    }
}
