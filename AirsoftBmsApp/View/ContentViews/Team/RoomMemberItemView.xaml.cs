using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Utils;
using AirsoftBmsApp.ViewModel.RoomViewModel;

namespace AirsoftBmsApp.View.ContentViews.Team;

public partial class RoomMemberItemView : Border
{
    public RoomMemberItemView()
	{
		InitializeComponent();
    }

    //protected override void OnBindingContextChanged()
    //{
    //    base.OnBindingContextChanged();

    //    ObservablePlayer player = this.BindingContext as ObservablePlayer;

    //    AdminIcon.IsVisible = player.IsAdmin;
    //}

    //private void OnLoaded(object? sender, EventArgs e)
    //{
    //    ObservablePlayer player = this.BindingContext as ObservablePlayer;
    //    var teamsScrollView = VisualTreeHelper.FindParent<TeamsScrollView>(this);

    //    if (teamsScrollView is not null)
    //    {

    //        IRoomViewModel roomViewModel = teamsScrollView.BindingContext as IRoomViewModel;
    //        ObservableTeam currentTeam = this.Parent.BindingContext as ObservableTeam;

    //        if (roomViewModel.Room.AdminPlayerId == player.Id) AdminIcon.IsVisible = true;
    //        if (currentTeam.OfficerId == player.Id) OfficerIcon.IsVisible = true;
    //    }
    //}
}