using System.Collections.Generic;
using DataStructures.Lists;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class SkipListTest
    {
        [Fact]
        public static void EmptyList()
        {
            var skipList = new SkipList<int>();

            Assert.True(skipList.Count == 0);
            Assert.True(skipList.IsEmpty);
            Assert.DoesNotContain(0, skipList);
        }

        [Fact]
        public static void AddOneElement()
        {
            var skipList = new SkipList<int>();

            skipList.Add(10);

            Assert.True(skipList.Count == 1);
            Assert.Contains(10, skipList);
        }

        [Fact]
        public static void AddBatchOfElements()
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
        }

        [Fact]
        public static void AddThreeElementsRemoveOneElement()
        {
            var skipList = new SkipList<int>();

            skipList.Add(1);
            skipList.Add(2);
            skipList.Add(3);
            skipList.Remove(2);

            Assert.True(skipList.Count == 2);
            Assert.Contains(1, skipList);
            Assert.Contains(3, skipList);
            Assert.DoesNotContain(2, skipList);
        }

        [Fact]
        public static void AddAndRemoveBatchOfElements()
        {
            var skipList = new SkipList<int>();

            for (int i = 100; i > 50; --i)
            {
                skipList.Add(i);
            }

            for (int i = 100; i > 50; --i)
            {
                skipList.Remove(i);
            }

            Assert.True(skipList.Count == 0);
            for (int i = 100; i > 50; --i)
            {
                Assert.DoesNotContain(i, skipList);
            }
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
