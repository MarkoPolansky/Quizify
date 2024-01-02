using Microsoft.AspNetCore.Components;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;
using System.ComponentModel.DataAnnotations;

namespace Quizify.Web.App.Pages;

public partial class Index
{
    [Inject]
    public QuizFacade QuizFacade { get; set; } = null!;
    
    [Inject]
    public UserFacade UserFacade { get; set; } = null!;
    
    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;
    
    
    public UserDetailModel UserLogged { get; set; } = new UserDetailModel
    {
        Id = Guid.Empty,
        Name = null,
        ImageUrl = null,
        CreatedQuizzes = null,
        Quizzes = null,
        Answers = null
    };

    [SupplyParameterFromQuery]
    [Parameter]
    public string Pin { get; set; }
    
    public async Task Join()
    {
     var result =   await QuizFacade.Join(Pin);

     if (result.Id == Guid.Empty)
     {
         return;
     }
     navigationManager.NavigateTo("/game/"+result.Id+"/question");
    }
    
    
    protected override async Task OnInitializedAsync()
    {  
        UserLogged = await UserFacade.Profile();
        
        if (UserLogged?.Id == Guid.Empty)
        {
            navigationManager.NavigateTo("/login"+"?pin="+Pin);
            return;
        }

        if (!string.IsNullOrEmpty(Pin))
        {
            await Join();
        }
        
        await base.OnInitializedAsync();
    }
    
}