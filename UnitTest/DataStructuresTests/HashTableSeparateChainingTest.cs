using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.Dictionaries;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class HashTableSeparateChainingTest
    {
        [Fact]
        public static void Add_ThreeElements_AllCanBeRetrieved()
        {
            var table = new ChainedHashTable<string, int>();

            table.Add("Name1", 1);
            table.Add("Name2", 5);
            table.Add(new KeyValuePair<string, int>("Name3", 3));

            Assert.Equal(1, table["Name1"]);
            Assert.Equal(5, table["Name2"]);
            Assert.Equal(3, table["Name3"]);
            Assert.Equal(3, table.Count);
        }

        [Fact]
        public static void Add_DuplicateKey_ThrowsArgumentException()
        {
            var table = new ChainedHashTable<string, int>();
            table.Add("Name1", 1);
            table.Add("Name2", 5);

            var exception = Assert.Throws<ArgumentException>(() => table.Add("Name2", 7));

            Assert.Equal("Key already exists in the hash table.", exception.Message);
            Assert.Equal(2, table.Count);
        }

        [Fact]
        public static void Indexer_ExistingKey_ReturnsValue()
        {
            var table = new ChainedHashTable<string, int>();
            table.Add("Name1", 1);
            table.Add("Name2", 5);

            Assert.Equal(5, table["Name2"]);
            Assert.Equal(2, table.Count);
        }

        [Fact]
        public static void Indexer_NonExistingKey_ThrowsKeyNotFoundException()
        {
            var table = new ChainedHashTable<string, int>();
            table.Add("Name1", 1);
            table.Add("Name2", 5);

            Assert.Throws<KeyNotFoundException>(() => _ = table["Name3"]);
            Assert.Equal(2, table.Count);
        }

        [Fact]
        public static void Remove_ExistingKey_RemovesItem()
        {
            var table = new ChainedHashTable<string, int>();
            table.Add("Name1", 1);
            table.Add("Name2", 5);
            table.Add("Name3", 3);

            table.Remove("Name2");

            Assert.Equal(1, table["Name1"]);
            Assert.Equal(3, table["Name3"]);
            Assert.False(table.ContainsKey("Name2"));
            Assert.Equal(2, table.Count);
        }

        [Fact]
        public static void Remove_AllKeys_EmptiesTable()
        {
            var table = new ChainedHashTable<string, int>();
            table.Add("Name1", 1);
            table.Add("Name2", 5);
            table.Add("Name3", 3);

            table.Remove("Name2");
            table.Remove("Name1");
            table.Remove("Name3");

            Assert.Empty(table);
        }

        [Fact]
        public static void CopyTo_FilledTable_CopiesAllItems()
        {
            var table = new ChainedHashTable<string, int>();
            table.Add("Name1", 1);
            table.Add("Name2", 5);
            table.Add("Name3", 3);

            var array = new KeyValuePair<string, int>[table.Count];
            table.CopyTo(array, 0);

            Assert.Equal(3, table.Count);
            Assert.Equal(3, array.Length);

            var keys = array.Select(x => x.Key).OrderBy(x => x).ToArray();
            Assert.Equal("Name1", keys[0]);
            Assert.Equal("Name2", keys[1]);
            Assert.Equal("Name3", keys[2]);

            var values = array.Select(x => x.Value).OrderBy(x => x).ToArray();
            Assert.Equal(1, values[0]);
            Assert.Equal(3, values[1]);
            Assert.Equal(5, values[2]);
        }

        [Fact]
        public static void CopyTo_EmptyTable_CopiesNothing()
        {
            var table = new ChainedHashTable<string, int>();
            table.Add("Name1", 1);
            table.Add("Name2", 5);
            table.Add("Name3", 3);

            table.Remove("Name2");
            table.Remove("Name1");
            table.Remove("Name3");

            Assert.Empty(table);
            var array = new KeyValuePair<string, int>[table.Count];
            table.CopyTo(array, 0);
            
            Assert.Empty(array);
        }

        [Fact]
        public static void ContainsKey_ExistingKey_ReturnsTrue()
        {
            var table = new ChainedHashTable<string, int>();
            table.Add("exists", 42);

            Assert.True(table.ContainsKey("exists"));
        }

        [Fact]
        public static void ContainsKey_NonExistingKey_ReturnsFalse()
        {
            var table = new ChainedHashTable<string, int>();
            table.Add("exists", 42);

            Assert.False(table.ContainsKey("does-not-exist"));
        }
    }
}
