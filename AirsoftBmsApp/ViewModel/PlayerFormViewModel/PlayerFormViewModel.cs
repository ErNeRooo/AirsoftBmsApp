using AirsoftBmsApp.Model;
using AirsoftBmsApp.Model.Dto.Login;
using AirsoftBmsApp.Model.Dto.Post;
using AirsoftBmsApp.Model.Dto.Register;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Validation.Rules;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AirsoftBmsApp.ViewModel.PlayerFormViewModel
{
    public partial class PlayerFormViewModel : ObservableObject, IPlayerFormViewModel
    {
        private IPlayerRestService _playerRestService;
        private IPlayerDataService _playerDataService;

        [ObservableProperty] 
        PlayerForm playerForm = new();

        [ObservableProperty]
        bool isLoading = false;

        [ObservableProperty]
        string errorMessage = "";

        public PlayerFormViewModel(IPlayerRestService playerRestService, IPlayerDataService playerDataService)
        {
            _playerRestService = playerRestService;
            _playerDataService = playerDataService;

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
        private void CleanErrorMessage()
        {
            ErrorMessage = "";
            //IsErrorMessageVisible = "false";
        }

        [RelayCommand]
        public async Task RegisterPlayerAsync()
        {
            CleanErrorMessage();
            ValidateName();

            if (!playerForm.Name.IsValid) return;

            IsLoading = true;

            PostPlayerDto playerDto = new PostPlayerDto
            {
                Name = playerForm.Name.Value
            };

            var result = await _playerRestService.RegisterPlayerAsync(playerDto);

            switch (result) { 
                case Success<Player> success:
                    _playerDataService.Player.Jwt = success.data.Jwt;
                    _playerDataService.Player.Id = success.data.Id;
                    _playerDataService.Player.Name = playerForm.Name.Value;
                    _playerDataService.Player.Account = null;

                    await Shell.Current.GoToAsync(nameof(RoomFormPage));

                    IsLoading = false;

                    break;
                case Failure<Player> failure:
                    ErrorMessage = failure.errorMessage;
                    //IsErrorMessageVisible = "true";

                    IsLoading = false;
                    break;
            }
        }

        [RelayCommand]
        async void LogIntoAccount()
        {
            CleanErrorMessage();
            ValidateEmail();
            ValidatePassword();

            if (!playerForm.Email.IsValid || !playerForm.Password.IsValid) return;

            IsLoading = true;

            LoginAccountDto accountDto = new LoginAccountDto
            {
                Email = playerForm.Email.Value,
                Password = playerForm.Password.Value
            };

            var result = await _playerRestService.LogInToAccountAsync(accountDto);

            switch (result)
            {
                case Success<Player> success:
                    _playerDataService.Player.Jwt = success.data.Jwt;
                    _playerDataService.Player.Id = success.data.Id;
                    _playerDataService.Player.Name = success.data.Name;
                    _playerDataService.Player.Account = success.data.Account;

                    await Shell.Current.GoToAsync(nameof(RoomFormPage));

                    IsLoading = false;

                    break;
                case Failure<Player> failure:
                    ErrorMessage = failure.errorMessage;
                    //IsErrorMessageVisible = "true";

                    IsLoading = false;
                    break;
            }
        }

        [RelayCommand]
        async void SignUpAccount()
        {
            CleanErrorMessage();
            Validate();

            if (!playerForm.Name.IsValid || !playerForm.Email.IsValid || !playerForm.Password.IsValid || !playerForm.ConfirmPassword.IsValid) return;

            IsLoading = true;

            RegisterAccountDto accountDto = new RegisterAccountDto
            {
                Name = playerForm.Name.Value,
                Email = playerForm.Email.Value,
                Password = playerForm.Password.Value
            };

            var result = await _playerRestService.SignUpAccountAsync(accountDto);

            switch (result)
            {
                case Success<Player> success:
                    _playerDataService.Player.Jwt = success.data.Jwt;
                    _playerDataService.Player.Id = success.data.Id;
                    _playerDataService.Player.Name = success.data.Name;
                    _playerDataService.Player.Account = success.data.Account;

                    await Shell.Current.GoToAsync(nameof(RoomFormPage));

                    IsLoading = false;

                    break;
                case Failure<Player> failure:
                    ErrorMessage = failure.errorMessage;
                    //IsErrorMessageVisible = "true";

                    IsLoading = false;
                    break;
            }
        }
    }
}
