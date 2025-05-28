using AirsoftBmsApp.Model;
using AirsoftBmsApp.Resources.Styles.TeamTheme;

namespace AirsoftBmsApp.View.ContentViews.Team;

public partial class RoomMemberItemView : Border
{
    public static readonly BindableProperty RoomMemberProperty = BindableProperty.Create(nameof(RoomMember), typeof(ObservablePlayer), typeof(RoomMemberItemView));

    public ObservablePlayer RoomMember
    {
        get => (ObservablePlayer)GetValue(RoomMemberProperty);
        set => SetValue(RoomMemberProperty, value);
    }

    public static readonly BindableProperty TeamThemeProperty = BindableProperty.Create(nameof(Theme), typeof(ITeamTheme), typeof(RoomMemberItemView));

    public ITeamTheme Theme
    {
        get => (ITeamTheme)GetValue(TeamThemeProperty);
        set => SetValue(TeamThemeProperty, value);
    }

    public RoomMemberItemView()
	{
		InitializeComponent();
	}
}