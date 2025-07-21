using System.Windows.Input;

namespace AirsoftBmsApp.View.ContentViews.Dialogs;

public partial class ConfirmationDialog : ContentView
{
	public static readonly BindableProperty MessageProperty =
        BindableProperty.Create(nameof(Message), typeof(string), typeof(ConfirmationDialog), string.Empty);

    public static readonly BindableProperty ConfirmCommandProperty =
        BindableProperty.Create(nameof(ConfirmCommand), typeof(ICommand), typeof(ConfirmationDialog), null);

    public string Message
    {
        get => (string)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    public ICommand ConfirmCommand
    {
        get => (ICommand)GetValue(ConfirmCommandProperty);
        set => SetValue(ConfirmCommandProperty, value);
    }

    public ConfirmationDialog()
	{
		InitializeComponent();

	}

    public async void ClearMessage(object sender, EventArgs e)
    {
        Message = "";
    }
}