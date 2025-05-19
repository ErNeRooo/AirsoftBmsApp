using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AirsoftBmsApp.Validation.Rules
{
    public class MatchPasswordRule : IValidationRule<string>
    {
        private readonly Func<string> _getPassword;

        public string ValidationMessage { get; set; }

        public MatchPasswordRule(Func<string> getPassword)
        {
            _getPassword = getPassword;
        }

        public bool Check(string confirmPassword)
        {
            var password = _getPassword.Invoke();
            return confirmPassword == password;
        }
    }

}
