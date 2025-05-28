using AirsoftBmsApp.Model;

namespace AirsoftBmsApp.View.ContentViews.Team;

public partial class RoomMemberItemView : Border
{
    public static readonly BindableProperty RoomMemberProperty = BindableProperty.Create(nameof(RoomMember), typeof(ObservablePlayer), typeof(RoomMemberItemView));

    public ObservablePlayer RoomMember
    {
        get => (ObservablePlayer)GetValue(RoomMemberProperty);
        set => SetValue(RoomMemberProperty, value);
    }

    public static readonly BindableProperty TeamThemeProperty = BindableProperty.Create(nameof(Theme), typeof(ObservableTeamTheme), typeof(RoomMemberItemView));

    public ObservableTeamTheme Theme
    {
        get => (ObservableTeamTheme)GetValue(TeamThemeProperty);
        set => SetValue(TeamThemeProperty, value);
    }

    public RoomMemberItemView()
	{
		InitializeComponent();
	}
}