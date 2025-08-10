using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Behaviors;
using System.Windows.Input;

namespace AirsoftBmsApp.View.ContentViews.Buttons;

public partial class BottomNavItem : Border
{
	public static readonly BindableProperty IsSelectedProperty =
        BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(BottomNavItem), false, propertyChanged: OnIsSelectedChanged);

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(BottomNavItem), string.Empty);
    
    public static readonly BindableProperty IconPathProperty =
        BindableProperty.Create(nameof(IconPath), typeof(string), typeof(BottomNavItem), string.Empty);

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(BottomNavItem), null);

    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(BottomNavItem), null);

    public static readonly BindableProperty CurrentPositionProperty =
        BindableProperty.Create(nameof(CurrentPosition), typeof(int), typeof(BottomNavItem), 0, propertyChanged: OnCurrentPositionChanged);

    public int CurrentPosition
    {
        get => (int)GetValue(CurrentPositionProperty);
        set => SetValue(CurrentPositionProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    public string IconPath
    {
        get => (string)GetValue(IconPathProperty);
        set => SetValue(IconPathProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public BottomNavItem()
    {
        InitializeComponent();
        UpdateIconColor(this);

        Dispatcher.Dispatch(() =>
        {
            UpdateIsSelected(this, CurrentPosition);
        });
    }

    private static void OnCurrentPositionChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is BottomNavItem navItem)
        {
            UpdateIsSelected(navItem, (int)newValue);
        }
    }

    private static void OnIsSelectedChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is BottomNavItem navItem)
        {
            UpdateIconColor(navItem);
        }
    }

    private static void UpdateIsSelected(BottomNavItem navItem, int position)
    {
        int parsedCommandParameter = int.Parse(navItem.CommandParameter?.ToString() ?? "0");
        navItem.IsSelected = position == parsedCommandParameter;
    }

    private static void UpdateIconColor(BottomNavItem navItem)
    {
        if (Application.Current is null) return;

        Color? primaryColor;
        Color? outlineColor;

        if (Application.Current.RequestedTheme == AppTheme.Light)
        {
            primaryColor = ((AppThemeColor)Application.Current.Resources["Primary"]).Light;
            outlineColor = ((AppThemeColor)Application.Current.Resources["Text"]).Light;
        }
        else
        {
            primaryColor = ((AppThemeColor)Application.Current.Resources["Primary"]).Dark;
            outlineColor = ((AppThemeColor)Application.Current.Resources["Text"]).Dark;
        }

        if (primaryColor is null) throw new Exception("Primary color is not implemented");
        if (outlineColor is null) throw new Exception("Text color is not implemented");

        var tintBehavior = new IconTintColorBehavior();
        tintBehavior.TintColor = navItem.IsSelected ? primaryColor : outlineColor;
        navItem.NavItemIcon.Behaviors.Clear();
        navItem.NavItemIcon.Behaviors.Add(tintBehavior);
    }
}