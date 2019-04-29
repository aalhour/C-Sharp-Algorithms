using Algorithms.Trees;
using DataStructures.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public static class BinaryTreeRecursiveWalkerTests
    {
        // Construct a Simple Binary Search Tree
        private static readonly BSTNode<int> Root = new BSTNode<int>(10)
        {
            LeftChild = new BSTNode<int>(5)
            {
                LeftChild = new BSTNode<int>(3),
                RightChild = new BSTNode<int>(7)
            },
            RightChild = new BSTNode<int>(15)
            {
                LeftChild = new BSTNode<int>(13),
                RightChild = new BSTNode<int>(17)
            }
        };

        #region <<Test Tree Traversal methods>>

        [Fact]
        private static void TestPreOrderTraversal()
        {
            var preOrder = BinaryTreeRecursiveWalker.TraversalMode.PreOrder;

            // List to contain items
            List<int> list = new List<int>();

            // ForEach Action
            var addToList = new Action<int>(list.Add);

            // Assert the fact that adding items PRE-ORDER will result in [3, 5, 7, 10, 13, 15, 17]
            BinaryTreeRecursiveWalker.ForEach(Root, addToList, preOrder);
            var expectedArray = new[] { 10, 5, 3, 7, 15, 13, 17 };
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != expectedArray[i])
                {
                    throw new InvalidOperationException("Wrong traversal, expected PreOrder enumeration of tree!");
                }
            }
        }

        [Fact]
        private static void TestInOrderTraversal()
        {
            var inOrder = BinaryTreeRecursiveWalker.TraversalMode.InOrder;

            // List to contain items
            var list = new List<int>();

            // ForEach Action
            var addToList = new Action<int>(list.Add);

            // Assert the fact that adding items IN-ORDER will result in [10, 5, 3, 7, 15, 13, 17]
            BinaryTreeRecursiveWalker.ForEach(Root, addToList, inOrder);

            var expectedArray = new[] { 3, 5, 7, 10, 13, 15, 17 };
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != expectedArray[i])
                {
                    throw new InvalidOperationException("Wrong traversal, expected InOrder enumeration of tree!");
                }
            }
        }

        [Fact]
        private static void TestPostOrderTraversal()
        {
            var postOrder = BinaryTreeRecursiveWalker.TraversalMode.PostOrder;

            // List to contain items
            List<int> list = new List<int>();

            // ForEach Action
            var addToList = new Action<int>(list.Add);

            // Assert the fact that adding items POST-ORDER will result in [3, 7, 5, 13, 17, 15, 10]
            BinaryTreeRecursiveWalker.ForEach(Root, addToList, postOrder);
            Assert.True(list.SequenceEqual(new[] { 3, 7, 5, 13, 17, 15, 10 }),
                "Wrong traversal, expected InOrder enumeration of tree!");
        }

        #endregion

        #region <<Test Contains method>>

        [Fact]
        private static void TestContainReturnsTrue()
        {
            var values = new[] { 10, 5, 3, 7, 15, 13, 17 };
            var preOrder = BinaryTreeRecursiveWalker.TraversalMode.PreOrder;
            var inOrder = BinaryTreeRecursiveWalker.TraversalMode.InOrder;
            var postOrder = BinaryTreeRecursiveWalker.TraversalMode.PostOrder;

            foreach (var value in values)
                Assert.True(BinaryTreeRecursiveWalker.Contains(Root, value, preOrder),
                    "Wrong boolean returned, expected True from Contains");

            foreach (var value in values)
                Assert.True(BinaryTreeRecursiveWalker.Contains(Root, value, inOrder),
                    "Wrong boolean returned, expected True from Contains");

            foreach (var value in values)
                Assert.True(BinaryTreeRecursiveWalker.Contains(Root, value, postOrder),
                    "Wrong boolean returned, expected True from Contains");
        }

        [Fact]
        private static void TestContainReturnsFalse()
        {
            var values = new[] { 0, 20, 30, 40, 50 };
            var preOrder = BinaryTreeRecursiveWalker.TraversalMode.PreOrder;
            var inOrder = BinaryTreeRecursiveWalker.TraversalMode.InOrder;
            var postOrder = BinaryTreeRecursiveWalker.TraversalMode.PostOrder;

            foreach (var value in values)
                Assert.False(BinaryTreeRecursiveWalker.Contains(Root, value, preOrder),
                    "Wrong boolean returned, expected False from Contains");

            foreach (var value in values)
                Assert.False(BinaryTreeRecursiveWalker.Contains(Root, value, inOrder),
                    "Wrong boolean returned, expected False from Contains");

            foreach (var value in values)
                Assert.False(BinaryTreeRecursiveWalker.Contains(Root, value, postOrder),
                    "Wrong boolean returned, expected False from Contains");
        }

        #endregion

        #region <<Test Binary Search method>>

        [Fact]
        private static void TestBinarySearchReturnsTrue()
        {
            var values = new[] { 10, 5, 3, 7, 15, 13, 17 };
            var preOrder = BinaryTreeRecursiveWalker.TraversalMode.PreOrder;
            var inOrder = BinaryTreeRecursiveWalker.TraversalMode.InOrder;
            var postOrder = BinaryTreeRecursiveWalker.TraversalMode.PostOrder;

            foreach (var value in values)
                Assert.True(BinaryTreeRecursiveWalker.BinarySearch(Root, value, preOrder),
                    "Wrong boolean returned, expected True from Contains");

            foreach (var value in values)
                Assert.True(BinaryTreeRecursiveWalker.BinarySearch(Root, value, inOrder),
                    "Wrong boolean returned, expected True from Contains");

            foreach (var value in values)
                Assert.True(BinaryTreeRecursiveWalker.BinarySearch(Root, value, postOrder),
                    "Wrong boolean returned, expected True from Contains");
        }

        [Fact]
        private static void TestBinarySearchReturnsFalse()
        {
            var values = new[] { 0, 20, 30, 40, 50 };
            var preOrder = BinaryTreeRecursiveWalker.TraversalMode.PreOrder;
            var inOrder = BinaryTreeRecursiveWalker.TraversalMode.InOrder;
            var postOrder = BinaryTreeRecursiveWalker.TraversalMode.PostOrder;

            foreach (var value in values)
                Assert.False(BinaryTreeRecursiveWalker.BinarySearch(Root, value, preOrder),
                    "Wrong boolean returned, expected False from Contains");

            foreach (var value in values)
                Assert.False(BinaryTreeRecursiveWalker.BinarySearch(Root, value, inOrder),
                    "Wrong boolean returned, expected False from Contains");

            foreach (var value in values)
                Assert.False(BinaryTreeRecursiveWalker.BinarySearch(Root, value, postOrder),
                    "Wrong boolean returned, expected False from Contains");
        }

        #endregion
    }
}