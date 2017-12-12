using System;
using System.Diagnostics;
using System.Collections.Generic;

using DataStructures.Trees;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class AVLTreeTest
    {
        [Fact]
        public static void DoTest()
        {
            AVLTree<int> avlTree = new AVLTree<int>();

            //
            // CASE #1
            // Insert: 4, 5, 7
            // SIMPLE *left* rotation for node 4.
            //
            /***************************************
             ** UNBALANCED    ===>    BALANCED
             **     4                   5
             **      \                 / \
             **       5       ===>    4   7
             **        \
             **         7
             **
             ***************************************
             */
            avlTree.Insert(4); // insert
            avlTree.Insert(5); // insert
            avlTree.Insert(7); // insert

            // ASSERT CAST 1;
            AssertCase_1(avlTree);


            //
            // CASE #2
            // Insert to the previous tree: 2 and then 1.
            // SIMPLE *right* rotation for node 4.
            //
            /*********************************************
             ** UNBALANCED    ===>    BALANCED
             **        5                 5
             **       / \               / \
             **      4   7    ===>     2   7
             **     /                 / \
             **    2                 1   4
             **   /
             **  1
             **
             *********************************************
             */
            avlTree.Insert(2); // insert
            avlTree.Insert(1); // insert

            // ASSERT CASE 2
            AssertCase_2(avlTree);


            //
            // CASE #3
            // Insert to the previous tree: 3.
            // DOUBLE *right* rotation for node 5.
            //
            // The double rotation is achieved by:
            // 1> Simple *left* rotation for node 2, and then
            // 2> Simple *right* rotation for node 5
            //
            /*************************************
             ** UNBALANCED     ===>    TRANSITION (1st R)    ===>    BALANCED (2nd Rt)
             **        5                      5                          4
             **       / \                    / \                        / \
             **      2   7     ===>         4   7        ===>          2   5
             **     / \                    /                          / \   \
             **    1   4                  2                          1   3   7
             **       /                  / \
             **      3                  1   3
             **
             *************************************
             */
            avlTree.Insert(3); // insert

            // ASSERT CASE 3
            AssertCase_3(avlTree);


            //
            // CASE #4
            // Insert to the previous tree: 6.
            // DOUBLE *right* rotation for node 5.
            //
            // The double rotation is achieved by:
            // 1> Simple *right* rotation for node 7, and then
            // 2> Simple *left* rotation for node 5
            //
            /**************************************************************************
             ** UNBALANCED     ===>    TRANSITION (1st R)    ===>    BALANCED (2nd Rt)
             **        4                      4                          ..4..
             **       / \                    / \                        /     \
             **      2   5     ===>         2   5         ===>         2       6
             **     / \   \                / \   \                    / \     / \
             **    1   3   7              1   3   6                  1   3   5   7
             **           /                        \
             **          6                          7
             **
             **************************************************************************
             */
            avlTree.Insert(6); // insert

            // ASSERT CASE 4
            AssertCase_4(avlTree);


            //
            // CASE #5
            // REMOVE the tree's root: 4.
            //
            /**************************************************************************
             ** UNBALANCED     ===>    TRANSITION (1st R)    ===>    BALANCED (2nd Rt)
             **       null                                              .6..
             **      /   \                                             /    \
             **     2     6    ===>                      ===>         2      7
             **    / \   / \                                         / \    /
             **   1   3 5   7                                       1   3  5
             **
             **************************************************************************
             */
            avlTree.Remove(avlTree.Root.Value); // REMOVE 4

            // ASSERT CASE 5
            AssertCase_5(avlTree);


            //
            // CLEAR THE TREE AND START OVER
            // Compare two binary trees with each other (height-wise) using bulk-inserts

            avlTree = new AVLTree<int>();
            var bsTree = new BinarySearchTree<int>();

            List<int> treeDataList = new List<int>() { 15, 25, 5, 12, 1, 16, 20, 9, 9, 7, 7, -1, 11, 19, 30, 8, 10, 13, 28, 39 };
            avlTree.Insert(treeDataList);
            bsTree.Insert(treeDataList);

            int avlTreeHeight = avlTree.Height;
            int bsTreeHeight = bsTree.Height;

            Assert.True(avlTreeHeight < bsTreeHeight, "Wrong heights. AVL Tree must be shorted than BS Tree.");

            treeDataList = new List<int>() { 15, 25, 5, 12, 1, 9, 7, -1, 11, 30, 8, 10, 13, 28, 39 };
            avlTree.Clear();
            avlTree.Insert(treeDataList);

            //
            // OUTPUT OF AVL TREE DRAWER
            /**
             **     ....9...
             **    /        \
             **    5       .12.
             **   / \     /    \
             **  1   7   11    25
             **  /   \   /    /  \
             ** -1    8 10   15   30
             **              /   / \
             **             13  28 39
             */

        }//end-do-test


        //
        // DEBUG.ASSERT <- CASE 1
        private static void AssertCase_1(AVLTree<int> avlTree)
        {
            var avlRoot = avlTree.Root;
            Assert.True(avlRoot.Value == 5, "Wrong root.");
            Assert.True(avlRoot.LeftChild.Value == 4, "Wrong left child.");
            Assert.True(avlRoot.RightChild.Value == 7, "Wrong right child.");

            Assert.True(
                avlRoot.LeftChild.Height == 0
                && avlRoot.RightChild.Height == 0
                ,
                "Wrong heights at the leaf nodes!.");
        }

        // DEBUG.ASSERT <- CASE 2
        private static void AssertCase_2(AVLTree<int> avlTree)
        {
            var avlRoot = avlTree.Root;
            Assert.True(avlRoot.Value == 5, "Wrong root.");
            Assert.True(avlRoot.Height == 2, "Wrong root height!");
            Assert.True(avlRoot.LeftChild.Value == 2, "Wrong left child from root.");
            Assert.True(avlRoot.RightChild.Value == 7, "Wrong right child from root.");
            Assert.True(avlRoot.LeftChild.LeftChild.Value == 1, "Wrong value at {root->left->left}.");
            Assert.True(avlRoot.LeftChild.RightChild.Value == 4, "Wrong value at {root->left->right}.");

            Assert.True(
                avlRoot.LeftChild.LeftChild.Height == 0
                && avlRoot.LeftChild.RightChild.Height == 0
                && avlRoot.RightChild.Height == 0
                ,
                "Wrong heights at the leaf nodes!.");
        }

        // DEBUG.ASSERT <- CASE 3
        private static void AssertCase_3(AVLTree<int> avlTree)
        {
            var avlRoot = avlTree.Root;
            Assert.True(avlRoot.Height == 2, "Wrong root height!");
            Assert.True(avlRoot.Value == 4, "Wrong root.");
            Assert.True(avlRoot.LeftChild.Value == 2, "Wrong left child from root.");
            Assert.True(avlRoot.RightChild.Value == 5, "Wrong right child from root.");
            Assert.True(avlRoot.LeftChild.LeftChild.Value == 1, "Wrong value at {root->left->left}.");
            Assert.True(avlRoot.LeftChild.RightChild.Value == 3, "Wrong value at {root->left->right}.");
            Assert.True(avlRoot.RightChild.RightChild.Value == 7, "Wrong value at {root->right->right}.");

            Assert.True(
                avlRoot.LeftChild.LeftChild.Height == 0
                && avlRoot.LeftChild.RightChild.Height == 0
                && avlRoot.RightChild.RightChild.Height == 0
                ,
                "Wrong heights at the leaf nodes!.");
        }

        // DEBUG.ASSERT <- CASE 4
        private static void AssertCase_4(AVLTree<int> avlTree)
        {
            var avlRoot = avlTree.Root;
            Assert.True(avlRoot.Height == 2, "Wrong root height!");
            Assert.True(avlRoot.Value == 4, "Wrong root.");
            Assert.True(avlRoot.LeftChild.Value == 2, "Wrong left child from root.");
            Assert.True(avlRoot.RightChild.Value == 6, "Wrong right child from root.");
            Assert.True(avlRoot.LeftChild.LeftChild.Value == 1, "Wrong value at {root->left->left}.");
            Assert.True(avlRoot.LeftChild.RightChild.Value == 3, "Wrong value at {root->left->right}.");
            Assert.True(avlRoot.RightChild.LeftChild.Value == 5, "Wrong value at {root->right->left}.");
            Assert.True(avlRoot.RightChild.RightChild.Value == 7, "Wrong value at {root->right->right}.");

            Assert.True(
                avlRoot.LeftChild.LeftChild.Height == 0
                && avlRoot.LeftChild.RightChild.Height == 0
                && avlRoot.RightChild.LeftChild.Height == 0
                && avlRoot.RightChild.RightChild.Height == 0
                ,
                "Wrong heights at the leaf nodes!.");
        }

        // DEBUG.ASSERT <- CASE 5
        private static void AssertCase_5(AVLTree<int> avlTree)
        {
            var avlRoot = avlTree.Root;
            Assert.True(avlRoot.Height == 2, "Wrong root height!");
            Assert.True(avlRoot.Value == 6, "Wrong root.");
            Assert.True(avlRoot.LeftChild.Value == 2, "Wrong left child from root.");
            Assert.True(avlRoot.RightChild.Value == 7, "Wrong right child from root.");
            Assert.True(avlRoot.LeftChild.LeftChild.Value == 1, "Wrong value at {root->left->left}.");
            Assert.True(avlRoot.LeftChild.RightChild.Value == 3, "Wrong value at {root->left->right}.");
            Assert.True(avlRoot.RightChild.LeftChild.Value == 5, "Wrong value at {root->right->left}.");
            Assert.True(avlRoot.RightChild.RightChild == null, "Wrong value at {root->right->right}.");

            Assert.True(
                avlRoot.LeftChild.LeftChild.Height == 0
                && avlRoot.LeftChild.RightChild.Height == 0
                && avlRoot.RightChild.LeftChild.Height == 0
                ,
                "Wrong heights at the leaf nodes!.");

        }
    }

}
