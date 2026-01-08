using DataStructures.Trees;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class TernarySearchTreeTest
    {
        //
        // Ternary Search Tree structure after inserting: cute, cup, at, as, he, us, i
        //
        //         c
        //       / | \
        //      a  u  h
        //      |  |  | \
        //      t  t  e u
        //     /  / |  / |
        //    s  p  e i  s
        //

        [Fact]
        public static void Insert_Words_BuildsCorrectTree()
        {
            string[] words = new string[] { "cute", "cup", "at", "as", "he", "us", "i" };
            var tree = new TernarySearchTree();

            tree.Insert(words);

            Assert.Equal('c', tree.Root.Value);
        }

        [Fact]
        public static void Insert_Words_RightChildIsCorrect()
        {
            string[] words = new string[] { "cute", "cup", "at", "as", "he", "us", "i" };
            var tree = new TernarySearchTree();

            tree.Insert(words);

            Assert.Equal('h', tree.Root.GetRightChild.Value);
            Assert.Equal('e', tree.Root.GetRightChild.GetMiddleChild.Value);
        }

        [Fact]
        public static void Insert_Words_LeftChildIsCorrect()
        {
            string[] words = new string[] { "cute", "cup", "at", "as", "he", "us", "i" };
            var tree = new TernarySearchTree();

            tree.Insert(words);

            Assert.Equal('a', tree.Root.GetLeftChild.Value);
            Assert.Equal('t', tree.Root.GetLeftChild.GetMiddleChild.Value);
            Assert.Equal('s', tree.Root.GetLeftChild.GetMiddleChild.GetLeftChild.Value);
        }

        [Fact]
        public static void Insert_Words_MiddleChildIsCorrect()
        {
            string[] words = new string[] { "cute", "cup", "at", "as", "he", "us", "i" };
            var tree = new TernarySearchTree();

            tree.Insert(words);

            Assert.Equal('u', tree.Root.GetMiddleChild.Value);
            Assert.Equal('t', tree.Root.GetMiddleChild.GetMiddleChild.Value);
            Assert.Equal('p', tree.Root.GetMiddleChild.GetMiddleChild.GetLeftChild.Value);
        }

        [Fact]
        public static void Insert_SingleWord_Works()
        {
            var tree = new TernarySearchTree();

            tree.Insert("hello");

            Assert.NotNull(tree.Root);
            Assert.Equal('h', tree.Root.Value);
        }

        [Fact]
        public static void Insert_EmptyTree_RootIsNull()
        {
            var tree = new TernarySearchTree();

            Assert.Null(tree.Root);
        }

        [Fact]
        public static void Insert_MultipleWords_BuildsTreeStructure()
        {
            var tree = new TernarySearchTree();
            string[] words = new string[] { "cat", "car", "card", "care", "careful", "egg" };

            tree.Insert(words);

            // Root should be 'c' (first letter of first word)
            Assert.NotNull(tree.Root);
            Assert.Equal('c', tree.Root.Value);

            // Middle child should be 'a' 
            Assert.NotNull(tree.Root.GetMiddleChild);
            Assert.Equal('a', tree.Root.GetMiddleChild.Value);
        }
    }
}
