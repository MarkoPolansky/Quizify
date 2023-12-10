using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Pages;

public partial class Login
{
    [Inject]
    public UserFacade UserFacade { get; set; } = null!;
    
    [Inject]
    IJSRuntime JSRuntime { get; set; } = null!;
    
    
    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;
    public string UserName { get; set; } = "";
    
    public async Task LoginTo()
    {
        await UserFacade.Login(UserName);
        await JSRuntime.InvokeVoidAsync("history.back");
    }
}