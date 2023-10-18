using AutoMapper;
using Quizify.Common.Extensions;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Common.Models;

namespace Quizify.Api.BL.MapperProfiles
{
    public class QuizMapperProfile : Profile
    {
        public QuizMapperProfile() 
        {
            CreateMap<QuizEntity, QuizDetailModel>()
                .MapMember(src => src.Users, dst => dst.Users)
                .MapMember(src => src.Questions, dst => dst.Questions);
            CreateMap<QuizEntity, QuestionListModel>();
            CreateMap<QuizUserEntity, QuizDetailUserModel>();

            CreateMap<QuizDetailModel, QuizEntity>()
                .Ignore(dst => dst.Questions)
                .Ignore(dst => dst.Users);
        }
    }
}
