using DataStructures.Trees;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class RedBlackTreeMapTests
    {
        #region Insert Tests

        [Fact]
        public static void Insert_SortedElements_MaintainsBalance()
        {
            var tree = new RedBlackTreeMap<int, string>(allowDuplicates: false);
            var values = CreateTestData(10);

            for (var i = 0; i < values.Length; ++i)
            {
                tree.Insert(values[i].Key, values[i].Value);
            }

            Assert.Equal(values.Length, tree.Count);
            Assert.True(tree.Height < tree.Count, "Tree should be balanced - height should be less than count");
        }

        [Fact]
        public static void Insert_Collection_MaintainsBalance()
        {
            var tree = new RedBlackTreeMap<int, string>(allowDuplicates: false);
            var values = CreateTestData(10);

            tree.Insert(values);

            Assert.True(tree.Height < tree.Count, "Tree should be balanced");
        }

        [Fact]
        public static void Insert_Duplicate_ThrowsException()
        {
            var tree = new RedBlackTreeMap<int, string>(allowDuplicates: false);
            tree.Insert(2, "int2");

            Assert.Throws<InvalidOperationException>(() => tree.Insert(2, "int2 duplicate"));
        }

        [Fact]
        public static void Insert_RandomOrder_MaintainsBalance()
        {
            var tree = new RedBlackTreeMap<int, string>(allowDuplicates: false);

            tree.Insert(4, "int4");
            tree.Insert(5, "int5");
            tree.Insert(7, "int7");
            tree.Insert(2, "int2");
            tree.Insert(1, "int1");
            tree.Insert(3, "int3");
            tree.Insert(6, "int6");
            tree.Insert(8, "int8");
            tree.Insert(10, "int10");
            tree.Insert(9, "int9");

            Assert.Equal(10, tree.Count);
            Assert.True(tree.Height < tree.Count, "Tree should be balanced");
        }

        #endregion

        #region GetInOrderEnumerator Tests

        [Fact]
        public static void GetInOrderEnumerator_ReturnsItemsInOrder()
        {
            var tree = new RedBlackTreeMap<int, string>(allowDuplicates: false);
            var values = CreateTestData(10);
            tree.Insert(values);

            var enumerator = tree.GetInOrderEnumerator();
            var prevKey = int.MinValue;

            while (enumerator.MoveNext())
            {
                Assert.True(enumerator.Current.Key > prevKey, "Items should be in ascending order");
                prevKey = enumerator.Current.Key;
            }
        }

        #endregion

        #region Find Tests

        [Fact]
        public static void Find_ExistingKey_ReturnsCorrectItem()
        {
            var tree = new RedBlackTreeMap<int, string>(allowDuplicates: false);
            tree.Insert(5, "int5");
            tree.Insert(3, "int3");
            tree.Insert(7, "int7");

            var result = tree.Find(5);

            Assert.Equal(5, result.Key);
        }

        [Fact]
        public static void Find_NonExistingKey_ThrowsKeyNotFoundException()
        {
            var tree = new RedBlackTreeMap<int, string>(allowDuplicates: false);
            tree.Insert(1, "one");

            Assert.Throws<KeyNotFoundException>(() => tree.Find(999));
        }

        [Fact]
        public static void FindMin_ReturnsSmallestKey()
        {
            var tree = new RedBlackTreeMap<int, string>(allowDuplicates: false);
            tree.Insert(5, "five");
            tree.Insert(1, "one");
            tree.Insert(10, "ten");

            var min = tree.FindMin();

            Assert.Equal(1, min.Key);
        }

        [Fact]
        public static void FindMax_ReturnsLargestKey()
        {
            var tree = new RedBlackTreeMap<int, string>(allowDuplicates: false);
            tree.Insert(5, "five");
            tree.Insert(1, "one");
            tree.Insert(10, "ten");

            var max = tree.FindMax();

            Assert.Equal(10, max.Key);
        }

        #endregion

        #region Contains Tests

        [Fact]
        public static void Contains_ExistingKey_ReturnsTrue()
        {
            var tree = new RedBlackTreeMap<int, string>(allowDuplicates: false);
            tree.Insert(1, "one");
            tree.Insert(3, "three");

            Assert.True(tree.Contains(1));
            Assert.True(tree.Contains(3));
        }

        [Fact]
        public static void Contains_NonExistingKey_ReturnsFalse()
        {
            var tree = new RedBlackTreeMap<int, string>(allowDuplicates: false);
            tree.Insert(1, "one");

            Assert.False(tree.Contains(999));
        }

        #endregion

        #region Remove Tests

        [Fact]
        public static void Remove_ExistingKeys_DecreasesCount()
        {
            var tree = new RedBlackTreeMap<int, string>(allowDuplicates: false);
            tree.Insert(4, "int4");
            tree.Insert(5, "int5");
            tree.Insert(7, "int7");
            tree.Insert(2, "int2");
            tree.Insert(1, "int1");
            tree.Insert(3, "int3");
            tree.Insert(6, "int6");
            tree.Insert(8, "int8");
            tree.Insert(10, "int10");
            tree.Insert(9, "int9");

            tree.Remove(7);
            tree.Remove(1);
            tree.Remove(3);

            Assert.Equal(7, tree.Count);
            Assert.False(tree.Contains(7));
            Assert.False(tree.Contains(1));
            Assert.False(tree.Contains(3));
        }

        [Fact]
        public static void Remove_RootKey_UpdatesRoot()
        {
            var tree = new RedBlackTreeMap<int, string>(allowDuplicates: false);
            tree.Insert(5, "five");
            tree.Insert(3, "three");
            tree.Insert(7, "seven");
            tree.Insert(2, "two");
            tree.Insert(4, "four");
            tree.Insert(6, "six");

            var oldRootKey = tree.Root.Key;
            tree.Remove(oldRootKey);

            Assert.False(tree.Contains(oldRootKey));
            Assert.Equal(5, tree.Count);
        }

        #endregion

        private static KeyValuePair<int, string>[] CreateTestData(int count)
        {
            var values = new KeyValuePair<int, string>[count];
            for (var i = 1; i <= count; ++i)
            {
                values[i - 1] = new KeyValuePair<int, string>(i, $"Integer: {i}");
            }
            return values;
        }
    }
}
