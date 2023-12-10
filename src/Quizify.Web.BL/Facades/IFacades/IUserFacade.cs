using Quizify.Common.Models;

namespace Quizify.Web.BL.Facades.IFacades;

public interface IUserFacade: IFacade<UserListModel,UserDetailModel>
{
    Task<Guid?> Login(string userName);
}