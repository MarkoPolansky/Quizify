using Microsoft.AspNetCore.Components;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Pages.Admin.Questions;

public partial class Index
{
        
    [Inject]
    private QuestionFacade QuestionFacade { get; set; } = null!;

    private ICollection<QuestionListModel> Questions { get; set; } = new List<QuestionListModel>();

    protected override async Task OnInitializedAsync()
    {
        Questions = await QuestionFacade.GetAllAsync();

        await base.OnInitializedAsync();
    }
    
    public async Task DeleteQuestionAsync(Guid guid)
    {
        await QuestionFacade.DeleteAsync(guid);
        Questions = await QuestionFacade.GetAllAsync();
    }
}