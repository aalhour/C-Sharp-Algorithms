using System;
using System.Collections.Generic;

namespace DataStructures.Trees
{
    /// <summary>
    /// Binary Search Tree node.
    /// This node extends the vanilla BSTNode class and adds an extra field to it for augmentation.
    /// The BST now augments the subtree-sizes on insert, delete and get-height.
    /// </summary>

    public class BSTRankedNode<T> : BSTNode<T> where T : IComparable<T>
    {
        private int _subtreeSize = 0;

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
        public virtual int SubtreeSize
        {
            get { return this._subtreeSize; }
            set { this._subtreeSize = value; }
        }

        public new BSTRankedNode<T> Parent
        {
            get { return (BSTRankedNode<T>)base.Parent; }
            set { base.Parent = value; }
        }

        public new BSTRankedNode<T> LeftChild
        {
            get { return (BSTRankedNode<T>)base.LeftChild; }
            set { base.LeftChild = value; }
        }

        public new BSTRankedNode<T> RightChild
        {
            get { return (BSTRankedNode<T>)base.RightChild; }
            set { base.RightChild = value; }
        }
    }


    /******************************************************************************/


    /// <summary>
    /// Binary Search Tree Data Structure.
    /// This is teh augmented version of BST. It is augmented to keep track of the nodes subtrees-sizes.
    /// </summary>

    public class AugmentedBinarySearchTree<T> : BinarySearchTree<T> where T : IComparable<T>
    {
        /// <summary>
        /// Override the Root node accessors.
        /// </summary>
        public new BSTRankedNode<T> Root
        {
            get { return (BSTRankedNode<T>)base.Root; }
            set { base.Root = value; }
        }


        /// <summary>
        /// CONSTRUCTOR.
        /// Allows duplicates by default.
        /// </summary>
        public AugmentedBinarySearchTree() : base() { }

        /// <summary>
        /// CONSTRUCTOR.
        /// If allowDuplictes is set to false, no duplicate items will be inserted.
        /// </summary>
        public AugmentedBinarySearchTree(bool allowDuplicates) : base(allowDuplicates) { }


        /// <summary>
        /// Returns the height of the tree.
        /// </summary>
        /// <returns>Hight</returns>
        public override int Height
        {
            get
            {
                if (IsEmpty)
                    return 0;

                var currentNode = this.Root;
                return this._getTreeHeight(currentNode);
            }
        }

        /// <summary>
        /// Returns the Subtrees size for a tree node if node exists; otherwise 0 (left and right nodes of leafs).
        /// This is used in the recursive function UpdateSubtreeSize.
        /// </summary>
        /// <returns>The size.</returns>
        /// <param name="node">BST Node.</param>
        protected int _subtreeSize(BSTRankedNode<T> node)
        {
            if (node == null)
                return 0;
            return node.SubtreeSize;
        }

        /// <summary>
        /// Updates the Subtree Size of a tree node.
        /// Used in recusively calculating the Subtrees Sizes of nodes.
        /// </summary>
        /// <param name="node">BST Node.</param>
        protected void _updateSubtreeSize(BSTRankedNode<T> node)
        {
            if (node == null)
                return;

            node.SubtreeSize = _subtreeSize(node.LeftChild) + _subtreeSize(node.RightChild) + 1;

            _updateSubtreeSize(node.Parent);
        }

        /// <summary>
        /// Remove the specified node.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <returns>>True if removed successfully; false if node wasn't found.</returns>
        protected bool _remove(BSTRankedNode<T> node)
        {
            if (node == null)
                return false;

            var parent = node.Parent;

            if (node.ChildrenCount == 2) // if both children are present
            {
                var successor = _findNextLarger(node);
                node.Value = successor.Value;
                return (true && _remove(successor));
            }

            if (node.HasLeftChild) // if the node has only a LEFT child
            {
                base._replaceNodeInParent(node, node.LeftChild);
                _updateSubtreeSize(parent);
                _count--;

            }
            else if (node.HasRightChild) // if the node has only a RIGHT child
            {
                base._replaceNodeInParent(node, node.RightChild);
                _updateSubtreeSize(parent);
                _count--;
            }
            else //this node has no children
            {
                base._replaceNodeInParent(node, null);
                _updateSubtreeSize(parent);
                _count--;
            }

            return true;
        }

        /// <summary>
        /// Calculates the tree height from a specific node, recursively.
        /// </summary>
        /// <param name="node">Node</param>
        /// <returns>Height of node's longest subtree</returns>
        protected int _getTreeHeight(BSTRankedNode<T> node)
        {
            if (node == null || node.HasChildren == false)
                return 0;

            if (node.ChildrenCount == 2) // it has both a right child and a left child
            {
                if (node.LeftChild.SubtreeSize > node.RightChild.SubtreeSize)
                    return (1 + _getTreeHeight(node.LeftChild));
                return (1 + _getTreeHeight(node.RightChild));
            }

            if (node.HasLeftChild)
            {
                return (1 + _getTreeHeight(node.LeftChild));
            }

            if (node.HasRightChild)
            {
                return (1 + _getTreeHeight(node.RightChild));
            }

            // return-functions-fix
            return 0;
        }

        /// <summary>
        /// Inserts an element to the tree
        /// </summary>
        /// <param name="item">Item to insert</param>
        public override void Insert(T item)
        {
            var newNode = new BSTRankedNode<T>(item);

            // Invoke the super BST insert node method.
            // This insert node recursively starting from the root and checks for success status (related to allowDuplicates flag).
            // The functions increments count on its own.
            var success = base._insertNode(newNode);

            if (success == false && _allowDuplicates == false)
                throw new InvalidOperationException("Tree does not allow inserting duplicate elements.");

            // Update the subtree-size for the newNode's parent.
            _updateSubtreeSize(newNode.Parent);
        }

        /// <summary>
        /// Inserts an array of elements to the tree.
        /// </summary>
        public override void Insert(T[] collection)
        {
            if (collection == null)
                throw new ArgumentNullException();

            if (collection.Length > 0)
                for (int i = 0; i < collection.Length; ++i)
                    this.Insert(collection[i]);
        }

        /// <summary>
        /// Inserts a list of elements to the tree.
        /// </summary>
        public override void Insert(List<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();

            if (collection.Count > 0)
                for (int i = 0; i < collection.Count; ++i)
                    this.Insert(collection[i]);
        }

        /// <summary>
        /// Deletes an element from the tree
        /// </summary>
        /// <param name="item">item to remove.</param>
        public override void Remove(T item)
        {
            if (IsEmpty)
                throw new Exception("Tree is empty.");

            var node = (BSTRankedNode<T>)base._findNode(this.Root, item);
            bool status = _remove(node);
            this._updateSubtreeSize(node.Parent);

            // If the element was found, remove it.
            if (status == false)
                throw new Exception("Item was not found.");
        }

        /// <summary>
        /// Removes the min value from tree.
        /// </summary>
        public override void RemoveMin()
        {
            if (IsEmpty)
                throw new Exception("Tree is empty.");

            var node = (BSTRankedNode<T>)_findMinNode(this.Root);
            var parent = node.Parent;
            this._remove(node);

            // Update the subtrees-sizes
            this._updateSubtreeSize(parent);
        }

        /// <summary>
        /// Removes the max value from tree.
        /// </summary>
        public override void RemoveMax()
        {
            if (IsEmpty)
                throw new Exception("Tree is empty.");

            var node = (BSTRankedNode<T>)_findMaxNode(this.Root);
            var parent = node.Parent;
            this._remove(node);

            // Update the subtrees-sizes
            this._updateSubtreeSize(parent);
        }

        /// <summary>
        /// Returns the rank of the specified element
        /// </summary>
        /// <param name="item">Tree element</param>
        /// <returns>Rank(item) if found; otherwise throws an exception.</returns>
        public virtual int Rank(T item)
        {
            var node = (BSTRankedNode<T>)base._findNode(this.Root, item);

            if (node == null)
                throw new Exception("Item was not found.");
            return (this._subtreeSize(node.LeftChild) + 1);
        }

    }

}
