using AirsoftBmsApp.Model.Validatable;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Validation.Rules;

namespace AirsoftBmsApp.Validation.ValidationHelpers
{
    public class UpdateRoomFormValidationHelper
    {
        public void AddAllValidations(ValidatableUpdateRoomForm roomForm)
        {
            AddJoinCodeValidations(roomForm);
            AddPasswordValidations(roomForm);
            AddMaxPlayersValidations(roomForm);
        }

        private void AddJoinCodeValidations(ValidatableUpdateRoomForm roomForm)
        {
            roomForm.JoinCode.Validations.Add(new OptionalLengthRule<string>
            {
                ValidationMessage =AppResources.PasswordIs6CharactersLongIfProvided,
                Length = 6
            });
        }

        private void AddPasswordValidations(ValidatableUpdateRoomForm roomForm)
        {

        }

        private void AddMaxPlayersValidations(ValidatableUpdateRoomForm roomForm)
        {
            roomForm.MaxPlayers.Validations.Add(
                new IsBetweenOrEqualRule<int>
                {
                    ValidationMessage = AppResources.MaxPlayersIsNotInRangeValidationMessage,
                    MinValue = 2,
                    MaxValue = 99999
                });
        }
    }
}
