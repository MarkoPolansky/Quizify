using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Quizify.Common.Enums;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Pages.Game;

public partial class Result
{
    [Inject]
    IJSRuntime JSRuntime { get; set; } = null!;
    
    private IJSObjectReference _jsModule;
    private string token;
    
    [Inject]
    public UserFacade UserFacade { get; set; }
    
    public int TotalPoints{ get; set; }
    public int UserPoints{ get; set; }
    
    public QuizDetailModel QuizDetailModel { get; set; } = new QuizDetailModel
    {
        Title = null,
        QuizState = QuizStateEnum.Published,
        CreatedByUser = new UserListModel
        {
            Name = null,
            Id = Guid.Empty
        },
        Id = default
    };
    
    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;
    
    [Parameter]
    public Guid Id { get; set; }
    
        
    [Parameter]
    public Guid UserId { get; set; }
    [Inject]
    public QuizFacade QuizFacade { get; set; }

    [Inject]
    public QuestionFacade QuestionFacade { get; set; }

    public QuestionDetailModel QuestionDetailModel { get; set; } = new QuestionDetailModel
    {
        Text = "Quiz is not running",
        Type = TypeEnum.SingleSelect,
        Points = 0,
        QuizId = default,
        Id = default
    };

    public UserDetailModel User { get; set; } = new UserDetailModel
    {
        Name = null,
        Id = default
    };


    public ICollection<QuestionDetailModel> Questions { get; set; } = new List<QuestionDetailModel>();

  

    protected override async Task OnInitializedAsync()
    {
        User = await UserFacade.GetByIdAsync(UserId);
        
        QuizDetailModel =  await QuizFacade.GetByIdAsync(Id);
        
        TotalPoints = QuizDetailModel.Questions.Sum(a => a.Points);
        UserPoints = User.Quizzes.First(a => a.Quiz.Id == Id).TotalPoints;

        foreach (var question in QuizDetailModel.Questions)
        {
            Questions.Add(await QuestionFacade.GetByIdAsync(question.Id));
        }
        await base.OnInitializedAsync();
    }
    
    public bool IsAnswerPicked(AnswerDetailModel answerDetailModel)
    {
        foreach (var answer in User.Answers)
        {
            if (answer.Answer.Id == answerDetailModel.Id)
                return true;
        }
        return false;
        
    }
    
    public int GetRecivedPointsForQuestion(QuestionDetailModel question)
    {
        var q = Questions.First(a => a.Id == question.Id);
        var correctAnswers = q.Answers.Where(a => a.IsCorrect).ToHashSet();
        var userAnswersForQuestion = User.Answers.Where(q => q.Answer.QuestionId == question.Id).Select(a => a.Answer).ToHashSet();
        return userAnswersForQuestion.SetEquals(correctAnswers) ? q.Points : 0;

    }
    
  

}