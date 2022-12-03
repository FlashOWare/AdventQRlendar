using AdventQRlendar.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics;

namespace AdventQRlendar.Pages;

public partial class QRlendar
{
    private IJSObjectReference? module;

    [Inject]
    public IJSRuntime JS { get; set; } = null!;

    [Inject]
    public QRlendarService QRlendarService { get; set; } = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            module = await JS.InvokeAsync<IJSObjectReference>(
                "import", "./Pages/QRlendar.razor.js");
        }
    }

    private void OnShuffle()
    {
        QRlendarService.Shuffle();
    }

    private async Task OnPrintAsync()
    {
        Debug.Assert(module is not null);

        await module.InvokeVoidAsync("print");
    }
}
