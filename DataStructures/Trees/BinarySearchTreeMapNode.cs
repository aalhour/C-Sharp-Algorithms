using System;

namespace DataStructures.Trees;

/// <summary>
/// The Binary Search Tree Map node.
/// </summary>
public class BSTMapNode<TKey, TValue> : IComparable<BSTMapNode<TKey, TValue>> where TKey : IComparable<TKey>
{
    public BSTMapNode() { }
    public BSTMapNode(TKey key) : this(key, default, 0, null, null, null) { }
    public BSTMapNode(TKey key, TValue value) : this(key, value, 0, null, null, null) { }
    public BSTMapNode(TKey key, TValue value, int subTreeSize, BSTMapNode<TKey, TValue> parent, BSTMapNode<TKey, TValue> left, BSTMapNode<TKey, TValue> right)
    {
        Key = key;
        Value = value;
        Parent = parent;
        LeftChild = left;
        RightChild = right;
    }

    public virtual TKey Key { get; set; }

    public virtual TValue Value { get; set; }

    public virtual BSTMapNode<TKey, TValue> Parent { get; set; }

    public virtual BSTMapNode<TKey, TValue> LeftChild { get; set; }

    public virtual BSTMapNode<TKey, TValue> RightChild { get; set; }

    /// <summary>
    /// Checks whether this node has any children.
    /// </summary>
    public virtual bool HasChildren => ChildrenCount > 0;

    /// <summary>
    /// Checks whether this node has left child.
    /// </summary>
    public virtual bool HasLeftChild => LeftChild != null;

    /// <summary>
    /// Checks whether this node has right child.
    /// </summary>
    public virtual bool HasRightChild => RightChild != null;

    /// <summary>
    /// Checks whether this node is the left child of it's parent.
    /// </summary>
    public virtual bool IsLeftChild => Parent != null && Parent.LeftChild == this;

    /// <summary>
    /// Checks whether this node is the left child of it's parent.
    /// </summary>
    public virtual bool IsRightChild => Parent != null && Parent.RightChild == this;

    /// <summary>
    /// Checks whether this node is a leaf node.
    /// </summary>
    public virtual bool IsLeafNode => ChildrenCount == 0;

    /// <summary>
    /// Returns number of direct descendents: 0, 1, 2 (none, left or right, or both).
    /// </summary>
    /// <returns>Number (0,1,2)</returns>
    public virtual int ChildrenCount
    {
        get
        {
            int count = 0;

            if (HasLeftChild)
                count++;
                
            if (HasRightChild)
                count++;

            return count;
        }
    }

    /// <summary>
    /// Compares to.
    /// </summary>
    public virtual int CompareTo(BSTMapNode<TKey, TValue> other)
    {
        if (other == null)
            return -1;

        return Key.CompareTo(other.Key);
    }
}//end-of-bstnode