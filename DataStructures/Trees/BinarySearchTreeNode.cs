namespace DataStructures.Trees
{
    /// <summary>
    /// The binary search tree node.
    /// </summary>
    public class BSTNode<T> : System.IComparable<BSTNode<T>> where T : System.IComparable<T>
    {
        private T _value;
        private BSTNode<T> _parent;
        private BSTNode<T> _left;
        private BSTNode<T> _right;

        public BSTNode() : this(default(T), 0, null, null, null) { }
        public BSTNode(T value) : this(value, 0, null, null, null) { }
        public BSTNode(T value, int subTreeSize, BSTNode<T> parent, BSTNode<T> left, BSTNode<T> right)
        {
            Value = value;
            Parent = parent;
            LeftChild = left;
            RightChild = right;
        }

        public virtual T Value
        {
            get { return this._value; }
            set { this._value = value; }
        }

        public virtual BSTNode<T> Parent
        {
            get { return this._parent; }
            set { this._parent = value; }
        }

        public virtual BSTNode<T> LeftChild
        {
            get { return this._left; }
            set { this._left = value; }
        }

        public virtual BSTNode<T> RightChild
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
        ///     Check if this node has only one child and whether it is the right child.
        /// </summary>
        public virtual bool HasOnlyRightChild => !this.HasLeftChild && this.HasRightChild;

        /// <summary>
        /// Checks whether this node has right child.
        /// </summary>
        public virtual bool HasRightChild
        {
            get { return (this.RightChild != null); }
        }

        /// <summary>
        ///     Check if this node has only one child and whether it is the left child.
        /// </summary>
        public virtual bool HasOnlyLeftChild => !this.HasRightChild && this.HasLeftChild;

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
        public virtual int CompareTo(BSTNode<T> other)
        {
            if (other == null)
                return -1;

            return this.Value.CompareTo(other.Value);
        }
    }//end-of-bstnode
}
