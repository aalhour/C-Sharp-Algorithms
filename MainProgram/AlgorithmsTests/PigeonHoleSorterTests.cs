using Algorithms.Sorting;
using NUnit.Framework;

namespace C_Sharp_Algorithms.AlgorithmsTests.Sorting
{
    [TestFixture]
    public class PigeonHoleSorterTests
    {
        [TestCase(new[] { 0, 2 }, new[] { 0, 2 })]
        [TestCase(new[] { 3, 2 }, new[] { 2, 3 })]
        [TestCase(new[] { 1, 9, 3, 7, 5, 2 }, new[] { 1, 2, 3, 5, 7, 9 })]
        [TestCase(new[] { 541, 87, 23, 87, 18, 687, 1587, 579, 25742, 5841, 2 }, new[] { 2, 18, 23, 87, 87, 541, 579, 687, 1587, 5841, 25742 })]
        public void PigeonHoleSortTest(int[] toSortInts, int[] expectedSortedInts)
        {
            toSortInts.PigeonHoleSort();
            CollectionAssert.AreEqual(toSortInts, expectedSortedInts);
        }

        [TestCase(new[] { 0, 2 }, new[] { 0, 2 })]
        [TestCase(new[] { 3, 2 }, new[] { 2, 3 })]
        [TestCase(new[] { 1, 9, 3, 7, 5, 2 }, new[] { 1, 2, 3, 5, 7, 9 })]
        [TestCase(new[] { 541, 87, 23, 87, 18, 687, 1587, 579, 25742, 5841, 2 }, new[] { 2, 18, 23, 87, 87, 541, 579, 687, 1587, 5841, 25742 })]
        public void PigeonHoleSortAscendingTest(int[] toSortInts, int[] expectedSortedInts)
        {
            toSortInts.PigeonHoleSortAscending();
            CollectionAssert.AreEqual(toSortInts, expectedSortedInts);
        }

        [TestCase(new[] { 0, 2 }, new[] { 2, 0 })]
        [TestCase(new[] { 3, 2 }, new[] { 3, 2 })]
        [TestCase(new[] { 1, 9, 3, 7, 5, 2 }, new[] { 9, 7, 5, 3, 2, 1 })]
        [TestCase(new[] { 541, 87, 23, 87, 18, 687, 1587, 579, 25742, 5841, 2 }, new[] { 25742, 5841, 1587, 687, 579, 541, 87, 87, 23, 18, 2 })]
        public void PigeonHoleSortDescendingTest(int[] toSortInts, int[] expectedSortedInts)
        {
            toSortInts.PigeonHoleSortDescending();
            CollectionAssert.AreEqual(toSortInts, expectedSortedInts);
        }
    }
}
