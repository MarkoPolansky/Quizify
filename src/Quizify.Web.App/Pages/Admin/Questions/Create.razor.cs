using Microsoft.AspNetCore.Components;
using Quizify.Common.Enums;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Pages.Admin.Questions;

public partial class Create
{
    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;
    
    [Inject]
    public QuizFacade QuizFacade { get; set; } = null!;

    [Inject]
    public QuestionFacade QuestionFacade { get; set; } = null!;
    
    public List<QuizListModel> Quizes { get; set; } = new();
    public QuestionDetailModel Data { get; set; } = new()
    {
        Id = Guid.NewGuid(),
        Text = null,
        Type = TypeEnum.SingleSelect,
        Points = 0,
        QuizId = default,
        
    };
    
    public async Task Save()
    {
        await QuestionFacade.CreateAsync(Data);
        NavigateBack();
    }
    
    public void NavigateBack()
    {
        navigationManager.NavigateTo("/admin/questions");
    }

    protected override async Task OnInitializedAsync()
    {
        Quizes = await QuizFacade.GetAllAsync();
        await base.OnInitializedAsync();
    }
}

 