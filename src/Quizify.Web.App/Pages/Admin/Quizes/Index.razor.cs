using Microsoft.AspNetCore.Components;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Pages.Admin.Quizes;

public partial class Index
{
        
    [Inject]
    private QuizFacade QuizFacade { get; set; } = null!;

    private ICollection<QuizListModel> Quizes { get; set; } = new List<QuizListModel>();

    protected override async Task OnInitializedAsync()
    {
        Quizes = await QuizFacade.GetAllAsync();

        await base.OnInitializedAsync();
    }
    
    public async Task DeleteQuizAsync(Guid guid)
    {
        await QuizFacade.DeleteAsync(guid);
        Quizes = await QuizFacade.GetAllAsync();
    }
}