using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BetRoulette.Application.Validators;

public class EnumAttribute : ValidationAttribute
{
    public Type Type { get; set; }

    private const string DefaultErrorMessage = "'{0}' is not valid.";


    public EnumAttribute(Type type)
        : base(DefaultErrorMessage)
    {
        Type = type;
    }

    public override string FormatErrorMessage(string name)
    {
        return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString, name);
    }

    public override bool IsValid(object value)
    {
        return value == null || Enum.IsDefined(Type, value);
    }
}
