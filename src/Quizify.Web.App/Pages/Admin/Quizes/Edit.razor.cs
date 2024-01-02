using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Quizify.Common.Enums;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Pages.Admin.Quizes;

public partial class Edit
{

    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;

    [Inject]
    public QuizFacade QuizFacade { get; set; } = null!;
    
    
    [Inject]
    public UserFacade UserFacade { get; set; } = null!;
    
    
    [Parameter]
    public Guid Id { get; init; }  [Parameter]

    public QuizDetailModel Data { get; set; } = new()
    {
        Id = Guid.Empty,
        Title = null,
        QuizState = QuizStateEnum.Creation,
        CreatedByUser = null
    };

    public ICollection<UserDetailModel> Users { get; set; } = new List<UserDetailModel>();

    protected override async Task OnInitializedAsync()
    {
        Data = await QuizFacade.GetByIdAsync(Id);
        foreach (var a in Data.Users)
        {
            UserDetailModel user = await UserFacade.GetByIdAsync(a.User.Id);
            
            user.Quizzes = user.Quizzes.Where(a => a.Quiz.Id == Data.Id).ToList();
            Users.Add(user);
        }
        await base.OnInitializedAsync();
    }

    public async Task Save()
    {
        await QuizFacade.UpdateAsync(Data);
        NavigateBack();
    }
    
    public async Task StartQuiz()
    {
        await QuizFacade.Publish(Data.Id);
           
        navigationManager.NavigateTo("/admin/quizes/"+Data.Id);
    }
    
    
    
    
    public void NavigateBack()
    {
        navigationManager.NavigateTo("/admin/quizes");
    }
    
    
}

 