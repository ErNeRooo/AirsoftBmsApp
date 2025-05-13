using AirsoftBmsApp.ViewModel.Abstractions;

namespace AirsoftBmsApp.View.Pages;

public partial class LogInPage : ContentPage
{
	public LogInPage(IFormViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}