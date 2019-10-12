using DataStructures.Common;
using System;
using System.Collections.Generic;

namespace DataStructures.Trees
{
    /// <summary>
    /// THE NODE COLORS TYPE
    /// </summary>
    public enum RedBlackTreeColors
    {
        Red = 0,
        Black = 1
    };

    /// <summary>
    /// Red-Black Tree Data Structure.
    /// </summary>
    public class RedBlackTree<TKey> : BinarySearchTree<TKey> where TKey : IComparable<TKey>
    {
        /// <summary>
        /// Root node accessors overriding
        /// </summary>
        public new RedBlackTreeNode<TKey> Root
        {
            get { return (RedBlackTreeNode<TKey>)base.Root; }
            internal set { base.Root = value; }
        }

        private bool IsRoot(RedBlackTreeNode<TKey> node)
        {
            return node == this.Root;
        }

        /// <summary>
        /// CONSTRUCTOR.
        /// Allows duplicates by default.
        /// </summary>
        public RedBlackTree() : base() { }

        /// <summary>
        /// CONSTRUCTOR.
        /// If allowDuplictes is set to false, no duplicate items will be inserted.
        /// </summary>
        public RedBlackTree(bool allowDuplicates) : base(allowDuplicates) { }



        /*************************************************************************************************/
        /***
         * Safety Checks/Getters/Setters.
         * 
         * The following are helper methods for safely checking, getting and updating possibly-null objects.
         * These helpers make the algorithms of adjusting the tree after insertion and removal more readable.
         */

        protected RedBlackTreeNode<TKey> _safeGetGrandParent(RedBlackTreeNode<TKey> node)
        {
            if (node == null || node.Parent == null)
                return null;

            return node.GrandParent;
        }

        protected RedBlackTreeNode<TKey> _safeGetParent(RedBlackTreeNode<TKey> node)
        {
            if (node == null || node.Parent == null)
                return null;

            return node.Parent;
        }

        protected RedBlackTreeNode<TKey> _safeGetSibling(RedBlackTreeNode<TKey> node)
        {
            if (node == null || node.Parent == null)
                return null;

            return node.Sibling;
        }

        protected RedBlackTreeNode<TKey> _safeGetLeftChild(RedBlackTreeNode<TKey> node)
        {
            if (node == null)
                return null;

            return node.LeftChild;
        }

        protected RedBlackTreeNode<TKey> _safeGetRightChild(RedBlackTreeNode<TKey> node)
        {
            if (node == null)
                return null;

            return node.RightChild;
        }

        protected virtual RedBlackTreeColors _safeGetColor(RedBlackTreeNode<TKey> node)
        {
            if (node == null)
                return RedBlackTreeColors.Black;

            return node.Color;
        }

        protected virtual void _safeUpdateColor(RedBlackTreeNode<TKey> node, RedBlackTreeColors color)
        {
            if (node == null)
                return;

            node.Color = color;
        }

        protected virtual bool _safeCheckIsBlack(RedBlackTreeNode<TKey> node)
        {
            return (node == null || (node != null && node.IsBlack));
        }

        protected virtual bool _safeCheckIsRed(RedBlackTreeNode<TKey> node)
        {
            return (node != null && node.IsRed);
        }


        /*************************************************************************************************/
        /***
         * Tree Rotations and Adjustements.
         * 
         * The following are methods for rotating the tree (left/right) and for adjusting the 
         * ... tree after inserting or removing nodes.
         */

        /// <summary>
        /// Rotates a node to the left in the Red-Black Tree.
        /// </summary>
        protected virtual void _rotateLeftAt(RedBlackTreeNode<TKey> currentNode)
        {
            // We check the right child because it's going to be a pivot node for the rotation
            if (currentNode == null || currentNode.HasRightChild == false)
                return;

            // Pivot on *right* child
            RedBlackTreeNode<TKey> pivotNode = currentNode.RightChild;

            // Parent of currentNode
            RedBlackTreeNode<TKey> parent = currentNode.Parent;

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
        }

        /// <summary>
        /// Rotates a node to the right in the Red-Black Tree.
        /// </summary>
        protected virtual void _rotateRightAt(RedBlackTreeNode<TKey> currentNode)
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
        }

        /// <summary>
        /// After insertion tree-adjustement helper.
        /// </summary>
        protected virtual void _adjustTreeAfterInsertion(RedBlackTreeNode<TKey> currentNode)
        {
            //
            // STEP 1:
            // Color the currentNode as red
            _safeUpdateColor(currentNode, RedBlackTreeColors.Red);

            //
            // STEP 2:
            // Fix the double red-consecutive-nodes problems, if there exists any.
            if (currentNode != null && currentNode != Root && _safeCheckIsRed(_safeGetParent(currentNode)))
            {
                //
                // STEP 2.A:
                // This is the simplest step: Basically recolor, and bubble up to see if more work is needed.
                if (_safeCheckIsRed(_safeGetSibling(currentNode.Parent)))
                {
                    // If it has a sibling and it is black, then then it has a parent
                    currentNode.Parent.Color = RedBlackTreeColors.Black;

                    // Color sibling of parent as black
                    _safeUpdateColor(_safeGetSibling(currentNode.Parent), RedBlackTreeColors.Black);

                    // Color grandparent as red
                    _safeUpdateColor(_safeGetGrandParent(currentNode), RedBlackTreeColors.Red);

                    // Adjust on the grandparent of currentNode
                    _adjustTreeAfterInsertion(_safeGetGrandParent(currentNode));
                }

                //
                // STEP 2.B:
                // Restructure the tree if the parent of currentNode is a left child to the grandparent of currentNode
                // (parent is a left child to its own parent).
                // If currentNode is also a left child, then do a single right rotation; otherwise, a left-right rotation.
                //
                // using the safe methods to check: currentNode.Parent.IsLeftChild == true
                else if (_safeGetParent(currentNode) == _safeGetLeftChild(_safeGetGrandParent(currentNode)))
                {
                    if (currentNode.IsRightChild)
                    {
                        currentNode = _safeGetParent(currentNode);
                        _rotateLeftAt(currentNode);
                    }

                    // Color parent as black
                    _safeUpdateColor(_safeGetParent(currentNode), RedBlackTreeColors.Black);

                    // Color grandparent as red
                    _safeUpdateColor(_safeGetGrandParent(currentNode), RedBlackTreeColors.Red);

                    // Right Rotate tree around the currentNode's grand parent
                    _rotateRightAt(_safeGetGrandParent(currentNode));
                }

                //
                // STEP 2.C: 
                // Restructure the tree if the parent of currentNode is a right child to the grandparent of currentNode
                // (parent is a right child to its own parent).
                // If currentNode is a right-child in it's parent, then do a single left rotation; otherwise a right-left rotation.
                //
                // using the safe methods to check: currentNode.Parent.IsRightChild == true
                else if (_safeGetParent(currentNode) == _safeGetRightChild(_safeGetGrandParent(currentNode)))
                {
                    if (currentNode.IsLeftChild)
                    {
                        currentNode = _safeGetParent(currentNode);
                        _rotateRightAt(currentNode);
                    }

                    // Color parent as black
                    _safeUpdateColor(_safeGetParent(currentNode), RedBlackTreeColors.Black);

                    // Color grandparent as red
                    _safeUpdateColor(_safeGetGrandParent(currentNode), RedBlackTreeColors.Red);

                    // Left Rotate tree around the currentNode's grand parent
                    _rotateLeftAt(_safeGetGrandParent(currentNode));
                }
            }

            // STEP 3:
            // Color the root node as black
            _safeUpdateColor(Root, RedBlackTreeColors.Black);
        }

        /// <summary>
        /// After removal tree-adjustement helper.
        /// </summary>
        protected virtual void _adjustTreeAfterRemoval(RedBlackTreeNode<TKey> currentNode)
        {
            while (currentNode != null && currentNode != Root && currentNode.IsBlack)
            {
                if (currentNode.IsLeftChild)
                {
                    // Get sibling of currentNode
                    // Safe equivalent of currentNode.Sibling or currentNode.Parent.RightChild
                    var sibling = _safeGetRightChild(_safeGetParent(currentNode));

                    // Safely check sibling.IsRed property
                    if (_safeCheckIsRed(sibling))
                    {
                        // Color currentNode.Sibling as black
                        _safeUpdateColor(sibling, RedBlackTreeColors.Black);

                        // Color currentNode.Parent as red
                        _safeUpdateColor(_safeGetParent(currentNode), RedBlackTreeColors.Red);

                        // Left Rotate on currentNode's parent
                        _rotateLeftAt(_safeGetParent(currentNode));

                        // Update sibling reference
                        // Might end be being set to null
                        sibling = _safeGetRightChild(_safeGetParent(currentNode));
                    }

                    // Check if the left and right children of the sibling node are black
                    // Use the safe methods to check for: (sibling.LeftChild.IsBlack && sibling.RightChild.IsBlack)
                    if (_safeCheckIsBlack(_safeGetLeftChild(sibling)) && _safeCheckIsBlack(_safeGetRightChild(sibling)))
                    {
                        // Color currentNode.Sibling as red
                        _safeUpdateColor(sibling, RedBlackTreeColors.Red);

                        // Assign currentNode.Parent to currentNode 
                        currentNode = _safeGetParent(currentNode);
                    }
                    else
                    {
                        if (_safeCheckIsBlack(_safeGetRightChild(sibling)))
                        {
                            // Color currentNode.Sibling.LeftChild as black
                            _safeUpdateColor(_safeGetLeftChild(sibling), RedBlackTreeColors.Black);

                            // Color currentNode.Sibling as red
                            _safeUpdateColor(sibling, RedBlackTreeColors.Red);

                            // Right Rotate on sibling
                            _rotateRightAt(sibling);

                            // Update sibling reference
                            // Might end be being set to null
                            sibling = _safeGetRightChild(_safeGetParent(currentNode));
                        }

                        // Color the Sibling node as currentNode.Parent.Color
                        _safeUpdateColor(sibling, _safeGetColor(_safeGetParent(currentNode)));

                        // Color currentNode.Parent as black
                        _safeUpdateColor(_safeGetParent(currentNode), RedBlackTreeColors.Black);

                        // Color Sibling.RightChild as black
                        _safeUpdateColor(_safeGetRightChild(sibling), RedBlackTreeColors.Black);

                        // Rotate on currentNode's parent
                        _rotateLeftAt(_safeGetParent(currentNode));

                        currentNode = Root;
                    }
                }
                else
                {
                    // Get sibling of currentNode
                    // Safe equivalent of currentNode.Sibling or currentNode.Parent.LeftChild
                    var sibling = _safeGetLeftChild(_safeGetParent(currentNode));

                    if (_safeCheckIsRed(sibling))
                    {
                        // Color currentNode.Sibling as black
                        _safeUpdateColor(sibling, RedBlackTreeColors.Black);

                        // Color currentNode.Parent as red
                        _safeUpdateColor(_safeGetParent(currentNode), RedBlackTreeColors.Red);

                        // Right Rotate tree around the parent of currentNode
                        _rotateRightAt(_safeGetParent(currentNode));

                        // Update sibling reference
                        // Might end be being set to null
                        sibling = _safeGetLeftChild(_safeGetParent(currentNode));
                    }

                    // Check if the left and right children of the sibling node are black
                    // Use the safe methods to check for: (sibling.LeftChild.IsBlack && sibling.RightChild.IsBlack)
                    if (_safeCheckIsBlack(_safeGetLeftChild(sibling)) && _safeCheckIsBlack(_safeGetRightChild(sibling)))
                    {
                        _safeUpdateColor(sibling, RedBlackTreeColors.Red);

                        // Assign currentNode.Parent to currentNode 
                        currentNode = _safeGetParent(currentNode);
                    }
                    else
                    {
                        // Check if sibling.LeftChild.IsBlack == true
                        if (_safeCheckIsBlack(_safeGetLeftChild(sibling)))
                        {
                            // Color currentNode.Sibling.RightChild as black
                            _safeUpdateColor(_safeGetRightChild(sibling), RedBlackTreeColors.Black);

                            // Color currentNode.Sibling as red
                            _safeUpdateColor(sibling, RedBlackTreeColors.Red);

                            // Left rotate on sibling
                            _rotateLeftAt(sibling);

                            // Update sibling reference
                            // Might end be being set to null
                            sibling = _safeGetLeftChild(_safeGetParent(currentNode));
                        }

                        // Color the Sibling node as currentNode.Parent.Color
                        _safeUpdateColor(sibling, _safeGetColor(_safeGetParent(currentNode)));

                        // Color currentNode.Parent as black
                        _safeUpdateColor(_safeGetParent(currentNode), RedBlackTreeColors.Black);

                        // Color Sibling.RightChild as black
                        _safeUpdateColor(_safeGetLeftChild(sibling), RedBlackTreeColors.Black);

                        // Right rotate on the parent of currentNode
                        _rotateRightAt(_safeGetParent(currentNode));

                        currentNode = Root;
                    }
                }
            }

            // Color currentNode as black
            _safeUpdateColor(currentNode, RedBlackTreeColors.Black);
        }

        /// <summary>
        /// Remove node helpers.
        /// </summary>
        protected override bool _remove(BSTNode<TKey> nodeToDelete)
        {
            return this._remove((RedBlackTreeNode<TKey>)nodeToDelete);
        }

        /// <summary>
        ///     The internal remove helper.
        ///     Separated from the overriden version to avoid casting the objects from BSTNode to RedBlackTreeNode.
        ///     This is called from the overriden _remove(BSTNode nodeToDelete) helper.
        /// </summary>
        protected bool _remove(RedBlackTreeNode<TKey> nodeToDelete)
        {
            if (nodeToDelete == null)
            {
                return false;
            }

            if (IsRoot(nodeToDelete) && !nodeToDelete.HasChildren)
            {
                Root = null;
            }
            else
            {
                // X is the node we will replace with the nodeToDelete in the tree once we remove it.
                RedBlackTreeNode<TKey> x;

                if (!nodeToDelete.HasChildren)
                {
                    x = nodeToDelete;
                    Transplant(nodeToDelete, null);
                }
                else if (nodeToDelete.HasOnlyRightChild)
                {
                    x = nodeToDelete.RightChild;
                    Transplant(nodeToDelete, nodeToDelete.RightChild);
                }
                else if (nodeToDelete.HasOnlyLeftChild)
                {
                    x = nodeToDelete.LeftChild;
                    Transplant(nodeToDelete, nodeToDelete.LeftChild);
                }
                else
                {
                    // Y is the node we will replace with the X in the tree once we move it to the nodeToDelete position.
                    var y = (RedBlackTreeNode<TKey>)_findMinNode(nodeToDelete.RightChild);
                    x = y.RightChild;

                    if (y.Parent == nodeToDelete)
                    {
                        if (x != null)
                        {
                            x.Parent = y;
                        }
                    }
                    else
                    {
                        Transplant(y, y.RightChild);
                        y.RightChild = nodeToDelete.RightChild;
                        y.RightChild.Parent = y;
                    }

                    Transplant(nodeToDelete, y);
                    y.LeftChild = nodeToDelete.LeftChild;
                    y.LeftChild.Parent = y;
                    y.Color = nodeToDelete.Color;

                    if (Root == nodeToDelete)
                    {
                        Root = y;
                        Root.Parent = null;
                    }
                }

                if (nodeToDelete.Color == RedBlackTreeColors.Black)
                {
                    _adjustTreeAfterRemoval(x);
                }
            }

            base._count--;

            return true;
        }

        /// <summary>
        ///     Insert one subtree in the place of the other in his parent.
        /// </summary>
        /// <param name="replaced">Subtree of node will be replaced by <param name="replacement">.</param></param>
        /// <param name="replacement">Subtree replaces <param name="replaced">.</param></param>
        private void Transplant(RedBlackTreeNode<TKey> replaced, RedBlackTreeNode<TKey> replacement)
        {
            if (replaced.Parent == null)
            {
                this.Root = replacement;
            }
            else if (replaced == replaced.Parent.LeftChild)
            {
                replaced.Parent.LeftChild = replacement;
            }
            else
            {
                replaced.Parent.RightChild = replacement;
            }

            if (replacement != null)
            {
                replacement.Parent = replaced.Parent;
            }
        }
        /*************************************************************************************************/


        /// <summary>
        /// Insert data item to tree
        /// </summary>
        public override void Insert(TKey item)
        {
            var newNode = new RedBlackTreeNode<TKey>(item);

            // Invoke the super BST insert node method.
            // This insert node recursively starting from the root and checks for success status (related to allowDuplicates flag).
            // The functions increments count on its own.
            var success = base._insertNode(newNode);

            if (success == false && _allowDuplicates == false)
                throw new InvalidOperationException("Tree does not allow inserting duplicate elements.");

            // Adjust Red-Black Tree rules
            if (!newNode.IsEqualTo(Root))
                if (newNode.Parent.Color != RedBlackTreeColors.Black) // Case 0: Parent is not black and we have to restructure tree
                    _adjustTreeAfterInsertion(newNode);

            // Always color root as black
            Root.Color = RedBlackTreeColors.Black;
        }

        /// <summary>
        /// Inserts an array of elements to the tree.
        /// </summary>
        public override void Insert(TKey[] collection)
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
        public override void Insert(List<TKey> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();

            if (collection.Count > 0)
                for (int i = 0; i < collection.Count; ++i)
                    this.Insert(collection[i]);
        }

        /// <summary>
        /// Removes an item from the tree.
        /// </summary>
        public override void Remove(TKey item)
        {
            if (IsEmpty)
                throw new Exception("Tree is empty.");

            // Get the node from the tree
            var node = (RedBlackTreeNode<TKey>)base._findNode(Root, item);

            // Invoke the internal remove node method.
            bool status = this._remove(node);

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

            // Get the node from the tree
            var node = (RedBlackTreeNode<TKey>)base._findMinNode(Root);

            // Invoke the internal remove node method.
            this._remove(node);
        }

        /// <summary>
        /// Removes the max value from tree.
        /// </summary>
        public override void RemoveMax()
        {
            if (IsEmpty)
                throw new Exception("Tree is empty.");

            // Get the node from the tree
            var node = (RedBlackTreeNode<TKey>)base._findMaxNode(Root);

            // Invoke the internal remove node method.
            this._remove(node);
        }
    }
}
