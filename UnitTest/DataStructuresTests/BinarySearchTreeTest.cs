using System.Collections.Generic;
using System.Linq;
using DataStructures.Trees;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public class BinarySearchTreeTest
    {
        #region Insert Tests

        [Fact]
        public void Insert_WithDuplicatesAllowed_CountsAllElements()
        {
            var tree = new AugmentedBinarySearchTree<int>(allowDuplicates: true);
            int[] values = { 15, 25, 5, 12, 1, 16, 20, 9, 9, 7, 7, 7, -1, 11, 19, 30, 8, 10, 13, 28, 39 };

            tree.Insert(values);

            Assert.Equal(21, tree.Count);
        }

        [Fact]
        public void Insert_WithDuplicatesNotAllowed_ThrowsOnDuplicate()
        {
            var tree = new AugmentedBinarySearchTree<int>(allowDuplicates: false);
            tree.Insert(1);
            tree.Insert(2);

            Assert.ThrowsAny<System.Exception>(() => tree.Insert(1));
        }

        [Fact]
        public void Insert_UniqueValues_CountsCorrectly()
        {
            var tree = new AugmentedBinarySearchTree<int>(allowDuplicates: false);
            int[] values = { 14, 15, 25, 5, 12, 1, 16, 20, 9, 7, -1, 11, 19, 30, 8, 10, 13, 28, 39 };

            tree.Insert(values);

            Assert.Equal(values.Length, tree.Count);
        }

        #endregion

        #region Contains Tests

        [Fact]
        public void Contains_ExistingElement_ReturnsTrue()
        {
            var tree = new AugmentedBinarySearchTree<int>(allowDuplicates: true);
            tree.Insert(new[] { 15, 25, 5, 12, 1, 10 });

            Assert.True(tree.Contains(10));
            Assert.True(tree.Contains(15));
            Assert.True(tree.Contains(1));
        }

        [Fact]
        public void Contains_NonExistingElement_ReturnsFalse()
        {
            var tree = new AugmentedBinarySearchTree<int>(allowDuplicates: true);
            tree.Insert(new[] { 15, 25, 5, 12, 1, 10 });

            Assert.False(tree.Contains(100));
            Assert.False(tree.Contains(-5));
        }

        #endregion

        #region FindAll Tests

        [Fact]
        public void FindAll_WithPredicate_ReturnsMatchingElements()
        {
            var tree = new AugmentedBinarySearchTree<int>(allowDuplicates: true);
            tree.Insert(new[] { 15, 25, 5, 12, 1, 16, 20, 9 });

            var result = tree.FindAll(element => element > 15).ToList();

            Assert.Equal(3, result.Count);
            Assert.Contains(25, result);
            Assert.Contains(16, result);
            Assert.Contains(20, result);
        }

        [Fact]
        public void FindAll_NoMatches_ReturnsEmpty()
        {
            var tree = new AugmentedBinarySearchTree<int>(allowDuplicates: true);
            tree.Insert(new[] { 1, 2, 3, 4, 5 });

            var result = tree.FindAll(element => element > 100).ToList();

            Assert.Empty(result);
        }

        #endregion

        #region FindMin/FindMax Tests

        [Fact]
        public void FindMin_ReturnsSmallestElement()
        {
            var tree = new AugmentedBinarySearchTree<int>(allowDuplicates: true);
            tree.Insert(new[] { 15, 25, 5, 12, 1, -1, 30 });

            Assert.Equal(-1, tree.FindMin());
        }

        [Fact]
        public void FindMax_ReturnsLargestElement()
        {
            var tree = new AugmentedBinarySearchTree<int>(allowDuplicates: true);
            tree.Insert(new[] { 15, 25, 5, 12, 1, -1, 39 });

            Assert.Equal(39, tree.FindMax());
        }

        #endregion

        #region RemoveMin/RemoveMax Tests

        [Fact]
        public void RemoveMin_RemovesSmallestElement()
        {
            var tree = new AugmentedBinarySearchTree<int>(allowDuplicates: true);
            tree.Insert(new[] { 15, 25, 5, 12, 1, -1, 30 });

            tree.RemoveMin();

            Assert.Equal(1, tree.FindMin());
        }

        [Fact]
        public void RemoveMax_RemovesLargestElement()
        {
            var tree = new AugmentedBinarySearchTree<int>(allowDuplicates: true);
            tree.Insert(new[] { 15, 25, 5, 12, 1, -1, 39 });

            tree.RemoveMax();

            Assert.Equal(25, tree.FindMax());
        }

        [Fact]
        public void RemoveMin_WithDuplicates_RemovesOnlyOne()
        {
            var tree = new AugmentedBinarySearchTree<int>(allowDuplicates: true);
            tree.Insert(new[] { 5, 7, 7, 7, 10 });

            tree.RemoveMin();

            Assert.Equal(7, tree.FindMin());
            // After removing 5, min is still 7 (duplicates remain)
        }

        #endregion

        #region Remove Tests

        [Fact]
        public void Remove_ExistingElement_DecreasesCount()
        {
            var tree = new AugmentedBinarySearchTree<int>(allowDuplicates: true);
            tree.Insert(new[] { 15, 25, 5, 12, 16 });
            var initialCount = tree.Count;

            tree.Remove(16);

            Assert.Equal(initialCount - 1, tree.Count);
            Assert.False(tree.Contains(16));
        }

        [Fact]
        public void Remove_NonExistingElement_ThrowsException()
        {
            var tree = new AugmentedBinarySearchTree<int>(allowDuplicates: true);
            tree.Insert(new[] { 15, 25, 5, 12, 16 });

            Assert.ThrowsAny<System.Exception>(() => tree.Remove(1000));
        }

        #endregion

        #region Sorting Tests

        [Fact]
        public void ToList_ReturnsSortedElements()
        {
            var tree = new AugmentedBinarySearchTree<int>(allowDuplicates: true);
            tree.Insert(new[] { 15, 25, 5, 12, 1, 16, 20, 9 });

            var sortedList = tree.ToList();

            for (int i = 1; i < sortedList.Count; i++)
            {
                Assert.True(sortedList[i - 1] <= sortedList[i]);
            }
        }

        #endregion

        #region Enumerator Tests

        [Fact]
        public void GetInOrderEnumerator_TraversesInOrder()
        {
            var tree = new AugmentedBinarySearchTree<int>(allowDuplicates: true);
            tree.Insert(new[] { 15, 5, 25, 3, 10 });

            var enumerator = tree.GetInOrderEnumerator();
            var values = new List<int>();
            while (enumerator.MoveNext())
            {
                values.Add(enumerator.Current);
            }

            Assert.Equal(new[] { 3, 5, 10, 15, 25 }, values);
        }

        #endregion

        #region Edge Cases

        [Fact]
        public void EmptyTree_CountIsZero()
        {
            var tree = new AugmentedBinarySearchTree<int>();

            Assert.Equal(0, tree.Count);
        }

        [Fact]
        public void Clear_EmptiesTheTree()
        {
            var tree = new AugmentedBinarySearchTree<int>(allowDuplicates: true);
            tree.Insert(new[] { 1, 2, 3, 4, 5 });

            tree.Clear();

            Assert.Equal(0, tree.Count);
        }

        #endregion
    }
}
