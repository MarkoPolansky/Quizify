﻿using AutoMapper;
using Quizify.Api.BL.Facades.IFacades;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Api.DAL.EF.Repositories.Interfaces;
using Quizify.Common.Models;

namespace Quizify.Api.BL.Facades
{
    public class QuestionFacade : BaseFacade<QuestionEntity, QuestionListModel, QuestionDetailModel>, IQuestionFacade
    {
        private readonly IQuestionRepository questionRepository;
        private readonly IMapper _mapper;
        public QuestionFacade(
            IQuestionRepository repository,
            IMapper mapper, IAnswerFacade answerFacade)
            : base(repository, mapper)
            {
            questionRepository = repository;
            _mapper = mapper;
            }

        public override Guid? Update(QuestionDetailModel questionModel)
        {
            var questionEntity = _mapper.Map<QuestionEntity>(questionModel);
            questionEntity.Answers = questionModel.Answers.Select(t =>
                new AnswerEntity
                {
                    Id = t.Id,
                    Type = t.Type,
                    Text = t.Text,
                    QuestionId = questionEntity.Id,
                    IsCorrect = t.IsCorrect,
                }).ToList();
            var result = questionRepository.Update(questionEntity);
            return result;
        }

        public List<QuestionListModel> GetQuestionByText(string? text)
        {
            var questionQuery = questionRepository.Get();

            if(text != null)
            {
                questionQuery = questionQuery.Where(quest => quest.Text.Contains(text));
            }

            List<QuestionEntity> questionEntities = questionQuery.ToList();

            var result = _mapper.Map<List<QuestionListModel>>(questionEntities);

            return result;
        }
    }
}
