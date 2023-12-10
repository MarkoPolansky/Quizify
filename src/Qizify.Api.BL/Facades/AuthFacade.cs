using AutoMapper;
using Quizify.Api.BL.Facades.IFacades;
using Quizify.Api.BL.Services.Interfaces;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Api.DAL.EF.Repositories.Interfaces;
using Quizify.Common.Models;

namespace Quizify.Api.BL.Facades;

public class AuthFacade : BaseFacade<UserEntity, UserListModel, UserDetailModel>, IAuthFacace
{
    private readonly IUserRepository userRepository;
    private readonly IMapper _mapper;
    public AuthFacade(
        IUserRepository repository,
        IMapper mapper)
        : base(repository, mapper)
    {
        userRepository = repository;
        _mapper = mapper;
       
    }
}