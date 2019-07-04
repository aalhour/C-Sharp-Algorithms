using DataStructures.Lists;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class StackTest
    {
        [Fact]
        public static void DoTest()
        {
            int top;
            Stack<int> stack = new Stack<int>();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);
            stack.Push(6);

            // Wrong top value.
            Assert.Equal(6, stack.Top);

            var array = stack.ToArray();

            // Wrong size!
            Assert.Equal(array.Length, stack.Count);

            top = stack.Pop();

            // Wrong top value.
            Assert.Equal(5, stack.Top);

            stack.Pop();
            stack.Pop();

            // Wrong top value.
            Assert.Equal(3, stack.Top);

            var array2 = stack.ToArray();

            // Wrong size!
            Assert.Equal(array2.Length, stack.Count);
        }
    }
}

