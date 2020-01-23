using DataStructures.Trees;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class TernarySearchTreeTest
    {

        //         c
        //       / | \
        //      a  u  h
        //      |  |  | \
        //      t  t  e u
        //     /  / |  / |
        //    s  p  e i  s
        [Fact]
        public static void DoTest()
        {
            string[] words = new string[] { "cute", "cup", "at", "as", "he", "us", "i" };

            TernarySearchTree tree = new TernarySearchTree();

            tree.Insert(words);

            Assert.Equal('c', tree.Root.Value);
            Assert.Equal('h', tree.Root.GetRightChild.Value);
            Assert.Equal('e', tree.Root.GetRightChild.GetMiddleChild.Value);
            Assert.Equal('p', tree.Root.GetMiddleChild.GetMiddleChild.GetLeftChild.Value);
            Assert.Equal('s', tree.Root.GetLeftChild.GetMiddleChild.GetLeftChild.Value);

        }
    }
}
