using System;
using System.Diagnostics;
using System.Collections.Generic;

using Algorithms.Trees;
using DataStructures.Trees;

namespace C_Sharp_Algorithms.AlgorithmsTests
{
    public static class BinaryTreeRecursiveWalkerTests
    {
        public static void DoTest()
        {
            // Construct a Simple Binary Search Tree 
            var root = new BSTNode<int>(10) {
                LeftChild = new BSTNode<int>(5) {
                    LeftChild = new BSTNode<int>(3),
                    RightChild = new BSTNode<int>(7)
                },
                RightChild = new BSTNode<int>(15) {
                    LeftChild = new BSTNode<int>(13),
                    RightChild = new BSTNode<int>(17)
                }
            };

            // Test Tree Traversal methods
            BinaryTreeRecursiveWalkerTests.Test_PreOrder_Traversal(root);
            BinaryTreeRecursiveWalkerTests.Test_InOrder_Traversal(root);
            BinaryTreeRecursiveWalkerTests.Test_PostOrder_Traversal(root);

            // Test Contains method
            BinaryTreeRecursiveWalkerTests.Test_Contain_Returns_True(root, new int[] { 10, 5, 3, 7, 15, 13, 17 });
            BinaryTreeRecursiveWalkerTests.Test_Contain_Returns_False(root, new int[] { 0, 20, 30, 40, 50 });

            // Test Binary Search method
            BinaryTreeRecursiveWalkerTests.Test_Binary_Search_Returns_True(root, new int [] { 10, 5, 3, 7, 15, 13, 17 });
            BinaryTreeRecursiveWalkerTests.Test_Binary_Search_Returns_False(root, new int[] { 0, 20, 30, 40, 50 });
        }

        private static void Test_PreOrder_Traversal(BSTNode<int> root)
        {
            var preOrder = BinaryTreeRecursiveWalker.TraversalMode.PreOrder;

            // List to contain items
            List<int> list = new List<int>();

            // ForEach Action
            var addToList = new Action<int>(list.Add);

            // Assert the fact that adding items PRE-ORDER will result in [3, 5, 7, 10, 13, 15, 17]
            BinaryTreeRecursiveWalker.ForEach(root, addToList, preOrder);
            
            Debug.Assert(
                list.ToArray() == new int[] { 3, 5, 7, 10, 13, 15 }, 
                "Wrong traversal, expected InOrder enumeration of tree!");
        }

        private static void Test_InOrder_Traversal(BSTNode<int> root)
        {
            var inOrder = BinaryTreeRecursiveWalker.TraversalMode.InOrder;

            // List to contain items
            var list = new List<int>();

            // ForEach Action
            var addToList = new Action<int>(list.Add);

            // Assert the fact that adding items IN-ORDER will result in [10, 5, 3, 7, 15, 13, 17]
            BinaryTreeRecursiveWalker.ForEach(root, addToList, inOrder);
            
            Debug.Assert(
                list.ToArray() == new int[] { 10, 5, 3, 7, 15, 13, 17 }, 
                "Wrong traversal, expected InOrder enumeration of tree!");
        }

        private static void Test_PostOrder_Traversal(BSTNode<int> root)
        {
            var postOrder = BinaryTreeRecursiveWalker.TraversalMode.PostOrder;

            // List to contain items
            List<int> list = new List<int>();

            // ForEach Action
            var addToList = new Action<int>(list.Add);

            // Assert the fact that adding items POST-ORDER will result in [3, 7, 5, 13, 17, 15, 10]
            BinaryTreeRecursiveWalker.ForEach(root, addToList, postOrder);
            
            Debug.Assert(
                list.ToArray() == new int[] { 3, 7, 5, 13, 17, 15, 10 }, 
                "Wrong traversal, expected InOrder enumeration of tree!");
        }

        private static void Test_Contain_Returns_True(BSTNode<int> root, int[] values)
        {
            var preOrder = BinaryTreeRecursiveWalker.TraversalMode.PreOrder;
            var inOrder = BinaryTreeRecursiveWalker.TraversalMode.InOrder;
            var postOrder = BinaryTreeRecursiveWalker.TraversalMode.PostOrder;

            foreach (var value in values)
                Debug.Assert(
                    true == BinaryTreeRecursiveWalker.Contains(root, value, preOrder), 
                    "Wrong boolean returned, expected True from Contains");

            foreach (var value in values)
                Debug.Assert(
                    true == BinaryTreeRecursiveWalker.Contains(root, value, inOrder), 
                    "Wrong boolean returned, expected True from Contains");

            foreach (var value in values)
                Debug.Assert(
                    true == BinaryTreeRecursiveWalker.Contains(root, value, postOrder), 
                    "Wrong boolean returned, expected True from Contains");
        }

        private static void Test_Contain_Returns_False(BSTNode<int> root, int[] values)
        {
            var preOrder = BinaryTreeRecursiveWalker.TraversalMode.PreOrder;
            var inOrder = BinaryTreeRecursiveWalker.TraversalMode.InOrder;
            var postOrder = BinaryTreeRecursiveWalker.TraversalMode.PostOrder;

            foreach (var value in values)
                Debug.Assert(
                    false == BinaryTreeRecursiveWalker.Contains(root, value, preOrder), 
                    "Wrong boolean returned, expected False from Contains");

            foreach (var value in values)
                Debug.Assert(
                    false == BinaryTreeRecursiveWalker.Contains(root, value, inOrder), 
                    "Wrong boolean returned, expected False from Contains");

            foreach (var value in values)
                Debug.Assert(
                    false == BinaryTreeRecursiveWalker.Contains(root, value, postOrder), 
                    "Wrong boolean returned, expected False from Contains");
        }

        private static void Test_Binary_Search_Returns_True(BSTNode<int> root, int[] values)
        {
            var preOrder = BinaryTreeRecursiveWalker.TraversalMode.PreOrder;
            var inOrder = BinaryTreeRecursiveWalker.TraversalMode.InOrder;
            var postOrder = BinaryTreeRecursiveWalker.TraversalMode.PostOrder;

            foreach (var value in values)
            {
                Debug.Assert(
                    true == BinaryTreeRecursiveWalker.BinarySearch(root, value, preOrder), 
                    "Wrong boolean returned, expected True from Contains");
            }

            foreach (var value in values)
                Debug.Assert(
                    true == BinaryTreeRecursiveWalker.BinarySearch(root, value, inOrder), 
                    "Wrong boolean returned, expected True from Contains");

            foreach (var value in values)
                Debug.Assert(
                    true == BinaryTreeRecursiveWalker.BinarySearch(root, value, postOrder), 
                    "Wrong boolean returned, expected True from Contains");
        }

        private static void Test_Binary_Search_Returns_False(BSTNode<int> root, int[] values)
        {
            var preOrder = BinaryTreeRecursiveWalker.TraversalMode.PreOrder;
            var inOrder = BinaryTreeRecursiveWalker.TraversalMode.InOrder;
            var postOrder = BinaryTreeRecursiveWalker.TraversalMode.PostOrder;

            foreach (var value in values)
                Debug.Assert(
                    false == BinaryTreeRecursiveWalker.BinarySearch(root, value, preOrder), 
                    "Wrong boolean returned, expected False from Contains");
            
            foreach (var value in values)
                Debug.Assert(
                    false == BinaryTreeRecursiveWalker.BinarySearch(root, value, inOrder), 
                    "Wrong boolean returned, expected False from Contains");

            foreach (var value in values)
                Debug.Assert(
                    false == BinaryTreeRecursiveWalker.BinarySearch(root, value, postOrder), 
                    "Wrong boolean returned, expected False from Contains");
        }
    }
}

