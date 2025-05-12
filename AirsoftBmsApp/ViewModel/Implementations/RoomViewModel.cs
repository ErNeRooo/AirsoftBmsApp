using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AirsoftBmsApp.Model;
using AirsoftBmsApp.ViewModel.Abstractions;

namespace AirsoftBmsApp.ViewModel
{
    public class RoomViewModel : INotifyPropertyChanged, IRoomViewModel
    {
        private ObservableCollection<Team> _teams;

        public ObservableCollection<Team> Teams
        {
            get => _teams;
            set
            {
                if (_teams != value)
                {
                    _teams = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _joinCode = "021370";

        public string JoinCode
        {
            get => _joinCode;
            set
            {
                if (value != _joinCode)
                {
                    _joinCode = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _id;

        public int Id
        {
            get => _id;
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public RoomViewModel()
        {
            Teams = new ObservableCollection<Team>
            {
                new Team
                {
                    Name = "Under No Flag",
                    Players = new ObservableCollection<RoomMember>
                    {
                        new RoomMember
                        {
                            Id = 1,
                            AccountId = 1,
                            Name = "consectetur",
                            IsDead = false
                        },
                        new RoomMember
                        {
                            Id = 2,
                            AccountId = 2,
                            Name = "Lorem",
                            IsDead = true
                        },
                        new RoomMember
                        {
                            Id = 3,
                            AccountId = 3,
                            Name = "Ipsum",
                            IsDead = false
                        }
                    },
                    TeamTheme = new TeamTheme
                    {
                        BackgroundColor = Color.FromArgb("#4F4F4F"),
                        SurfaceColor = Color.FromArgb("#1C1917"),
                        TitleColor = Color.FromArgb("#D4D4D4"),
                        FontColor = Color.FromArgb("#F5F5F5")
                    },
                    OfficerId = 1,
                    Id = 1,
                    RoomId = 1
                },



                new Team
                {
                    Name = "Blue Team",
                    Players = new ObservableCollection<RoomMember>
                    {
                        new RoomMember
                        {
                            Id = 4,
                            AccountId = 4,
                            Name = "ullamcorper",
                            IsDead = false
                        },
                        new RoomMember
                        {
                            Id = 5,
                            AccountId = 5,
                            Name = "gravida",
                            IsDead = true
                        },
                        new RoomMember
                        {
                            Id = 6,
                            AccountId = 6,
                            Name = "porttitor",
                            IsDead = false
                        },
                        new RoomMember
                        {
                            Id = 7,
                            AccountId = 7,
                            Name = "ultricies",
                            IsDead = false
                        }
                    },
                    TeamTheme = new TeamTheme
                    {
                        BackgroundColor = Color.FromArgb("#0E1838"),
                        SurfaceColor = Color.FromArgb("#172554"),
                        TitleColor = Color.FromArgb("#2563EB"),
                        FontColor = Color.FromArgb("#DBEAFE")
                    },
                    OfficerId = 7,
                    Id = 2,
                    RoomId = 1
                },



                new Team
                {
                    Name = "Red Team",
                    Players = new ObservableCollection<RoomMember>
                    {
                        new RoomMember
                        {
                            Id = 8,
                            AccountId = 8,
                            Name = "ErNeRooo",
                            IsDead = false
                        },
                        new RoomMember
                        {
                            Id = 9,
                            AccountId = 9,
                            Name = "Suspendisse",
                            IsDead = true
                        },
                        new RoomMember
                        {
                            Id = 10,
                            AccountId = 10,
                            Name = "volutpat",
                            IsDead = false
                        },
                        new RoomMember
                        {
                            Id = 11,
                            AccountId = 11,
                            Name = "himenaeos",
                            IsDead = false
                        },
                        new RoomMember
                        {
                            Id = 12,
                            AccountId = 12,
                            Name = "adipiscing",
                            IsDead = false
                        },
                        new RoomMember
                        {
                            Id = 13,
                            AccountId = 13,
                            Name = "dolor",
                            IsDead = false
                        }
                    },
                    TeamTheme = new TeamTheme
                    {
                        BackgroundColor = Color.FromArgb("#390D16"),
                        SurfaceColor = Color.FromArgb("#4C0519"),
                        TitleColor = Color.FromArgb("#E11D48"),
                        FontColor = Color.FromArgb("#FFE4E6")
                    },
                    OfficerId = 8,
                    Id = 3,
                    RoomId = 1
                }
            };
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
