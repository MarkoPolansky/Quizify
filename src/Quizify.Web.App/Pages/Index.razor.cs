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
    private NavigationManager navigationManager { get; set; } = null!;

    //[Required]
    //public string UserName { get; set; } = "";
    //[Required]
    //[RegularExpression(@"^\d\d\d\d*$", ErrorMessage ="Game pin must be at leat 4 numbers")]
    //public string GamePin { get; set; } = "";

    private UserLoginModel Data { get; set; } = GetNewUserLoginModel();
    
    public async Task Join()
    {
     var result =   await QuizFacade.Join(Data.GamePin,Data.Name);

     if (result == Guid.Empty)
     {
         return;
     }
     navigationManager.NavigateTo("/game/"+result);
    }

    private static UserLoginModel GetNewUserLoginModel() =>
        new()
        { 
            GamePin = string.Empty,
            Name = string.Empty
        };
    
}