using Quizify.Api.DAL.EF.Entities;
using Quizify.Common.Models;

namespace Quizify.Api.BL.Facades.IFacades
{
    public interface IQuestionFacade : IFacade<QuestionEntity, QuestionListModel, QuestionDetailModel>
    {
        public List<QuestionListModel> GetQuestionByText(string? text);
    }
}
