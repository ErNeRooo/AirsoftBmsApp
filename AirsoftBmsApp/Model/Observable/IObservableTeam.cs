using AirsoftBmsApp.Resources.Styles.TeamTheme;
using System.Collections.ObjectModel;

namespace AirsoftBmsApp.Model.Observable
{
    public interface IObservableTeam
    {
        int Id { get; set; }
        int RoomId { get; set; }
        int? OfficerId { get; set; }
        string Name { get; set; }
        ObservableCollection<ObservablePlayer> Players { get; set; }
        ITeamTheme TeamTheme { get; set; }

        void Attach(ObservablePlayer observer);
        void Detach(ObservablePlayer observer);
        void Notify();
    }
}
