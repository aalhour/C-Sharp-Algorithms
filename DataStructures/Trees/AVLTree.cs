using System;
using System.Collections.Generic;

using DataStructures;

namespace DataStructures.Trees
{
    /// <summary>
    /// AVL Tree Node.
    /// </summary>

    public class AVLTreeNode<T> : BSTNode<T> where T : IComparable<T>
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


    /*********************************************************************/


    /// <summary>
    /// AVL Tree Data Structure.
    /// </summary>

    public class AVLTree<T> : BinarySearchTree<T> where T : IComparable<T>
    {
        public new AVLTreeNode<T> Root
        {
            get { return (AVLTreeNode<T>)base.Root; }
            set { base.Root = value; }
        }

        public AVLTree()
            : base()
        {
        }

        /// <summary>
        /// Returns the height of a node.
        /// </summary>
        private int _getNodeHeight(AVLTreeNode<T> node)
        {
            if (node == null)
                return -1;
            else
                return node.Height;
        }

        /// <summary>
        /// Updates the height of a node.
        /// </summary>
        private void _updateNodeHeight(AVLTreeNode<T> node)
        {
            if (node == null)
                return;

            // height = 1 + the max between left and right children.
            node.Height = 1 + Math.Max(_getNodeHeight(node.LeftChild), _getNodeHeight(node.RightChild));
        }

        /// <summary>
        /// Updates the height of a node and it's parents' recursivley up to the root of the tree.
        /// </summary>
        private void _updateHeightRecursive(AVLTreeNode<T> node)
        {
            if (node == null)
                return;

            // height = 1 + the max between left and right children.
            node.Height = 1 + Math.Max(_getNodeHeight(node.LeftChild), _getNodeHeight(node.RightChild));

            _updateHeightRecursive(node.Parent);
        }

        /// <summary>
        /// Returns the AVL balance factor for a node.
        /// </summary>
        private int _getBalanceFactor(AVLTreeNode<T> node)
        {
            if (node == null)
                return -1;

            return (_getNodeHeight(node.RightChild) - _getNodeHeight(node.LeftChild));
        }

        /// <summary>
        /// Rotates a node to the left in the AVL tree.
        /// </summary>
        private void _rotateLeftAt(AVLTreeNode<T> currentNode)
        {
            // We check the right child because it's going to be a pivot node for the rotation
            if (currentNode == null || currentNode.HasRightChild == false)
                return;

            // Pivot on *right* child
            AVLTreeNode<T> pivotNode = currentNode.RightChild;

            // Parent of currentNode
            AVLTreeNode<T> parent = currentNode.Parent;

            // Check if currentNode is it's parent's left child.
            bool isLeftChild = currentNode.IsLeftChild;

            // Check if currentNode is the Root
            bool isRootNode = (currentNode == this.Root);

            // Perform the rotation
            currentNode.RightChild = pivotNode.LeftChild;
            pivotNode.LeftChild = currentNode;

            // Update parents references
            currentNode.Parent = pivotNode;
            pivotNode.Parent = parent;

            if (currentNode.HasRightChild)
                currentNode.RightChild.Parent = currentNode;

            //Update the entire tree's Root if necessary
            if (isRootNode)
                this.Root = pivotNode;

            // Update the original parent's child node
            if (isLeftChild)
                parent.LeftChild = pivotNode;
            else if (parent != null)
                parent.RightChild = pivotNode;

            // Update the AVL Heights of each node.
            // _updateHeightRecursive(pivotNode);
            _updateHeightRecursive(currentNode);
        }

        /// <summary>
        /// Rotates a node to the right in the AVL tree.
        /// </summary>
        private void _rotateRightAt(AVLTreeNode<T> currentNode)
        {
            // We check the right child because it's going to be a pivot node for the rotation
            if (currentNode == null || currentNode.HasLeftChild == false)
                return;

            // Pivot on *left* child
            var pivotNode = currentNode.LeftChild;

            // Parent of currentNode
            var parent = currentNode.Parent;

            // Check if currentNode is it's parent's left child.
            bool isLeftChild = currentNode.IsLeftChild;

            // Check if currentNode is the Root
            bool isRootNode = (currentNode == this.Root);

            // Perform the rotation
            currentNode.LeftChild = pivotNode.RightChild;
            pivotNode.RightChild = currentNode;

            // Update parents references
            currentNode.Parent = pivotNode;
            pivotNode.Parent = parent;

            if (currentNode.HasLeftChild)
                currentNode.LeftChild.Parent = currentNode;

            // Update the entire tree's Root if necessary
            if (isRootNode)
                this.Root = pivotNode;

            // Update the original parent's child node
            if (isLeftChild)
                parent.LeftChild = pivotNode;
            else if (parent != null)
                parent.RightChild = pivotNode;

            // Update the AVL Heights of each node.
            // _updateHeightRecursive(pivotNode);
            _updateHeightRecursive(currentNode);
        }

        /// <summary>
        /// Rebalances the tree around a node.
        /// </summary>
        private void _rebalanceSubtreeTreeAt(AVLTreeNode<T> currentNode)
        {
            if (currentNode == null)
                return;

            int balance = _getBalanceFactor(currentNode);

            // Balance the tree only if the balance factor was less than -1 or greater than +1.
            if (Math.Abs(balance) >= 2) // -2 or 2; -3 or 3 ... etc
            {
                // if balance is a positive number: 2, 3 ... etc
                // right subtree outweighs
                if (balance > 0)
                {
                    int rightSubtreeBalance = _getBalanceFactor(currentNode.RightChild);

                    if (rightSubtreeBalance == 0 || rightSubtreeBalance == 1)
                    {
                        // Rotate *LEFT* on current node
                        _rotateLeftAt(currentNode);
                    }
                    else if (rightSubtreeBalance == -1)
                    {
                        // Rotate *RIGHT* on right child
                        _rotateRightAt(currentNode.RightChild);

                        // Rotate *LEFT* on current node
                        _rotateLeftAt(currentNode);
                    }
                }
                // if balance is a negative number: -2, -3 ... etc
                // left subtree outweighs
                else
                {
                    int leftSubtreeBalance = _getBalanceFactor(currentNode.LeftChild);

                    if (leftSubtreeBalance == 0 || leftSubtreeBalance == 1)
                    {
                        // Rotate *RIGHT* on current node
                        _rotateRightAt(currentNode);
                    }
                    else if (leftSubtreeBalance == -1)
                    {
                        // Rotate *LEFT* on left child
                        _rotateLeftAt(currentNode.LeftChild);

                        // Rotate *RIGHT* on right child
                        _rotateRightAt(currentNode);
                    }
                }
            }
        }

        /// <summary>
        /// Rebalances the whole tree around a node.
        /// </summary>
        private void _rebalanceTreeAt(AVLTreeNode<T> node)
        {
            var currentNode = node;
            while (currentNode != null)
            {
                // Update this node's height value.
                _updateNodeHeight(currentNode);

                // Get left and right for comparisons
                var left = currentNode.LeftChild;
                var right = currentNode.RightChild;

                if (_getNodeHeight(left) >= 2 + _getNodeHeight(right))
                {
                    if (currentNode.HasLeftChild && _getNodeHeight(left.LeftChild) >= _getNodeHeight(left.RightChild))
                    {
                        _rotateRightAt(currentNode);
                    }
                    else
                    {
                        _rotateLeftAt(currentNode.LeftChild);
                        _rotateRightAt(currentNode);
                    }
                }
                else if (_getNodeHeight(right) >= 2 + _getNodeHeight(left))
                {
                    if (currentNode.HasRightChild && _getNodeHeight(right.RightChild) >= _getNodeHeight(right.LeftChild))
                    {
                        _rotateLeftAt(currentNode);
                    }
                    else
                    {
                        _rotateRightAt(currentNode.RightChild);
                        _rotateLeftAt(currentNode);
                    }
                }

                currentNode = currentNode.Parent;
            }
        }

        /// <summary>
        /// Inserts an item to the tree.
        /// </summary>
        public override void Insert(T item)
        {
            // New node object
            var newNode = new AVLTreeNode<T>() { Value = item };

            // Invoke the BST insert node method.
            base._insertNode(newNode);

            // Rebalance the tree
            _rebalanceTreeAt(newNode);
        }

        /// <summary>
        /// Removes an item fromt he tree
        /// </summary>
        public override void Remove(T item)
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");

            // Get the node from the tree
            var node = (AVLTreeNode<T>)base._findNode(Root, item);

            // Invoke the BST remove node method.
            bool status = base._remove(node);

            if (status == true)
            {
                // Rebalance the tree
                // node.parent is actually the old parent of the node,
                // which is the first potentially out-of-balance node.
                _rebalanceTreeAt(node);
            }
            else
            {
                throw new Exception("Item was not found.");
            }

            //// Update the node's parent height.
            //_updateHeightRecursively(node.Parent);

            //// Rebalance the tree
            //var parent = node.Parent;
            //while(parent != null)
            //{
            //    _rebalanceSubtreeTreeAt(node.Parent);
            //}
        }

        /// <summary>
        /// Removes the min value from tree.
        /// </summary>
        public override void RemoveMin()
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");

            // Get the node from the tree
            var node = (AVLTreeNode<T>)base._findMinNode(Root);

            // Invoke the BST remove node method.
            base._remove(node);

            // Rebalance the tree
            // node.parent is actually the old parent of the node,
            // which is the first potentially out-of-balance node.
            _rebalanceTreeAt(node);
        }

        /// <summary>
        /// Removes the max value from tree.
        /// </summary>
        public override void RemoveMax()
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");

            // Get the node from the tree
            var node = (AVLTreeNode<T>)base._findMaxNode(Root);

            // Invoke the BST remove node method.
            base._remove(node);

            // Rebalance the tree
            // node.parent is actually the old parent of the node,
            // which is the first potentially out-of-balance node.
            _rebalanceTreeAt(node);
        }

        /// <summary>
        /// Overrides the BST sort function. Sorts the tree using AVL Sort.
        /// </summary>
        public override List<T> Sort()
        {
            return this.AVLSort();
        }

        public List<T> AVLSort()
        {
            throw new NotImplementedException();
        }

    }

}
