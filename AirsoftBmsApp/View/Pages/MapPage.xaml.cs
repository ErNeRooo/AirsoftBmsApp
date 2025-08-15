using AirsoftBmsApp.ViewModel.MapViewModel;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps.Handlers;

namespace AirsoftBmsApp.View.Pages;

public partial class MapPage : ContentView
{
	public MapPage(IMapViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}