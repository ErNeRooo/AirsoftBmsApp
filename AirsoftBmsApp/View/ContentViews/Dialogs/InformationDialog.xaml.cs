using System.Windows.Input;

namespace AirsoftBmsApp.View.ContentViews.Dialogs;

public partial class InformationDialog : ContentView
{
    public static readonly BindableProperty MessageProperty =
    BindableProperty.Create(nameof(Message), typeof(string), typeof(ConfirmationDialog), string.Empty);

    public string Message
    {
        get => (string)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    public InformationDialog()
	{
		InitializeComponent();
	}

    public async void ClearMessage(object sender, EventArgs e)
    {
        Message = "";
    }
}