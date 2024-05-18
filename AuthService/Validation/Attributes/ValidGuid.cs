using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace AuthService.Validation.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
sealed public class ValidGuid : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        try
        {
            if (value != null)
            {
                var guid = new Guid(value.ToString());
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public override string FormatErrorMessage(string name)
    {
        return "Not a valid GUID";
    }

}