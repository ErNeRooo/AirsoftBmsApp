using AirsoftBmsApp.View.ContentViews.CustomMap;
using AirsoftBmsApp.ViewModel.MapViewModel;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Microsoft.Maui.Maps.Handlers;

namespace AirsoftBmsApp.View.Pages;

public partial class MapPage : ContentView
{
	public MapPage(IMapViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

		var mapSpan = new MapSpan(new Location(53.13125437031764, 21.042482965882634), 0.01, 0.01);
        map.MoveToRegion(mapSpan);
		map.MapClicked += viewModel.MapClicked;
    }
}