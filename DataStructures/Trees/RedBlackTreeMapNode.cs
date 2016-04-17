namespace DataStructures.Trees
{
    /// <summary>
    /// Red-Black Tree Map Node.
    /// </summary>
    public class RedBlackTreeMapNode<TKey, TValue> : BSTMapNode<TKey, TValue> where TKey : System.IComparable<TKey>
    {
        private RedBlackTreeColors _color;

        /// <summary>
        /// CONSTRUCTORS
        /// </summary>
        public RedBlackTreeMapNode(TKey key) : this(key, default(TValue), 0, null, null, null) { }
        public RedBlackTreeMapNode(TKey key, TValue value) : this(key, value, 0, null, null, null) { }
        public RedBlackTreeMapNode(TKey key, TValue value, int height, RedBlackTreeMapNode<TKey, TValue> parent, RedBlackTreeMapNode<TKey, TValue> left, RedBlackTreeMapNode<TKey, TValue> right)
        {
            Key = key;
            Value = value;
            Color = RedBlackTreeColors.Red;
            Parent = parent;
            LeftChild = left;
            RightChild = right;
        }

        public virtual RedBlackTreeColors Color
        {
            get { return this._color; }
            set { this._color = value; }
        }

        public new RedBlackTreeMapNode<TKey, TValue> Parent
        {
            get { return (RedBlackTreeMapNode<TKey, TValue>)base.Parent; }
            set { base.Parent = value; }
        }

        public new RedBlackTreeMapNode<TKey, TValue> LeftChild
        {
            get { return (RedBlackTreeMapNode<TKey, TValue>)base.LeftChild; }
            set { base.LeftChild = value; }
        }

        public new RedBlackTreeMapNode<TKey, TValue> RightChild
        {
            get { return (RedBlackTreeMapNode<TKey, TValue>)base.RightChild; }
            set { base.RightChild = value; }
        }


        /******************************************************************************/


        /// <summary>
        /// Returns if this node is colored red.
        /// </summary>
        public virtual bool IsRed
        {
            get { return Color == RedBlackTreeColors.Red; }
        }

        /// <summary>
        /// Checks whether this node is colored black.
        /// </summary>
        public virtual bool IsBlack
        {
            get { return Color == RedBlackTreeColors.Black; }
        }

        /// <summary>
        /// Returns the sibling of this node.
        /// </summary>
        public virtual RedBlackTreeMapNode<TKey, TValue> Sibling
        {
            get { return (this.Parent == null ? null : (this.IsLeftChild ? this.Parent.RightChild : this.Parent.LeftChild)); }
        }

        /// <summary>
        /// Returns the grandparent of this node.
        /// </summary>
        public virtual RedBlackTreeMapNode<TKey, TValue> GrandParent
        {
            get { return (this.Parent == null ? null : this.Parent.Parent); }
        }

    }

}
