using AirsoftBmsApp.Model;

namespace AirsoftBmsApp.View.ContentViews;

public partial class PlayerItemView : Border
{
    public static readonly BindableProperty PlayerProperty = BindableProperty.Create(nameof(Player), typeof(Player), typeof(PlayerItemView));

    public Player Player
    {
        get => (Player)GetValue(PlayerProperty);
        set => SetValue(PlayerProperty, value);
    }

    public static readonly BindableProperty TeamThemeProperty = BindableProperty.Create(nameof(Theme), typeof(TeamTheme), typeof(PlayerItemView));

    public TeamTheme Theme
    {
        get => (TeamTheme)GetValue(TeamThemeProperty);
        set => SetValue(TeamThemeProperty, value);
    }

    public PlayerItemView()
	{
		InitializeComponent();
	}
}