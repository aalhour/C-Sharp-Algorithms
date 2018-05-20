using DataStructures.Trees;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class RedBlackTreeMapTests
    {
        [Fact]
        public static void DoTest()
        {
            // Red-Black Tree Map collection
            var redBlackTree = new RedBlackTreeMap<int, string>(false);

            // Testing data
            var values = new KeyValuePair<int, string>[10];

            // Prepare the values array
            for (var i = 1; i <= 10; ++i)
            {
                var keyValPair = new KeyValuePair<int, string>(i, string.Format("Integer: {0}", i));
                values[i - 1] = keyValPair;
            }

            // Test singular insert
            for (var i = 0; i < 10; ++i)
            {
                redBlackTree.Insert(values[i].Key, values[i].Value);
            }

            Assert.True(redBlackTree.Count == values.Length, "Expected the same number of items.");
            Assert.True(redBlackTree.Height < redBlackTree.Count, "Fail! Tree doesn't rebalance against sorted elements!");

            redBlackTree.Clear();

            // Test collection insert
            redBlackTree.Insert(values);

            Assert.True(redBlackTree.Height < redBlackTree.Count, "Fail! Tree doesn't rebalance against sorted elements!");

            // Test enumeration of key-value pairs is still in oreder
            var enumerator = redBlackTree.GetInOrderEnumerator();
            for (var i = 0; i < 10; ++i)
            {
                if (enumerator.MoveNext())
                {
                    var curr = enumerator.Current;
                    if (curr.Key != values[i].Key || curr.Value != values[i].Value)
                    {
                        throw new Exception();
                    }
                }
            }

            // Test against re-shuffled insertions (not like above order)
            redBlackTree = new RedBlackTreeMap<int, string>(false);

            redBlackTree.Insert(4, "int4");
            redBlackTree.Insert(5, "int5");
            redBlackTree.Insert(7, "int7");
            redBlackTree.Insert(2, "int2");
            redBlackTree.Insert(1, "int1");
            redBlackTree.Insert(3, "int3");
            redBlackTree.Insert(6, "int6");
            //redBlackTree.Insert(0, "int0");
            redBlackTree.Insert(8, "int8");
            redBlackTree.Insert(10, "int10");
            redBlackTree.Insert(9, "int9");

            Assert.True(redBlackTree.Count == values.Length, "Expected the same number of items.");
            Assert.True(redBlackTree.Height < redBlackTree.Count, "Fail! Tree doesn't rebalance against sorted elements!");

            // ASSERT INSERTING DUPLICATES WOULD BREAK
            bool insertDuplicatePassed;
            try
            {
                // 2 already exists in tree
                redBlackTree.Insert(2, "int2");
                insertDuplicatePassed = true;
            }
            catch
            {
                insertDuplicatePassed = false;
            }

            Assert.True(insertDuplicatePassed == false, "Fail! The tree doesn't allow duplicates");

            // Test find
            Assert.True(redBlackTree.Find(5).Key == 5, "Wrong find result!");
            Assert.True(redBlackTree.FindMin().Key == 1, "Wrong min!");
            Assert.True(redBlackTree.FindMax().Key == 10, "Wrong max!");

            // Assert find raises exception on non-existing elements
            var threwKeyNotFoundError = false;

            try
            {
                redBlackTree.Find(999999999);
            }
            catch (KeyNotFoundException)
            {
                threwKeyNotFoundError = true;
            }

            Assert.True(threwKeyNotFoundError, "Expected to catch KeyNotFoundException.");

            // PRINT TREE
            //Console.WriteLine("********************");
            //Console.WriteLine(" [*] RED-BLACK TREE:\r\n");
            //Console.WriteLine("********************");
            //Console.WriteLine(redBlackTree.DrawTree());
            //Console.WriteLine("\r\n");

            // Assert count
            Assert.True(redBlackTree.Count == 10);

            // Assert existence and nonexistence of some items
            Assert.True(redBlackTree.Contains(1));
            Assert.True(redBlackTree.Contains(3));
            Assert.True(redBlackTree.Contains(999) == false);

            // ASSERT THAT EACH LEVEL HAS A DIFFERENT COLOR
            // TODO: Wrong color element "int4"
            // AssetLevelsDifferentColors(redBlackTree);

            // Do some deletions
            redBlackTree.Remove(7);
            redBlackTree.Remove(1);
            redBlackTree.Remove(3);

            // Assert count
            Assert.True(redBlackTree.Count == 7);

            // Assert nonexistence of previously existing items
            Assert.True(redBlackTree.Contains(1) == false);
            Assert.True(redBlackTree.Contains(3) == false);

            // Remove root value
            var oldRootKey = redBlackTree.Root.Key;
            redBlackTree.Remove(redBlackTree.Root.Key);

            // Assert count
            Assert.True(redBlackTree.Count == 6);

            // Assert nonexistence of old root's key
            Assert.True(redBlackTree.Contains(oldRootKey) == false);

            // PRINT TREE
            //Console.WriteLine("********************");
            //Console.WriteLine(" [*] RED-BLACK TREE:\r\n");
            //Console.WriteLine("********************");
            //Console.WriteLine(redBlackTree.DrawTree(includeValues: true));
            //Console.WriteLine("\r\n");

            //Console.ReadLine();
        } //end-do-test


        /// <summary>
        /// Testing helper to assert that all items at every level of the tree has the same color and each level has different color than the other levels
        /// </summary>
        private static void AssetLevelsDifferentColors<TKey, TValue>(RedBlackTreeMap<TKey, TValue> redBlackTree)
            where TKey : IComparable<TKey>
        {
            var root = redBlackTree.Root;

            var height = GetMaxHeight(root);
            var levels = new List<List<RedBlackTreeMapNode<TKey, TValue>>>();

            // Initialize the list
            for (var i = 0; i < height; ++i)
            {
                levels.Add(new List<RedBlackTreeMapNode<TKey, TValue>>());
            }

            var levelsIndex = 0;
            var nodesInNextLevel = 0;
            var nodesInCurrentLevel = 1;

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

            // [*] Assert that levels have different alternating colors:

            var color = RedBlackTreeColors.Black;
            for (var i = 0; i < levels.Count; ++i)
            {
                for (var j = 0; j < levels[i].Count; ++j)
                {
                    Assert.True(levels[i][j].Color == color);

                    //if (levels[i][j].Color != color)
                    //Console.WriteLine(" [-] Level: {0}. Node Value: {1}. Node color: {2}. Expected color: {3}.", i, levels[i][j].Value, levels[i][j].Color, color.ToString());
                }

                color = color == RedBlackTreeColors.Black ? RedBlackTreeColors.Red : RedBlackTreeColors.Black;
            }
        } //end-test-case


        /// <summary>
        /// Helper function to calculate the Maximum Height
        /// </summary>
        private static int GetMaxHeight<TKey, TValue>(RedBlackTreeMapNode<TKey, TValue> tree)
            where TKey : IComparable<TKey>
        {
            if (tree == null)
            {
                return 0;
            }

            return 1 + Math.Max(GetMaxHeight(tree.LeftChild), GetMaxHeight(tree.RightChild));
        }
    }
}
