using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Validation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Validatable;
using AirsoftBmsApp.Networking.ApiFacade;
using AirsoftBmsApp.Model.Dto.Account;
using System.Diagnostics;
using AirsoftBmsApp.View.Pages;

namespace AirsoftBmsApp.ViewModel.PlayerFormViewModel
{
    public partial class PlayerFormViewModel : ObservableObject, IPlayerFormViewModel
    {
        IApiFacade _apiFacade;

        [ObservableProperty] 
        ValidatablePlayerForm playerForm = new();

        [ObservableProperty]
        bool isLoading = false;

        [ObservableProperty]
        string errorMessage = "";

        public PlayerFormViewModel(
            IApiFacade apiFacade,
            IValidationHelperFactory validationHelperFactory
            )
        {
            _apiFacade = apiFacade;

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
        async Task Redirect(string path)
        {
#if DEBUG
            var ww = Stopwatch.StartNew();
#endif

            await Shell.Current.GoToAsync(path);

#if DEBUG
            ww.Stop();
            Debug.WriteLine($"Execution took {ww.Elapsed.TotalMilliseconds} ms");
#endif
        }

        [RelayCommand]
        public async Task RegisterPlayerAsync()
        {
            ErrorMessage = "";
            ValidateName();

            if (!playerForm.Name.IsValid) return;

            IsLoading = true;

            var playerDto = new PostPlayerDto
            {
                Name = playerForm.Name.Value
            };

            HttpResult result = await _apiFacade.Player.Register(playerDto);

            switch (result)
            {
                case Success:
                    await Redirect(nameof(RoomFormPage));
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
            ValidateName();

            if (!playerForm.Email.IsValid || !playerForm.Password.IsValid || !playerForm.Name.IsValid) return;

            IsLoading = true;

            var logInAccountDto = new LogInAccountDto
            {
                Email = playerForm.Email.Value,
                Password = playerForm.Password.Value
            };

            var result = await _apiFacade.Account.LogIn(logInAccountDto, playerForm.Name.Value);

            switch (result)
            {
                case Success:
                    await Redirect(nameof(RoomFormPage));
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

            var signUpAccountDto = new SignUpAccountDto
            {
                Email = playerForm.Email.Value,
                Password = playerForm.Password.Value
            };

            var result = await _apiFacade.Account.SignUp(signUpAccountDto, playerForm.Name.Value);

            switch (result)
            {
                case Success:
                    await Redirect(nameof(RoomFormPage));
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
        async Task GoBack()
        {
            await Redirect("..");
        }
    }
}
