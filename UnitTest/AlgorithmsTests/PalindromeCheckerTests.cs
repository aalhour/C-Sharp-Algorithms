using Algorithms.Numeric;
using Utils.XUnit;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public class PalindromeCheckerTests
    {
       
        [Fact]
        public static void Should_return_true_number()
        {
            var testInt = 1234321;
            var result = PalindromeChecker.IsPalindromeNumber(testInt);
            Assert.True(result.Equals(true));
        }
        
        [Fact]
        public static void Should_return_true()
        {
            var testString = "ABCDCBA";
            var result = PalindromeChecker.IsPalindromeString(testString);
            Assert.True(result.Equals(true));
        }

        [Fact]
        public static void Should_return_false()
        {
            var testString = "NotTrue";
            var result = PalindromeChecker.IsPalindromeString(testString);
            Assert.True(result.Equals(true));
        }

    }
}