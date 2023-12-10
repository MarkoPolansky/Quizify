using Quizify.Common.Models;


namespace Quizify.Web.BL.Facades.IFacades

{
    public interface IQuestionFacade : IFacade<QuestionListModel, QuestionDetailModel>
    {
        public List<QuestionListModel> GetQuestionByText(string? text);    
    }
}

