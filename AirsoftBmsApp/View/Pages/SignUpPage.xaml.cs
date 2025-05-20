using AirsoftBmsApp.ViewModel.PlayerFormViewModel;

namespace AirsoftBmsApp.View.Pages;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(IPlayerFormViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}