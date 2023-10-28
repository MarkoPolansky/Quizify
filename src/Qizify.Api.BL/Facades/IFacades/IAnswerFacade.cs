using Quizify.Api.DAL.EF.Entities;
using Quizify.Common.Models;

namespace Quizify.Api.BL.Facades.IFacades
{
    public interface IAnswerFacade : IFacade<AnswerEntity, AnswerListModel, AnswerDetailModel>
    {
        public List<AnswerListModel> GetAnswersByText(string? text);
    }
}
