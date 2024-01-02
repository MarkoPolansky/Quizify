using Quizify.Common;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades.IFacades;

namespace Quizify.Web.BL.Facades;

public class UserFacade : FacadeBase ,IUserFacade
{
    private readonly IUserApiClient apiClient;

    public UserFacade(IUserApiClient _apiClient)
    {
        apiClient = _apiClient;
    }
    public async Task<List<UserListModel>> GetAllAsync()
    {
        var users = new List<Common.Models.UserListModel>();
        var usersFromApi = await apiClient.UserGetAsync(culture);
        users.AddRange(usersFromApi);
        
        return users;
    }

    public async Task<UserDetailModel> GetByIdAsync(Guid id)
    {
        return await apiClient.UserGetAsync(id, culture);
    }

    public async Task<Guid> CreateAsync(UserDetailModel data)
    {
        return await apiClient.UserPostAsync(culture,data);
    }

    public async Task<Guid?> UpdateAsync(UserDetailModel data)
    {
        return await apiClient.UserPutAsync(culture,data);
    }
    public async Task DeleteAsync(Guid id)
    {
        await apiClient.UserDeleteAsync(id,culture);
    }

    public async Task<Guid?> Login(string userName)
    {
        return await apiClient.LoginAsync(userName,culture);
    }

    public async Task<UserDetailModel?> Profile()
    {
        return await apiClient.MeAsync(culture);
    }

    public async Task<List<UserListModel>> GetUsersByName(string? userName)
    {
        var users = new List<Common.Models.UserListModel>();
        var usersFromApi = await apiClient.SearchAsync(userName,culture);
        users.AddRange(usersFromApi);
        return users;
    }

    public async Task<Guid?> SubmitQuiz(UserDetailModel model, Guid quizId)
    {
        return await apiClient.QuizAsync(quizId, culture, model);
    }
}