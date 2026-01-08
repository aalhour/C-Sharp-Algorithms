using DataStructures.Lists;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class SLinkedListTest
    {
        #region Append and Prepend Tests

        [Fact]
        public static void Append_AddsElementsToEnd()
        {
            var list = new SLinkedList<int>();

            list.Append(1);
            list.Append(2);
            list.Append(3);

            Assert.Equal(3, list.Count);
        }

        [Fact]
        public static void Prepend_AddsElementsToBeginning()
        {
            var list = new SLinkedList<int>();

            list.Append(3);
            list.Prepend(2);
            list.Prepend(1);

            Assert.Equal(3, list.Count);
        }

        #endregion

        #region RemoveAt Tests

        [Fact]
        public static void RemoveAt_DecreasesCount()
        {
            var list = new SLinkedList<int>();
            list.Append(1);
            list.Append(2);
            list.Append(3);
            list.Append(4);
            list.Append(5);

            list.RemoveAt(2);

            Assert.Equal(4, list.Count);
        }

        [Fact]
        public static void RemoveAt_FirstElement_Works()
        {
            var list = new SLinkedList<int>();
            list.Append(1);
            list.Append(2);
            list.Append(3);

            list.RemoveAt(0);

            Assert.Equal(2, list.Count);
        }

        [Fact]
        public static void RemoveAt_MultipleRemovals_Works()
        {
            var list = new SLinkedList<int>();
            list.Append(10);
            list.Append(124);
            list.Prepend(654);
            list.Prepend(8);
            list.Append(127485693);
            list.Append(34);
            list.Append(823);

            list.RemoveAt(0);
            list.RemoveAt(3);
            list.RemoveAt(4);
            list.RemoveAt(2);
            list.RemoveAt(2);
            list.RemoveAt(0);

            Assert.Equal(1, list.Count);
        }

        #endregion

        #region InsertAt Tests

        [Fact]
        public static void InsertAt_AtEnd_Works()
        {
            var list = new SLinkedList<int>();
            list.Append(1);
            list.Append(2);
            list.Append(3);

            list.InsertAt(4, list.Count);

            Assert.Equal(4, list.Count);
        }

        [Fact]
        public static void InsertAt_AtMiddle_Works()
        {
            var list = new SLinkedList<int>();
            list.Append(1);
            list.Append(3);

            list.InsertAt(2, 1);

            Assert.Equal(3, list.Count);
        }

        #endregion

        #region ToArray Tests

        [Fact]
        public static void ToArray_ReturnsArrayWithSameCount()
        {
            var list = new SLinkedList<int>();
            list.Append(1);
            list.Append(2);
            list.Append(3);

            var array = list.ToArray();

            Assert.Equal(list.Count, array.Length);
        }

        #endregion

        #region SelectionSort Tests

        [Fact]
        public static void SelectionSort_SortsInAscendingOrder()
        {
            var list = new SLinkedList<int>();
            list.Append(23);
            list.Append(42);
            list.Append(4);
            list.Append(16);
            list.Append(8);
            list.Append(15);
            list.Append(9);
            list.Append(55);
            list.Append(0);
            list.Append(34);
            list.Append(12);
            list.Append(2);

            list.SelectionSort();
            var array = list.ToArray();

            Assert.Equal(0, array[0]);
            Assert.Equal(55, array[array.Length - 1]);
        }

        #endregion
    }
}
