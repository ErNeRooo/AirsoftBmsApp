using AirsoftBmsApp.Model;

namespace AirsoftBmsApp.View.ContentViews.Team;

public partial class RoomMemberItemView : Border
{
    public static readonly BindableProperty RoomMemberProperty = BindableProperty.Create(nameof(RoomMember), typeof(RoomMember), typeof(RoomMemberItemView));

    public RoomMember RoomMember
    {
        get => (RoomMember)GetValue(RoomMemberProperty);
        set => SetValue(RoomMemberProperty, value);
    }

    public static readonly BindableProperty TeamThemeProperty = BindableProperty.Create(nameof(Theme), typeof(TeamTheme), typeof(RoomMemberItemView));

    public TeamTheme Theme
    {
        get => (TeamTheme)GetValue(TeamThemeProperty);
        set => SetValue(TeamThemeProperty, value);
    }

    public RoomMemberItemView()
	{
		InitializeComponent();
	}
}