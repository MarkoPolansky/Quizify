using Microsoft.AspNetCore.Components;
using Quizify.Common.Enums;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Pages.Admin.Answers;

public partial class Create
{
    
    [SupplyParameterFromQuery]
    [Parameter]
    public string? answer { get; set; }
    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;
    
    [Inject]
    public AnswerFacade AnswerFacade { get; set; } = null!;

    [Inject]
    public QuestionFacade QuestionFacade { get; set; } = null!;
    
    public List<QuestionListModel> Questions { get; set; } = new();
    public AnswerDetailModel Data { get; set; } = new()
    {
        Id = Guid.NewGuid(),
        Text = null,
        Type = TypeEnum.SingleSelect,
        IsCorrect = false,
        QuestionId = default,

    };
    
    public async Task Save()
    {
        await AnswerFacade.CreateAsync(Data);
        NavigateBack();
    }
    
    public void NavigateBack()
    {
        navigationManager.NavigateTo("/admin/questions/"+Data.QuestionId);
    }

    protected override async Task OnInitializedAsync()
    {
        Questions = await QuestionFacade.GetAllAsync();
        if (Questions.Any(a => a.Id.ToString() == answer))
            Data.QuestionId = new Guid(answer);
        await base.OnInitializedAsync();
    }
}

 