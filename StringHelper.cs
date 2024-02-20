using System.Text.RegularExpressions;
public class StringHelper
{
    public static void ssss() { }
    public static string Wrap(string value)
    {
        return value.Replace("|", "\n");
    }
    public static bool IsNumerice(string value)
    {
        return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
    }
    public static bool IsInt(string value)
    {
        return Regex.IsMatch(value, @"^[+-]?\d*$");
    }
    public bool IsUnsign(string value)
    {
        return Regex.IsMatch(value, @"^[.]?\d*$");
    }
}
