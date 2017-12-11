using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class SortedDictionaryTests
    {
        [Fact]
        public static void DoTest()
        {
            var sortedDict = new DataStructures.SortedCollections.SortedDictionary<string, int>();

            string[] keys = new string[13] {
                "A", "B", "C", "D", "E", "ABC", "Ahmad", "Bic",
                "Carter", "Konstantinos", "Olympos", "Tareq", "Ziad"
            };

            int[] values = new int[13] {
                26, 27, 28, 29, 30, 40, 10, 11,
                12, 13, 14, 15, 16
            };

            //
            // Test Add
            for (int i = 0; i < 13; ++i)
            {
                // insert
                sortedDict.Add(keys[i], values[i]);
            }

            //
            // Assert correct number of elements
            Assert.True(sortedDict.Count == 13, "Wrong number of elements in dictionary.");

            //
            // Test get via index-access notation
            Assert.True(sortedDict["A"] == 26);
            Assert.True(sortedDict["B"] == 27);
            Assert.True(sortedDict["C"] == 28);
            Assert.True(sortedDict["D"] == 29);
            Assert.True(sortedDict["E"] == 30);
            Assert.True(sortedDict["ABC"] == 40);
            Assert.True(sortedDict["Ahmad"] == 10);
            Assert.True(sortedDict["Bic"] == 11);
            Assert.True(sortedDict["Carter"] == 12);
            Assert.True(sortedDict["Konstantinos"] == 13);
            Assert.True(sortedDict["Olympos"] == 14);
            Assert.True(sortedDict["Tareq"] == 15);
            Assert.True(sortedDict["Ziad"] == 16);

            //
            // Test update
            int bak1 = sortedDict["Ahmad"];
            int bak2 = sortedDict["ABC"];
            sortedDict["Ahmad"] = 100;
            sortedDict["ABC"] = 200;

            Assert.True(sortedDict["ABC"] == 200, "Expcted ABC to be set to 200.");
            Assert.True(sortedDict["Ahmad"] == 100, "Expected Ahmad to be set to 100.");

            // Restore
            sortedDict["Ahmad"] = bak1;
            sortedDict["ABC"] = bak2;

            //
            // Test TryGetValue for existing items
            int existingItemKeyValue;
            var tryGetStatus = sortedDict.TryGetValue("Ziad", out existingItemKeyValue);
            Assert.True(tryGetStatus, "Expected the TryGet returned status to be true.");
            Assert.True(existingItemKeyValue == 16, "Expected Ziad to be set to 16.");

            //
            // Test TryGetValue for non-existing items
            int nonExistingItemKeyValue;
            tryGetStatus = sortedDict.TryGetValue("SomeNonExistentKey", out nonExistingItemKeyValue);
            Assert.False(tryGetStatus, "Expected the TryGet returned status to be false.");
            Assert.True(existingItemKeyValue == 16, "Expected the returned value for a non-existent key to be 0.");

            //
            // Test Remove
            var previousCount = sortedDict.Count;
            var removeStatus = sortedDict.Remove("Ziad");
            Assert.True(removeStatus, "Expected removeStatus to be true.");
            Assert.False(sortedDict.ContainsKey("Ziad"), "Expected Ziad to be removed.");
            Assert.True(sortedDict.Count == previousCount - 1, "Expected Count to decrease after Remove operation.");

            //
            // Test CopyTo returns a sorted array of key-value pairs (sorted by key).
            var array = new KeyValuePair<string, int>[sortedDict.Count];
            sortedDict.CopyTo(array, 0);

            // Prepare the sort testing data
            var keyValuePairsList = new List<KeyValuePair<string, int>>(sortedDict.Count);
            for (int i = 0; i < sortedDict.Count; ++i)
            {
                if (keys[i] == "Ziad") // deleted previously from sortedDictionary
                    continue;
                keyValuePairsList.Add(new KeyValuePair<string, int>(keys[i], values[i]));
            }

            // Sort dictionary
            keyValuePairsList = keyValuePairsList.OrderBy(item => item.Key, new KeyComparer()).ToList();

            // begin sorting test
            for (int i = 0; i < sortedDict.Count; i++)
            {
                // Keys
                string key1 = array[i].Key;
                string key2 = keyValuePairsList[i].Key;

                // Values
                int val1 = array[i].Value;
                int val2 = keyValuePairsList[i].Value;

                Assert.True(key1.Equals(key2, System.StringComparison.Ordinal), "Unmatched order of items!");
                Assert.Equal(val1, val2);
            }

            //
            // Test Clear
            sortedDict.Clear();
            Assert.True(sortedDict.Count == 0, "Expected sortedDict to be empty!");
        }


        private class KeyComparer : IComparer<string>
        {
            public int Compare(string x, string y) => string.CompareOrdinal(x, y);
        }
    }

}

