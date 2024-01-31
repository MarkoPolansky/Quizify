using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Pages.Admin.Users;

public partial class Index
{
    [Inject]
    NavigationManager _navigationManager { get; set; }
    [Inject]
    IJSRuntime JSRuntime { get; set; } = null!;

    private string loggedUserId;
    
    private string token;
    
    
    [Inject]
    private UserFacade UserFacade { get; set; } = null!;

    private ICollection<UserListModel> Users { get; set; } = new List<UserListModel>();

    protected override async Task OnInitializedAsync()
    {
        token = await JSRuntime.InvokeAsync<string>("getToken");
        Users = await UserFacade.GetAllAsync();
        await base.OnInitializedAsync();
    }


    public async Task LoginAs(Guid guid)
    {   
        await JSRuntime.InvokeVoidAsync("storeToken", guid);
        _navigationManager.NavigateTo("/admin/users",true);

    }
    public async Task DeleteUserAsync(Guid guid)
    {
        await UserFacade.DeleteAsync(guid);
        Users = await UserFacade.GetAllAsync();
    }
}