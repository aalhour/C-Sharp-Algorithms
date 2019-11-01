using DataStructures.Lists;
using System;
using Xunit;

namespace UnitTest.DataStructuresTests 
{
    public class CircularBufferTest 
    {
        
        [Fact]
        public static void SetsFixedLength() 
        {
            var circularBuffer = new CircularBuffer<byte>(3);
            var length = circularBuffer.Length;
            Assert.Equal(3, length);
        }

        [Fact]
        public static void ChecksIsEmptyProperty() 
        {
            var circularBuffer = new CircularBuffer<byte>(4);
            Assert.True(circularBuffer.IsEmpty);
        }

        [Fact]
        public static void ChecksIsFilledProperty() 
        {
            var circularBuffer = new CircularBuffer<byte>(3, false);
            circularBuffer.Add(1);
            circularBuffer.Add(2);
            circularBuffer.Add(3);

            Assert.True(circularBuffer.IsFilledUp);
        }

        [Fact]
        public static void InitializesWithDefaultLengthOf10() 
        {
            var circularBuffer = new CircularBuffer<byte>();
            var length = circularBuffer.Length;

            Assert.Equal(10, length);
        }
        [Fact]
        public static void ThrowsArguementOutOfRangeExceptionForLengthLessThanOne() 
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => 
            {
                var circularBuffer = new CircularBuffer<byte>(0);
            });
            Assert.Throws<ArgumentOutOfRangeException>(() => 
            {
                var circularBuffer = new CircularBuffer<byte>(-2);
            });
        }

        [Fact]
        public static void ThrowsCircularBufferFullExceptionWhenInsertingInFullBuffer() 
        {
            Assert.Throws<CircularBufferFullException>(() => 
            {
                var circularBuffer = new CircularBuffer<byte>(3, false);
                circularBuffer.Add(1);
                circularBuffer.Add(2);
                circularBuffer.Add(3);
                circularBuffer.Add(4);
            });
        }

        [Fact]
        public static void WritesAndReadsValue() 
        {
            var circularBuffer = new CircularBuffer<byte>(4);
            circularBuffer.Add(13);
            circularBuffer.Add(43);
            circularBuffer.Add(23);
            circularBuffer.Add(2);

            var result1 = circularBuffer.Pop();
            var result2 = circularBuffer.Pop();
            var result3 = circularBuffer.Pop();
            var result4 = circularBuffer.Pop();
            var result5 = circularBuffer.Pop();
            var result6 = circularBuffer.Pop();
            var result7 = circularBuffer.Pop();
            var result8 = circularBuffer.Pop();

            Assert.Equal(13, result1);
            Assert.Equal(43, result2);
            Assert.Equal(23, result3);
            Assert.Equal(2, result4);
            Assert.Equal(0, result5);
            Assert.Equal(0, result6);
            Assert.Equal(0, result7);
            Assert.Equal(0, result8);
        }

        [Fact]
        public static void TestingCantOverrideFunctionality() 
        {
            var circularBuffer = new CircularBuffer<byte>(3, false);
            circularBuffer.Add(3);
            circularBuffer.Add(34);
            circularBuffer.Add(24);
            // if it doesn't override, then it will throw CircularBufferFullException
            Assert.Throws<CircularBufferFullException>(() => 
            {
                circularBuffer.Add(2);
            });
            
            // Ensuring that it reads the appropriate values in the buffer.
            var result1 = circularBuffer.Pop();
            var result2 = circularBuffer.Pop();
            var result3 = circularBuffer.Pop();

            Assert.Equal(3, result1);
            Assert.Equal(34, result2);
            Assert.Equal(24, result3);
        }

        [Fact]
        public static void TestingWritingAndReadingSimultenouslyWithoutOverriding() 
        {
            var circularBuffer = new CircularBuffer<byte>(3, false);
            circularBuffer.Add(3);
            circularBuffer.Add(34);
            circularBuffer.Add(24);
            var result1 = circularBuffer.Pop();
            var result2 = circularBuffer.Pop();

            circularBuffer.Add(4);
            circularBuffer.Add(14);
            var result3 = circularBuffer.Pop();
            var result4 = circularBuffer.Pop();
            var result5 = circularBuffer.Pop();

            Assert.Equal(24, result3);
            Assert.Equal(4, result4);
            Assert.Equal(14, result5);
        }

        [Fact]
        public static void TestingICollectionImplementation() 
        {
            var circularBuffer = new CircularBuffer<byte>(3, false);
            circularBuffer.Add(3);
            circularBuffer.Add(34);
            circularBuffer.Add(24);
            //Testing contains
            Assert.True(circularBuffer.Contains(3));
            
            //Testing CopyTo
            var array = new byte[3];
            circularBuffer.CopyTo(array, 0);
            Assert.Equal(3, array[0]);
            Assert.Equal(34, array[1]);
            Assert.Equal(24, array[2]);
            
            //Testing Remove
            Assert.True(circularBuffer.Remove(3));
            Assert.False(circularBuffer.Remove(14));

            //Testing Count
            Assert.Equal(2, circularBuffer.Count);
            //Testing clear
            circularBuffer.Clear();
            Assert.Equal(0, circularBuffer.Pop());
            Assert.Equal(0, circularBuffer.Pop());
            Assert.Equal(0, circularBuffer.Pop());
            Assert.Empty(circularBuffer);
        }
    }
}
