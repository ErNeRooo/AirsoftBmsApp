using AirsoftBmsApp.Model.Validatable;

namespace AirsoftBmsApp.Validation
{
    public interface IValidationHelperFactory
    {
        void AddValidations(ValidatablePlayerForm form);
        void AddValidations(ValidatableCreateRoomForm form);
        void AddValidations(ValidatableJoinRoomForm form);
        void AddValidations(ValidatableTeamForm form);
        void AddValidations(ValidatableUpdateRoomForm form, int currentNumberOfPlayersInRoom);
        void AddValidations(ValidatableUpdateBattleForm form);
    }
}
