using AirsoftBmsApp.Model.Validatable;
using AirsoftBmsApp.Validation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model.Observable
{
    public partial class ObservableBattleSettingsState : ObservableObject
    {
        [ObservableProperty]
        bool isVisible = false;

        [ObservableProperty]
        bool isActive = false;

        [ObservableProperty]
        ValidatableUpdateBattleForm battleForm = new();

        public ObservableBattleSettingsState(IValidationHelperFactory validationHelperFactory)
        {
            validationHelperFactory.AddValidations(BattleForm);
        }

        [RelayCommand]
        public void ValidateBattleName() => BattleForm.Name.Validate();
    }
}
