using Microsoft.AspNetCore.Components;

namespace Quizify.Web.App.Pages.Admin.Users;

public partial class Edit
{
    [Inject]
    private NavigationManager navigationManager { get; set; } = null!;
    
    [Parameter]
    public Guid Id { get; init; }
    
    public void NavigateBack()
    {
        navigationManager.NavigateTo("/admin/users");
    }
}