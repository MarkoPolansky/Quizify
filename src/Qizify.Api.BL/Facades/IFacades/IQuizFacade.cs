using Quizify.Api.DAL.EF.Entities;
using Quizify.Common.Models;

namespace Quizify.Api.BL.Facades.IFacades
{
    public interface IQuizFacade : IFacade<QuizEntity, QuizListModel, QuizDetailModel>
    {
        Guid? Start(Guid modelId);
        QuizDetailModel Publish(Guid modelId);
        Guid? End(Guid modelId);
        
        
        QuizDetailModel JoinQuiz(string gamePin);
        QuizDetailModel? GetByGamePin(string gamePin);
        void DeleteQuizUser(Guid id);
    }
}
