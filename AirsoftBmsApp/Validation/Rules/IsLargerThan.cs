namespace AirsoftBmsApp.Validation.Rules;

public class IsLargerThan<T> : IValidationRule<T>
{
    public string ValidationMessage { get; set; }
    public int MinValue { get; set; }

    public bool Check(T value)
    {
        if (value == null)
            return false;

        var str = value.ToString();
        int intValue = int.Parse(str);

        return intValue > MinValue;
    }
}
