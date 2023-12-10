using Microsoft.AspNetCore.Components;
using Quizify.Common.Enums;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Pages.Admin.Quizes;

public partial class Edit
{
    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;

    [Inject]
    public QuizFacade QuizFacade { get; set; } = null!;
    
    [Parameter]
    public Guid Id { get; init; }  [Parameter]

    public QuizDetailModel Data { get; set; } = new()
    {
        Id = Guid.Empty,
        Title = null,
        QuizState = QuizStateEnum.Creation,
        CreatedByUser = null
    };

    protected override async Task OnInitializedAsync()
    {
        Data = await QuizFacade.GetByIdAsync(Id);
        await base.OnInitializedAsync();
    }

    public async Task Save()
    {
        await QuizFacade.UpdateAsync(Data);
        NavigateBack();
    }
    
    
    public void NavigateBack()
    {
        navigationManager.NavigateTo("/admin/quizes");
    }
    
}

 