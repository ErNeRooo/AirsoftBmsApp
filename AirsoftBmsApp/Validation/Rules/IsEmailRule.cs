using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AirsoftBmsApp.Validation.Rules
{
    public class IsEmailRule<T> : IValidationRule<T>
    {
        private readonly Regex _regex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        public string ValidationMessage { get; set; }

        public bool Check(T value) =>
            value is string str && _regex.IsMatch(str);
    }
}
