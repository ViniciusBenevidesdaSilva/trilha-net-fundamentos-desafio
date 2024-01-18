using RegularExpression = System.Text.RegularExpressions.Regex;

namespace WebAPI.Domain.Utils;

public static class Regex
{
    private static bool ValidaPadrao(string input, string pattern)
    {
        var regex = new RegularExpression(pattern);
        return regex.IsMatch(input);
    }

    public static bool ValidaFormatoEmail(string email)
    {
        string pattern = @"^[a-zA-Z][a-zA-Z0-9._]*@[a-zA-Z]+(\.[a-zA-Z]+)+$";
        return ValidaPadrao(email, pattern);
    }

    public static bool ValidaFormatoPlaca(string placa)
    {
        string pattern = @"^[a-zA-Z]{3}-\d(([a-zA-Z]\d{2})|(\d{3}))$";
        return ValidaPadrao(placa, pattern);
    }
}
