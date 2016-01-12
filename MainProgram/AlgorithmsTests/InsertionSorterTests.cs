using Algorithms.Sorting;
using DataStructures.Lists;
using NUnit.Framework;

namespace C_Sharp_Algorithms.AlgorithmsTests.Sorting
{
    [TestFixture]
    public class InsertionSorterTests
    {
        [TestCase(new[] { 0, 2 }, new[] { 0, 2 })]
        [TestCase(new[] { 3, 2 }, new[] { 2, 3 })]
        [TestCase(new[] { 1, 9, 3, 7, 5, 2 }, new[] { 1, 2, 3, 5, 7, 9 })]
        [TestCase(new[] { 541, 87, 23, 87, 18, 687, 1587, 579, 25742, 5841, 2 }, new[] { 2, 18, 23, 87, 87, 541, 579, 687, 1587, 5841, 25742 })]
        public void InsertionSortTest(int[] toSortInts, int[] expectedSortedInts)
        {
            toSortInts.InsertionSort();
            CollectionAssert.AreEqual(toSortInts, expectedSortedInts);
        }

        [TestCase(new[] { 0, 2 }, new[] { 0, 2 })]
        [TestCase(new[] { 3, 2 }, new[] { 2, 3 })]
        [TestCase(new[] { 1, 9, 3, 7, 5, 2 }, new[] { 1, 2, 3, 5, 7, 9 })]
        [TestCase(new[] { 541, 87, 23, 87, 18, 687, 1587, 579, 25742, 5841, 2 }, new[] { 2, 18, 23, 87, 87, 541, 579, 687, 1587, 5841, 25742 })]
        public void InsertionSortArrayListTest(int[] toSortInts, int[] expectedSortedInts)
        {
            ArrayList<int> toSort = new ArrayList<int>();
            toSort.AddRange(toSortInts);
            toSort.InsertionSort();
            CollectionAssert.AreEqual(toSort, expectedSortedInts);
        }
    }
}
