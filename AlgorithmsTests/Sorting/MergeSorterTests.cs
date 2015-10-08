using System;
using System.Linq;
using Algorithms.Sorting;
using NUnit.Framework;

namespace AlgorithmsTests.Sorting
{
    [TestFixture]
    public class MergeSorterTests
    {
        [TestCase(new[] { 0, 2 }, new[] { 0, 2 })]
        [TestCase(new[] { 3, 2 }, new[] { 2, 3 })]
        [TestCase(new[] { 1, 9, 3, 7, 5, 2 }, new[] { 1, 2, 3, 5, 7, 9 })]
        [TestCase(new[] { 541, 87, 23, 87, 18, 687, 1587, 579, 25742, 5841, 2 }, new[] { 2, 18, 23, 87, 87, 541, 579, 687, 1587, 5841, 25742 })]
        public void CountingSortTest(int[] toSortInts, int[] expectedSortedInts)
        {
            int[] target = toSortInts.ToList().MergeSort().ToArray();
            CollectionAssert.AreEqual(target, expectedSortedInts);
        }
    }
}
