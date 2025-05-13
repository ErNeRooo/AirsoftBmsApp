using AirsoftBmsApp.ViewModel.Abstractions;

namespace AirsoftBmsApp.View.Pages;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(IFormViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}