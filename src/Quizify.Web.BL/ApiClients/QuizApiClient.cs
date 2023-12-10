namespace Quizify.Web.BL;

public partial class QuizApiClient
{
    public QuizApiClient(HttpClient httpClient, string baseUrl)
        : this(httpClient)
    {
        BaseUrl = baseUrl;
    }
}