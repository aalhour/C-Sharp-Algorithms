using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

using DataStructures.Trees;

namespace C_Sharp_Algorithms
{
    public static class BinarySearchTreeTest
    {
        public static void DoTest()
        {
            var binarySearchTree = new AugmentedBinarySearchTree<int>(allowDuplicates: true);

            //
            // FIRST TEST TREE WITH DUPLICATES ELEMENTS
            Assert_Tree_With_Duplicates_Elements(ref binarySearchTree);

            // NEXT TEST TREE THAT DOES NOT ALLOW DUPLICATES
            Assert_Tree_With_Unique_Elements(ref binarySearchTree);

            // Wait
            Console.ReadLine();
        }


        private static void Assert_Tree_With_Duplicates_Elements(ref AugmentedBinarySearchTree<int> binarySearchTree)
        {
            // New tree which doesn't allow duplicates
            binarySearchTree = new AugmentedBinarySearchTree<int>(allowDuplicates: true);

            int[] values = new int[21] { 15, 25, 5, 12, 1, 16, 20, 9, 9, 7, 7, 7, -1, 11, 19, 30, 8, 10, 13, 28, 39 };

            // Insert values with duplicates
            binarySearchTree.Insert(values);

            // ASSERT COUNT = 20 (allows duplicates)
            Debug.Assert(binarySearchTree.Count == 21);

            // Test contains/find
            Debug.Assert(binarySearchTree.Contains(10) == true, "Wrong element.");

            // Test find all
            var list = binarySearchTree.FindAll(element => element > 15).ToList();
            Debug.Assert(list.Count == 7, "Wrong FindAll result!");

            // test sort
            List<int> sortedList = binarySearchTree.ToList();
            for (int i = 1; i < sortedList.Count; ++i)
                Debug.Assert(sortedList[i - 1] <= sortedList[i], "BST sort is wrong!");

            // ASSERT MIN ITEM
            Debug.Assert(binarySearchTree.FindMin() == -1, "Min is wrong.");

            // ASSERT MAX ITEM
            Debug.Assert(binarySearchTree.FindMax() == 39, "Max is wrong.");

            // Remove min & max
            binarySearchTree.RemoveMin();
            binarySearchTree.RemoveMax();

            // ASSERT MIN AFTER REMOVE-MIN
            Debug.Assert(binarySearchTree.FindMin() == 1, "Min is wrong.");

            // ASSERT MAX AFTER REMOVE MAX
            Debug.Assert(binarySearchTree.FindMax() == 30, "Max is wrong.");

            // Remove min twice
            binarySearchTree.RemoveMin();
            binarySearchTree.RemoveMin();

            // ASSERT MIN
            Debug.Assert(binarySearchTree.FindMin() == 7, "Min is wrong.");

            // 7 STILL EXISTS BECAUSE IT WAS DUPLICATED
            binarySearchTree.RemoveMin();
            Debug.Assert(binarySearchTree.FindMin() == 7, "Min is wrong.");

            // Remove max thrice
            binarySearchTree.RemoveMax();
            binarySearchTree.RemoveMax();
            binarySearchTree.RemoveMax();

            // ASSERT MAX AFTER REMOVE-MAX 3 TIMES
            Debug.Assert(binarySearchTree.FindMax() == 20, "Max is wrong.");

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

            var enumerator = binarySearchTree.GetInOrderEnumerator();
            enumerator.MoveNext();
            Debug.Assert(enumerator.Current == 7);

            enumerator.MoveNext();
            enumerator.MoveNext();
            Debug.Assert(enumerator.Current == 8, "Wrong in-order enumeration.");
        }


        private static void Assert_Tree_With_Unique_Elements(ref AugmentedBinarySearchTree<int> binarySearchTree)
        {
            // New tree which doesn't allow duplicates
            binarySearchTree = new AugmentedBinarySearchTree<int>(allowDuplicates: false);

            int[] values = new int[24] { 14, 15, 25, 5, 12, 1, 16, 20, 9, 9, 9, 7, 7, 7, -1, 11, 19, 30, 8, 10, 13, 28, 39, 39 };

            var inserting_duplicates_passed = true;
            try
            {
                // Insert values with duplicates
                binarySearchTree.Insert(values);
                inserting_duplicates_passed = true;
            }
            catch
            {
                inserting_duplicates_passed = false;
            }

            Debug.Assert(inserting_duplicates_passed == false, "Fail! Tree doesn't allow duplicates");


            //
            // Reduce values array to an array of distinct values
            binarySearchTree.Clear();
            values = values.Distinct().ToArray();

            var inserting_unique_passed = true;
            try
            {
                // Insert values with duplicates
                binarySearchTree.Insert(values);
                inserting_duplicates_passed = true;
            }
            catch
            {
                inserting_duplicates_passed = false;
            }

            Debug.Assert(inserting_unique_passed == true, "Fail! Inserting unique elements should pass!");

            // ASSERT COUNT
            Debug.Assert(binarySearchTree.Count == values.Length);
        }

    }

}

