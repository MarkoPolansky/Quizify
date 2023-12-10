using Quizify.Common.Models;
using Quizify.Web.BL.Facades.IFacades;

namespace Quizify.Web.BL.Facades;

public class AnswerFacade: FacadeBase ,IAnswerFacade
{
    private readonly IAnswerApiClient apiClient;
    
    public AnswerFacade(IAnswerApiClient _apiClient)
    {
        apiClient = _apiClient;
    }

    public async Task<List<AnswerListModel>> GetAllAsync()
    {
        var answers = new List<AnswerListModel>();
        var answersFromApi = await apiClient.AnswerGetAsync(culture);
        answers.AddRange(answersFromApi);
        return answers;
    }

    public async Task<AnswerDetailModel> GetByIdAsync(Guid id)
    {
        return await apiClient.AnswerGetAsync(id, culture);
    }

    public async Task<Guid> CreateAsync(AnswerDetailModel data)
    {
        return await apiClient.AnswerPostAsync(culture,data);
    }

    public async Task<Guid?> UpdateAsync(AnswerDetailModel data)
    {
        return await apiClient.AnswerPutAsync(culture,data);
    }

    public async Task DeleteAsync(Guid id)
    {
        await apiClient.AnswerDeleteAsync(id,culture);
    }

    public List<AnswerListModel> GetAnswersByText(string? text)
    {
        throw new NotImplementedException();
    }
}