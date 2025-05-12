using AirsoftBmsApp.ViewModel;
using AirsoftBmsApp.ViewModel.Abstractions;

namespace AirsoftBmsApp.View.Pages;

public partial class RoomMembersPage : ContentPage
{
	public RoomMembersPage(IRoomViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}