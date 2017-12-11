using Algorithms.Trees;
using DataStructures.Trees;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTest
{
    public class BinaryTreeRecursiveWalkerTests
    {
        // Construct a Simple Binary Search Tree 
        private static readonly BSTNode<int> _root = new BSTNode<int>(10)
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
            BinaryTreeRecursiveWalker.ForEach(_root, addToList, preOrder);
            Assert.True(list.ToArray() == new int[] { 3, 5, 7, 10, 13, 15 }, "Wrong traversal, expected InOrder enumeration of tree!");
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
            BinaryTreeRecursiveWalker.ForEach(_root, addToList, inOrder);
            Assert.True(list.ToArray() == new int[] { 10, 5, 3, 7, 15, 13, 17 }, "Wrong traversal, expected InOrder enumeration of tree!");
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
            BinaryTreeRecursiveWalker.ForEach(_root, addToList, postOrder);
            Assert.True(list.ToArray() == new int[] { 3, 7, 5, 13, 17, 15, 10 }, "Wrong traversal, expected InOrder enumeration of tree!");
        }

        #endregion

        #region <<Test Contains method>>

        [Fact]
        private static void TestContainReturnsTrue()
        {
            var values = new int[] { 10, 5, 3, 7, 15, 13, 17 };
            var preOrder = BinaryTreeRecursiveWalker.TraversalMode.PreOrder;
            var inOrder = BinaryTreeRecursiveWalker.TraversalMode.InOrder;
            var postOrder = BinaryTreeRecursiveWalker.TraversalMode.PostOrder;

            foreach (var value in values)
                Assert.True(BinaryTreeRecursiveWalker.Contains(_root, value, preOrder),
                    "Wrong boolean returned, expected True from Contains");

            foreach (var value in values)
                Assert.True(BinaryTreeRecursiveWalker.Contains(_root, value, inOrder),
                    "Wrong boolean returned, expected True from Contains");

            foreach (var value in values)
                Assert.True(BinaryTreeRecursiveWalker.Contains(_root, value, postOrder),
                    "Wrong boolean returned, expected True from Contains");
        }

        [Fact]
        private static void TestContainReturnsFalse()
        {
            var values = new int[] { 0, 20, 30, 40, 50 };
            var preOrder = BinaryTreeRecursiveWalker.TraversalMode.PreOrder;
            var inOrder = BinaryTreeRecursiveWalker.TraversalMode.InOrder;
            var postOrder = BinaryTreeRecursiveWalker.TraversalMode.PostOrder;

            foreach (var value in values)
                Assert.False(BinaryTreeRecursiveWalker.Contains(_root, value, preOrder),
                    "Wrong boolean returned, expected False from Contains");

            foreach (var value in values)
                Assert.False(BinaryTreeRecursiveWalker.Contains(_root, value, inOrder),
                    "Wrong boolean returned, expected False from Contains");

            foreach (var value in values)
                Assert.False(BinaryTreeRecursiveWalker.Contains(_root, value, postOrder),
                    "Wrong boolean returned, expected False from Contains");
        }

        #endregion

        #region <<Test Binary Search method>>

        [Fact]
        private static void TestBinarySearchReturnsTrue()
        {
            var values = new int[] { 10, 5, 3, 7, 15, 13, 17 };
            var preOrder = BinaryTreeRecursiveWalker.TraversalMode.PreOrder;
            var inOrder = BinaryTreeRecursiveWalker.TraversalMode.InOrder;
            var postOrder = BinaryTreeRecursiveWalker.TraversalMode.PostOrder;

            foreach (var value in values)
                Assert.True(BinaryTreeRecursiveWalker.BinarySearch(_root, value, preOrder),
                    "Wrong boolean returned, expected True from Contains");

            foreach (var value in values)
                Assert.True(BinaryTreeRecursiveWalker.BinarySearch(_root, value, inOrder),
                    "Wrong boolean returned, expected True from Contains");

            foreach (var value in values)
                Assert.True(BinaryTreeRecursiveWalker.BinarySearch(_root, value, postOrder),
                    "Wrong boolean returned, expected True from Contains");
        }

        [Fact]
        private static void TestBinarySearchReturnsFalse()
        {
            var values = new int[] { 0, 20, 30, 40, 50 };
            var preOrder = BinaryTreeRecursiveWalker.TraversalMode.PreOrder;
            var inOrder = BinaryTreeRecursiveWalker.TraversalMode.InOrder;
            var postOrder = BinaryTreeRecursiveWalker.TraversalMode.PostOrder;

            foreach (var value in values)
                Assert.False(BinaryTreeRecursiveWalker.BinarySearch(_root, value, preOrder),
                    "Wrong boolean returned, expected False from Contains");

            foreach (var value in values)
                Assert.False(BinaryTreeRecursiveWalker.BinarySearch(_root, value, inOrder),
                    "Wrong boolean returned, expected False from Contains");

            foreach (var value in values)
                Assert.False(BinaryTreeRecursiveWalker.BinarySearch(_root, value, postOrder),
                    "Wrong boolean returned, expected False from Contains");
        }

        #endregion
    }
}
