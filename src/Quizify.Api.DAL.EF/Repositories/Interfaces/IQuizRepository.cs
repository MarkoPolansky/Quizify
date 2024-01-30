using Quizify.Api.DAL.EF.Entities;

namespace Quizify.Api.DAL.EF.Repositories.Interfaces
{
    public interface IQuizRepository : IApiRepository<QuizEntity> 
    {
        int CountGamePin(string gamePin);

        void RemoveQuizUser(Guid quizUser);
    }
}
