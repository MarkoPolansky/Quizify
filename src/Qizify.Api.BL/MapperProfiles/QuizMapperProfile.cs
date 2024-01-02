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
            CreateMap<QuizDetailModel, QuizListModel>();

            CreateMap<QuizDetailModel, QuizEntity>()
                .Ignore(src => src.Questions)
                .Ignore(src => src.Users)
                .Ignore(dst => dst.CreatedByUser)
                .MapMember(dst => dst.CreatedByUserId, src => src.CreatedByUser.Id)
                .MapMember(dst => dst.ActiveQuestionId, src => src.ActiveQuestion.Id);


        }
    }
}
