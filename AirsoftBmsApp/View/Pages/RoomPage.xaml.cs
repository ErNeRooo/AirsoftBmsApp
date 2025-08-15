

using AirsoftBmsApp.ViewModel.RoomViewModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace AirsoftBmsApp.View.Pages;

public partial class RoomPage : ContentPage
{
    private readonly IRoomViewModel _viewModel;

    public readonly BindableProperty NavItemClickedCommandProperty =
        BindableProperty.Create(nameof(NavItemClickedCommand), typeof(ICommand), typeof(RoomPage), null);

    public readonly BindableProperty CarouselPositionSelectedProperty =
        BindableProperty.Create(nameof(CarouselPositionSelected), typeof(int), typeof(RoomPage), 0);

    public int CarouselPositionSelected
    {
        get => (int)GetValue(CarouselPositionSelectedProperty);
        set => SetValue(CarouselPositionSelectedProperty, value);
    }

    public ICommand NavItemClickedCommand
    {
        get => (ICommand)GetValue(NavItemClickedCommandProperty);
        set => SetValue(NavItemClickedCommandProperty, value);
    }

    public RoomPage(
        IRoomViewModel viewModel,
        RoomMembersPage roomPage,
        BattlePage battlePage,
        MapPage mapPage)
    {
        InitializeComponent();

        _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        MainCarousel.ItemsSource = new List<ContentView>
        {
            roomPage,
            battlePage,
            mapPage
        };
        MainCarousel.CurrentItemChangedCommand = new RelayCommand(() =>
        {
            if (CarouselPositionSelected == 2) MainCarousel.IsSwipeEnabled = false;
            else MainCarousel.IsSwipeEnabled = true;
        });

        NavItemClickedCommand = new RelayCommand<string>(OnNavItemClicked);
    }

    private void OnNavItemClicked(string position)
    {
        if (int.TryParse(position, out int parsedPosition)) 
        {
            CarouselPositionSelected = parsedPosition;
        }
    }

    protected override bool OnBackButtonPressed()
    {
        _viewModel.LeaveRoom();
        return true;
    }
}