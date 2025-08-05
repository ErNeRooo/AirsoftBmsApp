using AirsoftBmsApp.Model.Validatable;
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
                ValidationMessage = "Password must be exactly 6 characters, if provided.",
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
                    ValidationMessage = "Max players must be between 2 and 99999.",
                    MinValue = 2,
                    MaxValue = 99999
                });
        }
    }
}
