using AirsoftBmsApp.ViewModel.Abstractions;

namespace AirsoftBmsApp.View.Pages;

public partial class JoinRoomPage : ContentPage
{
	public JoinRoomPage(IJoinRoomFormViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}