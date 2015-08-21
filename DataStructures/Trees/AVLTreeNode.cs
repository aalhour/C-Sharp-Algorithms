namespace DataStructures.Trees
{
    /// <summary>
    /// AVL Tree Node.
    /// </summary>
    public class AVLTreeNode<T> : BSTNode<T> where T : System.IComparable<T>
    {
        private int _height = 0;

        public AVLTreeNode() : this(default(T), 0, null, null, null) { }
        public AVLTreeNode(T value) : this(value, 0, null, null, null) { }
        public AVLTreeNode(T value, int height, AVLTreeNode<T> parent, AVLTreeNode<T> left, AVLTreeNode<T> right)
        {
            base.Value = value;
            Height = height;
            Parent = parent;
            LeftChild = left;
            RightChild = right;
        }

        public virtual int Height
        {
            get { return this._height; }
            set { this._height = value; }
        }

        public new AVLTreeNode<T> Parent
        {
            get { return (AVLTreeNode<T>)base.Parent; }
            set { base.Parent = value; }
        }

        public new AVLTreeNode<T> LeftChild
        {
            get { return (AVLTreeNode<T>)base.LeftChild; }
            set { base.LeftChild = value; }
        }

        public new AVLTreeNode<T> RightChild
        {
            get { return (AVLTreeNode<T>)base.RightChild; }
            set { base.RightChild = value; }
        }
    }
}
