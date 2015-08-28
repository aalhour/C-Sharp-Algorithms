using System;
using System.Diagnostics;
using System.Collections.Generic;

using DataStructures.Trees;

namespace C_Sharp_Algorithms.DataStructuresTests
{
    public static class RedBlackTreeTest
    {
        public static void DoTest()
        {
            // Test against the worst case of insertion
            // Case: insert increasing numbers and check if it leads to a linked list
            var redBlackTree = new RedBlackTree<int>(allowDuplicates: false);

            redBlackTree.Insert(1);
            redBlackTree.Insert(2);
            redBlackTree.Insert(3);
            redBlackTree.Insert(4);
            redBlackTree.Insert(5);
            redBlackTree.Insert(6);
            redBlackTree.Insert(7);
            redBlackTree.Insert(8);
            redBlackTree.Insert(9);
            redBlackTree.Insert(10);

            Debug.Assert(redBlackTree.Height < redBlackTree.Count, "Fail! Tree doesn't rebalance against sorted elements!");

            Console.WriteLine("********************");
            Console.WriteLine(" [*] RED-BLACK TREE:\r\n");
            Console.WriteLine("********************");
            Console.WriteLine(redBlackTree.DrawTree());
            Console.WriteLine("\r\n");


            //
            // Test against re-shuffled insertions (not like above order)
            redBlackTree = new RedBlackTree<int>(allowDuplicates: false);

            redBlackTree.Insert(4);
            redBlackTree.Insert(5);
            redBlackTree.Insert(7);
            redBlackTree.Insert(2);
            redBlackTree.Insert(1);
            redBlackTree.Insert(3);
            redBlackTree.Insert(6);
            redBlackTree.Insert(0);
            redBlackTree.Insert(8);
            redBlackTree.Insert(10);
            redBlackTree.Insert(9);

            //
            // ASSERT INSERTING DUPLICATES WOULD BREAK
            var insert_duplicate_passed = true;
            try
            {
                // 2 already exists in tree
                redBlackTree.Insert(2);
                insert_duplicate_passed = true;
            }
            catch
            {
                insert_duplicate_passed = false;
            }

            Debug.Assert(insert_duplicate_passed == false, "Fail! The tree doesn't allow duplicates");


            //
            // PRINT TREE
            Console.WriteLine("********************");
            Console.WriteLine(" [*] RED-BLACK TREE:\r\n");
            Console.WriteLine("********************");
            Console.WriteLine(redBlackTree.DrawTree());
            Console.WriteLine("\r\n");


            // Assert count
            Debug.Assert(redBlackTree.Count == 11);

            // Assert existence and nonexistence of some items
            Debug.Assert(redBlackTree.Contains(1) == true);
            Debug.Assert(redBlackTree.Contains(3) == true);
            Debug.Assert(redBlackTree.Contains(999) == false);

            // ASSERT THAT EACH LEVEL HAS A DIFFERENT COLOR
            Asset_Levels_Different_Colors(redBlackTree);

            // Do some deletions
            redBlackTree.Remove(7);
            redBlackTree.Remove(1);
            redBlackTree.Remove(3);

            // Assert count
            Debug.Assert(redBlackTree.Count == 8);

            // Assert nonexistence of previously existing items
            Debug.Assert(redBlackTree.Contains(1) == false);
            Debug.Assert(redBlackTree.Contains(3) == false);

            // Remove root value
            var oldRootVal = redBlackTree.Root.Value;
            redBlackTree.Remove(redBlackTree.Root.Value);

            // Assert count
            Debug.Assert(redBlackTree.Count == 7);

            // Assert nonexistence of old root's value
            Debug.Assert(redBlackTree.Contains(oldRootVal) == false);


            //
            // PRINT TREE
            Console.WriteLine("********************");
            Console.WriteLine(" [*] RED-BLACK TREE:\r\n");
            Console.WriteLine("********************");
            Console.WriteLine(redBlackTree.DrawTree());
            Console.WriteLine("\r\n");

            Console.ReadLine();
        }//end-do-test


        /// <summary>
        /// Testing helper to assert that all items at every level of the tree has the same color and each level has different color than the other levels
        /// </summary>
        private static void Asset_Levels_Different_Colors(RedBlackTree<int> redBlackTree)
        {
            var root = redBlackTree.Root;

            int height = GetMaxHeight(root);
            var levels = new List<List<RedBlackTreeNode<int>>>();

            // Initialize the list
            for (int i = 0; i < height; ++i)
                levels.Add(new List<RedBlackTreeNode<int>>());

            int levelsIndex = 0;
            int nodesInNextLevel = 0;
            int nodesInCurrentLevel = 1;

            var queue = new Queue<RedBlackTreeNode<int>>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var curr = queue.Dequeue();
                nodesInCurrentLevel--;

                if (curr != null)
                {
                    levels[levelsIndex].Add(curr);
                    queue.Enqueue(curr.LeftChild);
                    queue.Enqueue(curr.RightChild);
                    nodesInNextLevel += 2;
                }

                if (nodesInCurrentLevel == 0)
                {
                    levelsIndex++;
                    nodesInCurrentLevel = nodesInNextLevel;
                    nodesInNextLevel = 0;
                }
            }

            Console.WriteLine("******************************************");
            Console.WriteLine(" [*] Assert that levels have different alternating colors:\r\n");

            var color = RedBlackTreeColors.Black;
            for (int i = 0; i < levels.Count; ++i)
            {
                for(int j = 0; j < levels[i].Count; ++j)
                {
                    Debug.Assert(levels[i][j].Color == color);

                    //if (levels[i][j].Color != color)
                    Console.WriteLine(" [-] Level: {0}. Node Value: {1}. Node color: {2}. Expected color: {3}.", i, levels[i][j].Value, levels[i][j].Color, color.ToString());
                }

                Console.WriteLine();
                color = (color == RedBlackTreeColors.Black ? RedBlackTreeColors.Red : RedBlackTreeColors.Black);
            }

            Console.WriteLine("******************************************\r\n");
        }//end-test-case


        /// <summary>
        /// Helper function to calculate the Maximum Height
        /// </summary>
        private static int GetMaxHeight(RedBlackTreeNode<int> tree)
        {
            if (tree == null)
                return 0;
            else
                return 1 + Math.Max(GetMaxHeight(tree.LeftChild), GetMaxHeight(tree.RightChild));
        }
    }

}
