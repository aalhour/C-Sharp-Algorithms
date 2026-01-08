using System;
using System.Linq;
using DataStructures.Lists;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class SkipListTest
    {
        #region Constructor and Empty List Tests

        [Fact]
        public static void Constructor_NewList_IsEmpty()
        {
            var skipList = new SkipList<int>();

            Assert.True(skipList.IsEmpty);
            Assert.Equal(0, skipList.Count);
        }

        [Fact]
        public static void Constructor_NewList_LevelIsOne()
        {
            var skipList = new SkipList<int>();

            Assert.Equal(1, skipList.Level);
        }

        #endregion

        #region Add Tests

        [Fact]
        public static void Add_SingleElement_CountIsOne()
        {
            var skipList = new SkipList<int>();

            skipList.Add(42);

            Assert.Equal(1, skipList.Count);
            Assert.False(skipList.IsEmpty);
        }

        [Fact]
        public static void Add_MultipleElements_CountIsCorrect()
        {
            var skipList = new SkipList<int>();

            skipList.Add(10);
            skipList.Add(20);
            skipList.Add(30);

            Assert.Equal(3, skipList.Count);
        }

        [Fact]
        public static void Add_DescendingOrder_MaintainsCount()
        {
            var skipList = new SkipList<int>();

            for (int i = 100; i >= 0; --i)
                skipList.Add(i);

            Assert.Equal(101, skipList.Count);
        }

        [Fact]
        public static void Add_AscendingOrder_MaintainsCount()
        {
            var skipList = new SkipList<int>();

            for (int i = 0; i <= 100; ++i)
                skipList.Add(i);

            Assert.Equal(101, skipList.Count);
        }

        [Fact]
        public static void Add_MixedOrder_MaintainsCount()
        {
            var skipList = new SkipList<int>();

            // Add in various orders
            for (int i = 100; i >= 50; --i)
                skipList.Add(i);

            for (int i = 0; i <= 35; ++i)
                skipList.Add(i);

            for (int i = -15; i <= 0; ++i)
                skipList.Add(i);

            for (int i = -15; i >= -35; --i)
                skipList.Add(i);

            Assert.Equal(124, skipList.Count);
        }

        [Fact]
        public static void Add_NegativeNumbers_Works()
        {
            var skipList = new SkipList<int>();

            skipList.Add(-5);
            skipList.Add(-10);
            skipList.Add(-1);

            Assert.Equal(3, skipList.Count);
            Assert.True(skipList.Contains(-5));
            Assert.True(skipList.Contains(-10));
            Assert.True(skipList.Contains(-1));
        }

        [Fact]
        public static void Add_Duplicates_AddsAll()
        {
            var skipList = new SkipList<int>();

            skipList.Add(42);
            skipList.Add(42);
            skipList.Add(42);

            // SkipList allows duplicates
            Assert.Equal(3, skipList.Count);
        }

        #endregion

        #region Contains Tests

        [Fact]
        public static void Contains_ExistingElement_ReturnsTrue()
        {
            var skipList = new SkipList<int>();
            skipList.Add(10);
            skipList.Add(20);
            skipList.Add(30);

            Assert.True(skipList.Contains(20));
        }

        [Fact]
        public static void Contains_NonExistingElement_ReturnsFalse()
        {
            var skipList = new SkipList<int>();
            skipList.Add(10);
            skipList.Add(20);
            skipList.Add(30);

            Assert.False(skipList.Contains(25));
        }

        [Fact]
        public static void Contains_EmptyList_ReturnsFalse()
        {
            var skipList = new SkipList<int>();

            Assert.False(skipList.Contains(42));
        }

        [Fact]
        public static void Contains_FirstElement_ReturnsTrue()
        {
            var skipList = new SkipList<int>();
            skipList.Add(10);
            skipList.Add(5);
            skipList.Add(15);

            Assert.True(skipList.Contains(5)); // smallest
        }

        [Fact]
        public static void Contains_LastElement_ReturnsTrue()
        {
            var skipList = new SkipList<int>();
            skipList.Add(10);
            skipList.Add(5);
            skipList.Add(15);

            Assert.True(skipList.Contains(15)); // largest
        }

        #endregion

        #region Remove Tests

        [Fact]
        public static void Remove_ExistingElement_ReturnsTrue()
        {
            var skipList = new SkipList<int>();
            skipList.Add(10);
            skipList.Add(20);
            skipList.Add(30);

            var result = skipList.Remove(20);

            Assert.True(result);
        }

        [Fact]
        public static void Remove_ExistingElement_DecreasesCount()
        {
            var skipList = new SkipList<int>();
            skipList.Add(10);
            skipList.Add(20);
            skipList.Add(30);

            skipList.Remove(20);

            Assert.Equal(2, skipList.Count);
            Assert.False(skipList.Contains(20));
        }

        [Fact]
        public static void Remove_NonExistingElement_ReturnsFalse()
        {
            var skipList = new SkipList<int>();
            skipList.Add(10);
            skipList.Add(20);
            skipList.Add(30);

            var result = skipList.Remove(25);

            Assert.False(result);
            Assert.Equal(3, skipList.Count);
        }

        [Fact]
        public static void Remove_FirstElement_Works()
        {
            var skipList = new SkipList<int>();
            skipList.Add(10);
            skipList.Add(5);
            skipList.Add(15);

            skipList.Remove(5);

            Assert.False(skipList.Contains(5));
            Assert.Equal(2, skipList.Count);
        }

        [Fact]
        public static void Remove_LastElement_Works()
        {
            var skipList = new SkipList<int>();
            skipList.Add(10);
            skipList.Add(5);
            skipList.Add(15);

            skipList.Remove(15);

            Assert.False(skipList.Contains(15));
            Assert.Equal(2, skipList.Count);
        }

        [Fact]
        public static void Remove_AllElements_ListIsEmpty()
        {
            var skipList = new SkipList<int>();
            skipList.Add(10);
            skipList.Add(20);
            skipList.Add(30);

            skipList.Remove(10);
            skipList.Remove(20);
            skipList.Remove(30);

            Assert.True(skipList.IsEmpty);
            Assert.Equal(0, skipList.Count);
        }

        #endregion

        #region Clear Tests

        [Fact]
        public static void Clear_ResetsCount()
        {
            var skipList = new SkipList<int>();

            for (int i = 0; i < 50; i++)
                skipList.Add(i);

            skipList.Clear();

            Assert.Equal(0, skipList.Count);
            Assert.True(skipList.IsEmpty);
        }

        [Fact]
        public static void Clear_ThenAdd_Works()
        {
            var skipList = new SkipList<int>();
            skipList.Add(10);
            skipList.Add(20);
            skipList.Clear();

            skipList.Add(30);

            Assert.Equal(1, skipList.Count);
            Assert.True(skipList.Contains(30));
        }

        #endregion

        #region Peek and DeleteMin Tests

        [Fact]
        public static void Peek_NonEmptyList_ReturnsSmallest()
        {
            var skipList = new SkipList<int>();
            skipList.Add(30);
            skipList.Add(10);
            skipList.Add(20);

            var peek = skipList.Peek();

            Assert.Equal(10, peek);
            Assert.Equal(3, skipList.Count); // Peek doesn't remove
        }

        [Fact]
        public static void Peek_EmptyList_ThrowsInvalidOperationException()
        {
            var skipList = new SkipList<int>();

            Assert.Throws<InvalidOperationException>(() => skipList.Peek());
        }

        [Fact]
        public static void TryPeek_NonEmptyList_ReturnsTrueAndValue()
        {
            var skipList = new SkipList<int>();
            skipList.Add(30);
            skipList.Add(10);
            skipList.Add(20);

            var result = skipList.TryPeek(out int value);

            Assert.True(result);
            Assert.Equal(10, value);
        }

        [Fact]
        public static void TryPeek_EmptyList_ReturnsFalse()
        {
            var skipList = new SkipList<int>();

            var result = skipList.TryPeek(out int value);

            Assert.False(result);
        }

        [Fact]
        public static void DeleteMin_NonEmptyList_RemovesSmallest()
        {
            var skipList = new SkipList<int>();
            skipList.Add(30);
            skipList.Add(10);
            skipList.Add(20);

            var min = skipList.DeleteMin();

            Assert.Equal(10, min);
            Assert.Equal(2, skipList.Count);
            Assert.False(skipList.Contains(10));
        }

        [Fact]
        public static void DeleteMin_EmptyList_ThrowsInvalidOperationException()
        {
            var skipList = new SkipList<int>();

            Assert.Throws<InvalidOperationException>(() => skipList.DeleteMin());
        }

        #endregion

        #region Enumeration Tests

        [Fact]
        public static void GetEnumerator_ReturnsElementsInSortedOrder()
        {
            var skipList = new SkipList<int>();
            skipList.Add(30);
            skipList.Add(10);
            skipList.Add(50);
            skipList.Add(20);
            skipList.Add(40);

            var list = skipList.ToList();

            Assert.Equal(new[] { 10, 20, 30, 40, 50 }, list);
        }

        [Fact]
        public static void GetEnumerator_EmptyList_ReturnsNoElements()
        {
            var skipList = new SkipList<int>();

            var list = skipList.ToList();

            Assert.Empty(list);
        }

        #endregion

        #region CopyTo Tests

        [Fact]
        public static void CopyTo_CopiesElementsInOrder()
        {
            var skipList = new SkipList<int>();
            skipList.Add(30);
            skipList.Add(10);
            skipList.Add(20);

            var array = new int[3];
            skipList.CopyTo(array, 0);

            Assert.Equal(new[] { 10, 20, 30 }, array);
        }

        [Fact]
        public static void CopyTo_NullArray_ThrowsArgumentNullException()
        {
            var skipList = new SkipList<int>();
            skipList.Add(10);

            Assert.Throws<ArgumentNullException>(() => skipList.CopyTo(null, 0));
        }

        #endregion

        #region Bug Regression Tests

        /// <summary>
        /// Bug #137: _getNextLevel() could return 0, causing items to not be added.
        /// Fixed by starting level at 1 instead of 0.
        /// </summary>
        [Fact]
        public static void Bug137_AddedElementIsAlwaysContained()
        {
            // Run multiple times to catch probabilistic failures
            for (int trial = 0; trial < 100; trial++)
            {
                var skipList = new SkipList<int>();
                var random = new Random(trial); // Different seed each trial

                for (int i = 0; i < 10; i++)
                {
                    int value = random.Next(-1000, 1000);
                    skipList.Add(value);
                    Assert.True(skipList.Contains(value), 
                        $"Trial {trial}: Added {value} but Contains returned false");
                }
            }
        }

        /// <summary>
        /// Bug #138: Empty SkipList<int>.Contains(0) should return false.
        /// The sentinel node has default(T) as its value, which is 0 for int.
        /// The Contains method must not match the sentinel.
        /// </summary>
        [Fact]
        public static void Bug138_EmptyIntList_ContainsZero_ReturnsFalse()
        {
            var skipList = new SkipList<int>();

            // Empty list should not contain anything, including 0
            Assert.False(skipList.Contains(0), 
                "Empty SkipList<int> should not contain 0 (sentinel value issue)");
        }

        /// <summary>
        /// Bug #139: Remove(0) on list with negative int should not remove sentinel.
        /// </summary>
        [Fact]
        public static void Bug139_RemoveZeroOnListWithNegatives_DoesNotCorrupt()
        {
            var skipList = new SkipList<int>();
            skipList.Add(-23);
            skipList.Add(-5);
            skipList.Add(-100);

            // Attempt to remove 0 (which was never added)
            var result = skipList.Remove(0);

            // Should return false - 0 was never added
            Assert.False(result, "Remove(0) should return false when 0 was never added");
            
            // List should still be intact
            Assert.Equal(3, skipList.Count);
            Assert.True(skipList.Contains(-23));
            Assert.True(skipList.Contains(-5));
            Assert.True(skipList.Contains(-100));
            
            // Enumeration should work
            var items = skipList.ToList();
            Assert.Equal(3, items.Count);
        }

        #endregion

        #region String Type Tests

        [Fact]
        public static void StringSkipList_AddAndContains_Works()
        {
            var skipList = new SkipList<string>();

            skipList.Add("banana");
            skipList.Add("apple");
            skipList.Add("cherry");

            Assert.Equal(3, skipList.Count);
            Assert.True(skipList.Contains("apple"));
            Assert.True(skipList.Contains("banana"));
            Assert.True(skipList.Contains("cherry"));
        }

        [Fact]
        public static void StringSkipList_EnumeratesInSortedOrder()
        {
            var skipList = new SkipList<string>();
            skipList.Add("banana");
            skipList.Add("apple");
            skipList.Add("cherry");

            var list = skipList.ToList();

            Assert.Equal(new[] { "apple", "banana", "cherry" }, list);
        }

        /// <summary>
        /// Searching for null in a SkipList with reference types.
        /// Currently it returns false because the Find method compares values
        /// and null comparison logic happens to return false.
        /// 
        /// Issue: https://github.com/aalhour/C-Sharp-Algorithms/issues/172
        /// </summary>
        [Fact]
        public static void StringSkipList_ContainsNull_ReturnsFalse()
        {
            var skipList = new SkipList<string>();
            skipList.Add("apple");
            skipList.Add("banana");

            // Contains(null) returns false - null is not found
            var result = skipList.Contains(null);
            Assert.False(result);
        }

        /// <summary>
        /// Adding null to a SkipList of reference types succeeds but may cause
        /// issues since null matches the sentinel node's value.
        /// 
        /// Issue: https://github.com/aalhour/C-Sharp-Algorithms/issues/138
        /// </summary>
        [Fact]
        public static void StringSkipList_AddNull_Succeeds()
        {
            var skipList = new SkipList<string>();

            // Adding null doesn't throw but may cause issues
            skipList.Add(null);
            
            Assert.Equal(1, skipList.Count);
        }

        #endregion
    }
}
