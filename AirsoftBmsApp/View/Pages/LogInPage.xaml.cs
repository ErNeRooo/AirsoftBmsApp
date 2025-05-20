using AirsoftBmsApp.ViewModel.PlayerFormViewModel;

namespace AirsoftBmsApp.View.Pages;

public partial class LogInPage : ContentPage
{
	public LogInPage(IPlayerFormViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}