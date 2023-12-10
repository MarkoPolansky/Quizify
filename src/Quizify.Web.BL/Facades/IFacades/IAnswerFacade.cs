using Quizify.Common.Models;

namespace Quizify.Web.BL.Facades.IFacades

{
    public interface IAnswerFacade : IFacade<AnswerListModel, AnswerDetailModel>
    {
        public List<AnswerListModel> GetAnswersByText(string? text);
    }
}
