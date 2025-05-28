using AirsoftBmsApp.ViewModel;
using AirsoftBmsApp.ViewModel.RoomViewModel;

namespace AirsoftBmsApp.View.Pages;

public partial class RoomMembersPage : ContentPage
{
    IRoomViewModel _viewModel;

	public RoomMembersPage(IRoomViewModel viewModel)
	{
        _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override bool OnBackButtonPressed()
    {
        _viewModel.LeaveRoom();
        return true;
    }
}