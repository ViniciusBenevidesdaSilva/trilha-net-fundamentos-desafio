using RegularExpression = System.Text.RegularExpressions.Regex;

namespace WebAPI.Domain.Utils;

public static class Regex
{
    public static bool ValidaFormatoEmail(string email)
    {
        string pattern = @"^[a-zA-Z][a-zA-Z0-9._]*@[a-zA-Z]+(\.[a-zA-Z]+)+$";
        var regex = new RegularExpression(pattern);
        return regex.IsMatch(email);
    }
}
