using AutoMapper;
using Quizify.Common.Extensions;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Common.Models;

namespace Quizify.Api.BL.MapperProfiles
{
    public class QuestionMapperProfile : Profile
    {
        public QuestionMapperProfile() 
        {
            CreateMap<QuestionEntity, QuestionDetailModel>()
                .MapMember(src => src.Answers, dst => dst.Answers);
            CreateMap<QuestionEntity, QuestionListModel>();

            CreateMap<QuestionDetailModel, QuestionEntity>()
                .Ignore(dst => dst.Answers)
                .Ignore(dst => dst.Quiz);
        }
    }
}
