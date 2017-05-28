using System;
using System.Diagnostics;

using DataStructures.Dictionaries;

namespace C_Sharp_Algorithms.DataStructuresTests
{
    public static class CuckooHashTableTest
    {
        public static void DoTest()
        {
            var cuckooTable = new CuckooHashTable<string, int>();

            cuckooTable.Add("Ahmad", 10);
            cuckooTable.Add("Oliver", 11);
            cuckooTable.Add("Konstantinos", 12);
            cuckooTable.Add("Olympos", 13);
            cuckooTable.Add("Bic", 14);
            cuckooTable.Add("Carter", 15);
            cuckooTable.Add("Sameeros", 16);

            var Ahmad = cuckooTable["Ahmad"];
            Debug.Assert(Ahmad == 10);

            var Oliver = cuckooTable["Oliver"];
            Debug.Assert(Oliver == 11);

            var Konstantinos = cuckooTable["Konstantinos"];
            Debug.Assert(Konstantinos == 12);

            var Olympos = cuckooTable["Olympos"];
            Debug.Assert(Olympos == 13);

            var Bic = cuckooTable["Bic"];
            Debug.Assert(Bic == 14);

            var Carter = cuckooTable["Carter"];
            Debug.Assert(Carter == 15);

            var Sameeros = cuckooTable["Sameeros"];
            Debug.Assert(Sameeros == 16);

            cuckooTable.Clear();
        }
    }
}
