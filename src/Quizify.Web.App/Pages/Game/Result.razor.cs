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
    public UserDetailModel UserLogged { get; set; } = new UserDetailModel
    {
        Id = Guid.Empty,
        Name = null,
        ImageUrl = null,
        CreatedQuizzes = null,
        Quizzes = null,
        Answers = null
    };
    
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

    public ICollection<QuestionDetailModel> Questions { get; set; } = new List<QuestionDetailModel>();

  

    protected override async Task OnInitializedAsync()
    {
        UserLogged = await UserFacade.Profile();
        
        if (UserLogged?.Id == Guid.Empty)
        {
            navigationManager.NavigateTo("/login");
        }
        
        QuizDetailModel =  await QuizFacade.GetByIdAsync(Id);
        
        TotalPoints = QuizDetailModel.Questions.Sum(a => a.Points);
        UserPoints = UserLogged.Quizzes.First(a => a.Quiz.Id == Id).TotalPoints;

        foreach (var question in QuizDetailModel.Questions)
        {
            Questions.Add(await QuestionFacade.GetByIdAsync(question.Id));
        }
        await base.OnInitializedAsync();
    }

    public async Task Choose(AnswerDetailModel answerDetailModel)
    {
        var answer = new UserDetailAnswerModel
        {
            Answer = new AnswerListModel
            {
                Id = answerDetailModel.Id,
                Text = answerDetailModel.Text,
                ImageUrl = answerDetailModel.ImageUrl,
                Type = answerDetailModel.Type,
                IsCorrect = answerDetailModel.IsCorrect,
                QuestionId = answerDetailModel.QuestionId,
            },
            Id = Guid.NewGuid(),
        };
        if(UserLogged.Answers.Count(a => a.Answer.Id == answerDetailModel.Id) == 0)
            UserLogged.Answers.Add(answer);
        else
            UserLogged.Answers =  UserLogged.Answers.Where(a => a.Answer.Id != answerDetailModel.Id).ToList();
      
     
    }

    
    public async Task SubmitQuiz()
    {
        await UserFacade.UpdateAsync(UserLogged);
    }
    public bool IsAnswerPicked(AnswerDetailModel answerDetailModel)
    {
    
        
        foreach (var answer in UserLogged.Answers)
        {
            if (answer.Answer.Id == answerDetailModel.Id)
                return true;
        }
        return false;
        
    }
    
    public async Task NextQuestion()
    {
        if(IsCreator())
            await QuizFacade.Start(QuizDetailModel.Id);
    }


    private bool IsCreator()
    {
        return UserLogged.Id == QuizDetailModel.CreatedByUser.Id;
    }

    public int GetRecivedPointsForQuestion(QuestionDetailModel question)
    {
        var q = Questions.First(a => a.Id == question.Id);
        var correctAnswers = q.Answers.Where(a => a.IsCorrect).ToHashSet();
        var userAnswersForQuestion = UserLogged.Answers.Where(q => q.Answer.QuestionId == question.Id).Select(a => a.Answer).ToHashSet();
        return userAnswersForQuestion.SetEquals(correctAnswers) ? q.Points : 0;

    }
    
  

}