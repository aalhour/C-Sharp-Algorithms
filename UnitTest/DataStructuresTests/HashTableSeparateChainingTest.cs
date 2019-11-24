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
        public static void Adding_ThreeDifferentElements_ReturnsSuccessful()
        {
            var studentsMarks = new ChainedHashTable<string, int>();

            studentsMarks.Add("Name1", 1);
            studentsMarks.Add("Name2", 5);
            studentsMarks.Add(new KeyValuePair<string, int>("Name3", 3));

            var mark = studentsMarks["Name1"];
            Assert.True(mark == 1);

            mark = studentsMarks["Name2"];
            Assert.True(mark == 5);

            mark = studentsMarks["Name3"];
            Assert.True(mark == 3);

            Assert.True(studentsMarks.Count == 3);
        }

        [Fact]
        public static void Adding_TwoDuplicateElements_ReturnsException()
        {
            var studentsMarks = new ChainedHashTable<string, int>();
            studentsMarks.Add("Name1", 1);
            studentsMarks.Add("Name2", 5);

            Action act = () => studentsMarks.Add("Name2", 7);
            
            var exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Key already exists in the hash table.", exception.Message);
            Assert.True(studentsMarks.Count == 2);
        }

        [Fact]
        public static void GetElement_ExistingElement_ReturnsElement()
        {
            var studentsMarks = new ChainedHashTable<string, int>();
            studentsMarks.Add("Name1", 1);
            studentsMarks.Add("Name2", 5);

            var value = studentsMarks["Name2"];

            Assert.Equal(5, value);
            Assert.True(studentsMarks.Count == 2);
        }

        [Fact]
        public static void GetElement_NonExistingElement_ReturnsException()
        {
            var studentsMarks = new ChainedHashTable<string, int>();
            studentsMarks.Add("Name1", 1);
            studentsMarks.Add("Name2", 5);

            int value;
            Action act = () => value = studentsMarks["Name3"];

            Assert.Throws<KeyNotFoundException>(act);
            Assert.True(studentsMarks.Count == 2);
        }

        [Fact]
        public static void RemovingOneElement_ThreeDifferentElements_ReturnsSuccessful()
        {
            var studentsMarks = new ChainedHashTable<string, int>();
            studentsMarks.Add("Name1", 1);
            studentsMarks.Add("Name2", 5);
            studentsMarks.Add(new KeyValuePair<string, int>("Name3", 3));

            studentsMarks.Remove("Name2");

            var mark = studentsMarks["Name1"];
            Assert.True(mark == 1);

            mark = studentsMarks["Name3"];
            Assert.True(mark == 3);

            Assert.False(studentsMarks.ContainsKey("Name2"));

            Assert.True(studentsMarks.Count == 2);
        }

        [Fact]
        public static void RemovingAllElement_ThreeDifferentElements_ReturnsSuccessful()
        {
            var studentsMarks = new ChainedHashTable<string, int>();
            studentsMarks.Add("Name1", 1);
            studentsMarks.Add("Name2", 5);
            studentsMarks.Add(new KeyValuePair<string, int>("Name3", 3));

            studentsMarks.Remove("Name2");
            studentsMarks.Remove("Name1");
            studentsMarks.Remove("Name3");

            Assert.True(studentsMarks.Count == 0);
        }

        [Fact]
        public static void CopyTo_FilledHashTable_ReturnsSuccessful()
        {
            var studentsMarks = new ChainedHashTable<string, int>();
            studentsMarks.Add("Name1", 1);
            studentsMarks.Add("Name2", 5);
            studentsMarks.Add(new KeyValuePair<string, int>("Name3", 3));

            var array = new KeyValuePair<string, int>[studentsMarks.Count];
            studentsMarks.CopyTo(array, 0);

            Assert.True(studentsMarks.Count == 3);
            Assert.True(array.Length == 3);
            var arrayKeys = array.Select(x => x.Key).OrderBy(x => x).ToArray();
            Assert.Equal("Name1", arrayKeys[0]);
            Assert.Equal("Name2", arrayKeys[1]);
            Assert.Equal("Name3", arrayKeys[2]);
            var arrayValues = array.Select(x => x.Value).OrderBy(x => x).ToArray();
            Assert.Equal(1, arrayValues[0]);
            Assert.Equal(3, arrayValues[1]);
            Assert.Equal(5, arrayValues[2]);

        }

        [Fact]
        public static void CopyTo_EmptyHashTable_ReturnsSuccessful()
        {
            var studentsMarks = new ChainedHashTable<string, int>();
            studentsMarks.Add("Name1", 1);
            studentsMarks.Add("Name2", 5);
            studentsMarks.Add(new KeyValuePair<string, int>("Name3", 3));

            studentsMarks.Remove("Name2");
            studentsMarks.Remove("Name1");
            studentsMarks.Remove("Name3");

            Assert.True(studentsMarks.Count == 0);
            var array = new KeyValuePair<string, int>[studentsMarks.Count];
            studentsMarks.CopyTo(array, 0);
        }
    }
}
