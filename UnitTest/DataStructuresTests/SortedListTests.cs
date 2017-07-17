using DataStructures.SortedCollections;
using System;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class SortedListTests
    {
        [Fact]
        public static void DoTest()
        {
            // New empty sorted list
            var sortedList = new SortedList<int>();

            // Expeted outcome
            var expectedSort = new int[15] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 15, 20, 25, 30, 35 };

            // Insert items in arbitrary-order
            sortedList.Add(35);
            sortedList.Add(5);
            sortedList.Add(10);
            sortedList.Add(15);
            sortedList.Add(20);
            sortedList.Add(1);
            sortedList.Add(6);
            sortedList.Add(2);
            sortedList.Add(7);
            sortedList.Add(3);
            sortedList.Add(8);
            sortedList.Add(4);
            sortedList.Add(9);
            sortedList.Add(30);
            sortedList.Add(25);


            //
            // Helper variables
            int index = 0;
            var enumerator = sortedList.GetEnumerator();

            //
            // Begin comparison
            // Compare length and count
            Assert.Equal(sortedList.Count, expectedSort.Length);

            //
            // Compare sort order
            while (enumerator.MoveNext() && (index < expectedSort.Length))
            {
                Assert.Equal(enumerator.Current, expectedSort[index]);
                index++;
            }

            //
            // Assert index access
            index = 0;
            while (index < sortedList.Count && index < expectedSort.Length)
            {
                Assert.Equal(sortedList[index], expectedSort[index]);
                index++;
            }

            //
            // Assert removal of items correctly
            Assert.True(sortedList.Contains(10), "Expected 10 to exist in sortedList.");
            var remove10Status = sortedList.Remove(10);
            Assert.True(remove10Status, "Expected 10 to be removed successfully.");
            Assert.False(sortedList.Contains(10), "Expected 10 to be removed from sortedList.");

            //
            // Assert non-removal of non-existing items
            Assert.False(sortedList.Contains(999999999), "Expected 999999999 to not exist in sortedList.");
            var remove999999999Status = sortedList.Remove(999999999);
            Assert.False(remove999999999Status, "Expected 999999999 to not be removed successfully.");
            Assert.False(sortedList.Contains(999999999), "Expected 999999999 to not exist in sortedList.");

            //
            // Assert throws exception
            var threwException = false;

            try
            {
                sortedList.RemoveAt(sortedList.Count * 2);  // illegal index
            }
            catch (IndexOutOfRangeException)
            {
                threwException = true;
            }

            Assert.True(threwException, "Expected to throw an exception on illegal index.");

            //
            // Assert indexOf returns correct information
            Assert.True(0 == sortedList.IndexOf(1), "Expected 1 to be the smallest number and hence at index 0.");
            Assert.True(-1 == sortedList.IndexOf(987654321), "Expected 987654321 not to be in sortedList.");

            //
            // Assert correct sort after updating on index
            // Add back 10
            sortedList.Add(10);
            // Modify elements in increasing order
            sortedList[11] = 11;
            sortedList[12] = 12;
            sortedList[13] = 13;
            sortedList[14] = 14;

            var newExpectedSort = new int[15] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            index = 0;
            enumerator = sortedList.GetEnumerator();

            // Compare length and count
            Assert.True(sortedList.Count == newExpectedSort.Length, "Wrong number of items.");

            // Compare sort order
            while (enumerator.MoveNext() && (index < newExpectedSort.Length))
            {
                // TODO: Verify
                Assert.True(enumerator.Current == newExpectedSort[index], "Wrong sorting order.");
                index++;
            }
        }
    }
}

