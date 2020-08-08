using System.Runtime.InteropServices.ComTypes;
using DataStructures.Lists;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class SkipListTest
    {
        [Fact]
        public static void Initialization_ListIsEmpty()
        {
            var skipList = new SkipList<int>();

            Assert.True(skipList.Count == 0);
            Assert.True(skipList.IsEmpty);
            Assert.DoesNotContain(0, skipList);
        }

        [Fact]
        public static void Add_NullElement_ListContainNullElement()
        {
            var skipList = new SkipList<string>();

            skipList.Add(null);

            Assert.True(skipList.Count == 1);
            Assert.Contains(null, skipList);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(-10)]
        public static void Add_OneElement_ListContainOneElement(int testValue)
        {
            var skipList = new SkipList<int>();

            skipList.Add(testValue);

            Assert.True(skipList.Count == 1);
            Assert.Contains(testValue, skipList);
        }

        [Fact]
        public static void Add_OneElementTwice_ListContainOneElement()
        {
            var skipList = new SkipList<int>();

            skipList.Add(10);
            skipList.Add(10);

            Assert.True(skipList.Count == 1);
            Assert.Contains(10, skipList);
        }

        [Fact]
        public static void Add_DefaultElement_ListContainOneElement()
        {
            var skipList = new SkipList<int>();

            skipList.Add(default(int));

            Assert.True(skipList.Count == 1);
            Assert.Contains(0, skipList);
        }

        [Fact]
        public static void Add_SomeElements_ListContainTheseElements()
        {
            var skipList = new SkipList<int>();
            skipList.Add(0);
            skipList.Add(-1);
            skipList.Add(1);
            skipList.Add(2);

            Assert.True(skipList.Count == 4);
            int checkValue = -1;
            foreach(var item in skipList)
            {
                Assert.Equal(checkValue, item);
                checkValue++;
            }
        }

        [Fact]
        public static void Remove_RemoveOneElement_ListIsEmpty()
        {
            var skipList = new SkipList<int>();
            skipList.Add(-10);

            skipList.Remove(-10);

            Assert.True(skipList.Count == 0);
            Assert.DoesNotContain(0, skipList);
            Assert.DoesNotContain(-10, skipList);
        }

        [Fact]
        public static void Remove_RemoveDefaultElement_DoNoRemoveAndListIsEmptyAndAddingIsWork()
        {
            var skipList = new SkipList<int>();

            Assert.DoesNotContain(0, skipList);
            skipList.Remove(default(int));

            Assert.True(skipList.Count == 0);
            Assert.DoesNotContain(0, skipList);
            
            skipList.Add(10);
            Assert.True(skipList.Count == 1);
            Assert.Contains(10, skipList);
        }

        [Fact]
        public static void Remove_RemoveSomeElements_ListContainOneElement()
        {
            var skipList = new SkipList<int>();
            skipList.Add(0);
            skipList.Add(-1);
            skipList.Add(2);
            skipList.Add(3);

            skipList.Remove(2);
            skipList.Remove(-1);
            skipList.Remove(3);

            Assert.True(skipList.Count == 1);
            Assert.Contains(0, skipList);
        }

        [Fact]
        public static void AddAndRemove_BatchOfElements_ListIsEmpty()
        {
            var skipList = new SkipList<int>();

            for (int i = 100; i > 50; --i)
            {
                skipList.Add(i);
            }

            Assert.True(skipList.Count == 50);
            for (int i = 100; i > 50; --i)
            {
                Assert.Contains(i, skipList);
            }

            for (int i = 100; i > 50; --i)
            {
                skipList.Remove(i);
            }

            for (int i = 100; i > 50; --i)
            {
                Assert.DoesNotContain(i, skipList);
            }
            Assert.True(skipList.Count == 0);
        }

        [Fact]
        public static void ClearList()
        {
            var skipList = new SkipList<int>();

            skipList.Add(1);
            skipList.Add(2);
            skipList.Add(3);
            skipList.Clear();

            Assert.True(skipList.Count == 0);
            Assert.True(skipList.IsEmpty);
            Assert.DoesNotContain(1, skipList);
            Assert.DoesNotContain(2, skipList);
            Assert.DoesNotContain(3, skipList);
        }
    }
}
