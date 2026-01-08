using DataStructures.Lists;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class DLinkedListTest
    {
        #region Append and Prepend Tests

        [Fact]
        public static void Append_AddsElementsToEnd()
        {
            var list = new DLinkedList<string>();

            list.Append("first");
            list.Append("second");
            list.Append("third");

            Assert.Equal(3, list.Count);
            Assert.Equal("first", list[0]);
            Assert.Equal("second", list[1]);
            Assert.Equal("third", list[2]);
        }

        [Fact]
        public static void Prepend_AddsElementsToBeginning()
        {
            var list = new DLinkedList<string>();

            list.Prepend("third");
            list.Prepend("second");
            list.Prepend("first");

            Assert.Equal(3, list.Count);
            Assert.Equal("first", list[0]);
            Assert.Equal("second", list[1]);
            Assert.Equal("third", list[2]);
        }

        #endregion

        #region RemoveAt Tests

        [Fact]
        public static void RemoveAt_FirstElement_ShiftsRemaining()
        {
            var list = new DLinkedList<string>();
            list.Append("zero");
            list.Append("first");
            list.Append("second");

            list.RemoveAt(0);

            Assert.Equal(2, list.Count);
            Assert.Equal("first", list[0]);
        }

        [Fact]
        public static void RemoveAt_MiddleElement_ShiftsRemaining()
        {
            var list = new DLinkedList<string>();
            list.Append("a");
            list.Append("b");
            list.Append("c");
            list.Append("d");

            list.RemoveAt(1);

            Assert.Equal(3, list.Count);
            Assert.Equal("a", list[0]);
            Assert.Equal("c", list[1]);
            Assert.Equal("d", list[2]);
        }

        [Fact]
        public static void RemoveAt_LastElement_DecreasesCount()
        {
            var list = new DLinkedList<string>();
            list.Append("a");
            list.Append("b");
            list.Append("c");

            list.RemoveAt(list.Count - 1);

            Assert.Equal(2, list.Count);
            Assert.Equal("b", list[list.Count - 1]);
        }

        #endregion

        #region Remove Tests

        [Fact]
        public static void Remove_ExistingItem_RemovesIt()
        {
            var list = new DLinkedList<string>();
            list.Append("a");
            list.Append("b");
            list.Append("c");

            list.Remove("b");

            Assert.Equal(2, list.Count);
            Assert.Equal("a", list[0]);
            Assert.Equal("c", list[1]);
        }

        #endregion

        #region InsertAt Tests

        [Fact]
        public static void InsertAt_Beginning_InsertsCorrectly()
        {
            var list = new DLinkedList<string>();
            list.Append("b");
            list.Append("c");

            list.InsertAt("a", 0);

            Assert.Equal(3, list.Count);
            Assert.Equal("a", list[0]);
        }

        [Fact]
        public static void InsertAt_End_InsertsCorrectly()
        {
            var list = new DLinkedList<string>();
            list.Append("a");
            list.Append("b");

            list.InsertAt("c", list.Count);

            Assert.Equal(3, list.Count);
            Assert.Equal("c", list[list.Count - 1]);
        }

        [Fact]
        public static void InsertAt_Middle_InsertsCorrectly()
        {
            var list = new DLinkedList<string>();
            list.Append("a");
            list.Append("c");

            list.InsertAt("b", 1);

            Assert.Equal(3, list.Count);
            Assert.Equal("b", list[1]);
        }

        #endregion

        #region InsertAfter Tests

        [Fact]
        public static void InsertAfter_InsertsAfterGivenIndex()
        {
            var list = new DLinkedList<string>();
            list.Append("a");
            list.Append("c");

            list.InsertAfter("b", 0);

            Assert.Equal(3, list.Count);
            Assert.Equal("b", list[1]);
        }

        #endregion

        #region ToArray Tests

        [Fact]
        public static void ToArray_ReturnsCorrectArray()
        {
            var list = new DLinkedList<string>();
            list.Append("a");
            list.Append("b");
            list.Append("c");

            var array = list.ToArray();

            Assert.Equal(list.Count, array.Length);
            Assert.Equal("a", array[0]);
            Assert.Equal("b", array[1]);
            Assert.Equal("c", array[2]);
        }

        #endregion

        #region Enumerator Tests

        [Fact]
        public static void GetEnumerator_EnumeratesInOrder()
        {
            var list = new DLinkedList<string>();
            list.Append("first");
            list.Append("second");
            list.Append("third");

            var enumerator = list.GetEnumerator();

            Assert.True(enumerator.MoveNext());
            Assert.Equal("first", enumerator.Current);
            Assert.True(enumerator.MoveNext());
            Assert.Equal("second", enumerator.Current);
            Assert.True(enumerator.MoveNext());
            Assert.Equal("third", enumerator.Current);
            Assert.False(enumerator.MoveNext());

            enumerator.Dispose();
            Assert.True(list.Count > 0); // Enumerator shouldn't have side effects
        }

        #endregion

        #region SelectionSort Tests

        [Fact]
        public static void SelectionSort_SortsInAscendingOrder()
        {
            var list = new DLinkedList<int>();
            list.Append(23);
            list.Append(42);
            list.Append(4);
            list.Append(16);
            list.Append(8);
            list.Append(15);
            list.Append(9);
            list.Append(55);
            list.Append(0);

            list.SelectionSort();
            var array = list.ToArray();

            Assert.Equal(0, array[0]);
            Assert.Equal(55, array[array.Length - 1]);

            for (int i = 0; i < array.Length - 1; i++)
            {
                Assert.True(array[i] <= array[i + 1], $"Not sorted at index {i}");
            }
        }

        [Fact]
        public static void SelectionSort_WithMoreElements_SortsCorrectly()
        {
            var list = new DLinkedList<int>();
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
