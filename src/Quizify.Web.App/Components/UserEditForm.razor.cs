using Microsoft.AspNetCore.Components;
using Quizify.Common.Models;
using Quizify.Web.BL.Facades;

namespace Quizify.Web.App.Components;

public partial class UserEditForm
{
    [Inject]
    public UserFacade UserFacade { get; set; } = null!;
    
    [Parameter]
    public Guid Id { get; init; }  [Parameter]
    public EventCallback OnModification { get; set; }

    public UserDetailModel Data { get; set; } = new()
    {
        Name = null,
        Id = Guid.Empty
    };

    protected override async Task OnInitializedAsync()
    {
        Data = await UserFacade.GetByIdAsync(Id);
     
        await base.OnInitializedAsync();
    }

    public async Task Save()
    {
        await UserFacade.UpdateAsync(Data);
        await NotifyOnModification();
    }
    
    private async Task NotifyOnModification()
    {
        if (OnModification.HasDelegate)
        {
            await OnModification.InvokeAsync(null);
        }
    }
}
