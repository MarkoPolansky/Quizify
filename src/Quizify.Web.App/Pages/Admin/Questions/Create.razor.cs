using Microsoft.AspNetCore.Components;
using Quizify.Common.Enums;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Pages.Admin.Questions;

public partial class Create
{
    [SupplyParameterFromQuery]
    [Parameter]
    public string? quiz { get; set; }

    
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
        Points = 10,
        QuizId = default,
        
    };
    
    public async Task Save()
    {
        var res = await QuestionFacade.CreateAsync(Data);
        NavigateBack(res);
    }
    
    public void NavigateBack(Guid id)
    {
        navigationManager.NavigateTo("/admin/questions/"+id);
    }
    


    protected override async Task OnInitializedAsync()
    {
        Quizes = await QuizFacade.GetAllAsync();
        if (Quizes.Any(a => a.Id.ToString() == quiz))
            Data.QuizId = new Guid(quiz);
        
        await base.OnInitializedAsync();
    }
}

 