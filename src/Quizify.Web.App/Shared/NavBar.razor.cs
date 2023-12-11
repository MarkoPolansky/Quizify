using Microsoft.AspNetCore.Components;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Shared;

public partial class NavBar
{
    
    [Inject]
    private UserFacade UserFacade { get; set; } = null!;
    
    [Inject]
    private QuestionFacade QuestionFacade { get; set; } = null!;
    
    [Inject]
    private AnswerFacade AnswerFacade { get; set; } = null!;    
    
    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;

    public UserDetailModel? UserLogged { get; set; } = new UserDetailModel
    {
        Id = Guid.Empty,
        Name = null,
        ImageUrl = null,
        CreatedQuizzes = null,
        Quizzes = null,
        Answers = null
    };

    public string SearchInput { get; set; } = "";

    public bool IsFocused { get; set; } = false;
    
    private ICollection<QuestionListModel> Questions { get; set; } = new List<QuestionListModel>();
    
    private ICollection<UserListModel> Users { get; set; } = new List<UserListModel>();
    
    private ICollection<AnswerListModel> Answer { get; set; } = new List<AnswerListModel>();

    public async Task Search()
    {
        IsFocused = true;
        Questions = await QuestionFacade.GetQuestionByText(SearchInput);
        Users = await UserFacade.GetUsersByName(SearchInput);
        Answer = await AnswerFacade.GetAnswersByText(SearchInput);
    }


    public async Task Close()
    {
        Questions  = new List<QuestionListModel>();
        Users = new List<UserListModel>();
        Answer = new List<AnswerListModel>();
    }
    
    protected override async Task OnInitializedAsync()
    {
        UserLogged = await UserFacade.Profile();
        
        if (UserLogged == null)
        {
           // navigationManager.NavigateTo("/login");
        }
        
        await base.OnInitializedAsync();
    }

}