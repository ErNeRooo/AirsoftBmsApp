using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AirsoftBmsApp.Model;
using AirsoftBmsApp.Services.Abstractions;
using AirsoftBmsApp.ViewModel.Abstractions;

namespace AirsoftBmsApp.ViewModel
{
    public class FormViewModel : INotifyPropertyChanged, IFormViewModel
    {
        private IPlayerRestService _playerRestService;
        public event PropertyChangedEventHandler? PropertyChanged;
        private Player _player;
        private PlayerForm _playerForm;

        public ICommand RegisterPlayer { get; }
        public ICommand LogIntoAccount { get; }
        public ICommand SignUpAccount { get; }

        public Player Player
        {
            get => _player;
            set
            {
                if (_player != value)
                {
                    _player = value;
                    OnPropertyChanged();
                }
            }
        }

        public PlayerForm PlayerForm
        {
            get => _playerForm;
            set
            {
                if (_playerForm != value)
                {
                    _playerForm = value;
                    OnPropertyChanged();
                }
            }
        }

        public FormViewModel(IPlayerRestService playerRestService)
        {
            RegisterPlayer = new Command(OnRegisterPlayer);
            LogIntoAccount = new Command(OnLogIntoAccount);
            SignUpAccount = new Command(OnSignUpAccount);

            _playerRestService = playerRestService;

            PlayerForm = new PlayerForm
            {

            };
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void OnRegisterPlayer()
        {
            Task<bool> response = _playerRestService.RegisterPlayerAsync(PlayerForm.Name);

            if (response.Result)
            {
                await Shell.Current.GoToAsync(nameof(RoomFormPage));
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public async void OnLogIntoAccount()
        {
            Task<bool> response = _playerRestService.LogInToAccountAsync(PlayerForm.Email, PlayerForm.Password);

            if (response.Result)
            {
                await Shell.Current.GoToAsync(nameof(RoomFormPage));
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public async void OnSignUpAccount()
        {
            Task<bool> response = _playerRestService.SignUpAccountAsync(PlayerForm.Name, PlayerForm.Email, PlayerForm.Password);

            if (response.Result)
            {
                await Shell.Current.GoToAsync(nameof(RoomFormPage));
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
