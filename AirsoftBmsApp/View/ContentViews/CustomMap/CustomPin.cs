using Microsoft.Maui.Controls.Maps;
using System.Windows.Input;

namespace AirsoftBmsApp.View.ContentViews.CustomMap;

public class CustomPin : Pin
{
    public ICommand ClickedCommand { get; set; }
    public string IconSource { get; set; }
}