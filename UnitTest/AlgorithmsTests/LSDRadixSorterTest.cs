using Algorithms.Sorting;
using System.Collections.Generic;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public static class LSDRadixSorterTest
    {
        #region String character sorting tests

        [Fact]
        public static void LSDRadixSort_SortsCharactersInName()
        {
            var name = "Mr. Ahmad Alhour";
            
            var result = name.LSDRadixSort().Trim();
            
            Assert.Equal(".AAMadhhlmorru", result);
        }

        [Fact]
        public static void LSDRadixSort_SortsCharactersInLongerName()
        {
            var name = "Msr. Anna John Hopcraft";
            
            var result = name.LSDRadixSort().Trim();
            
            Assert.Equal(".AHJMaacfhnnnooprrst", result);
        }

        [Fact]
        public static void LSDRadixSort_SortsDigitsInNumber()
        {
            var number = "0987654321";
            
            var result = number.LSDRadixSort();
            
            Assert.Equal("0123456789", result);
        }

        [Fact]
        public static void LSDRadixSort_SortsDigitsInLongNumber()
        {
            var number = "000999888777111222333777666555";
            
            var result = number.LSDRadixSort();
            
            Assert.Equal("000111222333555666777777888999", result);
        }

        #endregion

        #region Fixed-width string list sorting tests

        [Fact]
        public static void LSDRadixSort_SortsListOfFixedWidthStrings()
        {
            // All strings must have the same length (5 characters)
            var toBeSorted = new List<string>() { "ahmad", "ahmed", "johny", "ammy1", "ammy2", "zeyad", "aliaa", "aaaaa", "mmmmm", "zzzzz" };
            var expected = new List<string>() { "aaaaa", "ahmad", "ahmed", "aliaa", "ammy1", "ammy2", "johny", "mmmmm", "zeyad", "zzzzz" };

            toBeSorted.LSDRadixSort(stringFixedWidth: 5);

            Assert.Equal(expected, toBeSorted);
        }

        [Fact]
        public static void LSDRadixSort_FixedWidth_AlreadySorted()
        {
            var list = new List<string>() { "aaa", "bbb", "ccc", "ddd" };
            var expected = new List<string>() { "aaa", "bbb", "ccc", "ddd" };

            list.LSDRadixSort(stringFixedWidth: 3);

            Assert.Equal(expected, list);
        }

        [Fact]
        public static void LSDRadixSort_FixedWidth_ReverseSorted()
        {
            var list = new List<string>() { "ddd", "ccc", "bbb", "aaa" };
            var expected = new List<string>() { "aaa", "bbb", "ccc", "ddd" };

            list.LSDRadixSort(stringFixedWidth: 3);

            Assert.Equal(expected, list);
        }

        [Fact]
        public static void LSDRadixSort_FixedWidth_SingleCharStrings()
        {
            var list = new List<string>() { "c", "a", "b", "e", "d" };
            var expected = new List<string>() { "a", "b", "c", "d", "e" };

            list.LSDRadixSort(stringFixedWidth: 1);

            Assert.Equal(expected, list);
        }

        #endregion

        #region Edge cases

        [Fact]
        public static void LSDRadixSort_EmptyString_ReturnsEmpty()
        {
            var str = "";
            
            var result = str.LSDRadixSort();
            
            Assert.Equal("", result);
        }

        [Fact]
        public static void LSDRadixSort_SingleChar_ReturnsSame()
        {
            var str = "x";
            
            var result = str.LSDRadixSort();
            
            Assert.Equal("x", result);
        }

        [Fact]
        public static void LSDRadixSort_NullList_DoesNotThrow()
        {
            List<string> list = null;
            
            // Should not throw - just returns early
            list.LSDRadixSort(stringFixedWidth: 3);
            
            Assert.Null(list);
        }

        [Fact]
        public static void LSDRadixSort_EmptyList_DoesNotThrow()
        {
            var list = new List<string>();
            
            list.LSDRadixSort(stringFixedWidth: 3);
            
            Assert.Empty(list);
        }

        [Fact]
        public static void LSDRadixSort_SingleElementList_ReturnsSame()
        {
            var list = new List<string>() { "hello" };
            
            list.LSDRadixSort(stringFixedWidth: 5);
            
            Assert.Single(list);
            Assert.Equal("hello", list[0]);
        }

        #endregion
    }
}
