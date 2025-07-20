namespace AirsoftBmsApp.View.ContentViews.ErrorMessageView;

public partial class ErrorMessageView : ContentView
{
	public static readonly BindableProperty ErrorMessageProperty = BindableProperty.Create(
        nameof(ErrorMessage),
        typeof(string),
        typeof(ErrorMessageView),
        default(string));

    public string ErrorMessage
    {
        get => (string)GetValue(ErrorMessageProperty);
        set => SetValue(ErrorMessageProperty, value);
    }

    public async void ClearErrorMessage(object sender, EventArgs e)
    {
        ErrorMessage = "";
    }

    public ErrorMessageView()
	{
		InitializeComponent();
	}
}