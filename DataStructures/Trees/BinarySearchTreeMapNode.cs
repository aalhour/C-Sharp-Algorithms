using System;

namespace DataStructures.Trees
{
    /// <summary>
    /// The Binary Search Tree Map node.
    /// </summary>
    public class BSTMapNode<TKey, TValue> : IComparable<BSTMapNode<TKey, TValue>> where TKey : IComparable<TKey>
    {
        private TKey _key;
        private TValue _value;
        private BSTMapNode<TKey, TValue> _parent;
        private BSTMapNode<TKey, TValue> _left;
        private BSTMapNode<TKey, TValue> _right;

        public BSTMapNode() { }
        public BSTMapNode(TKey key) : this(key, default(TValue), 0, null, null, null) { }
        public BSTMapNode(TKey key, TValue value) : this(key, value, 0, null, null, null) { }
        public BSTMapNode(TKey key, TValue value, int subTreeSize, BSTMapNode<TKey, TValue> parent, BSTMapNode<TKey, TValue> left, BSTMapNode<TKey, TValue> right)
        {
            Key = key;
            Value = value;
            Parent = parent;
            LeftChild = left;
            RightChild = right;
        }

        public virtual TKey Key
        {
            get { return this._key; }
            set { this._key = value; }
        }

        public virtual TValue Value
        {
            get { return this._value; }
            set { this._value = value; }
        }

        public virtual BSTMapNode<TKey, TValue> Parent
        {
            get { return this._parent; }
            set { this._parent = value; }
        }

        public virtual BSTMapNode<TKey, TValue> LeftChild
        {
            get { return this._left; }
            set { this._left = value; }
        }

        public virtual BSTMapNode<TKey, TValue> RightChild
        {
            get { return this._right; }
            set { this._right = value; }
        }

        /// <summary>
        /// Checks whether this node has any children.
        /// </summary>
        public virtual bool HasChildren
        {
            get { return (this.ChildrenCount > 0); }
        }

        /// <summary>
        /// Checks whether this node has left child.
        /// </summary>
        public virtual bool HasLeftChild
        {
            get { return (this.LeftChild != null); }
        }

        /// <summary>
        /// Checks whether this node has right child.
        /// </summary>
        public virtual bool HasRightChild
        {
            get { return (this.RightChild != null); }
        }

        /// <summary>
        /// Checks whether this node is the left child of it's parent.
        /// </summary>
        public virtual bool IsLeftChild
        {
            get { return (this.Parent != null && this.Parent.LeftChild == this); }
        }

        /// <summary>
        /// Checks whether this node is the left child of it's parent.
        /// </summary>
        public virtual bool IsRightChild
        {
            get { return (this.Parent != null && this.Parent.RightChild == this); }
        }

        /// <summary>
        /// Checks whether this node is a leaf node.
        /// </summary>
        public virtual bool IsLeafNode
        {
            get { return (this.ChildrenCount == 0); }
        }

        /// <summary>
        /// Returns number of direct descendents: 0, 1, 2 (none, left or right, or both).
        /// </summary>
        /// <returns>Number (0,1,2)</returns>
        public virtual int ChildrenCount
        {
            get
            {
                int count = 0;

                if (this.HasLeftChild)
                    count++;
                
                if (this.HasRightChild)
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

            return this.Key.CompareTo(other.Key);
        }
    }//end-of-bstnode
}
