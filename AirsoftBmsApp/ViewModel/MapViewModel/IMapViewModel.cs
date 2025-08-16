using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.View.ContentViews.CustomMap;
using Microsoft.Maui.Controls.Maps;

namespace AirsoftBmsApp.ViewModel.MapViewModel;

public interface IMapViewModel
{
    ObservableRoom Room { get; }
    ObservablePlayer Player { get; }
    List<CustomPin> VisiblePlayers { get; }

    public void MapClicked(object sender, MapClickedEventArgs e);
}
