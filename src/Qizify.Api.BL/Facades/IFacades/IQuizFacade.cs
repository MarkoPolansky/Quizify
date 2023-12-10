using Quizify.Api.DAL.EF.Entities;
using Quizify.Common.Models;

namespace Quizify.Api.BL.Facades.IFacades
{
    public interface IQuizFacade : IFacade<QuizEntity, QuizListModel, QuizDetailModel>
    {
        Guid? Start(Guid modelId);
        string? Publish(Guid modelId);
        Guid? End(Guid modelId);
        
        Guid? Join(string gamePin,string userName);
    }
}
