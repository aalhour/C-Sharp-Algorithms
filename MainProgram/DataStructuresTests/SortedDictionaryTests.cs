using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

using DataStructures.SortedCollections;

namespace C_Sharp_Algorithms.DataStructuresTests
{
    public static class SortedDictionaryTests
    {
        public static void DoTest()
        {
            var sortedDict = new DataStructures.SortedCollections.SortedDictionary<string, int>();

            string[] keys = new string[13] {
                "A", "B", "C", "D", "E", "ABC", "Ahmad", "Bic", 
                "Carter", "Konstantinos", "Olympos", "Tareq", "Ziad"
            };

            int[] values = new int[13] { 26, 27, 28, 29, 30, 40, 10, 11, 12, 13, 14, 15, 16};


            //
            // Test Add
            for (int i = 0; i < 13; ++i)
            {
                // insert
                sortedDict.Add(keys[i], values[i]);
            }


            //
            // Assert correct number of elements
            Debug.Assert(sortedDict.Count == 13, "Wrong number of elements in dictionary.");


            //
            // Test get via index-access notation
            Debug.Assert(sortedDict["Ahmad"] == 10);
            Debug.Assert(sortedDict["Tareq"] == 15);
            Debug.Assert(sortedDict["Ziad"] == 16);
            Debug.Assert(sortedDict["Konstantinos"] == 13);
            Debug.Assert(sortedDict["Olympos"] == 14);
            Debug.Assert(sortedDict["Bic"] == 12);
            Debug.Assert(sortedDict["Carter"] == 13);
            Debug.Assert(sortedDict["A"] == 26);
            Debug.Assert(sortedDict["B"] == 27);
            Debug.Assert(sortedDict["C"] == 28);
            Debug.Assert(sortedDict["D"] == 29);
            Debug.Assert(sortedDict["E"] == 30);
            Debug.Assert(sortedDict["ABC"] == 40);


            //
            // Test update
            sortedDict["Ahmad"] = 100;
            sortedDict["ABC"] = 200;

            Debug.Assert(sortedDict["ABC"] == 200, "Expcted ABC to be set to 200.");
            Debug.Assert(sortedDict["Ahmad"] == 100, "Expected Ahmad to be set to 100.");


            //
            // Test TryGetValue for existing items
            int existingItemKeyValue;
            var tryGetStatus = sortedDict.TryGetValue("Ziad", out existingItemKeyValue);
            Debug.Assert(tryGetStatus == true, "Expected the TryGet returned status to be true.");
            Debug.Assert(existingItemKeyValue == 16, "Expected Ziad to be set to 16.");


            //
            // Test TryGetValue for non-existing items
            int nonExistingItemKeyValue;
            tryGetStatus = sortedDict.TryGetValue("SomeNonExistentKey", out nonExistingItemKeyValue);
            Debug.Assert(tryGetStatus == false, "Expected the TryGet returned status to be false.");
            Debug.Assert(existingItemKeyValue == 16, "Expected the returned value for a non-existent key to be 0.");


            //
            // Test Remove
            var previousCount = sortedDict.Count;
            var removeStatus = sortedDict.Remove("Ziad");
            Debug.Assert(removeStatus == true, "Expected removeStatus to be true.");
            Debug.Assert(sortedDict.ContainsKey("Ziad") == false, "Expected Ziad to be removed.");
            Debug.Assert(sortedDict.Count == previousCount - 1, "Expected Count to decrease after Remove operation.");


            //
            // Test CopyTo returns a sorted array of key-value pairs (sorted by key).
            var array = new KeyValuePair<string, int>[sortedDict.Count];
            sortedDict.CopyTo(array, 0);

            // Prepare the sort testing data
            var keyValuePairsList = new List<KeyValuePair<string, int>>(sortedDict.Count);
            for (int i = 0; i < sortedDict.Count; ++i)
            {
                if(keys[i] == "Ziad") // deleted previously from sortedDictionary
                    continue;
                
                keyValuePairsList.Add(new KeyValuePair<string, int>(keys[i], values[i]));
            }
            keyValuePairsList.OrderBy(item => item.Key);

            // begin sorting test
            for (int i = 0; i < sortedDict.Count; i++)
                Debug.Assert(array[i].Key == keyValuePairsList[i].Key && array[i].Value == keyValuePairsList[i].Value, "Unmatched order of items!");


            //
            // Test Clear
            sortedDict.Clear();
            Debug.Assert(sortedDict.Count == 0, "Expected sortedDict to be empty!");
        }

    }

}

