using DataStructures.Lists;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public class ArrayListTest
    {
        #region Add Tests

        [Fact]
        public void Add_SingleElement_CountIsOne()
        {
            var list = new ArrayList<int>();

            list.Add(42);

            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void Add_MultipleElements_CountIsCorrect()
        {
            var list = new ArrayList<int>();

            list.Add(1);
            list.Add(2);
            list.Add(3);

            Assert.Equal(3, list.Count);
        }

        [Fact]
        public void AddRepeatedly_AddsCorrectCount()
        {
            var list = new ArrayList<long>();

            list.AddRepeatedly(11, 32);

            Assert.Equal(32, list.Count);
        }

        #endregion

        #region Remove Tests

        [Fact]
        public void Remove_ExistingElement_DecreasesCount()
        {
            var list = new ArrayList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            list.Remove(2);

            Assert.Equal(2, list.Count);
            Assert.False(list.Contains(2));
        }

        [Fact]
        public void RemoveAt_RemovesElementAtIndex()
        {
            var list = new ArrayList<int>();
            list.Add(10);
            list.Add(20);
            list.Add(30);

            list.RemoveAt(1);

            Assert.Equal(2, list.Count);
            Assert.False(list.Contains(20));
        }

        #endregion

        #region Find Tests

        [Fact]
        public void Find_ExistingElement_ReturnsElement()
        {
            var list = new ArrayList<int>();
            list.Add(10);
            list.Add(20);
            list.Add(30);

            var result = list.Find(item => item == 20);

            Assert.Equal(20, result);
        }

        [Fact]
        public void FindAll_ReturnsAllMatching()
        {
            var list = new ArrayList<int>();
            list.Add(5);
            list.Add(10);
            list.Add(15);
            list.Add(20);
            list.Add(25);

            var result = list.FindAll(item => item > 10);

            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void FindIndex_ReturnsCorrectIndex()
        {
            var list = new ArrayList<int>();
            list.Add(10);
            list.Add(20);
            list.Add(30);

            var index = list.FindIndex(item => item == 20);

            Assert.Equal(1, index);
        }

        [Fact]
        public void FindIndex_NonExisting_ReturnsMinusOne()
        {
            var list = new ArrayList<int>();
            list.Add(10);
            list.Add(20);
            list.Add(30);

            var index = list.FindIndex(item => item == 100);

            Assert.Equal(-1, index);
        }

        #endregion

        #region Contains/Exists Tests

        [Fact]
        public void Contains_ExistingElement_ReturnsTrue()
        {
            var list = new ArrayList<int>();
            list.Add(10);
            list.Add(20);
            list.Add(30);

            Assert.True(list.Contains(20));
        }

        [Fact]
        public void Contains_NonExistingElement_ReturnsFalse()
        {
            var list = new ArrayList<int>();
            list.Add(10);
            list.Add(20);
            list.Add(30);

            Assert.False(list.Contains(100));
        }

        [Fact]
        public void Exists_MatchingPredicate_ReturnsTrue()
        {
            var list = new ArrayList<int>();
            list.Add(10);
            list.Add(20);
            list.Add(30);

            Assert.True(list.Exists(item => item > 25));
        }

        [Fact]
        public void Exists_NoMatchingPredicate_ReturnsFalse()
        {
            var list = new ArrayList<int>();
            list.Add(10);
            list.Add(20);
            list.Add(30);

            Assert.False(list.Exists(item => item > 100));
        }

        #endregion

        #region InsertAt Tests

        [Fact]
        public void InsertAt_InsertsAtCorrectPosition()
        {
            var list = new ArrayList<int>();
            list.Add(10);
            list.Add(30);

            list.InsertAt(20, 1);

            Assert.Equal(3, list.Count);
            Assert.Equal(20, list[1]);
        }

        [Fact]
        public void InsertAt_MultipleTimes_AllInserted()
        {
            var list = new ArrayList<int>();
            list.Add(0);
            list.Add(100);

            list.InsertAt(50, 1);
            list.InsertAt(25, 1);
            list.InsertAt(75, 3);

            Assert.Equal(5, list.Count);
        }

        #endregion

        #region Edge Cases

        [Fact]
        public void EmptyList_CountIsZero()
        {
            var list = new ArrayList<int>();

            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void EmptyList_IsEmptyReturnsTrue()
        {
            var list = new ArrayList<int>();

            Assert.True(list.IsEmpty);
        }

        [Fact]
        public void AfterAdd_IsEmptyReturnsFalse()
        {
            var list = new ArrayList<int>();
            list.Add(1);

            Assert.False(list.IsEmpty);
        }

        #endregion

        #region Large Dataset Tests

        [Fact]
        public void Add_LargeDataset_HandlesCorrectly()
        {
            var list = new ArrayList<long>();

            for (long i = 1; i < 10000; i++)
            {
                list.Add(i);
            }

            Assert.Equal(9999, list.Count);
        }

        [Fact]
        public void FindAll_LargeDataset_ReturnsCorrectResults()
        {
            var list = new ArrayList<long>();
            for (long i = 1; i <= 1000; i++)
            {
                list.Add(i);
            }

            var result = list.FindAll(item => item > 900);

            Assert.Equal(100, result.Count);
        }

        #endregion
    }
}
