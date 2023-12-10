using Microsoft.AspNetCore.Components;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Pages.Admin.Users;

public partial class Index
{
    
    [Inject]
    private UserFacade UserFacade { get; set; } = null!;

    private ICollection<UserListModel> Users { get; set; } = new List<UserListModel>();

    protected override async Task OnInitializedAsync()
    {
        Users = await UserFacade.GetAllAsync();

        await base.OnInitializedAsync();
    }
    
    public async Task DeleteUserAsync(Guid guid)
    {
        await UserFacade.DeleteAsync(guid);
        Users = await UserFacade.GetAllAsync();
    }
}