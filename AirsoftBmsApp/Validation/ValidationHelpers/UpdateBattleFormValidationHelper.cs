using AirsoftBmsApp.Model.Validatable;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Validation.Rules;

namespace AirsoftBmsApp.Validation.ValidationHelpers
{
    public class UpdateBattleFormValidationHelper
    {
        public void AddAllValidations(ValidatableUpdateBattleForm roomForm)
        {
            AddNameValidations(roomForm);
        }

        private void AddNameValidations(ValidatableUpdateBattleForm roomForm)
        {
            roomForm.Name.Validations.Add(new HasMaxLengthRule<string>()
            {
                ValidationMessage = string.Format(AppResources.BattleNameIsTooShortValidationMessage, 60),
                MaxLength = 60
            });
        }
    }
}
