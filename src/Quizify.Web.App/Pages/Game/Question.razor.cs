using Microsoft.AspNetCore.Components;
using Quizify.Common.Enums;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Pages.Game;

public partial class Question
{
    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;
    
    [Parameter]
    public Guid Id { get; set; }
    [Inject]
    public QuizFacade QuizFacade { get; set; }


    public QuestionDetailModel QuestionDetailModel { get; set; } = new QuestionDetailModel
    {
        Text = "Quiz is not running",
        Type = TypeEnum.SingleSelect,
        Points = 0,
        QuizId = default,
        Id = default
    };


    protected override async Task OnInitializedAsync()
    {
        var quiz = await QuizFacade.GetByIdAsync(Id);
        QuestionDetailModel = quiz.ActiveQuestion ?? QuestionDetailModel;
        await base.OnInitializedAsync();
    }

    public async Task Choose()
    {
        
    }   

}