using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Shared;

public partial class Auth
{
    [Inject]
    IJSRuntime JSRuntime { get; set; } = null!;
    
    private IJSObjectReference _jsModule;
    private string token;
    
    [Inject]
    private UserFacade UserFacade { get; set; } = null!;
    
    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;

    public UserDetailModel? UserLogged { get; set; } = new UserDetailModel
    {
        Id = Guid.Empty,
        Name = null,
        ImageUrl = null,
        CreatedQuizzes = null,
        Quizzes = null,
        Answers = null
    };

    protected override async Task OnInitializedAsync()
    {
        token = await _jsModule.InvokeAsync<string>("getToken");
        UserLogged = await UserFacade.Profile();
        
        if (UserLogged == null)
        {
            navigationManager.NavigateTo("/login");
        }
        
        await base.OnInitializedAsync();
    }

}