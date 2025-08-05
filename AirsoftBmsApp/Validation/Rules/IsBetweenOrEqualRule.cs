using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AirsoftBmsApp.Validation.Rules
{
    public class IsBetweenOrEqualRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public bool Check(T value)
        {
            if (value == null)
                return false;

            var str = value.ToString();
            int intValue = int.Parse(str);

            return intValue >= MinValue && intValue <= MaxValue;
        }
    }
}
