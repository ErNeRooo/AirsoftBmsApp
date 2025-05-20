using AirsoftBmsApp.ViewModel.JoinRoomFormViewModel;

namespace AirsoftBmsApp.View.Pages;

public partial class JoinRoomPage : ContentPage
{
	public JoinRoomPage(IJoinRoomFormViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}