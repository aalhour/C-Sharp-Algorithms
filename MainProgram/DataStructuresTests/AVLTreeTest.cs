using System;
using System.Diagnostics;
using System.Collections.Generic;

using DataStructures.Trees;

namespace C_Sharp_Algorithms.DataStructuresTests
{
    public static class AVLTreeTest
    {
        public static void DoTest()
        {
            AVLTreeNode<int> avlRoot;
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
            AssertCase1(avlTree);


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
            AssertCase2(avlTree);


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
            AssertCase3(avlTree);


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
             **        4                      4                           4
             **       / \                    / \                        /   \
             **      2   5     ===>         2   5         ===>         2     6
             **     / \   \                / \   \                    / \   / \
             **    1   3   7              1   3   6                  1   3 5   7
             **           /                        \
             **          6                          7
             **
             **************************************************************************
             */
            avlTree.Insert(6); // insert

            // ASSERT CASE 4
            AssertCase4(avlTree);


            //
            // CLEAR THE TREE AND START OVER
            avlTree = new AVLTree<int>();
            var bsTree = new BinarySearchTree<int>();

            List<int> list = new List<int>() { 15, 25, 5, 12, 1, 16, 20, 9, 9, 7, 7, -1, 11, 19, 30, 8, 10, 13, 28, 39 };
            avlTree.Insert(list);
            bsTree.Insert(list);

            int avlTreeHeight = avlTree.Height();
            int bsTreeHeight = bsTree.Height();

            Debug.Assert(avlTreeHeight < bsTreeHeight, "Wrong heights. AVL Tree must be shorted than BS Tree.");
        }


        //
        // DEBUG.ASSERT <- CASE 1
        private static void AssertCase1(AVLTree<int> avlTree)
        {
            var avlRoot = avlTree.Root;
            Debug.Assert(avlRoot.Value == 5, "Wrong root.");
            Debug.Assert(avlRoot.LeftChild.Value == 4, "Wrong left child.");
            Debug.Assert(avlRoot.RightChild.Value == 7, "Wrong right child.");

            Debug.Assert(
                avlRoot.LeftChild.Height == 0
                && avlRoot.RightChild.Height == 0
                ,
                "Wrong heights at the leaf nodes!.");
        }


        //
        // DEBUG.ASSERT <- CASE 2
        private static void AssertCase2(AVLTree<int> avlTree)
        {
            var avlRoot = avlTree.Root;
            Debug.Assert(avlRoot.Value == 5, "Wrong root.");
            Debug.Assert(avlRoot.Height == 2, "Wrong root height!");
            Debug.Assert(avlRoot.LeftChild.Value == 2, "Wrong left child from root.");
            Debug.Assert(avlRoot.RightChild.Value == 7, "Wrong right child from root.");
            Debug.Assert(avlRoot.LeftChild.LeftChild.Value == 1, "Wrong value at {root->left->left}.");
            Debug.Assert(avlRoot.LeftChild.RightChild.Value == 4, "Wrong value at {root->left->right}.");

            Debug.Assert(
                avlRoot.LeftChild.LeftChild.Height == 0
                && avlRoot.LeftChild.RightChild.Height == 0
                && avlRoot.RightChild.Height == 0
                ,
                "Wrong heights at the leaf nodes!.");
        }


        //
        // DEBUG.ASSERT <- CASE 3
        private static void AssertCase3(AVLTree<int> avlTree)
        {
            var avlRoot = avlTree.Root;
            Debug.Assert(avlRoot.Height == 2, "Wrong root height!");
            Debug.Assert(avlRoot.Value == 4, "Wrong root.");
            Debug.Assert(avlRoot.LeftChild.Value == 2, "Wrong left child from root.");
            Debug.Assert(avlRoot.RightChild.Value == 5, "Wrong right child from root.");
            Debug.Assert(avlRoot.LeftChild.LeftChild.Value == 1, "Wrong value at {root->left->left}.");
            Debug.Assert(avlRoot.LeftChild.RightChild.Value == 3, "Wrong value at {root->left->right}.");
            Debug.Assert(avlRoot.RightChild.RightChild.Value == 7, "Wrong value at {root->right->right}.");

            Debug.Assert(
                avlRoot.LeftChild.LeftChild.Height == 0
                && avlRoot.LeftChild.RightChild.Height == 0
                && avlRoot.RightChild.RightChild.Height == 0
                ,
                "Wrong heights at the leaf nodes!.");
        }


        //
        // DEBUG.ASSERT <- CASE 4
        private static void AssertCase4(AVLTree<int> avlTree)
        {
            var avlRoot = avlTree.Root;
            Debug.Assert(avlRoot.Height == 2, "Wrong root height!");
            Debug.Assert(avlRoot.Value == 4, "Wrong root.");
            Debug.Assert(avlRoot.LeftChild.Value == 2, "Wrong left child from root.");
            Debug.Assert(avlRoot.RightChild.Value == 6, "Wrong right child from root.");
            Debug.Assert(avlRoot.LeftChild.LeftChild.Value == 1, "Wrong value at {root->left->left}.");
            Debug.Assert(avlRoot.LeftChild.RightChild.Value == 3, "Wrong value at {root->left->right}.");
            Debug.Assert(avlRoot.RightChild.LeftChild.Value == 5, "Wrong value at {root->right->left}.");
            Debug.Assert(avlRoot.RightChild.RightChild.Value == 7, "Wrong value at {root->right->right}.");

            Debug.Assert(
                avlRoot.LeftChild.LeftChild.Height == 0
                && avlRoot.LeftChild.RightChild.Height == 0
                && avlRoot.RightChild.LeftChild.Height == 0
                && avlRoot.RightChild.RightChild.Height == 0
                ,
                "Wrong heights at the leaf nodes!.");
        }

    }

}
