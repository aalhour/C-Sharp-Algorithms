using DataStructures.Dictionaries;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class CuckooHashTableTest
    {
        [Fact]
        public static void Add_SingleItem_CanBeRetrieved()
        {
            var table = new CuckooHashTable<string, int>();

            table.Add("Ahmad", 10);

            Assert.Equal(10, table["Ahmad"]);
        }

        [Fact]
        public static void Add_MultipleItems_AllCanBeRetrieved()
        {
            var table = new CuckooHashTable<string, int>();

            table.Add("Ahmad", 10);
            table.Add("Oliver", 11);
            table.Add("Konstantinos", 12);
            table.Add("Olympos", 13);
            table.Add("Bic", 14);
            table.Add("Carter", 15);
            table.Add("Sameeros", 16);

            Assert.Equal(10, table["Ahmad"]);
            Assert.Equal(11, table["Oliver"]);
            Assert.Equal(12, table["Konstantinos"]);
            Assert.Equal(13, table["Olympos"]);
            Assert.Equal(14, table["Bic"]);
            Assert.Equal(15, table["Carter"]);
            Assert.Equal(16, table["Sameeros"]);
        }

        [Fact]
        public static void Clear_RemovesAllItems()
        {
            var table = new CuckooHashTable<string, int>();
            table.Add("key1", 1);
            table.Add("key2", 2);

            table.Clear();

            Assert.Equal(0, table.Count());
        }

        [Fact]
        public static void ContainsKey_ExistingKey_ReturnsTrue()
        {
            var table = new CuckooHashTable<string, int>();
            table.Add("exists", 42);

            Assert.True(table.ContainsKey("exists"));
        }

        [Fact]
        public static void ContainsKey_NonExistingKey_ReturnsFalse()
        {
            var table = new CuckooHashTable<string, int>();
            table.Add("exists", 42);

            Assert.False(table.ContainsKey("does-not-exist"));
        }

        [Fact]
        public static void Count_ReflectsNumberOfItems()
        {
            var table = new CuckooHashTable<string, int>();

            table.Add("a", 1);
            table.Add("b", 2);
            table.Add("c", 3);

            Assert.Equal(3, table.Count());
        }

        [Fact]
        public static void Remove_ExistingKey_RemovesItem()
        {
            var table = new CuckooHashTable<string, int>();
            table.Add("toRemove", 100);

            table.Remove("toRemove");

            Assert.False(table.ContainsKey("toRemove"));
        }
    }
}
