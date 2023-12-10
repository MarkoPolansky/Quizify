using Quizify.Common.Models;
using Quizify.Web.BL.Facades.IFacades;

namespace Quizify.Web.BL.Facades;

public class QuizFacade : FacadeBase ,IQuizFacade
{
    
    private readonly IQuizApiClient apiClient;

    public QuizFacade(IQuizApiClient _apiClient)
    {
        apiClient = _apiClient;
    }
    public async Task<List<QuizListModel>> GetAllAsync()
    {
        var quizes = new List<QuizListModel>();
        var quizesFromApi = await apiClient.QuizGetAsync(culture);
        quizes.AddRange(quizesFromApi);
        return quizes;
    }

    public async Task<QuizDetailModel> GetByIdAsync(Guid id)
    {
        return await apiClient.QuizGetAsync(id,culture);
    }

    public async Task<Guid> CreateAsync(QuizDetailModel data)
    {
        return await apiClient.QuizPostAsync(culture,data);
    }

    public async Task<Guid?> UpdateAsync(QuizDetailModel data)
    {
        return await apiClient.QuizPutAsync(culture,data);
    }

    public async Task DeleteAsync(Guid id)
    {
        await apiClient.QuizDeleteAsync(id,culture);
    }

    public async Task<Guid?> Start(Guid modelId)
    {
        return await apiClient.StartAsync(modelId,culture);
    }

    public async Task<string?> Publish(Guid modelId)
    {
        return await apiClient.PublishAsync(modelId,culture);
    }

    public async Task<Guid?> End(Guid modelId)
    {
        return await apiClient.EndAsync(modelId,culture);
    }

    public async Task<Guid?> Join(string gamePin, string userName)
    {
        return await apiClient.JoinAsync(gamePin, userName,culture);
    }
}