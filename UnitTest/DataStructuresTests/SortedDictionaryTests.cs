using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class SortedDictionaryTests
    {
        #region Add Tests

        [Fact]
        public static void Add_MultipleItems_SetsCorrectCount()
        {
            var sortedDict = new DataStructures.SortedCollections.SortedDictionary<string, int>();

            sortedDict.Add("A", 26);
            sortedDict.Add("B", 27);
            sortedDict.Add("C", 28);

            Assert.Equal(3, sortedDict.Count);
        }

        #endregion

        #region Indexer Get Tests

        [Fact]
        public static void Indexer_Get_ReturnsCorrectValues()
        {
            var sortedDict = CreateTestDictionary();

            Assert.Equal(26, sortedDict["A"]);
            Assert.Equal(27, sortedDict["B"]);
            Assert.Equal(10, sortedDict["Ahmad"]);
            Assert.Equal(13, sortedDict["Konstantinos"]);
        }

        #endregion

        #region Indexer Set Tests

        [Fact]
        public static void Indexer_Set_UpdatesValue()
        {
            var sortedDict = CreateTestDictionary();

            sortedDict["Ahmad"] = 100;
            sortedDict["ABC"] = 200;

            Assert.Equal(100, sortedDict["Ahmad"]);
            Assert.Equal(200, sortedDict["ABC"]);
        }

        #endregion

        #region TryGetValue Tests

        [Fact]
        public static void TryGetValue_ExistingKey_ReturnsTrueAndValue()
        {
            var sortedDict = CreateTestDictionary();

            var result = sortedDict.TryGetValue("Ziad", out int value);

            Assert.True(result);
            Assert.Equal(16, value);
        }

        [Fact]
        public static void TryGetValue_NonExistingKey_ReturnsFalse()
        {
            var sortedDict = CreateTestDictionary();

            var result = sortedDict.TryGetValue("NonExistent", out int _);

            Assert.False(result);
        }

        #endregion

        #region Remove Tests

        [Fact]
        public static void Remove_ExistingKey_ReturnsTrueAndDecreasesCount()
        {
            var sortedDict = CreateTestDictionary();
            var previousCount = sortedDict.Count;

            var result = sortedDict.Remove("Ziad");

            Assert.True(result);
            Assert.False(sortedDict.ContainsKey("Ziad"));
            Assert.Equal(previousCount - 1, sortedDict.Count);
        }

        [Fact]
        public static void Remove_NonExistingKey_ReturnsFalse()
        {
            var sortedDict = CreateTestDictionary();

            var result = sortedDict.Remove("NonExistent");

            Assert.False(result);
        }

        #endregion

        #region ContainsKey Tests

        [Fact]
        public static void ContainsKey_ExistingKey_ReturnsTrue()
        {
            var sortedDict = CreateTestDictionary();

            Assert.True(sortedDict.ContainsKey("Ahmad"));
            Assert.True(sortedDict.ContainsKey("Ziad"));
        }

        [Fact]
        public static void ContainsKey_NonExistingKey_ReturnsFalse()
        {
            var sortedDict = CreateTestDictionary();

            Assert.False(sortedDict.ContainsKey("NonExistent"));
        }

        #endregion

        #region CopyTo Tests

        [Fact]
        public static void CopyTo_ReturnsSortedArray()
        {
            var sortedDict = CreateTestDictionary();
            var array = new KeyValuePair<string, int>[sortedDict.Count];

            sortedDict.CopyTo(array, 0);

            // Verify array is sorted by key
            for (int i = 0; i < array.Length - 1; i++)
            {
                Assert.True(string.CompareOrdinal(array[i].Key, array[i + 1].Key) < 0,
                    $"Array not sorted: {array[i].Key} should come before {array[i + 1].Key}");
            }
        }

        #endregion

        #region Clear Tests

        [Fact]
        public static void Clear_RemovesAllItems()
        {
            var sortedDict = CreateTestDictionary();

            sortedDict.Clear();

            Assert.Equal(0, sortedDict.Count);
        }

        #endregion

        private static DataStructures.SortedCollections.SortedDictionary<string, int> CreateTestDictionary()
        {
            var sortedDict = new DataStructures.SortedCollections.SortedDictionary<string, int>();

            string[] keys = new string[] {
                "A", "B", "C", "D", "E", "ABC", "Ahmad", "Bic",
                "Carter", "Konstantinos", "Olympos", "Tareq", "Ziad"
            };

            int[] values = new int[] {
                26, 27, 28, 29, 30, 40, 10, 11,
                12, 13, 14, 15, 16
            };

            for (int i = 0; i < keys.Length; ++i)
            {
                sortedDict.Add(keys[i], values[i]);
            }

            return sortedDict;
        }
    }
}
