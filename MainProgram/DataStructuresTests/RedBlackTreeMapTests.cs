using System;
using System.Diagnostics;
using System.Collections.Generic;

using DataStructures.Common;
using DataStructures.Trees;

namespace C_Sharp_Algorithms.DataStructuresTests
{
    public static class RedBlackTreeMapTests
    {
        public static void DoTest()
        {
            // Red-Black Tree Map collection
            var redBlackTree = new RedBlackTreeMap<int, string>(allowDuplicates: false);

            // Testing data
            KeyValuePair<int, string>[] values = new KeyValuePair<int, string>[10];

            // Prepare the values array
            for(int i = 1; i <= 10; ++i)
            {
                var keyValPair = new KeyValuePair<int, string>(i, String.Format("Integer: {0}", i));
                values[i - 1] = keyValPair;
            }


            //
            // Test singular insert
            for (int i = 0; i < 10; ++i)
                redBlackTree.Insert(values[i].Key, values[i].Value);

            Debug.Assert(redBlackTree.Count == values.Length, "Expected the same number of items.");
            Debug.Assert(redBlackTree.Height < redBlackTree.Count, "Fail! Tree doesn't rebalance against sorted elements!");

            redBlackTree.Clear();


            //
            // Test collection insert
            redBlackTree.Insert(values);

            Debug.Assert(redBlackTree.Height < redBlackTree.Count, "Fail! Tree doesn't rebalance against sorted elements!");


            //
            // Test enumeration of key-value pairs is still in oreder
            var enumerator = redBlackTree.GetInOrderEnumerator();
            for (int i = 0; i < 10; ++i)
            {
                if (enumerator.MoveNext())
                {
                    var curr = enumerator.Current;
                    if (curr.Key != values[i].Key || curr.Value != values[i].Value)
                        throw new Exception();
                }
            }


            //
            // Test against re-shuffled insertions (not like above order)
            redBlackTree = new RedBlackTreeMap<int, string>(allowDuplicates: false);

            redBlackTree.Insert(4, "int4");
            redBlackTree.Insert(5, "int5");
            redBlackTree.Insert(7, "int7");
            redBlackTree.Insert(2, "int2");
            redBlackTree.Insert(1, "int1");
            redBlackTree.Insert(3, "int3");
            redBlackTree.Insert(6, "int6");
            redBlackTree.Insert(0, "int0");
            redBlackTree.Insert(8, "int8");
            redBlackTree.Insert(10, "int10");
            redBlackTree.Insert(9, "int9");

            Debug.Assert(redBlackTree.Count == values.Length, "Expected the same number of items.");
            Debug.Assert(redBlackTree.Height < redBlackTree.Count, "Fail! Tree doesn't rebalance against sorted elements!");


            //
            // ASSERT INSERTING DUPLICATES WOULD BREAK
            var insert_duplicate_passed = true;
            try
            {
                // 2 already exists in tree
                redBlackTree.Insert(2, "int2");
                insert_duplicate_passed = true;
            }
            catch
            {
                insert_duplicate_passed = false;
            }

            Debug.Assert(insert_duplicate_passed == false, "Fail! The tree doesn't allow duplicates");


            //
            // Test find
            Debug.Assert(redBlackTree.Find(5).Key == 5, "Wrong find result!");
            Debug.Assert(redBlackTree.FindMin().Key == 0, "Wrong min!");
            Debug.Assert(redBlackTree.FindMax().Key == 10, "Wrong max!");


            //
            // Assert find raises exception on non-existing elements
            bool threwKeyNotFoundError = false;

            try
            {
                redBlackTree.Find(999999999);
                threwKeyNotFoundError = false;
            }
            catch(KeyNotFoundException)
            {
                threwKeyNotFoundError = true;
            }

            Debug.Assert(true == threwKeyNotFoundError, "Expected to catch KeyNotFoundException.");


            //
            // PRINT TREE
            Console.WriteLine("********************");
            Console.WriteLine(" [*] RED-BLACK TREE:\r\n");
            Console.WriteLine("********************");
            Console.WriteLine(redBlackTree.DrawTree());
            Console.WriteLine("\r\n");


            //
            // Assert count
            Debug.Assert(redBlackTree.Count == 11);


            //
            // Assert existence and nonexistence of some items
            Debug.Assert(redBlackTree.Contains(1) == true);
            Debug.Assert(redBlackTree.Contains(3) == true);
            Debug.Assert(redBlackTree.Contains(999) == false);


            //
            // ASSERT THAT EACH LEVEL HAS A DIFFERENT COLOR
            Asset_Levels_Different_Colors(redBlackTree);


            //
            // Do some deletions
            redBlackTree.Remove(7);
            redBlackTree.Remove(1);
            redBlackTree.Remove(3);


            //
            // Assert count
            Debug.Assert(redBlackTree.Count == 8);


            //
            // Assert nonexistence of previously existing items
            Debug.Assert(redBlackTree.Contains(1) == false);
            Debug.Assert(redBlackTree.Contains(3) == false);


            //
            // Remove root value
            var oldRootKey = redBlackTree.Root.Key;
            redBlackTree.Remove(redBlackTree.Root.Key);


            //
            // Assert count
            Debug.Assert(redBlackTree.Count == 7);


            //
            // Assert nonexistence of old root's key
            Debug.Assert(redBlackTree.Contains(oldRootKey) == false);


            //
            // PRINT TREE
            Console.WriteLine("********************");
            Console.WriteLine(" [*] RED-BLACK TREE:\r\n");
            Console.WriteLine("********************");
            Console.WriteLine(redBlackTree.DrawTree(includeValues: true));
            Console.WriteLine("\r\n");

            Console.ReadLine();

        }//end-do-test


        /// <summary>
        /// Testing helper to assert that all items at every level of the tree has the same color and each level has different color than the other levels
        /// </summary>
        private static void Asset_Levels_Different_Colors<TKey, TValue>(RedBlackTreeMap<TKey, TValue> redBlackTree) where TKey : IComparable<TKey>
        {
            var root = redBlackTree.Root;

            int height = GetMaxHeight(root);
            var levels = new List<List<RedBlackTreeMapNode<TKey, TValue>>>();

            // Initialize the list
            for (int i = 0; i < height; ++i)
                levels.Add(new List<RedBlackTreeMapNode<TKey, TValue>>());

            int levelsIndex = 0;
            int nodesInNextLevel = 0;
            int nodesInCurrentLevel = 1;

            var queue = new Queue<RedBlackTreeMapNode<TKey, TValue>>();
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
        private static int GetMaxHeight<TKey, TValue>(RedBlackTreeMapNode<TKey, TValue> tree) where TKey : IComparable<TKey>
        {
            if (tree == null)
                return 0;
            else
                return 1 + Math.Max(GetMaxHeight(tree.LeftChild), GetMaxHeight(tree.RightChild));
        }
    }
}
