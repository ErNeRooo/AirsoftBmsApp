using AirsoftBmsApp.ViewModel.CreateRoomFormViewModel;

namespace AirsoftBmsApp.View.Pages;

public partial class CreateRoomPage : ContentPage
{
	public CreateRoomPage(ICreateRoomFormViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}