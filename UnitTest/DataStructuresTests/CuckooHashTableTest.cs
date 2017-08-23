using System;
using System.Diagnostics;

using DataStructures.Dictionaries;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class CuckooHashTableTest
    {
        [Fact]
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
            Assert.True(Ahmad == 10);

            var Oliver = cuckooTable["Oliver"];
            Assert.True(Oliver == 11);

            var Konstantinos = cuckooTable["Konstantinos"];
            Assert.True(Konstantinos == 12);

            var Olympos = cuckooTable["Olympos"];
            Assert.True(Olympos == 13);

            var Bic = cuckooTable["Bic"];
            Assert.True(Bic == 14);

            var Carter = cuckooTable["Carter"];
            Assert.True(Carter == 15);

            var Sameeros = cuckooTable["Sameeros"];
            Assert.True(Sameeros == 16);

            cuckooTable.Clear();
        }
    }
}
