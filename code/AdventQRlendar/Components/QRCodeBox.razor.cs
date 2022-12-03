using AdventQRlendar.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AdventQRlendar.Components;

public partial class QRCodeBox
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

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            module = await JS.InvokeAsync<IJSObjectReference>(
                "import", "./Components/QRCodeBox.razor.js");

            if (Door.IsSet())
            {
                byte[] qrCode = QRCodeService.GetQRCode(Door.Url, 4);
                await using MemoryStream imageStream = new(qrCode);
                using DotNetStreamReference dotnetImageStream = new(imageStream);
                await module.InvokeVoidAsync("setQRCode", qrCodeElement, dotnetImageStream);
            }
        }
    }
}
