using System.Text.RegularExpressions;

namespace DAL.Models
{
    public static class StudentIdRegex
    {
        // validate MSSV: cụm đầu SE/SS/IB/MC + 6 số, 2 số đầu >= 14 
        public const string Pattern = @"^(SE|SS|IB|MC)(1[4-9]|[2-9][0-9])[0-9]{4}$";

        public static bool IsValid(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            return Regex.IsMatch(value, Pattern);
        }
    }
}


