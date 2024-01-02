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
    
    [SupplyParameterFromQuery]
    [Parameter]
    public string? Pin { get; set; }

    
    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;
    public string UserName { get; set; } = "";
    
    public async Task LoginTo()
    {
        var userId =  await UserFacade.Login(UserName);
        await JSRuntime.InvokeVoidAsync("storeToken", userId);
        navigationManager.NavigateTo("/?pin="+Pin);
    }
}