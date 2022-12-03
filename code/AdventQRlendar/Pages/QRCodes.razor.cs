using AdventQRlendar.Services;
using Microsoft.AspNetCore.Components;

namespace AdventQRlendar.Pages;

public partial class QRCodes
{
    [Inject]
    public QRlendarService QRlendar { get; set; } = null!;

    private void OnFill()
    {
        QRlendar.Fill();
    }
}
