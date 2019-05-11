using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Trees
{
    public class TernarySearchTree
    {
        public TernaryTreeNode Root { get; private set; }

        public void Insert(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                throw new Exception("Inputted value is empty");

            if (Root == null)
                Root = new TernaryTreeNode(null, word[0], word.Length == 1);

            WordInsertion(word);
        }

        public void Insert(string[] words)
        {
            foreach (var word in words)
            {
                Insert(word);
            }
        }

        void WordInsertion(string word)
        {
            int index = 0;
            TernaryTreeNode currentNode = Root;

            while (index < word.Length)
                currentNode = ChooseNode(currentNode, word, ref index);
        }

        TernaryTreeNode ChooseNode(TernaryTreeNode currentNode, string word, ref int index)
        {
            //Center Branch
            if (word[index] == currentNode.Value)
            {
                index++;

                if (currentNode.GetMiddleChild == null)
                    InsertInTree(currentNode.AddMiddleChild(word[index], word.Length == index + 1), word, ref index);
                

                return currentNode.GetMiddleChild;
            }
            //Right Branch
            else if (word[index] > currentNode.Value)
            {
                if (currentNode.GetRightChild == null)
                    InsertInTree(currentNode.AddRightChild(word[index], word.Length == index + 1), word, ref index);

                return currentNode.GetRightChild;
            }
            //Left Branch
            else
            {
                if (currentNode.GetLeftChild == null)
                    InsertInTree(currentNode.AddLeftChild(word[index], word.Length == index + 1), word, ref index);

                return currentNode.GetLeftChild;
            }
        }

        void InsertInTree(TernaryTreeNode currentNode, string word, ref int currentIndex)
        {
            int length = word.Length;

            currentIndex++;
            var currNode = currentNode;
            for (int i = currentIndex; i < length; i++)
                currNode = currNode.AddMiddleChild(word[i], word.Length == currentIndex + 1);

            currentIndex = length;
        }
    }
}
