using DataStructures.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class OpenScatterHashTableTest
    {
        #region Constructor Tests

        [Fact]
        public static void Constructor_Default_CreatesEmptyTable()
        {
            var hashTable = new OpenScatterHashTable<int, string>();

            Assert.Empty(hashTable);
        }

        [Fact]
        public static void Constructor_WithCapacity_CreatesEmptyTable()
        {
            var hashTable = new OpenScatterHashTable<int, string>(100);

            Assert.Empty(hashTable);
        }

        [Fact]
        public static void Constructor_NegativeCapacity_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new OpenScatterHashTable<int, string>(-1));
        }

        #endregion

        #region Add Tests

        [Fact]
        public static void Add_SingleItem_IncreasesCount()
        {
            var hashTable = new OpenScatterHashTable<int, string>();

            hashTable.Add(1, "one");

            Assert.Single(hashTable);
            Assert.True(hashTable.ContainsKey(1));
        }

        [Fact]
        public static void Add_MultipleItems_IncreasesCount()
        {
            var hashTable = new OpenScatterHashTable<int, string>();

            hashTable.Add(1, "one");
            hashTable.Add(2, "two");
            hashTable.Add(3, "three");

            Assert.Equal(3, hashTable.Count);
        }

        [Fact]
        public static void Add_DuplicateKey_ThrowsArgumentException()
        {
            var hashTable = new OpenScatterHashTable<int, string>();
            hashTable.Add(1, "one");

            Assert.Throws<ArgumentException>(() => hashTable.Add(1, "duplicate"));
        }

        [Fact]
        public static void Add_NullKey_ThrowsArgumentNullException()
        {
            var hashTable = new OpenScatterHashTable<string, string>();

            Assert.Throws<ArgumentNullException>(() => hashTable.Add(null, "value"));
        }

        [Fact]
        public static void Add_ManyItems_TriggersResize()
        {
            var hashTable = new OpenScatterHashTable<int, string>();

            for (int i = 0; i < 100; i++)
            {
                hashTable.Add(i, $"value{i}");
            }

            Assert.Equal(100, hashTable.Count);

            // Verify all items are retrievable
            for (int i = 0; i < 100; i++)
            {
                Assert.True(hashTable.ContainsKey(i));
                Assert.Equal($"value{i}", hashTable[i]);
            }
        }

        [Fact]
        public static void Add_KeyValuePair_Works()
        {
            var hashTable = new OpenScatterHashTable<int, string>();

            hashTable.Add(new KeyValuePair<int, string>(1, "one"));

            Assert.Single(hashTable);
            Assert.Equal("one", hashTable[1]);
        }

        #endregion

        #region Indexer Tests

        [Fact]
        public static void Indexer_Get_ReturnsCorrectValue()
        {
            var hashTable = new OpenScatterHashTable<int, string>();
            hashTable.Add(42, "answer");

            Assert.Equal("answer", hashTable[42]);
        }

        [Fact]
        public static void Indexer_Get_NonExistingKey_ThrowsKeyNotFoundException()
        {
            var hashTable = new OpenScatterHashTable<int, string>();

            Assert.Throws<KeyNotFoundException>(() => { var x = hashTable[99]; });
        }

        [Fact]
        public static void Indexer_Set_NewKey_AddsItem()
        {
            var hashTable = new OpenScatterHashTable<int, string>();

            hashTable[1] = "one";

            Assert.Single(hashTable);
            Assert.Equal("one", hashTable[1]);
        }

        [Fact]
        public static void Indexer_Set_ExistingKey_UpdatesValue()
        {
            var hashTable = new OpenScatterHashTable<int, string>();
            hashTable.Add(1, "one");

            hashTable[1] = "ONE";

            Assert.Single(hashTable);
            Assert.Equal("ONE", hashTable[1]);
        }

        #endregion

        #region ContainsKey Tests

        [Fact]
        public static void ContainsKey_ExistingKey_ReturnsTrue()
        {
            var hashTable = new OpenScatterHashTable<string, int>();
            hashTable.Add("key", 123);

            Assert.True(hashTable.ContainsKey("key"));
        }

        [Fact]
        public static void ContainsKey_NonExistingKey_ReturnsFalse()
        {
            var hashTable = new OpenScatterHashTable<string, int>();
            hashTable.Add("key", 123);

            Assert.False(hashTable.ContainsKey("other"));
        }

        [Fact]
        public static void ContainsKey_NullKey_ThrowsArgumentNullException()
        {
            var hashTable = new OpenScatterHashTable<string, int>();

            Assert.Throws<ArgumentNullException>(() => hashTable.ContainsKey(null));
        }

        #endregion

        #region TryGetValue Tests

        [Fact]
        public static void TryGetValue_ExistingKey_ReturnsTrueAndValue()
        {
            var hashTable = new OpenScatterHashTable<int, string>();
            hashTable.Add(1, "one");

            bool found = hashTable.TryGetValue(1, out string value);

            Assert.True(found);
            Assert.Equal("one", value);
        }

        [Fact]
        public static void TryGetValue_NonExistingKey_ReturnsFalseAndDefault()
        {
            var hashTable = new OpenScatterHashTable<int, string>();

            bool found = hashTable.TryGetValue(99, out string value);

            Assert.False(found);
            Assert.Null(value);
        }

        #endregion

        #region Remove Tests

        [Fact]
        public static void Remove_ExistingKey_ReturnsTrueAndDecreasesCount()
        {
            var hashTable = new OpenScatterHashTable<int, string>();
            hashTable.Add(1, "one");
            hashTable.Add(2, "two");

            bool removed = hashTable.Remove(1);

            Assert.True(removed);
            Assert.Single(hashTable);
            Assert.False(hashTable.ContainsKey(1));
            Assert.True(hashTable.ContainsKey(2));
        }

        [Fact]
        public static void Remove_NonExistingKey_ReturnsFalse()
        {
            var hashTable = new OpenScatterHashTable<int, string>();
            hashTable.Add(1, "one");

            bool removed = hashTable.Remove(99);

            Assert.False(removed);
            Assert.Single(hashTable);
        }

        [Fact]
        public static void Remove_KeyValuePair_OnlyRemovesIfBothMatch()
        {
            var hashTable = new OpenScatterHashTable<int, string>();
            hashTable.Add(1, "one");

            // Wrong value - should not remove
            bool removed1 = hashTable.Remove(new KeyValuePair<int, string>(1, "wrong"));
            Assert.False(removed1);
            Assert.Single(hashTable);

            // Correct key and value - should remove
            bool removed2 = hashTable.Remove(new KeyValuePair<int, string>(1, "one"));
            Assert.True(removed2);
            Assert.Empty(hashTable);
        }

        [Fact]
        public static void Remove_ThenAddSameKey_Works()
        {
            var hashTable = new OpenScatterHashTable<int, string>();
            hashTable.Add(1, "one");
            hashTable.Remove(1);

            hashTable.Add(1, "new one");

            Assert.Single(hashTable);
            Assert.Equal("new one", hashTable[1]);
        }

        #endregion

        #region Clear Tests

        [Fact]
        public static void Clear_RemovesAllItems()
        {
            var hashTable = new OpenScatterHashTable<int, string>();
            hashTable.Add(1, "one");
            hashTable.Add(2, "two");
            hashTable.Add(3, "three");

            hashTable.Clear();

            Assert.Empty(hashTable);
            Assert.False(hashTable.ContainsKey(1));
        }

        #endregion

        #region Keys and Values Tests

        [Fact]
        public static void Keys_ReturnsAllKeys()
        {
            var hashTable = new OpenScatterHashTable<int, string>();
            hashTable.Add(1, "one");
            hashTable.Add(2, "two");
            hashTable.Add(3, "three");

            var keys = hashTable.Keys;

            Assert.Equal(3, keys.Count);
            Assert.Contains(1, keys);
            Assert.Contains(2, keys);
            Assert.Contains(3, keys);
        }

        [Fact]
        public static void Values_ReturnsAllValues()
        {
            var hashTable = new OpenScatterHashTable<int, string>();
            hashTable.Add(1, "one");
            hashTable.Add(2, "two");
            hashTable.Add(3, "three");

            var values = hashTable.Values;

            Assert.Equal(3, values.Count);
            Assert.Contains("one", values);
            Assert.Contains("two", values);
            Assert.Contains("three", values);
        }

        #endregion

        #region Contains Tests

        [Fact]
        public static void Contains_ExistingPair_ReturnsTrue()
        {
            var hashTable = new OpenScatterHashTable<int, string>();
            hashTable.Add(1, "one");

            Assert.Contains(new KeyValuePair<int, string>(1, "one"), hashTable);
        }

        [Fact]
        public static void Contains_WrongValue_ReturnsFalse()
        {
            var hashTable = new OpenScatterHashTable<int, string>();
            hashTable.Add(1, "one");

            Assert.DoesNotContain(new KeyValuePair<int, string>(1, "wrong"), hashTable);
        }

        [Fact]
        public static void Contains_NonExistingKey_ReturnsFalse()
        {
            var hashTable = new OpenScatterHashTable<int, string>();
            hashTable.Add(1, "one");

            Assert.DoesNotContain(new KeyValuePair<int, string>(99, "whatever"), hashTable);
        }

        #endregion

        #region CopyTo Tests

        [Fact]
        public static void CopyTo_CopiesAllItems()
        {
            var hashTable = new OpenScatterHashTable<int, string>();
            hashTable.Add(1, "one");
            hashTable.Add(2, "two");

            var array = new KeyValuePair<int, string>[2];
            hashTable.CopyTo(array, 0);

            Assert.Equal(2, array.Length);
            Assert.Contains(new KeyValuePair<int, string>(1, "one"), array);
            Assert.Contains(new KeyValuePair<int, string>(2, "two"), array);
        }

        [Fact]
        public static void CopyTo_NullArray_ThrowsArgumentNullException()
        {
            var hashTable = new OpenScatterHashTable<int, string>();

            Assert.Throws<ArgumentNullException>(() => hashTable.CopyTo(null, 0));
        }

        [Fact]
        public static void CopyTo_NegativeIndex_ThrowsArgumentOutOfRangeException()
        {
            var hashTable = new OpenScatterHashTable<int, string>();
            var array = new KeyValuePair<int, string>[1];

            Assert.Throws<ArgumentOutOfRangeException>(() => hashTable.CopyTo(array, -1));
        }

        [Fact]
        public static void CopyTo_ArrayTooSmall_ThrowsArgumentException()
        {
            var hashTable = new OpenScatterHashTable<int, string>();
            hashTable.Add(1, "one");
            hashTable.Add(2, "two");

            var array = new KeyValuePair<int, string>[1];

            Assert.Throws<ArgumentException>(() => hashTable.CopyTo(array, 0));
        }

        #endregion

        #region Enumeration Tests

        [Fact]
        public static void GetEnumerator_EnumeratesAllItems()
        {
            var hashTable = new OpenScatterHashTable<int, string>();
            hashTable.Add(1, "one");
            hashTable.Add(2, "two");
            hashTable.Add(3, "three");

            var items = new List<KeyValuePair<int, string>>();
            foreach (var item in hashTable)
            {
                items.Add(item);
            }

            Assert.Equal(3, items.Count);
            Assert.Contains(new KeyValuePair<int, string>(1, "one"), items);
            Assert.Contains(new KeyValuePair<int, string>(2, "two"), items);
            Assert.Contains(new KeyValuePair<int, string>(3, "three"), items);
        }

        [Fact]
        public static void GetEnumerator_EmptyTable_EnumeratesNothing()
        {
            var hashTable = new OpenScatterHashTable<int, string>();

            var items = hashTable.ToList();

            Assert.Empty(items);
        }

        #endregion

        #region IsReadOnly Test

        [Fact]
        public static void IsReadOnly_ReturnsFalse()
        {
            var hashTable = new OpenScatterHashTable<int, string>();

            Assert.False(hashTable.IsReadOnly);
        }

        #endregion

        #region Collision Handling Tests

        [Fact]
        public static void Add_ItemsWithSameHashBucket_HandlesCollisions()
        {
            // This tests that linear probing works correctly
            var hashTable = new OpenScatterHashTable<int, string>();

            // Add many items to force collisions (small initial capacity)
            for (int i = 0; i < 50; i++)
            {
                hashTable.Add(i, $"value{i}");
            }

            // Verify all items are stored and retrievable
            for (int i = 0; i < 50; i++)
            {
                Assert.True(hashTable.ContainsKey(i), $"Key {i} should exist");
                Assert.Equal($"value{i}", hashTable[i]);
            }
        }

        [Fact]
        public static void Remove_ItemsWithCollisions_StillFindsOtherItems()
        {
            var hashTable = new OpenScatterHashTable<int, string>();

            for (int i = 0; i < 20; i++)
            {
                hashTable.Add(i, $"value{i}");
            }

            // Remove some items
            hashTable.Remove(5);
            hashTable.Remove(10);
            hashTable.Remove(15);

            // Verify remaining items are still accessible
            Assert.Equal(17, hashTable.Count);
            Assert.False(hashTable.ContainsKey(5));
            Assert.False(hashTable.ContainsKey(10));
            Assert.False(hashTable.ContainsKey(15));

            for (int i = 0; i < 20; i++)
            {
                if (i == 5 || i == 10 || i == 15)
                    continue;
                Assert.True(hashTable.ContainsKey(i), $"Key {i} should still exist");
                Assert.Equal($"value{i}", hashTable[i]);
            }
        }

        #endregion

        #region String Key Tests

        [Fact]
        public static void StringKeys_WorkCorrectly()
        {
            var hashTable = new OpenScatterHashTable<string, int>();

            hashTable.Add("apple", 1);
            hashTable.Add("banana", 2);
            hashTable.Add("cherry", 3);

            Assert.Equal(3, hashTable.Count);
            Assert.Equal(1, hashTable["apple"]);
            Assert.Equal(2, hashTable["banana"]);
            Assert.Equal(3, hashTable["cherry"]);
        }

        #endregion
    }
}
