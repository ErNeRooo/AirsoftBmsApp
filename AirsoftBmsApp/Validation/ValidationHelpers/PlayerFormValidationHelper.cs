using AirsoftBmsApp.Model.Validatable;
using AirsoftBmsApp.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                ValidationMessage = "Name is required."
            });

            playerForm.Name.Validations.Add(new HasMaxLengthRule<string>
            {
                ValidationMessage = "Name must be 20 characters or fewer.",
                MaxLength = 20
            });
        }

        private void AddEmailValidations(ValidatablePlayerForm playerForm)
        {
            playerForm.Email.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Email is required."
            });

            playerForm.Email.Validations.Add(new IsEmailRule<string>
            {
                ValidationMessage = "Wrong Email Format."
            });
        }

        private void AddPasswordValidations(ValidatablePlayerForm playerForm)
        {
            playerForm.Password.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Password is required."
            });

            playerForm.Password.Validations.Add(new HasMinLengthRule<string>
            {
                ValidationMessage = "Password must be at least 10 characters long.",
                MinLength = 10
            });

            playerForm.Password.Validations.Add(new HasMinDigitCountRule<string>
            {
                ValidationMessage = "Password must contain at least 1 digit.",
                MinCount = 1
            });

            playerForm.Password.Validations.Add(new HasMinLowercaseCountRule<string>
            {
                ValidationMessage = "Password must contain at least 1 lowercase letter.",
                MinCount = 1
            });

            playerForm.Password.Validations.Add(new HasMinUppercaseCountRule<string>
            {
                ValidationMessage = "Password must contain at least 1 uppercase letter.",
                MinCount = 1
            });

            playerForm.Password.Validations.Add(new HasMinSpecialCharCountRule<string>
            {
                ValidationMessage = "Password must contain at least 1 special character.",
                MinCount = 1
            });
        }

        private void AddConfirmPasswordValidations(ValidatablePlayerForm playerForm)
        {
            playerForm.ConfirmPassword.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Confirm password is required."
            });

            playerForm.ConfirmPassword.Validations.Add(new MatchPasswordRule(() => playerForm.Password.Value)
            {
                ValidationMessage = "Passwords do not match."
            });
        }
    }
}
