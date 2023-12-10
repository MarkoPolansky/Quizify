using AutoMapper;
using Quizify.Api.BL.Facades.IFacades;
using Quizify.Api.BL.Services.Interfaces;
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
        private readonly IAuthService _auth;
        public UserFacade(
            IUserRepository repository,
            IMapper mapper,
            IAuthService auth)
            : base(repository, mapper)
            {
            userRepository = repository;
            _mapper = mapper;
            _auth = auth;
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

        public Guid Login(string? userName)
        {
            var user = new UserDetailModel
            {
                Name = userName,
                Id = Guid.NewGuid()
            };
            var id = Create(user);
            _auth.SetCookieToResponse(id.ToString());
            return id;
        }
    }
}
