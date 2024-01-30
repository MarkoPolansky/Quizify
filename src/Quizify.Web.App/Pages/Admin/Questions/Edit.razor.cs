using Microsoft.AspNetCore.Components;
using Quizify.Common.Enums;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Pages.Admin.Questions;

public partial class Edit
{
    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;

    [Inject]
    public QuestionFacade QuestionFacade { get; set; } = null!;
    
    [Inject]
    public QuizFacade QuizFacade { get; set; } = null!;
    
    [Parameter]
    public Guid Id { get; init; }  
    public QuestionDetailModel Data { get; set; } = new()
    {
        Id = Guid.Empty,
        Text = null,
        Type = TypeEnum.SingleSelect,
        Points = 0,
        QuizId = default
    };

    public List<QuizListModel> Quizes { get; set; } = new();
   

    protected override async Task OnInitializedAsync()
    {
        Data = await QuestionFacade.GetByIdAsync(Id);
        Quizes = await QuizFacade.GetAllAsync();
        await base.OnInitializedAsync();
    }

    public async Task Save()
    {
        await QuestionFacade.UpdateAsync(Data);
        NavigateBack();
    }
    
    public void AddAnswer()
    {
        navigationManager.NavigateTo("admin/answers/create?answer="+Data.Id);
    }
    
    
    public void NavigateBack()
    {
        navigationManager.NavigateTo("/admin/questions");
    }
    
}

 