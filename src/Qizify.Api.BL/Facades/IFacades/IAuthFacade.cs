using Quizify.Api.DAL.EF.Entities;
using Quizify.Common.Models;

namespace Quizify.Api.BL.Facades.IFacades;

public interface IAuthFacace : IFacade<UserEntity, UserListModel, UserDetailModel>
{
    
        
}