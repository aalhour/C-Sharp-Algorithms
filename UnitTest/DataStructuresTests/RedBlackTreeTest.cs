using DataStructures.Trees;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class RedBlackTreeTest
    {
        [Fact]
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

            Assert.True(redBlackTree.Height < redBlackTree.Count, "Fail! Tree doesn't rebalance against sorted elements!");


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

            Assert.True(insert_duplicate_passed == false, "Fail! The tree doesn't allow duplicates");


            // Assert count
            Assert.True(redBlackTree.Count == 11);

            // Assert existence and nonexistence of some items
            Assert.True(redBlackTree.Contains(1));
            Assert.True(redBlackTree.Contains(3));
            Assert.False(redBlackTree.Contains(999));

            // ASSERT THAT EACH LEVEL HAS A DIFFERENT COLOR
            AssetLevelsDifferentColors(redBlackTree);

            // Do some deletions
            redBlackTree.Remove(7);
            redBlackTree.Remove(1);
            redBlackTree.Remove(3);

            // Assert count
            Assert.True(redBlackTree.Count == 8);

            // Assert nonexistence of previously existing items
            Assert.False(redBlackTree.Contains(1));
            Assert.False(redBlackTree.Contains(3));

            // Remove root value
            var oldRootVal = redBlackTree.Root.Value;
            redBlackTree.Remove(redBlackTree.Root.Value);

            // Assert count
            Assert.True(redBlackTree.Count == 7);

            // Assert nonexistence of old root's value
            Assert.False(redBlackTree.Contains(oldRootVal));

        }//end-do-test


        /// <summary>
        /// Testing helper to assert that all items at every level of the tree has the same color and each level has different color than the other levels
        /// </summary>
        private static void AssetLevelsDifferentColors(RedBlackTree<int> redBlackTree)
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
            var color = RedBlackTreeColors.Black;
            for (int i = 0; i < levels.Count; ++i)
            {
                for (int j = 0; j < levels[i].Count; ++j)
                {
                    // TODO: [2,1] == red?
                    Assert.True(levels[i][j].Color == color);
                }
                color = (color == RedBlackTreeColors.Black ? RedBlackTreeColors.Red : RedBlackTreeColors.Black);
            }
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
