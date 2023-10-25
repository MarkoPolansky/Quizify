using AutoMapper;
using Quizify.Common.Extensions;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Common.Models;

namespace Quizify.Api.BL.MapperProfiles
{
    public class AnswerMapperProfile : Profile
    {
        public AnswerMapperProfile() 
        {
            CreateMap<AnswerEntity, AnswerDetailModel>();
            CreateMap<AnswerEntity, AnswerListModel>();

            CreateMap<AnswerDetailModel, AnswerEntity>()
                .Ignore(dst => dst.Users)
                .Ignore(dst => dst.Question);
        }
    }
}
