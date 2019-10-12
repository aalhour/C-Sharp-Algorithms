using DataStructures.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public class RedBlackTreeTest
    {
        /** Input tree for test cases, (r -> red, b -> black):
        **          11(b)
        **         /    \
        **      (r)3    13(b)
        **       / \      \
        **   (b)1  7(b)   15(r)
        **         / \
        **     (r)5   8(r)
        **/
        private RedBlackTree<int> redBlackTree;

        public RedBlackTreeTest()
        {
            redBlackTree = new RedBlackTree<int>(false);

            redBlackTree.Insert(11);
            redBlackTree.Insert(3);
            redBlackTree.Insert(13);
            redBlackTree.Insert(1);
            redBlackTree.Insert(7);
            redBlackTree.Insert(15);
            redBlackTree.Insert(5);
            redBlackTree.Insert(8);
        }

        [Fact]
        public void Insert_CheckCorrectConstructionOfInputTree()
        {
            RedBlackTreeRule.CheckRedBlackTreeRules(redBlackTree);

            Assert.Equal(8, redBlackTree.Count);
            Assert.Equal(11, redBlackTree.Root.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.Color);

            Assert.Equal(3, redBlackTree.Root.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.Color);

            Assert.Equal(1, redBlackTree.Root.LeftChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.LeftChild.LeftChild.Color);

            Assert.Equal(7, redBlackTree.Root.LeftChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.LeftChild.RightChild.Color);

            Assert.Equal(5, redBlackTree.Root.LeftChild.RightChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.RightChild.LeftChild.Color);

            Assert.Equal(8, redBlackTree.Root.LeftChild.RightChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.RightChild.RightChild.Color);

            Assert.Equal(13, redBlackTree.Root.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.RightChild.Color);

            Assert.Equal(15, redBlackTree.Root.RightChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.RightChild.RightChild.Color);
        }

        /** Insert 4, (r -> red, b -> black):
         **          11(b)             ===>          7(b)
         **         /    \             ===>        /      \
         **      (r)3    13(b)         ===>      (r)3     11(r)
         **       / \      \           ===>      / \       /  \
         **   (b)1  7(b)   15(r)       ===>  (b)1  5(b)  (b)8 13(b)
         **         / \                ===>        /            \
         **     (r)5   8(r)            ===>      4(r)           15(r)
         **/
        [Fact]
        public void Insert_ParentSiblingIsRed()
        {
            redBlackTree.Insert(4);

            RedBlackTreeRule.CheckRedBlackTreeRules(redBlackTree);

            Assert.Equal(9, redBlackTree.Count);
            Assert.Equal(7, redBlackTree.Root.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.Color);

            Assert.Equal(3, redBlackTree.Root.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.Color);

            Assert.Equal(1, redBlackTree.Root.LeftChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.LeftChild.LeftChild.Color);

            Assert.Equal(5, redBlackTree.Root.LeftChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.LeftChild.RightChild.Color);

            Assert.Equal(4, redBlackTree.Root.LeftChild.RightChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.RightChild.LeftChild.Color);

            Assert.Equal(11, redBlackTree.Root.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.RightChild.Color);

            Assert.Equal(8, redBlackTree.Root.RightChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.RightChild.LeftChild.Color);

            Assert.Equal(13, redBlackTree.Root.RightChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.RightChild.RightChild.Color);

            Assert.Equal(15, redBlackTree.Root.RightChild.RightChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.RightChild.RightChild.RightChild.Color);
        }

        /** Insert 14, (r -> red, b -> black):
         **          11(b)             ===>        11(b)
         **         /    \             ===>       /     \
         **      (r)3    13(b)         ===>    (r)3     14(b)
         **       / \      \           ===>     / \      /   \
         **   (b)1  7(b)   15(r)       ===> (b)1  7(b) 13(r) 15(r)
         **         / \                ===>       / \
         **     (r)5   8(r)            ===>   (r)5   8(r)
         **/
        [Fact]
        public void Insert_ParentSiblingIsBlackAndNewElementIsLeftChild()
        {
            redBlackTree.Insert(14);

            RedBlackTreeRule.CheckRedBlackTreeRules(redBlackTree);

            Assert.Equal(9, redBlackTree.Count);
            Assert.Equal(11, redBlackTree.Root.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.Color);

            Assert.Equal(3, redBlackTree.Root.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.Color);

            Assert.Equal(1, redBlackTree.Root.LeftChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.LeftChild.LeftChild.Color);

            Assert.Equal(7, redBlackTree.Root.LeftChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.LeftChild.RightChild.Color);

            Assert.Equal(5, redBlackTree.Root.LeftChild.RightChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.RightChild.LeftChild.Color);

            Assert.Equal(8, redBlackTree.Root.LeftChild.RightChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.RightChild.RightChild.Color);

            Assert.Equal(14, redBlackTree.Root.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.RightChild.Color);

            Assert.Equal(13, redBlackTree.Root.RightChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.RightChild.LeftChild.Color);

            Assert.Equal(15, redBlackTree.Root.RightChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.RightChild.RightChild.Color);
        }

        /** Insert 16, (r -> red, b -> black):
         **          11(b)             ===>        11(b)
         **         /    \             ===>       /     \
         **      (r)3    13(b)         ===>    (r)3     15(b)
         **       / \      \           ===>     / \      /   \
         **   (b)1  7(b)   15(r)       ===> (b)1  7(b) 13(r) 16(r)
         **         / \                ===>       / \
         **     (r)5   8(r)            ===>   (r)5   8(r)
         **/
        [Fact]
        public void Insert_ParentSiblingIsBlackAndNewElementIsRightChild()
        {
            redBlackTree.Insert(16);

            RedBlackTreeRule.CheckRedBlackTreeRules(redBlackTree);

            Assert.Equal(9, redBlackTree.Count);
            Assert.Equal(11, redBlackTree.Root.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.Color);

            Assert.Equal(3, redBlackTree.Root.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.Color);

            Assert.Equal(1, redBlackTree.Root.LeftChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.LeftChild.LeftChild.Color);

            Assert.Equal(7, redBlackTree.Root.LeftChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.LeftChild.RightChild.Color);

            Assert.Equal(5, redBlackTree.Root.LeftChild.RightChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.RightChild.LeftChild.Color);

            Assert.Equal(8, redBlackTree.Root.LeftChild.RightChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.RightChild.RightChild.Color);

            Assert.Equal(15, redBlackTree.Root.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.RightChild.Color);

            Assert.Equal(13, redBlackTree.Root.RightChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.RightChild.LeftChild.Color);

            Assert.Equal(16, redBlackTree.Root.RightChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.RightChild.RightChild.Color);
        }

        [Fact]
        public void Insert_ThrowExceptionWhenNotAllowDuplicate()
        {
            var redBlackTreeWithoutDuplicates = new RedBlackTree<int>(false);

            redBlackTreeWithoutDuplicates.Insert(1);
            redBlackTreeWithoutDuplicates.Insert(2);
            redBlackTreeWithoutDuplicates.Insert(3);

            //TODO Create more specyfic exception type for this kind of errors, with inheritance from InvalidOperationException.
            Assert.Throws<InvalidOperationException>(() => redBlackTreeWithoutDuplicates.Insert(2));
        }

        /** Remove 13, (r -> red, b -> black):
         **          11(b)             ===>        11(b)
         **         /    \             ===>       /     \
         **      (r)3    13(b)         ===>    (r)3     15(b)
         **       / \      \           ===>     / \
         **   (b)1  7(b)   15(r)       ===> (b)1  7(b)
         **         / \                ===>       / \
         **     (r)5   8(r)            ===>   (r)5   8(r)
         **/
        [Fact]
        public void Remove_SiblingIsRed()
        {
            redBlackTree.Remove(13);

            RedBlackTreeRule.CheckRedBlackTreeRules(redBlackTree);

            Assert.Equal(7, redBlackTree.Count);
            Assert.Equal(11, redBlackTree.Root.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.Color);

            Assert.Equal(3, redBlackTree.Root.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.Color);

            Assert.Equal(1, redBlackTree.Root.LeftChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.LeftChild.LeftChild.Color);

            Assert.Equal(7, redBlackTree.Root.LeftChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.LeftChild.RightChild.Color);

            Assert.Equal(5, redBlackTree.Root.LeftChild.RightChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.RightChild.LeftChild.Color);

            Assert.Equal(8, redBlackTree.Root.LeftChild.RightChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.RightChild.RightChild.Color);

            Assert.Equal(15, redBlackTree.Root.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.RightChild.Color);
        }

        /** Remove 3, (r -> red, b -> black):
         **          11(b)             ===>        11(b)
         **         /    \             ===>       /     \
         **      (r)3    13(b)         ===>    (r)5     13(b)
         **       / \      \           ===>    /  \       \
         **   (b)1  7(b)   15(r)       ===> (b)1  7(b)    15(r)
         **         / \                ===>         \
         **     (r)5   8(r)            ===>         8(r)
         **/
        [Fact]
        public void Remove_SiblingIsBlackAndBothChildAreBlack()
        {
            redBlackTree.Remove(3);

            RedBlackTreeRule.CheckRedBlackTreeRules(redBlackTree);

            Assert.Equal(7, redBlackTree.Count);
            Assert.Equal(11, redBlackTree.Root.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.Color);

            Assert.Equal(5, redBlackTree.Root.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.Color);

            Assert.Equal(1, redBlackTree.Root.LeftChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.LeftChild.LeftChild.Color);

            Assert.Equal(7, redBlackTree.Root.LeftChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.LeftChild.RightChild.Color);

            Assert.Equal(8, redBlackTree.Root.LeftChild.RightChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.RightChild.RightChild.Color);

            Assert.Equal(13, redBlackTree.Root.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.RightChild.Color);

            Assert.Equal(15, redBlackTree.Root.RightChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.RightChild.RightChild.Color);
        }

        /** Remove 7, (r -> red, b -> black):
         **          11(b)             ===>        11(b)
         **         /    \             ===>       /     \
         **      (r)3    13(b)         ===>    (r)3     13(b)
         **       / \      \           ===>     / \       \
         **   (b)1  7(b)   15(r)       ===> (b)1  8(b)    15(r)
         **         / \                ===>       /
         **     (r)5   8(r)            ===>     5(r)
         **/
        [Fact]
        public void Remove_SiblingIsBlackAndRightChildIsRed()
        {
            redBlackTree.Remove(7);

            RedBlackTreeRule.CheckRedBlackTreeRules(redBlackTree);

            Assert.Equal(7, redBlackTree.Count);
            Assert.Equal(11, redBlackTree.Root.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.Color);

            Assert.Equal(3, redBlackTree.Root.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.Color);

            Assert.Equal(1, redBlackTree.Root.LeftChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.LeftChild.LeftChild.Color);

            Assert.Equal(8, redBlackTree.Root.LeftChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.LeftChild.RightChild.Color);

            Assert.Equal(5, redBlackTree.Root.LeftChild.RightChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.RightChild.LeftChild.Color);

            Assert.Equal(13, redBlackTree.Root.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.RightChild.Color);

            Assert.Equal(15, redBlackTree.Root.RightChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.RightChild.RightChild.Color);
        }

        /** Remove 11, (r -> red, b -> black):
         **          11(b)             ===>        13(b)
         **         /    \             ===>       /     \
         **      (r)3    13(b)         ===>    (r)3     15(b)
         **       / \      \           ===>     / \
         **   (b)1  7(b)   15(r)       ===> (b)1  7(b)
         **         / \                ===>       / \
         **     (r)5   8(r)            ===>     5(r) 8(r)
         **/
        [Fact]
        public void Remove_CurrentRoot()
        {
            redBlackTree.Remove(11);

            RedBlackTreeRule.CheckRedBlackTreeRules(redBlackTree);

            Assert.Equal(7, redBlackTree.Count);
            Assert.Equal(13, redBlackTree.Root.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.Color);

            Assert.Equal(3, redBlackTree.Root.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.Color);

            Assert.Equal(1, redBlackTree.Root.LeftChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.LeftChild.LeftChild.Color);

            Assert.Equal(7, redBlackTree.Root.LeftChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.LeftChild.RightChild.Color);

            Assert.Equal(5, redBlackTree.Root.LeftChild.RightChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.RightChild.LeftChild.Color);

            Assert.Equal(8, redBlackTree.Root.LeftChild.RightChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.RightChild.RightChild.Color);

            Assert.Equal(15, redBlackTree.Root.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.RightChild.Color);
        }

        [Fact]
        public void Remove_ThrowExceptionWhenTryRemoveNonExistentNode()
        {
            //TODO Create more specyfic exception type for this kind of errors, with inheritance from ArgumentException.
            Assert.Throws<Exception>(() =>redBlackTree.Remove(999));
        }

        /** Remove 8, (r -> red, b -> black):
         **          11(b)             ===>        11(b)
         **         /    \             ===>       /     \
         **      (r)3    13(b)         ===>    (r)3     13(b)
         **       / \      \           ===>     / \      \
         **   (b)1  7(b)   15(r)       ===> (b)1  7(b)   15(r)
         **         / \                ===>       /
         **     (r)5   8(r)            ===>     5(r)
         **/
        [Fact]
        public void Remove_NodeWithoutChildren()
        {
            redBlackTree.Remove(8);

            RedBlackTreeRule.CheckRedBlackTreeRules(redBlackTree);

            Assert.Equal(7, redBlackTree.Count);
            Assert.Equal(11, redBlackTree.Root.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.Color);

            Assert.Equal(3, redBlackTree.Root.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.Color);

            Assert.Equal(1, redBlackTree.Root.LeftChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.LeftChild.LeftChild.Color);

            Assert.Equal(7, redBlackTree.Root.LeftChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.LeftChild.RightChild.Color);

            Assert.Equal(5, redBlackTree.Root.LeftChild.RightChild.LeftChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.LeftChild.RightChild.LeftChild.Color);

            Assert.Null(redBlackTree.Root.LeftChild.RightChild.RightChild);

            Assert.Equal(13, redBlackTree.Root.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Black, redBlackTree.Root.RightChild.Color);

            Assert.Equal(15, redBlackTree.Root.RightChild.RightChild.Value);
            Assert.Equal(RedBlackTreeColors.Red, redBlackTree.Root.RightChild.RightChild.Color);
        }

        [Fact]
        public void Remove_OneAndOnlyTreeNode()
        {
            var oneElementTree = new RedBlackTree<int>();
            Assert.Equal(0, oneElementTree.Count);
            Assert.Null(oneElementTree.Root);
            Assert.True(oneElementTree.IsEmpty);

            oneElementTree.Insert(1);
            Assert.Equal(1, oneElementTree.Count);
            Assert.NotNull(oneElementTree.Root);
            Assert.False(oneElementTree.IsEmpty);

            oneElementTree.Remove(1);
            Assert.Equal(0, oneElementTree.Count);
            Assert.Null(oneElementTree.Root);
            Assert.True(oneElementTree.IsEmpty);
        }
    }

    /// <summary>
    ///     Contains set of method that facilitate work. Only for unit tests.
    /// </summary>
    internal static class RedBlackTreeHelper
    {
        public static IEnumerable<RedBlackTreeNode<T>> GetNodes<T>(this RedBlackTreeNode<T> node)
            where T : IComparable<T>
        {
            if (node.LeftChild != null)
            {
                yield return node.LeftChild;

                foreach (var child in node.LeftChild.GetNodes())
                {
                    yield return child;
                }
            }

            if (node.RightChild != null)
            {
                yield return node.RightChild;

                foreach (var child in node.RightChild.GetNodes())
                {
                    yield return child;
                }
            }

            if (node.Parent == null)
            {
                yield return node;
            }
        }

        public static IEnumerable<IEnumerable<RedBlackTreeNode<T>>> GetPathToLeaves<T>(this RedBlackTreeNode<T> root)
            where T : IComparable<T>
        {
            if (root.Parent != null)
            {
                throw new ArgumentException("The given node is not root.");
            }

            var leaves = root.GetNodes().Where(node => node.IsLeafNode);
            var paths = new List<List<RedBlackTreeNode<T>>>();
            foreach (var leaf in leaves)
            {
                paths.Add(new List<RedBlackTreeNode<T>>() { leaf });
            }

            for (var index = 0; index < paths.Count; index++)
            {
                var path = paths[index];

                while (path.Last().Parent != null)
                {
                    path.Add(path.Last().Parent);
                }
            }

            return paths;
        }
    }

    /// <summary>
    ///     Contains method to check asked red black tree fulfills red black tree rules. Only for unit tests.
    /// </summary>
    internal static class RedBlackTreeRule
    {
        public static void CheckRedBlackTreeRules(RedBlackTree<int> redBlackTree)
        {
            CheckIsEveryNodeRedOrBlack(redBlackTree);
            CheckRootIsBlack(redBlackTree);
            CheckNodeIsRedIfBothChildrenAreBlack(redBlackTree);
            CheckPathToEveryLeafHasSameNumberOfBlackNode(redBlackTree);
        }

        private static void CheckIsEveryNodeRedOrBlack(RedBlackTree<int> redBlackTree)
        {
            var nodes = redBlackTree.Root.GetNodes().ToList();

            if (!nodes.Any(node => node.IsBlack || node.IsRed))
            {
                throw new RedBlackTreeViolationRuleException();
            }
        }

        private static void CheckRootIsBlack(RedBlackTree<int> redBlackTree)
        {
            if (!redBlackTree.Root.IsBlack)
            {
                throw new RedBlackTreeViolationRuleException();
            }
        }

        private static void CheckNodeIsRedIfBothChildrenAreBlack(RedBlackTree<int> redBlackTree)
        {
            var nodes = redBlackTree.Root.GetNodes().ToList();

            foreach (var redNode in nodes.Where(node => node.IsRed))
            {
                if (redNode.RightChild != null
                        && !redNode.RightChild.IsBlack)
                {
                    throw new RedBlackTreeViolationRuleException();
                }

                if (redNode.LeftChild != null
                        && !redNode.LeftChild.IsBlack)
                {
                    throw new RedBlackTreeViolationRuleException();
                }
            }
        }

        private static void CheckPathToEveryLeafHasSameNumberOfBlackNode(RedBlackTree<int> redBlackTree)
        {
            var paths = redBlackTree.Root.GetPathToLeaves();

            if (paths != null)
            {
                var blacks = paths.FirstOrDefault().Count(node => node.IsBlack);
                foreach (var path in paths)
                {
                    if (blacks != path.Count(node => node.IsBlack))
                    {
                        throw new RedBlackTreeViolationRuleException();
                    }
                }
            }
        }
    }

    /// <summary>
    ///     Exception class throw when any of red black tree rule is violation. Only for unit tests.
    /// </summary>
    internal class RedBlackTreeViolationRuleException : Exception
    { }
}
