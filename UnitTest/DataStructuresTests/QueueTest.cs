using System;
using System.Diagnostics;

using DataStructures.Lists;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class QueueTest
    {
        [Fact]
        public static void DoTest()
        {
            string top;
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("aaa");
            queue.Enqueue("bbb");
            queue.Enqueue("ccc");
            queue.Enqueue("ddd");
            queue.Enqueue("eee");
            queue.Enqueue("fff");
            queue.Enqueue("ggg");
            queue.Enqueue("hhh");
            Assert.Equal(8, queue.Count);

            var array = queue.ToArray();
            // fails if wrong size
            Assert.Equal(8, array.Length);

            queue.Dequeue();
            queue.Dequeue();
            top = queue.Dequeue();
            Assert.Equal("ccc", top);

            queue.Dequeue();
            queue.Dequeue();
            Assert.Equal("fff", queue.Top);

            var array2 = queue.ToArray();
            // fails if wrong size
            Assert.Equal(3, array2.Length);
        }
    }
}

