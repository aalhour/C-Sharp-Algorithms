using Xunit;

namespace UnitTest.DataStructuresTests
{
    public class QueueTest
    {
        #region Enqueue Tests

        [Fact]
        public void Enqueue_SingleElement_CountIsOne()
        {
            var queue = new DataStructures.Lists.Queue<string>();

            queue.Enqueue("first");

            Assert.Equal(1, queue.Count);
        }

        [Fact]
        public void Enqueue_MultipleElements_CountIsCorrect()
        {
            var queue = new DataStructures.Lists.Queue<string>();

            queue.Enqueue("aaa");
            queue.Enqueue("bbb");
            queue.Enqueue("ccc");

            Assert.Equal(3, queue.Count);
        }

        #endregion

        #region Dequeue Tests

        [Fact]
        public void Dequeue_ReturnsFirstElement()
        {
            var queue = new DataStructures.Lists.Queue<string>();
            queue.Enqueue("first");
            queue.Enqueue("second");
            queue.Enqueue("third");

            var result = queue.Dequeue();

            Assert.Equal("first", result);
        }

        [Fact]
        public void Dequeue_RemovesFirstElement()
        {
            var queue = new DataStructures.Lists.Queue<string>();
            queue.Enqueue("first");
            queue.Enqueue("second");

            queue.Dequeue();

            Assert.Equal(1, queue.Count);
            Assert.Equal("second", queue.Top);
        }

        [Fact]
        public void Dequeue_MultipleTimes_ReturnsFIFOOrder()
        {
            var queue = new DataStructures.Lists.Queue<string>();
            queue.Enqueue("aaa");
            queue.Enqueue("bbb");
            queue.Enqueue("ccc");

            Assert.Equal("aaa", queue.Dequeue());
            Assert.Equal("bbb", queue.Dequeue());
            Assert.Equal("ccc", queue.Dequeue());
        }

        #endregion

        #region Top Tests

        [Fact]
        public void Top_ReturnsFirstElementWithoutRemoving()
        {
            var queue = new DataStructures.Lists.Queue<string>();
            queue.Enqueue("first");
            queue.Enqueue("second");

            var top = queue.Top;

            Assert.Equal("first", top);
            Assert.Equal(2, queue.Count);
        }

        [Fact]
        public void Top_AfterDequeue_ReturnsNextElement()
        {
            var queue = new DataStructures.Lists.Queue<string>();
            queue.Enqueue("aaa");
            queue.Enqueue("bbb");
            queue.Enqueue("ccc");
            queue.Enqueue("ddd");
            queue.Enqueue("eee");

            queue.Dequeue();
            queue.Dequeue();

            Assert.Equal("ccc", queue.Top);
        }

        #endregion

        #region ToArray Tests

        [Fact]
        public void ToArray_ReturnsCorrectSize()
        {
            var queue = new DataStructures.Lists.Queue<string>();
            queue.Enqueue("aaa");
            queue.Enqueue("bbb");
            queue.Enqueue("ccc");

            var array = queue.ToArray();

            Assert.Equal(3, array.Length);
        }

        [Fact]
        public void ToArray_AfterDequeue_ReturnsUpdatedArray()
        {
            var queue = new DataStructures.Lists.Queue<string>();
            queue.Enqueue("aaa");
            queue.Enqueue("bbb");
            queue.Enqueue("ccc");
            queue.Dequeue();
            queue.Dequeue();

            var array = queue.ToArray();

            Assert.Single(array);
        }

        #endregion

        #region Edge Cases

        [Fact]
        public void IsEmpty_NewQueue_ReturnsTrue()
        {
            var queue = new DataStructures.Lists.Queue<string>();

            Assert.True(queue.IsEmpty);
        }

        [Fact]
        public void IsEmpty_AfterEnqueue_ReturnsFalse()
        {
            var queue = new DataStructures.Lists.Queue<string>();
            queue.Enqueue("item");

            Assert.False(queue.IsEmpty);
        }

        [Fact]
        public void Count_AfterEnqueueAndDequeue_IsCorrect()
        {
            var queue = new DataStructures.Lists.Queue<string>();
            queue.Enqueue("aaa");
            queue.Enqueue("bbb");
            queue.Enqueue("ccc");
            queue.Enqueue("ddd");
            queue.Enqueue("eee");
            queue.Dequeue();
            queue.Dequeue();

            Assert.Equal(3, queue.Count);
        }

        #endregion
    }
}
