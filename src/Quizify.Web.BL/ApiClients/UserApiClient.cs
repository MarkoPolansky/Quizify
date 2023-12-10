namespace Quizify.Web.BL;


public partial class UserApiClient
{
    public UserApiClient(HttpClient httpClient, string baseUrl)
        : this(httpClient)
    {
        BaseUrl = baseUrl;
    }
}
