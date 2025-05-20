using AirsoftBmsApp.ViewModel.Abstractions;

namespace AirsoftBmsApp.View.Pages;

public partial class CreateRoomPage : ContentPage
{
	public CreateRoomPage(ICreateRoomFormViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}