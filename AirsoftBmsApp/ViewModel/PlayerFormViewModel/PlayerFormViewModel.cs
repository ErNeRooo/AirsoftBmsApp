using AirsoftBmsApp.Model;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Validation.Rules;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AirsoftBmsApp.ViewModel.PlayerFormViewModel
{
    public partial class PlayerFormViewModel : ObservableObject, IPlayerFormViewModel
    {
        private IPlayerRestService _playerRestService;

        [ObservableProperty] 
        PlayerForm playerForm = new();

        public PlayerFormViewModel(IPlayerRestService playerRestService)
        {
            _playerRestService = playerRestService;

            playerForm.Name.Validations.Add(new IsNotNullOrEmptyRule<string> 
            { 
                ValidationMessage = "Name is required." 
            });

            playerForm.Name.Validations.Add(new HasMaxLengthRule<string>
            {
                ValidationMessage = "Name must be 20 characters or fewer.",
                MaxLength = 20
            });

            playerForm.Email.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Email is required."
            });

            playerForm.Email.Validations.Add(new IsEmailRule<string>
            {
                ValidationMessage = "Wrong Email Format."
            });

            playerForm.Password.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Password is required."
            });

            playerForm.ConfirmPassword.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Confirm password is required."
            }); 

            playerForm.ConfirmPassword.Validations.Add(new MatchPasswordRule(() => playerForm.Password.Value)
            {
                ValidationMessage = "Passwords do not match."
            });
        }

        [RelayCommand]
        public void Validate()
        {
            ValidateName();
            ValidateEmail();
            ValidatePassword();
            ValidateConfirmPassword();
        }

        [RelayCommand]
        public void ValidateName()
        {
            playerForm.Name.Validate();
        }

        [RelayCommand]
        public void ValidateEmail()
        {
            playerForm.Email.Validate();
        }

        [RelayCommand]
        public void ValidatePassword()
        {
            playerForm.Password.Validate();
        }

        [RelayCommand]
        public void ValidateConfirmPassword()
        {
            playerForm.ConfirmPassword.Validate();
        }

        [RelayCommand]
        public async Task RegisterPlayerAsync()
        {
            ValidateName();

            if (!playerForm.Name.IsValid) return;

            Task<bool> response = _playerRestService.RegisterPlayerAsync(playerForm.Name.Value);

            if (response.Result)
            {
                await Shell.Current.GoToAsync(nameof(RoomFormPage));
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        [RelayCommand]
        async void LogIntoAccount()
        {
            ValidateEmail();
            ValidatePassword();

            if (!playerForm.Email.IsValid || !playerForm.Password.IsValid) return;

            Task<bool> response = _playerRestService.LogInToAccountAsync(playerForm.Email.Value, playerForm.Password.Value);

            if (response.Result)
            {
                await Shell.Current.GoToAsync(nameof(RoomFormPage));
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        [RelayCommand]
        async void SignUpAccount()
        {
            Validate();

            if (!playerForm.Name.IsValid || !playerForm.Email.IsValid || !playerForm.Password.IsValid || !playerForm.ConfirmPassword.IsValid) return;

            Task<bool> response = _playerRestService.SignUpAccountAsync(playerForm.Name.Value, playerForm.Email.Value, playerForm.Password.Value);

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
