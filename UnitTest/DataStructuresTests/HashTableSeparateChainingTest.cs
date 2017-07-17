using DataStructures.Dictionaries;
using System.Collections.Generic;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class HashTableSeparateChainingTest
    {
        [Fact]
        public static void DoTest()
        {
            var studentsMarks = new ChainedHashTable<string, int>();

            //
            // TEST ADD KEY-VALUE PAIRS
            studentsMarks.Add("Konstantinos", 124);
            studentsMarks.Add("Bic", 224);
            studentsMarks.Add("Z", 324);
            studentsMarks.Add("Ioanna", 424);
            studentsMarks.Add("Mark Zuckerberg", 524);
            studentsMarks.Add("Semsem", 624);
            studentsMarks.Add("Sa3eeed", 724);
            studentsMarks.Add("Sameer", 824);
            studentsMarks.Add("Ahmad", 924);
            studentsMarks.Add("Zeyad", 999);
            studentsMarks.Add("Mahmoood 3eed", 111);
            studentsMarks.Add("Mahmoood Abu 3eed", 222);
            studentsMarks.Add("EISA", 333);
            studentsMarks.Add("Test1", 3210);
            studentsMarks.Add("Test11", 3210);
            studentsMarks.Add("Test222", 3210);
            studentsMarks.Add("Test3333", 3210);
            studentsMarks.Add("Test44444", 3210);
            studentsMarks.Add("Test555555", 3210);
            studentsMarks.Add("Test6666666", 3210);
            studentsMarks.Add("Test77777777", 3210);
            studentsMarks.Add("Test888888888", 3210);

            //
            // TEST FETCH KEY-VALUE
            var mark = studentsMarks["Ahmad"];
            Assert.True(mark == 924);

            mark = studentsMarks["Konstantinos"];
            Assert.True(mark == 124);

            mark = studentsMarks["Bic"];
            Assert.True(mark == 224);

            mark = studentsMarks["Z"];
            Assert.True(mark == 324);

            mark = studentsMarks["Ioanna"];
            Assert.True(mark == 424);

            mark = studentsMarks["Mark Zuckerberg"];
            Assert.True(mark == 524);

            mark = studentsMarks["Semsem"];
            Assert.True(mark == 624);

            mark = studentsMarks["Sa3eeed"];
            Assert.True(mark == 724);

            mark = studentsMarks["Sameer"];
            Assert.True(mark == 824);

            mark = studentsMarks["Zeyad"];
            Assert.True(mark == 999);

            mark = studentsMarks["Mahmoood 3eed"];
            Assert.True(mark == 111);

            mark = studentsMarks["Mahmoood Abu 3eed"];
            Assert.True(mark == 222);

            mark = studentsMarks["EISA"];
            Assert.True(mark == 333);

            //
            // TEST DELETE BY KEYS
            studentsMarks.Remove("Ahmad");
            studentsMarks.Remove("Zeyad");
            studentsMarks.Remove("Bic");
            studentsMarks.Remove("Konstantinos");
            studentsMarks.Remove("Sameer");
            studentsMarks.Remove("Z");
            studentsMarks.Remove("Ioanna");
            studentsMarks.Remove("Mark Zuckerberg");
            studentsMarks.Remove("Semsem");
            studentsMarks.Remove("Sa3eeed");
            studentsMarks.Remove("Test1");
            studentsMarks.Remove("Test11");
            studentsMarks.Remove("Test222");
            studentsMarks.Remove("Test3333");
            studentsMarks.Remove("Test44444");
            studentsMarks.Remove("Test555555");
            studentsMarks.Remove("Test6666666");
            studentsMarks.Remove("Test77777777");
            studentsMarks.Remove("Test888888888");

            Assert.True(studentsMarks.Count == 3);

            KeyValuePair<string, int>[] array = new KeyValuePair<string, int>[studentsMarks.Count];
            studentsMarks.CopyTo(array, 0);

            Assert.True(array.Length == studentsMarks.Count);
        }
    }
}
