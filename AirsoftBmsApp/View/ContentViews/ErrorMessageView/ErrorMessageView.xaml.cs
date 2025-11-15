using System.Windows.Input;

namespace AirsoftBmsApp.View.ContentViews.ErrorMessageView;

public partial class ErrorMessageView : ContentView
{
	public static readonly BindableProperty ErrorMessageProperty = BindableProperty.Create(
        nameof(ErrorMessage),
        typeof(string),
        typeof(ErrorMessageView),
        default(string));

    public ICommand ClearErrorMessageCommand { get; private set; }

    public string ErrorMessage
    {
        get => (string)GetValue(ErrorMessageProperty);
        set => SetValue(ErrorMessageProperty, value);
    }

    public void ClearErrorMessage()
    {
        ErrorMessage = "";
    }

    public ErrorMessageView()
	{
        ClearErrorMessageCommand = new Command(ClearErrorMessage);
        InitializeComponent();
	}
}