using AirsoftBmsApp.ViewModel.RoomViewModel;

namespace AirsoftBmsApp.View.Pages;

public partial class RoomMembersPage : ContentView
{
	public RoomMembersPage(IRoomViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
    }
}