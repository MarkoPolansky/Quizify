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
                .MapMember(src => src.Answers, dst => dst.Answers)
                .MapMember(src => src.Quizzes, dst => dst.Quizzes)
                .MapMember(src => src.CreatedQuizzes, dst => dst.CreatedQuizzes);
            CreateMap<UserEntity, UserListModel>();
            CreateMap<QuizUserEntity, UserDetailQuizModel>();

            CreateMap<UserDetailModel, UserEntity>()
                .Ignore(dst => dst.Quizzes)
                .Ignore(dst => dst.Answers)
                .Ignore(dst => dst.CreatedQuizzes);

        }
    }
}
