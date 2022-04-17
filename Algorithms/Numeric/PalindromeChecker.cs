using System;

namespace Algorithms.Numeric
{
    public static class PalindromeChecker
    {
        public static bool IsPalindromeNumber(int input)
        {
            var stringInput = input.ToString();

            return IsPalindromeString(stringInput);
        }
        
        public static bool IsPalindromeString(string input)  
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            var inputString = input.ToCharArray();
            var reversedString = string.Empty;
            
            for (var i = inputString.Length - 1; i >= 0; i--)
            {
                reversedString = inputString[i] + reversedString;
            }

            return string.Equals(input, reversedString, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}