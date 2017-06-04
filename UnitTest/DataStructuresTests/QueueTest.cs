using System;
using System.Diagnostics;

using DataStructures.Lists;
using Xunit;

namespace C_Sharp_Algorithms.DataStructuresTests
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
            Assert.Equal(queue.Count, 8);

            var array = queue.ToArray();
            // fails if wrong size
            Assert.Equal(array.Length, 8);

            queue.Dequeue();
            queue.Dequeue();
            top = queue.Dequeue();
            Assert.Equal(top, "ccc");

            queue.Dequeue();
            queue.Dequeue();
            Assert.Equal(queue.Top, "fff");

            var array2 = queue.ToArray();
            // fails if wrong size
            Assert.Equal(array2.Length, 3);
        }
    }
}

