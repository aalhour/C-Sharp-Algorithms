using System;
using System.Diagnostics;
using System.Collections.Generic;

using DataStructures;

namespace C_Sharp_Algorithms
{
    public static class BinarySearchTreeTest
    {
        public static void DoTest()
        {
            BinarySearchTree<int> binarySearchTree = new BinarySearchTree<int>();

            binarySearchTree.Insert(15);
            binarySearchTree.Insert(25);
            binarySearchTree.Insert(5);
            binarySearchTree.Insert(12);
            binarySearchTree.Insert(1);
            binarySearchTree.Insert(16);
            binarySearchTree.Insert(20);
            binarySearchTree.Insert(9);
            binarySearchTree.Insert(9);
            binarySearchTree.Insert(7);
            binarySearchTree.Insert(7);
            binarySearchTree.Insert(-1);
            binarySearchTree.Insert(11);
            binarySearchTree.Insert(19);
            binarySearchTree.Insert(30);
            binarySearchTree.Insert(8);
            binarySearchTree.Insert(10);
            binarySearchTree.Insert(13);
            binarySearchTree.Insert(28);
            binarySearchTree.Insert(39);

            // test tree visualization
            //binarySearchTree.VisualizeTree();
            //Console.ReadLine();

            // Test find
            var found = binarySearchTree.Find(10);
            Debug.Assert(found == 10, "Wrong element.");

            var rankOf9 = binarySearchTree.Rank(9);
            Debug.Assert(rankOf9 == 5, "Wrong rank value!");

            // Test find all
            var list = binarySearchTree.FindAll(element => element > 15);
            Debug.Assert(list.Count == 7, "Wrong FindAll result!");

            // test sort
            List<int> sortedList = binarySearchTree.BSTSort();
            int first = 0, mid = sortedList.Count / 2, last = sortedList.Count - 1;
            bool sortCheck = (sortedList[first] <= sortedList[mid]) && (sortedList[mid] <= sortedList[last]);
            Debug.Assert(sortCheck == true, "BST sort is wrong!");

            // Test min & max
            var min = binarySearchTree.FindMin();
            Debug.Assert(min == -1, "Min is wrong.");

            var max = binarySearchTree.FindMax();
            Debug.Assert(max == 39, "Max is wrong.");

            // Remove min & max
            binarySearchTree.RemoveMin();
            binarySearchTree.RemoveMax();

            min = binarySearchTree.FindMin();
            Debug.Assert(min == 1, "Min is wrong.");

            max = binarySearchTree.FindMax();
            Debug.Assert(max == 30, "Max is wrong.");

            // Remove min twice
            binarySearchTree.RemoveMin();
            binarySearchTree.RemoveMin();

            min = binarySearchTree.FindMin();
            Debug.Assert(min == 7, "Min is wrong.");

            // Remove max thrice
            binarySearchTree.RemoveMax();
            binarySearchTree.RemoveMax();
            binarySearchTree.RemoveMax();

            max = binarySearchTree.FindMax();
            Debug.Assert(max == 20, "Max is wrong.");

            // Test removing an element with subtrees
            try
            {
                // doesn't exist!
                binarySearchTree.Remove(1000);
            }
            catch (Exception)
            {
                // does exist!
                binarySearchTree.Remove(16);
            }


            // Clear
            binarySearchTree.Clear();

            // Test adding lists
            List<int> numbersList = new List<int> { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
            binarySearchTree.Insert(numbersList);
            Debug.Assert(binarySearchTree.Count() == numbersList.Count, "Insertion Failed!");
        }

    }

}

