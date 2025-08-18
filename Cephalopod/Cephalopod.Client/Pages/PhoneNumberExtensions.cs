using System.Text.RegularExpressions;

namespace Cephalopod.Client.Pages;

public static class PhoneNumberExtensions
{
    public static string ToIranianPhoneNumber(this string phoneNumber)
    {
        if (Regex.IsMatch(phoneNumber, @"^09\d{9}$"))
            return $"+98{phoneNumber.TrimStart('0')}";

        if (Regex.IsMatch(phoneNumber, @"^\+989\d{9}$"))
            return phoneNumber;

        if (Regex.IsMatch(phoneNumber, @"^989\d{9}$"))
            return $"+{phoneNumber}";

        throw new InvalidDataException("Phone number is not valid");
    }
}