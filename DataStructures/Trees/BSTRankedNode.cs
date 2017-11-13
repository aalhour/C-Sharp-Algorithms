using System;

namespace DataStructures.Trees
{
    /// <inheritdoc />
    /// <summary>
    /// Binary Search Tree node.
    /// This node extends the vanilla BSTNode class and adds an extra field to it for augmentation.
    /// The BST now augments the subtree-sizes on insert, delete and get-height.
    /// </summary>

    public class BSTRankedNode<T> : BSTNode<T> where T : IComparable<T>
    {
        public BSTRankedNode() : this(default(T), 0, null, null, null) { }
        public BSTRankedNode(T value) : this(value, 0, null, null, null) { }
        public BSTRankedNode(T value, int subtreeSize, BSTRankedNode<T> parent, BSTRankedNode<T> left, BSTRankedNode<T> right)
        {
            base.Value = value;
            SubtreeSize = subtreeSize;
            Parent = parent;
            LeftChild = left;
            RightChild = right;
        }

        // Size of subtrees
        public virtual int SubtreeSize { get; set; } = 0;

        public new BSTRankedNode<T> Parent
        {
            get => (BSTRankedNode<T>)base.Parent;
            set => base.Parent = value; 
        }

        public new BSTRankedNode<T> LeftChild
        {
            get => (BSTRankedNode<T>)base.LeftChild;
            set => base.LeftChild = value;
        }

        public new BSTRankedNode<T> RightChild
        {
            get => (BSTRankedNode<T>)base.RightChild;
            set => base.RightChild = value;
        }
    }
}