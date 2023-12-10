using Microsoft.AspNetCore.Components;
using Quizify.Common.Enums;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Pages.Game;

public partial class Index
{
    
    [Parameter]
    public Guid Id { get; set; }
    
    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;

    [Inject]
    public QuizFacade QuizFacade { get; set; }

    public QuizDetailModel QuizDetailModel { get; set; } = new QuizDetailModel
    {
        Title = null,
        QuizState = QuizStateEnum.Published,
        CreatedByUser = null,
        Id = default
    };

    protected override async Task OnInitializedAsync()
    {
        QuizDetailModel = await QuizFacade.GetByIdAsync(Id);
        if (QuizDetailModel.ActiveQuestion != null)
        {
            navigationManager.NavigateTo("/game/"
             +QuizDetailModel.ActiveQuestion.Id+"/question");
        }

        await base.OnInitializedAsync();
    }


}