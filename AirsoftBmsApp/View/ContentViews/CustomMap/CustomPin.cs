using Microsoft.Maui.Controls.Maps;
using System.Windows.Input;

namespace AirsoftBmsApp.View.ContentViews.CustomMap;

public class CustomPin : Pin
{
    public ICommand ClickedCommand { get; set; }
    public string IconSource { get; set; }
    public int IconSizeInPixels { get; set; } = 40;
    public float HorizontalAnchor { get; set; } = 0.5f;
    public float VerticalAnchor { get; set; } = 0.5f;
    public float Rotation { get; set; } = 0;
}