using DataStructures.SortedCollections;
using System;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class SortedListTests
    {
        #region Add Tests

        [Fact]
        public static void Add_ArbitraryOrder_MaintainsSortedOrder()
        {
            var sortedList = new SortedList<int>();
            var expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 15, 20, 25, 30, 35 };

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

            Assert.Equal(expected.Length, sortedList.Count);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], sortedList[i]);
            }
        }

        #endregion

        #region GetEnumerator Tests

        [Fact]
        public static void GetEnumerator_ReturnsItemsInSortedOrder()
        {
            var sortedList = CreateTestList();
            var expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 15, 20, 25, 30, 35 };

            var enumerator = sortedList.GetEnumerator();
            var index = 0;

            while (enumerator.MoveNext() && index < expected.Length)
            {
                Assert.Equal(expected[index], enumerator.Current);
                index++;
            }
        }

        #endregion

        #region Indexer Tests

        [Fact]
        public static void Indexer_Get_ReturnsCorrectElements()
        {
            var sortedList = CreateTestList();

            Assert.Equal(1, sortedList[0]);
            Assert.Equal(5, sortedList[4]);
            Assert.Equal(35, sortedList[sortedList.Count - 1]);
        }

        [Fact]
        public static void Indexer_Set_ReplacesElementAtIndex()
        {
            var sortedList = CreateTestList();
            // CreateTestList returns: {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 15, 20, 25, 30, 35}

            // Replace element at index 0 (value 1) with 0 - should stay at index 0
            sortedList[0] = 0;
            Assert.Equal(0, sortedList[0]);

            // Replace element at last index (value 35) with 100 - should stay at last index
            sortedList[sortedList.Count - 1] = 100;
            Assert.Equal(100, sortedList[sortedList.Count - 1]);

            // Verify list structure is still correct
            Assert.Equal(15, sortedList.Count);
        }

        #endregion

        #region Contains Tests

        [Fact]
        public static void Contains_ExistingElement_ReturnsTrue()
        {
            var sortedList = CreateTestList();

            Assert.Contains(10, sortedList);
            Assert.Contains(1, sortedList);
            Assert.Contains(35, sortedList);
        }

        [Fact]
        public static void Contains_NonExistingElement_ReturnsFalse()
        {
            var sortedList = CreateTestList();

            Assert.DoesNotContain(999999999, sortedList);
        }

        #endregion

        #region Remove Tests

        [Fact]
        public static void Remove_ExistingElement_RemovesAndReturnsTrue()
        {
            var sortedList = CreateTestList();

            var result = sortedList.Remove(10);

            Assert.True(result);
            Assert.DoesNotContain(10, sortedList);
        }

        [Fact]
        public static void Remove_NonExistingElement_ReturnsFalse()
        {
            var sortedList = CreateTestList();

            var result = sortedList.Remove(999999999);

            Assert.False(result);
        }

        #endregion

        #region RemoveAt Tests

        [Fact]
        public static void RemoveAt_InvalidIndex_ThrowsIndexOutOfRangeException()
        {
            var sortedList = CreateTestList();

            Assert.Throws<IndexOutOfRangeException>(() => sortedList.RemoveAt(sortedList.Count * 2));
        }

        [Fact]
        public static void RemoveAt_ValidIndex_RemovesElement()
        {
            var sortedList = CreateTestList();
            var previousCount = sortedList.Count;

            sortedList.RemoveAt(0);

            Assert.Equal(previousCount - 1, sortedList.Count);
            Assert.Equal(2, sortedList[0]); // 1 was removed, now 2 is first
        }

        #endregion

        #region IndexOf Tests

        [Fact]
        public static void IndexOf_SmallestElement_ReturnsZero()
        {
            var sortedList = CreateTestList();

            Assert.Equal(0, sortedList.IndexOf(1));
        }

        [Fact]
        public static void IndexOf_NonExistingElement_ReturnsNegativeOne()
        {
            var sortedList = CreateTestList();

            Assert.Equal(-1, sortedList.IndexOf(987654321));
        }

        #endregion

        private static SortedList<int> CreateTestList()
        {
            var sortedList = new SortedList<int>();

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

            return sortedList;
        }
    }
}
