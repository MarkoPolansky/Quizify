using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.BL.Facades.IFacades

{
    public interface IQuizFacade : IFacade<QuizListModel,QuizDetailModel>
    {
        Task<Guid?> Start(Guid modelId);
        Task<QuizDetailModel> Publish(Guid modelId);
        Task<Guid?> End(Guid modelId);
        Task<QuizDetailModel> Join(string gamePin);
        
        Task DeleteQuizUser(Guid id);
 
    }
}
