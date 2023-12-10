using Microsoft.AspNetCore.Components;
using Quizify.Common.Enums;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Pages.Admin.Answers;

public partial class Edit
{
    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;
    
    [Inject]
    public AnswerFacade AnswerFacade { get; set; } = null!;

    [Inject]
    public QuestionFacade QuestionFacade { get; set; } = null!;
    
    [Parameter]
    public Guid Id { get; init; }  
    public AnswerDetailModel Data { get; set; } = new()
    {
        Id = Guid.Empty,
        Text = null,
        Type = TypeEnum.SingleSelect,
        IsCorrect = false,
        QuestionId = default
    };

    public List<QuestionListModel> Questions = new List<QuestionListModel>();

    public List<QuizListModel> Quizes { get; set; } = new();
   

    protected override async Task OnInitializedAsync()
    {
        Data = await AnswerFacade.GetByIdAsync(Id);
        Questions = await QuestionFacade.GetAllAsync();
        await base.OnInitializedAsync();
    }

    public async Task Save()
    {
        await AnswerFacade.UpdateAsync(Data);
        NavigateBack();
    }
    
    
    public void NavigateBack()
    {
        navigationManager.NavigateTo("/admin/answers");
    }
    
}

 