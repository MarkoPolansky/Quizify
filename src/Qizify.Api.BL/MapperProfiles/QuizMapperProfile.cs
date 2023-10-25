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
                .MapMember(dst => dst.Users, src => src.Users)
                .MapMember(dst => dst.Questions, src => src.Questions);
            CreateMap<QuizEntity, QuizListModel>();
            CreateMap<QuizUserEntity, QuizDetailUserModel>();

            CreateMap<QuizDetailModel, QuizEntity>()
                .Ignore(dst => dst.Questions)
                .Ignore(dst => dst.Users)
                .Ignore(dst => dst.CreatedByUser)
                .MapMember(dst => dst.CreatedByUserId, src => src.CreatedByUser.Id);
        }
    }
}
