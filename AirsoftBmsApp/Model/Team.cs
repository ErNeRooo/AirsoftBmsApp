using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model
{
    public class Team : INotifyPropertyChanged
    {
        private int _id;
        private int _roomId;
        private int _officerId;
        private string _name;
        private ObservableCollection<RoomMember> _players;
        private TeamTheme _teamTheme;

        public int Id
        {
            get => _id;
            set { if (_id != value) { _id = value; OnPropertyChanged(); } }
        }

        public int RoomId
        {
            get => _roomId;
            set { if (_roomId != value) { _roomId = value; OnPropertyChanged(); } }
        }

        public int OfficerId
        {
            get => _officerId;
            set { if (_officerId != value) { _officerId = value; OnPropertyChanged(); } }
        }

        public string Name
        {
            get => _name;
            set { if (_name != value) { _name = value; OnPropertyChanged(); } }
        }

        public ObservableCollection<RoomMember> Players
        {
            get => _players;
            set { if (_players != value) { _players = value; OnPropertyChanged(); } }
        }

        public TeamTheme TeamTheme
        {
            get => _teamTheme;
            set { if (_teamTheme != value) { _teamTheme = value; OnPropertyChanged(); } }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
