using AirsoftBmsApp.Model;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Validation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.AccountRestService.Abstractions;
using AirsoftBmsApp.Model.Dto.Player;

namespace AirsoftBmsApp.ViewModel.PlayerFormViewModel
{
    public partial class PlayerFormViewModel : ObservableObject, IPlayerFormViewModel
    {
        private IPlayerRestService _playerRestService;
        private IPlayerDataService _playerDataService;
        private IAccountRestService _accountRestService;

        [ObservableProperty] 
        PlayerForm playerForm = new();

        [ObservableProperty]
        bool isLoading = false;

        [ObservableProperty]
        string errorMessage = "";

        public PlayerFormViewModel(
            IPlayerRestService playerRestService, 
            IPlayerDataService playerDataService, 
            IAccountRestService accountRestService, 
            IValidationHelperFactory validationHelperFactory
            )
        {
            _playerRestService = playerRestService;
            _playerDataService = playerDataService;
            _accountRestService = accountRestService;

            validationHelperFactory.AddValidations(playerForm);
        }

        [RelayCommand]
        void Validate()
        {
            ValidateName();
            ValidateEmail();
            ValidatePassword();
            ValidateConfirmPassword();
        }

        [RelayCommand]
        void ValidateName()
        {
            playerForm.Name.Validate();
        }

        [RelayCommand]
        void ValidateEmail()
        {
            playerForm.Email.Validate();
        }

        [RelayCommand]
        void ValidatePassword()
        {
            playerForm.Password.Validate();
        }

        [RelayCommand]
        void ValidateConfirmPassword()
        {
            playerForm.ConfirmPassword.Validate();
        }

        [RelayCommand]
        public async Task RegisterPlayerAsync()
        {
            ErrorMessage = "";
            ValidateName();

            if (!playerForm.Name.IsValid) return;

            IsLoading = true;

            var registerPlayer = new PlayerRegisterHandler(_playerRestService, _playerDataService);

            var data = new PostPlayerDto
            {
                Name = playerForm.Name.Value
            };

            var result = await registerPlayer.Handle(data);

            switch (result)
            {
                case SuccessBase success:
                    await Shell.Current.GoToAsync(nameof(RoomFormPage));
                    break;
                case Failure failure:
                    ErrorMessage = failure.errorMessage;
                    break;
                case Error error:
                    ErrorMessage = error.errorMessage;
                    break;
                default:
                    throw new InvalidOperationException("Unknown result type");
            }

            IsLoading = false;
        }

        [RelayCommand]
        async Task OnLogInButtonClicked()
        {
            ErrorMessage = "";
            ValidateEmail();
            ValidatePassword();

            if (!playerForm.Email.IsValid || !playerForm.Password.IsValid) return;

            IsLoading = true;

            var registerPlayer = new PlayerRegisterHandler(_playerRestService, _playerDataService);
            var logInAccount = new AccountLogInHandler(_accountRestService, _playerDataService);

            registerPlayer.SetNext(logInAccount);

            var data = new
            {
                Name = playerForm.Name.Value,
                Email = playerForm.Email.Value,
                Password = playerForm.Password.Value
            };

            var result = await registerPlayer.Handle(data);

            switch (result)
            {
                case SuccessBase success:
                    await Shell.Current.GoToAsync(nameof(RoomFormPage));
                    break;
                case Failure failure:
                    ErrorMessage = failure.errorMessage;
                    break;
                case Error error:
                    ErrorMessage = error.errorMessage;
                    break;
                default:
                    throw new InvalidOperationException("Unknown result type");
            }

            IsLoading = false;
        }

        [RelayCommand]
        async Task OnSignUpButtonClicked()
        {
            ErrorMessage = "";
            Validate();

            if (!playerForm.Name.IsValid || !playerForm.Email.IsValid || !playerForm.Password.IsValid || !playerForm.ConfirmPassword.IsValid) return;

            IsLoading = true;

            var registerPlayer = new PlayerRegisterHandler(_playerRestService, _playerDataService);
            var signUpAccount = new AccountSignUpHandler(_accountRestService, _playerDataService);

            registerPlayer.SetNext(signUpAccount);

            var data = new
            {
                Name = playerForm.Name.Value,
                Email = playerForm.Email.Value,
                Password = playerForm.Password.Value
            };

            var result = await registerPlayer.Handle(data);

            switch (result)
            {
                case SuccessBase success:
                    await Shell.Current.GoToAsync(nameof(RoomFormPage));
                    break;
                case Failure failure:
                    ErrorMessage = failure.errorMessage;
                    break;
                case Error error:
                    ErrorMessage = error.errorMessage;
                    break;
                default:
                    throw new InvalidOperationException("Unknown result type");
            }

            IsLoading = false;
        }
    }
}
