using AirsoftBmsApp.ViewModel;

namespace AirsoftBmsApp.View.Pages;

public partial class RoomMembersPage : ContentPage
{
	public RoomMembersPage(RoomViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}