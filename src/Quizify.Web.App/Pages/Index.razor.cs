using Microsoft.AspNetCore.Components;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Pages;

public partial class Index
{
    [Inject]
    public QuizFacade QuizFacade { get; set; } = null!;
    
    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;

    public string UserName { get; set; } = "";
    public string GamePin { get; set; } = "";
    
    public async Task Join()
    {
     var result =   await QuizFacade.Join(GamePin,UserName);

     if (result == Guid.Empty)
     {
         return;
     }
     navigationManager.NavigateTo("/game/"+result);
    }
    
}