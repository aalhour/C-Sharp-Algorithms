using DataStructures.Lists;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class SkipListTest
    {
        [Fact]
        public static void DoTest()
        {
            var skipList = new SkipList<int>();

            for (int i = 100; i >= 50; --i)
                skipList.Add(i);

            for (int i = 0; i <= 35; ++i)
                skipList.Add(i);

            for (int i = -15; i <= 0; ++i)
                skipList.Add(i);

            for (int i = -15; i >= -35; --i)
                skipList.Add(i);

            Assert.True(skipList.Count == 124);

            skipList.Clear();

            for (int i = 100; i >= 0; --i)
                skipList.Add(i);

            Assert.True(skipList.Count == 101);
        }
    }
}
