using AirsoftBmsApp.Model.Validatable;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Validation.Rules;

namespace AirsoftBmsApp.Validation.ValidationHelpers
{
    public class PlayerFormValidationHelper
    {
        public void AddAllValidations(ValidatablePlayerForm playerForm)
        {
            AddNameValidations(playerForm);
            AddEmailValidations(playerForm);
            AddPasswordValidations(playerForm);
            AddConfirmPasswordValidations(playerForm);
        }

        public void AddNameValidations(ValidatablePlayerForm playerForm)
        {
            playerForm.Name.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = AppResources.NameIsRequiredValidationMessage
            });

            playerForm.Name.Validations.Add(new HasMaxLengthRule<string>
            {
                ValidationMessage = AppResources.PlayerNameIsUnder21CharactersLongValidationMessage,
                MaxLength = 20
            });
        }

        private void AddEmailValidations(ValidatablePlayerForm playerForm)
        {
            playerForm.Email.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = AppResources.EmailIsRequiredValidationMessage
            });

            playerForm.Email.Validations.Add(new IsEmailRule<string>
            {
                ValidationMessage = AppResources.EmailFormatValidationMessage
            });
        }

        private void AddPasswordValidations(ValidatablePlayerForm playerForm)
        {
            playerForm.Password.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = AppResources.PasswordIsRequiredValidationMessage
            });

            playerForm.Password.Validations.Add(new HasMinLengthRule<string>
            {
                ValidationMessage = AppResources.PasswordIsLongerThanValidationMessage,
                MinLength = 10
            });

            playerForm.Password.Validations.Add(new HasMinDigitCountRule<string>
            {
                ValidationMessage = AppResources.PasswordHasDigitValidationMessage,
                MinCount = 1
            });

            playerForm.Password.Validations.Add(new HasMinLowercaseCountRule<string>
            {
                ValidationMessage = AppResources.PasswordHasLowercaseLetterValidationMessage,
                MinCount = 1
            });

            playerForm.Password.Validations.Add(new HasMinUppercaseCountRule<string>
            {
                ValidationMessage = AppResources.PasswordHasUppercaseLetterValidationMessage,
                MinCount = 1
            });

            playerForm.Password.Validations.Add(new HasMinSpecialCharCountRule<string>
            {
                ValidationMessage = AppResources.PasswordHasSpecialCharacterValidationMessage,
                MinCount = 1
            });
        }

        private void AddConfirmPasswordValidations(ValidatablePlayerForm playerForm)
        {
            playerForm.ConfirmPassword.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = AppResources.ConfirmPasswordIsRequiredValidationMessage
            });

            playerForm.ConfirmPassword.Validations.Add(new MatchPasswordRule(() => playerForm.Password.Value)
            {
                ValidationMessage = AppResources.PasswordsDontMatchValidationMessage
            });
        }
    }
}
