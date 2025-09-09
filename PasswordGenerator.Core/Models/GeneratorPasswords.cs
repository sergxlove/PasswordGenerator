using PasswordGenerator.Core.Enums;
using System.Text;
using System.Text.RegularExpressions;

namespace PasswordGenerator.Core.Models
{
    public class GeneratorPasswords
    {
        public static string GeneratePassword(int length, bool isLowChar, bool isStrongChar, 
            bool isValuesChar, bool isSpecificChar)
        {
            int quantityValues = 0;
            int quantitySpecific = 0;
            List<char> aviableChars = new List<char>(length);
            Random rnd = new();
            if (isValuesChar)
            {
                quantityValues = GetQuantityValuesSymbols(length);
                aviableChars.AddRange(SelectRandomValueSymbols(quantityValues));
            }
            if (isSpecificChar)
            {
                quantitySpecific = GetQuantitySpecificSymbols(length);
                aviableChars.AddRange(SelectRandomSpecificSymbols(quantitySpecific));
            }
            aviableChars.AddRange(SelectRandomSymbols(length - quantitySpecific - quantityValues,
                isLowChar, isStrongChar));
            List<char> resultList = aviableChars.OrderBy(x => rnd.Next()).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (char c in resultList)
            {
                sb.Append(c);
            }
            return sb.ToString();
        }

        public static LevelComplexity GetLevelComplexity(string password)
        {
            bool hasDigits = Regex.IsMatch(password, @"\d");
            bool hasLowChars = Regex.IsMatch(password, "[a-z]");
            bool hasHighChars = Regex.IsMatch(password, "[A-Z]");
            bool hasSpecificChar = Regex.IsMatch(password, "[!@#$%]");
            if (password.Length > 18 && hasDigits && hasLowChars && hasHighChars && hasSpecificChar)
            {
                return LevelComplexity.VeryHigh;
            }
            if (password.Length > 18 && hasDigits && hasLowChars && hasHighChars)
            {
                return LevelComplexity.High;
            }
            if (password.Length > 12 && password.Length <= 18 && hasDigits && hasLowChars &&
                hasHighChars && hasSpecificChar)
            {
                return LevelComplexity.High;
            }
            if (password.Length > 18)
            {
                return LevelComplexity.Middle;
            }
            if (password.Length > 8 && password.Length <= 12)
            {
                return LevelComplexity.Middle;
            }
            if (password.Length >= 6 && password.Length <= 8)
            {
                return LevelComplexity.Low;
            }
            if (password.Length < 6)
            {
                return LevelComplexity.VeryLow;
            }
            return LevelComplexity.VeryLow;
        }

        private static int GetQuantitySpecificSymbols(int length)
        {
            switch (length)
            {
                case int l when l >= 5 && l <= 10:
                    return 1;
                case int l when l > 10 && l <= 20:
                    return 2;
                case int l when l > 20 && l <= 40:
                    return 4;
                case int l when l > 40 && l <= 80:
                    return 7;
                case int l when l > 80 && l <= 128:
                    return 10;
                default:
                    return 1;
            }
        }

        private static int GetQuantityValuesSymbols(int length)
        {
            switch (length)
            {
                case int l when l >= 5 && l <= 10:
                    return 1;
                case int l when l > 10 && l <= 20:
                    return 2;
                case int l when l > 20 && l <= 40:
                    return 3;
                case int l when l > 40 && l <= 80:
                    return 7;
                case int l when l > 80 && l <= 128:
                    return 10;
                default:
                    return 1;
            }
        }

        private static List<char> SelectRandomValueSymbols(int count)
        {
            List<char> result = new List<char>();
            Random rnd = new Random();
            List<char> values = new()
            {
                '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
            };
            for (int i = 0; i < count; i++)
            {
                result.Add(values[rnd.Next(0, values.Count)]);
            }
            return result;
        }

        private static List<char> SelectRandomSpecificSymbols(int count)
        {
            List<char> result = new List<char>();
            Random rnd = new Random();
            List<char> symbols = new()
            {
                '*', '@', '#', '$', ')', '(', ':'
            };
            for (int i = 0; i < count; i++)
            {
                result.Add(symbols[rnd.Next(0, symbols.Count)]);
            }
            return result;
        }

        private static List<char> SelectRandomSymbols(int count, bool isLow, bool isStrong)
        {
            List<char> result = new();
            if (!isLow && !isStrong) return result;
            Random rnd = new Random();
            List<char> symbols = new();
            if (isLow)
            {
                symbols.AddRange('a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l',
                    'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z');
            }
            if (isStrong)
            {
                symbols.AddRange('A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
                    'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z');
            }
            for (int i = 0; i < count; i++)
            {
                result.Add(symbols[rnd.Next(0, symbols.Count)]);
            }
            return result;
        }
    }
}
