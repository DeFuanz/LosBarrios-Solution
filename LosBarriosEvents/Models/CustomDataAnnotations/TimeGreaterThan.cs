using System.ComponentModel.DataAnnotations;

namespace LosBarriosEvents.Models.CustomDataAnnotations;
public class TimeGreaterThanAttribute : ValidationAttribute
{

    private readonly string _comparisonProperty;

    public TimeGreaterThanAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        ErrorMessage = ErrorMessageString;
        var currentValue = DateTime.Parse((string)value);

        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

        if (property == null)
            throw new ArgumentException("Property with this name not found");

        var comparisonValue = DateTime.Parse((string)property.GetValue(validationContext.ObjectInstance));

        if (currentValue < comparisonValue)
            return new ValidationResult(ErrorMessage);

        return ValidationResult.Success;
    }

}