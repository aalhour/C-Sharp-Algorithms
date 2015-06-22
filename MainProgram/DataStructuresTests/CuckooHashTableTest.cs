using System;

using DataStructures.Dictionaries;

namespace C_Sharp_Algorithms.DataStructuresTests
{
    public static class CuckooHashTableTest
    {
        public static void DoTest()
        {
            var cuckooTable = new CuckooHashTable<string, int>();

            cuckooTable.Insert("Ahmad", 15);
            cuckooTable.Insert("Oliver", 15);
            cuckooTable.Insert("Konstantinos", 15);
            cuckooTable.Insert("Olympos", 15);
            cuckooTable.Insert("Bic", 15);
            cuckooTable.Insert("Carter", 15);
            cuckooTable.Insert("Sameeros", 15);
        }
    }
}
