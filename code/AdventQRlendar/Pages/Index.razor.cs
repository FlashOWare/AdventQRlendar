using Microsoft.AspNetCore.Components;

namespace AdventQRlendar.Pages;

public partial class Index
{
    [Inject]
    public NavigationManager NavManager { get; set; } = null!;

    protected override void OnInitialized()
    {
        NavManager.NavigateTo("/qrlendar");
    }
}
