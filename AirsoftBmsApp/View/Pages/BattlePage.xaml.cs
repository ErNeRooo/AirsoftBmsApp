using AirsoftBmsApp.ViewModel.BattleViewModel;
using AirsoftBmsApp.ViewModel.RoomViewModel;

namespace AirsoftBmsApp.View.Pages;

public partial class BattlePage : ContentView
{
	public BattlePage(IBattleViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}