using AirsoftBmsApp.ViewModel;
using AirsoftBmsApp.ViewModel.RoomViewModel;

namespace AirsoftBmsApp.View.Pages;

public partial class RoomMembersPage : ContentPage
{
	public RoomMembersPage(IRoomViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}