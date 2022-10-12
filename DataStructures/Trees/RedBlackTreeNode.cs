﻿namespace DataStructures.Trees;

/// <summary>
/// Red-Black Tree Node.
/// </summary>
public class RedBlackTreeNode<TKey> : BSTNode<TKey> where TKey : System.IComparable<TKey>
{
    /// <summary>
    /// CONSTRUCTORS
    /// </summary>
    public RedBlackTreeNode() : this(default, 0, null, null, null) { }
    public RedBlackTreeNode(TKey value) : this(value, 0, null, null, null) { }
    public RedBlackTreeNode(TKey value, int height, RedBlackTreeNode<TKey> parent, RedBlackTreeNode<TKey> left, RedBlackTreeNode<TKey> right)
    {
        base.Value = value;
        Color = RedBlackTreeColors.Red;
        Parent = parent;
        LeftChild = left;
        RightChild = right;
    }

    public virtual RedBlackTreeColors Color { get; set; }

    public new RedBlackTreeNode<TKey> Parent
    {
        get => (RedBlackTreeNode<TKey>)base.Parent;
        set => base.Parent = value;
    }

    public new RedBlackTreeNode<TKey> LeftChild
    {
        get => (RedBlackTreeNode<TKey>)base.LeftChild;
        set => base.LeftChild = value;
    }

    public new RedBlackTreeNode<TKey> RightChild
    {
        get => (RedBlackTreeNode<TKey>)base.RightChild;
        set => base.RightChild = value;
    }


    /******************************************************************************/


    /// <summary>
    /// Returns if this node is colored red.
    /// </summary>
    public virtual bool IsRed => Color == RedBlackTreeColors.Red;

    /// <summary>
    /// Checks whether this node is colored black.
    /// </summary>
    public virtual bool IsBlack => Color == RedBlackTreeColors.Black;

    /// <summary>
    /// Returns the sibling of this node.
    /// </summary>
    public virtual RedBlackTreeNode<TKey> Sibling => Parent == null ? null : IsLeftChild ? Parent.RightChild : Parent.LeftChild;

    /// <summary>
    /// Returns the grandparent of this node.
    /// </summary>
    public virtual RedBlackTreeNode<TKey> GrandParent => Parent == null ? null : Parent.Parent;
}