using DataStructures.Trees;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class BinarySearchTreeMapTests
    {
        #region Insert Tests

        [Fact]
        public static void Insert_SingleItems_IncreasesCount()
        {
            var bstMap = new BinarySearchTreeMap<int, string>(allowDuplicates: false);
            var values = CreateTestData(10);

            for (int i = 0; i < values.Length; ++i)
                bstMap.Insert(values[i].Key, values[i].Value);

            Assert.Equal(values.Length, bstMap.Count);
        }

        [Fact]
        public static void Insert_Collection_IncreasesCount()
        {
            var bstMap = new BinarySearchTreeMap<int, string>(allowDuplicates: false);
            var values = CreateTestData(10);

            bstMap.Insert(values);

            Assert.Equal(values.Length, bstMap.Count);
        }

        [Fact]
        public static void Insert_Duplicate_ThrowsException()
        {
            var bstMap = new BinarySearchTreeMap<int, string>(allowDuplicates: false);
            bstMap.Insert(1, "one");
            bstMap.Insert(2, "two");

            Assert.Throws<InvalidOperationException>(() => bstMap.Insert(2, "two again"));
        }

        #endregion

        #region GetInOrderEnumerator Tests

        [Fact]
        public static void GetInOrderEnumerator_ReturnsItemsInOrder()
        {
            var bstMap = new BinarySearchTreeMap<int, string>(allowDuplicates: false);
            var values = CreateTestData(10);
            bstMap.Insert(values);

            var enumerator = bstMap.GetInOrderEnumerator();
            var index = 0;

            while (enumerator.MoveNext())
            {
                Assert.Equal(values[index].Key, enumerator.Current.Key);
                Assert.Equal(values[index].Value, enumerator.Current.Value);
                index++;
            }

            Assert.Equal(values.Length, index);
        }

        #endregion

        #region Find Tests

        [Fact]
        public static void Find_ExistingKey_ReturnsCorrectItem()
        {
            var bstMap = new BinarySearchTreeMap<int, string>(allowDuplicates: false);
            bstMap.Insert(4, "int4");
            bstMap.Insert(5, "int5");
            bstMap.Insert(2, "int2");

            var result = bstMap.Find(5);

            Assert.Equal(5, result.Key);
        }

        [Fact]
        public static void Find_NonExistingKey_ThrowsKeyNotFoundException()
        {
            var bstMap = new BinarySearchTreeMap<int, string>(allowDuplicates: false);
            bstMap.Insert(1, "one");
            bstMap.Insert(2, "two");

            Assert.Throws<KeyNotFoundException>(() => bstMap.Find(999));
        }

        [Fact]
        public static void FindMin_ReturnsSmallestKey()
        {
            var bstMap = new BinarySearchTreeMap<int, string>(allowDuplicates: false);
            bstMap.Insert(5, "five");
            bstMap.Insert(2, "two");
            bstMap.Insert(8, "eight");
            bstMap.Insert(0, "zero");

            var min = bstMap.FindMin();

            Assert.Equal(0, min.Key);
        }

        [Fact]
        public static void FindMax_ReturnsLargestKey()
        {
            var bstMap = new BinarySearchTreeMap<int, string>(allowDuplicates: false);
            bstMap.Insert(5, "five");
            bstMap.Insert(2, "two");
            bstMap.Insert(10, "ten");
            bstMap.Insert(8, "eight");

            var max = bstMap.FindMax();

            Assert.Equal(10, max.Key);
        }

        #endregion

        #region Contains Tests

        [Fact]
        public static void Contains_ExistingKey_ReturnsTrue()
        {
            var bstMap = new BinarySearchTreeMap<int, string>(allowDuplicates: false);
            bstMap.Insert(1, "one");
            bstMap.Insert(3, "three");

            Assert.True(bstMap.Contains(1));
            Assert.True(bstMap.Contains(3));
        }

        [Fact]
        public static void Contains_NonExistingKey_ReturnsFalse()
        {
            var bstMap = new BinarySearchTreeMap<int, string>(allowDuplicates: false);
            bstMap.Insert(1, "one");

            Assert.False(bstMap.Contains(999));
        }

        #endregion

        #region Remove Tests

        [Fact]
        public static void Remove_ExistingKeys_DecreasesCount()
        {
            var bstMap = new BinarySearchTreeMap<int, string>(allowDuplicates: false);
            bstMap.Insert(4, "int4");
            bstMap.Insert(5, "int5");
            bstMap.Insert(7, "int7");
            bstMap.Insert(2, "int2");
            bstMap.Insert(1, "int1");
            bstMap.Insert(3, "int3");

            bstMap.Remove(7);
            bstMap.Remove(1);
            bstMap.Remove(3);

            Assert.Equal(3, bstMap.Count);
            Assert.False(bstMap.Contains(7));
            Assert.False(bstMap.Contains(1));
            Assert.False(bstMap.Contains(3));
        }

        [Fact]
        public static void Remove_RootKey_UpdatesRoot()
        {
            var bstMap = new BinarySearchTreeMap<int, string>(allowDuplicates: false);
            bstMap.Insert(5, "five");
            bstMap.Insert(3, "three");
            bstMap.Insert(7, "seven");

            var oldRootKey = bstMap.Root.Key;
            bstMap.Remove(oldRootKey);

            Assert.False(bstMap.Contains(oldRootKey));
            Assert.Equal(2, bstMap.Count);
        }

        #endregion

        #region Clear Tests

        [Fact]
        public static void Clear_RemovesAllItems()
        {
            var bstMap = new BinarySearchTreeMap<int, string>(allowDuplicates: false);
            bstMap.Insert(1, "one");
            bstMap.Insert(2, "two");
            bstMap.Insert(3, "three");

            bstMap.Clear();

            Assert.Equal(0, bstMap.Count);
        }

        #endregion

        private static KeyValuePair<int, string>[] CreateTestData(int count)
        {
            var values = new KeyValuePair<int, string>[count];
            for (int i = 1; i <= count; ++i)
            {
                values[i - 1] = new KeyValuePair<int, string>(i, $"Integer: {i}");
            }
            return values;
        }
    }
}
