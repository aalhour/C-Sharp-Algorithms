using DataStructures.Lists;
using Xunit;

namespace UnitTest.DataStructuresTests {
    public class CircularBufferTest {
        
        [Fact]
        public static void SetsFixedLength() {
            var circularBuffer = new CircularBuffer<byte>(3);
            var length = circularBuffer.Length;

            Assert.Equal(3, length);
        }

        [Fact]
        public static void InitializesWithDefaultLengthOf10() {
            var circularBuffer = new CircularBuffer<byte>();
            var length = circularBuffer.Length;

            Assert.Equal(10, length);
        }

        [Fact]
        public static void WritesAndReadsValue() {
            var circularBuffer = new CircularBuffer<byte>(4);
            circularBuffer.Write(13);
            circularBuffer.Write(43);
            circularBuffer.Write(23);
            circularBuffer.Write(2);

            var result1 = circularBuffer.Read();
            var result2 = circularBuffer.Read();
            var result3 = circularBuffer.Read();
            var result4 = circularBuffer.Read();
            var result5 = circularBuffer.Read();
            var result6 = circularBuffer.Read();
            var result7 = circularBuffer.Read();
            var result8 = circularBuffer.Read();

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
        public static void TestingCantOverrideFunctionality() {
            var circularBuffer = new CircularBuffer<byte>(3, false);
            circularBuffer.Write(3);
            circularBuffer.Write(34);
            circularBuffer.Write(24);
            // if it doesn't override, then the values above will be read else, 
            // the value below will be read
            circularBuffer.Write(2);
            circularBuffer.Write(1);
            circularBuffer.Write(3);

            var result1 = circularBuffer.Read();
            var result2 = circularBuffer.Read();
            var result3 = circularBuffer.Read();

            Assert.Equal(3, result1);
            Assert.Equal(34, result2);
            Assert.Equal(24, result3);
        }

        [Fact]
        public static void TestingWritingAndReadingSimultenouslyWithoutOverriding() {
            var circularBuffer = new CircularBuffer<byte>(3, false);
            circularBuffer.Write(3);
            circularBuffer.Write(34);
            circularBuffer.Write(24);
            var result1 = circularBuffer.Read();
            var result2 = circularBuffer.Read();

            circularBuffer.Write(4);
            circularBuffer.Write(14);
            var result3 = circularBuffer.Read();
            var result4 = circularBuffer.Read();
            var result5 = circularBuffer.Read();

            Assert.Equal(24, result3);
            Assert.Equal(4, result4);
            Assert.Equal(14, result5);
        }
    }
}
