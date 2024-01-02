using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Quizify.Common.Enums;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Pages.Admin.Quizes;

public partial class Create
{
    [Inject]
    IJSRuntime JSRuntime { get; set; } = null!;
    
    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;

    [Inject]
    public QuizFacade QuizFacade { get; set; } = null!;
    
    [Inject]
    public UserFacade UserFacade { get; set; } = null!;
    
    [Parameter]
    public Guid Id { get; init; }  [Parameter]

    public QuizDetailModel Data { get; set; } = new()
    {
        Id = Guid.Empty,
        Title = null,
        QuizState = QuizStateEnum.Creation,
        CreatedByUser = null,
    };
    
    public async Task Save()
    {
        
        var user = await UserFacade.Profile();
        Data.CreatedByUser = new UserListModel
        {
            Name = user.Name,
            Id = user.Id
        };
        await QuizFacade.CreateAsync(Data);
        NavigateBack();
    }
    
    
    public void NavigateBack()
    {
        navigationManager.NavigateTo("/admin/quizes");
    }
    
}

 