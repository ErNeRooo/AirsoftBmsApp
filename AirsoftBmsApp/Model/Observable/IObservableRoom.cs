using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Model.Observable
{
    public interface IObservableRoom
    {
        void Attach(IObservablePlayer observer);
        void Detach(IObservablePlayer observer);
        void Notify();

        int Id { get; set; }

        int MaxPlayers { get; set; }

        string JoinCode { get; set; }

        int? AdminPlayerId { get; set; }

        ObservableCollection<ObservableTeam> Teams { get; set; } 

        ObservableBattle? Battle { get; set; }
    }
}
