using AdventQRlendar.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics;

namespace AdventQRlendar.Components;

public partial class QRCodeItem
{
    private IJSObjectReference? module;
    private ElementReference qrCodeElement;

    [Inject]
    public IJSRuntime JS { get; set; } = null!;

    [Inject]
    public QRCodeService QRCodeService { get; set; } = null!;

    [Parameter]
    [EditorRequired]
    public Door Door { get; set; } = null!;

    private async Task OnUrlChangedAsync(ChangeEventArgs e)
    {
        if (module is null)
        {
            return;
        }

        string? text = e.Value?.ToString();

        if (text is not null)
        {
            await SetQRCodeAsync(text);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            module = await JS.InvokeAsync<IJSObjectReference>("import",
    "./Components/QRCodeItem.razor.js");

            if (!string.IsNullOrWhiteSpace(Door.Url))
            {
                await SetQRCodeAsync(Door.Url);
            }
        }
    }

    private async Task SetQRCodeAsync(string text)
    {
        Debug.Assert(module is not null);

        byte[] qrCode = QRCodeService.GetQRCode(text, 1);
        await using MemoryStream imageStream = new(qrCode);
        using DotNetStreamReference dotnetImageStream = new(imageStream);
        await module.InvokeVoidAsync("setImage", qrCodeElement, dotnetImageStream);

        Door.Url = text;
    }
}
