using Microsoft.AspNetCore.Components;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Pages.Admin.Answers;

public partial class Index
{
        
    [Inject]
    private AnswerFacade AnswerFacade{ get; set; } = null!;

    private ICollection<AnswerListModel> Answers { get; set; } = new List<AnswerListModel>();

    protected override async Task OnInitializedAsync()
    {
        Answers = await AnswerFacade.GetAllAsync();

        await base.OnInitializedAsync();
    }
    
    public async Task DeleteQuestionAsync(Guid guid)
    {
        await AnswerFacade.DeleteAsync(guid);
        Answers = await AnswerFacade.GetAllAsync();
    }
}