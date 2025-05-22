using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AirsoftBmsApp.Validation.Rules
{
    public class HasMinUppercaseCountRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public int MinCount { get; set; }

        public bool Check(T value)
        {
            if (value == null)
                return false;

            var str = value.ToString();
            int uppercaseCount = str.Count(char.IsUpper);

            return uppercaseCount >= MinCount;
        }
    }
}
