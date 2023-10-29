using AutoMapper;
using Quizify.Api.BL.Facades.IFacades;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Api.DAL.EF.Entities.Interfaces;
using Quizify.Api.DAL.EF.Repositories.Interfaces;
using Quizify.Common.Models;

namespace Quizify.Api.BL.Facades
{
    public class UserFacade : BaseFacade<UserEntity, UserListModel, UserDetailModel>, IUserFacade
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper _mapper;
        public UserFacade(
            IUserRepository repository,
            IMapper mapper)
            : base(repository, mapper)
            {
            userRepository = repository;
            _mapper = mapper;
            }

        public override Guid? Update(UserDetailModel userModel)
        {
            var userEntity = _mapper.Map<UserEntity>(userModel);
            userEntity.Answers = userModel.Answers.Select(t =>
                new UserAnswerEntity
                {
                    Id = t.Id,
                    UserId = userEntity.Id,
                    AnswerId = t.Answer.Id,
                    UserInput = t.UserInput,

                }).ToList();
            userEntity.Quizzes = userModel.Quizzes.Select(t =>
            new QuizUserEntity
            {
                Id = t.Id,
                UserId = userEntity.Id, 
                QuizId = t.Quiz.Id,
            }).ToList();
            var result = userRepository.Update(userEntity);
            return result;
        }

        public List<UserListModel> GetUsersByName(string? userName)
        {
            var userQuery = userRepository.Get();
            
            if (userName != null) 
            {
                userQuery = userQuery.Where(usr => usr.Name.Contains(userName));    
            }

            List<UserEntity> userEntities = userQuery.ToList();

            var result = _mapper.Map<List<UserListModel>>(userEntities);

            return result;
        }

    }
}
