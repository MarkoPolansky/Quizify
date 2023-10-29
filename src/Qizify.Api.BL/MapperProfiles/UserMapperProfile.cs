using AutoMapper;
using Quizify.Common.Extensions;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Common.Models;

namespace Quizify.Api.BL.MapperProfiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserEntity, UserDetailModel>()
                .MapMember(dst => dst.Answers, src => src.Answers)
                .MapMember(dst => dst.Quizzes, src => src.Quizzes)
                .MapMember(dst => dst.CreatedQuizzes, src => src.CreatedQuizzes);
            

            CreateMap<UserEntity, UserListModel>();
            CreateMap<QuizUserEntity, UserDetailQuizModel>();
            CreateMap<UserAnswerEntity, UserDetailAnswerModel>();

            CreateMap<UserDetailModel, UserEntity>()
                .Ignore(dst => dst.Quizzes)
                .Ignore(dst => dst.Answers)
                .Ignore(dst => dst.CreatedQuizzes);

        }
    }
}
