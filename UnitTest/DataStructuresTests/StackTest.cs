using DataStructures.Lists;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public class StackTest
    {
        #region Push Tests

        [Fact]
        public void Push_SingleElement_TopEqualsElement()
        {
            var stack = new DataStructures.Lists.Stack<int>();

            stack.Push(42);

            Assert.Equal(42, stack.Top);
        }

        [Fact]
        public void Push_MultipleElements_TopEqualsLastPushed()
        {
            var stack = new DataStructures.Lists.Stack<int>();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            Assert.Equal(3, stack.Top);
        }

        #endregion

        #region Pop Tests

        [Fact]
        public void Pop_ReturnsTopElement()
        {
            var stack = new DataStructures.Lists.Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            var popped = stack.Pop();

            Assert.Equal(3, popped);
        }

        [Fact]
        public void Pop_RemovesTopElement()
        {
            var stack = new DataStructures.Lists.Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            stack.Pop();

            Assert.Equal(2, stack.Top);
        }

        [Fact]
        public void Pop_MultipleTimes_ReturnsInReverseOrder()
        {
            var stack = new DataStructures.Lists.Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            Assert.Equal(3, stack.Pop());
            Assert.Equal(2, stack.Pop());
            Assert.Equal(1, stack.Pop());
        }

        #endregion

        #region Count Tests

        [Fact]
        public void Count_EmptyStack_ReturnsZero()
        {
            var stack = new DataStructures.Lists.Stack<int>();

            Assert.Equal(0, stack.Count);
        }

        [Fact]
        public void Count_AfterPush_ReturnsCorrectCount()
        {
            var stack = new DataStructures.Lists.Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            Assert.Equal(3, stack.Count);
        }

        [Fact]
        public void Count_AfterPop_DecreasesCorrectly()
        {
            var stack = new DataStructures.Lists.Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            stack.Pop();

            Assert.Equal(2, stack.Count);
        }

        #endregion

        #region ToArray Tests

        [Fact]
        public void ToArray_ReturnsCorrectSize()
        {
            var stack = new DataStructures.Lists.Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);
            stack.Push(6);

            var array = stack.ToArray();

            Assert.Equal(stack.Count, array.Length);
        }

        [Fact]
        public void ToArray_AfterPop_ReturnsUpdatedArray()
        {
            var stack = new DataStructures.Lists.Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Pop();
            stack.Pop();

            var array = stack.ToArray();

            Assert.Single(array);
        }

        #endregion

        #region Edge Cases

        [Fact]
        public void IsEmpty_NewStack_ReturnsTrue()
        {
            var stack = new DataStructures.Lists.Stack<int>();

            Assert.True(stack.IsEmpty);
        }

        [Fact]
        public void IsEmpty_AfterPush_ReturnsFalse()
        {
            var stack = new DataStructures.Lists.Stack<int>();
            stack.Push(1);

            Assert.False(stack.IsEmpty);
        }

        [Fact]
        public void IsEmpty_AfterPushAndPopAll_ReturnsTrue()
        {
            var stack = new DataStructures.Lists.Stack<int>();
            stack.Push(1);
            stack.Pop();

            Assert.True(stack.IsEmpty);
        }

        #endregion
    }
}
