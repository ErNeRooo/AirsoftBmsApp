using AirsoftBmsApp.Model.Validatable;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Validation.Rules;

namespace AirsoftBmsApp.Validation.ValidationHelpers
{
    public class UpdateRoomFormValidationHelper
    {
        public void AddAllValidations(ValidatableUpdateRoomForm roomForm, int currentNumberOfPlayersInRoom)
        {
            AddJoinCodeValidations(roomForm);
            AddPasswordValidations(roomForm);
            AddMaxPlayersValidations(roomForm, currentNumberOfPlayersInRoom);
        }

        private void AddJoinCodeValidations(ValidatableUpdateRoomForm roomForm)
        {
            roomForm.JoinCode.Validations.Add(new OptionalLengthRule<string>
            {
                ValidationMessage = AppResources.PasswordIs6CharactersLongIfProvided,
                Length = 6
            });
        }

        private void AddPasswordValidations(ValidatableUpdateRoomForm roomForm)
        {

        }

        public void AddMaxPlayersValidations(ValidatableUpdateRoomForm roomForm, int currentNumberOfPlayersInRoom)
        {
            roomForm.MaxPlayers.Validations.Add(
                new IsBetweenOrEqualRule<int>
                {
                    ValidationMessage = AppResources.MaxPlayersIsNotInRangeValidationMessage,
                    MinValue = 2,
                    MaxValue = 99999
                });

            roomForm.MaxPlayers.Validations.Add(
                new IsLargerThan<int>
                {
                    ValidationMessage = AppResources.MaxPlayersMustBeLargerThanCurrentNumberOfPlayersInRoom,
                    MinValue = currentNumberOfPlayersInRoom
                });
        }
    }
}
