using AirsoftBmsApp.ViewModel.Abstractions;

namespace AirsoftBmsApp.View.Pages;

public partial class LogInPage : ContentPage
{
	public LogInPage(IPlayerFormViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}