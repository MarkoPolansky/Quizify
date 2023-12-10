using Quizify.Common.Models;
using Quizify.Web.BL.Facades.IFacades;

namespace Quizify.Web.BL.Facades;

public class QuestionFacade: FacadeBase ,IQuestionFacade
{
    private readonly IQuestionApiClient apiClient;
    
    public QuestionFacade(IQuestionApiClient _apiClient)
    {
        apiClient = _apiClient;
    }
    public async Task<List<QuestionListModel>> GetAllAsync()
    {
        var questions = new List<QuestionListModel>();
        var questionsFromApi = await apiClient.QuestionGetAsync(culture);

        questions.AddRange(questionsFromApi);
        
        return questions;
    }

    public async Task<QuestionDetailModel> GetByIdAsync(Guid id)
    {
        return await apiClient.QuestionGetAsync(id,culture);

    }

    public async Task<Guid> CreateAsync(QuestionDetailModel data)
    {
        return await apiClient.QuestionPostAsync(culture,data);
    }

    public async Task<Guid?> UpdateAsync(QuestionDetailModel data)
    {
        return await apiClient.QuestionPutAsync(culture,data);
    }

    public async Task DeleteAsync(Guid id)
    {
        await apiClient.QuestionDeleteAsync(id,culture);
    }

    public List<QuestionListModel> GetQuestionByText(string? text)
    {
        throw new NotImplementedException();
    }
}