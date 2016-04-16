using System;
using System.Collections.Generic;

using DataStructures.Common;

namespace DataStructures.Trees
{
    /// <summary>
    /// Red-Black Tree Data Structure.
    /// </summary>
    public class RedBlackTreeMap<TKey, TValue> : BinarySearchTreeMap<TKey, TValue> where TKey : IComparable<TKey>
    {
        /// <summary>
        /// Root node accessors overriding
        /// </summary>
        public new RedBlackTreeMapNode<TKey, TValue> Root
        {
            get { return (RedBlackTreeMapNode<TKey, TValue>)base.Root; }
            internal set { base.Root = value; }
        }


        /// <summary>
        /// CONSTRUCTOR.
        /// Allows duplicates by default.
        /// </summary>
        public RedBlackTreeMap() : base() { }

        /// <summary>
        /// CONSTRUCTOR.
        /// If allowDuplictes is set to false, no duplicate items will be inserted.
        /// </summary>
        public RedBlackTreeMap(bool allowDuplicates) : base(allowDuplicates) { }



        /*************************************************************************************************/
        /***
        * Safety Checks/Getters/Setters.
        * 
        * The following are helper methods for safely checking, getting and updating possibly-null objects.
            * These helpers make the algorithms of adjusting the tree after insertion and removal more readable.
            */

            protected RedBlackTreeMapNode<TKey, TValue> _safeGetGrandParent(RedBlackTreeMapNode<TKey, TValue> node)
        {
            if (node == null || node.Parent == null)
                return null;

            return node.GrandParent;
        }

        protected RedBlackTreeMapNode<TKey, TValue> _safeGetParent(RedBlackTreeMapNode<TKey, TValue> node)
        {
            if (node == null || node.Parent == null)
                return null;

            return node.Parent;
        }

        protected RedBlackTreeMapNode<TKey, TValue> _safeGetSibling(RedBlackTreeMapNode<TKey, TValue> node)
        {
            if (node == null || node.Parent == null)
                return null;

            return node.Sibling;
        }

        protected RedBlackTreeMapNode<TKey, TValue> _safeGetLeftChild(RedBlackTreeMapNode<TKey, TValue> node)
        {
            if (node == null)
                return null;

            return node.LeftChild;
        }

        protected RedBlackTreeMapNode<TKey, TValue> _safeGetRightChild(RedBlackTreeMapNode<TKey, TValue> node)
        {
            if (node == null)
                return null;

            return node.RightChild;
        }

        protected virtual RedBlackTreeColors _safeGetColor(RedBlackTreeMapNode<TKey, TValue> node)
        {
            if (node == null)
                return RedBlackTreeColors.Black;

            return node.Color;
        }

        protected virtual void _safeUpdateColor(RedBlackTreeMapNode<TKey, TValue> node, RedBlackTreeColors color)
        {
            if (node == null)
                return;

            node.Color = color;
        }

        protected virtual bool _safeCheckIsBlack(RedBlackTreeMapNode<TKey, TValue> node)
        {
            return (node == null || (node != null && node.IsBlack));
        }

        protected virtual bool _safeCheckIsRed(RedBlackTreeMapNode<TKey, TValue> node)
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
            protected virtual void _rotateLeftAt(RedBlackTreeMapNode<TKey, TValue> currentNode)
        {
            // We check the right child because it's going to be a pivot node for the rotation
            if (currentNode == null || currentNode.HasRightChild == false)
                return;

            // Pivot on *right* child
            RedBlackTreeMapNode<TKey, TValue> pivotNode = currentNode.RightChild;

            // Parent of currentNode
            RedBlackTreeMapNode<TKey, TValue> parent = currentNode.Parent;

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
        protected virtual void _rotateRightAt(RedBlackTreeMapNode<TKey, TValue> currentNode)
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
        protected virtual void _adjustTreeAfterInsertion(RedBlackTreeMapNode<TKey, TValue> currentNode)
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
                    // If it has a sibling and it is red, then then it has a parent
                    currentNode.Parent.Color = RedBlackTreeColors.Red;

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
        protected virtual void _adjustTreeAfterRemoval(RedBlackTreeMapNode<TKey, TValue> currentNode)
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
        protected override bool _remove(BSTMapNode<TKey, TValue> nodeToDelete)
        {
            return this._remove((RedBlackTreeMapNode<TKey, TValue>)nodeToDelete);
        }

        /// <summary>
        /// The internal remove helper.
        /// Separated from the overriden version to avoid casting the objects from BSTMapNode to RedBlackTreeMapNode.
        /// This is called from the overriden _remove(BSTMapNode nodeToDelete) helper.
        /// </summary>
        protected bool _remove(RedBlackTreeMapNode<TKey, TValue> nodeToDelete)
        {
            if (nodeToDelete == null)
                return false;

            // Temporary nodes
            RedBlackTreeMapNode<TKey, TValue> node1, node2;

            // If nodeToDelete has either one child or no children at all
            if (!nodeToDelete.HasLeftChild || !nodeToDelete.HasRightChild)
            {
                node1 = nodeToDelete;
            }
            else
            {
                // nodeToDelete has two children
                node1 = (RedBlackTreeMapNode<TKey, TValue>)base._findNextLarger(nodeToDelete);
            }

            // Left child case
            if (node1.HasLeftChild)
            {
                node2 = node1.LeftChild;
            }
            else
            {
                node2 = node1.RightChild;
            }

            // If node2 is not null, copy parent references
            if (node2 != null)
                node2.Parent = node1.Parent;

            if (node1.Parent != null)
            {
                if (node1.IsLeftChild)
                {
                    node1.Parent.LeftChild = node2;
                }
                else
                {
                    node1.Parent.RightChild = node2;
                }
            }
            else
            {
                Root = node2;
                Root.Parent = null;
            }

            // Swap values
            if (!node1.IsEqualTo(nodeToDelete))
            {
                nodeToDelete.Key = node1.Key;
                nodeToDelete.Value = node1.Value;
            }

            // Adjust the Red-Black Tree rules
            if (node1.Color == RedBlackTreeColors.Black && node2 != null)
            {
                _adjustTreeAfterRemoval(node2);
                Root.Color = RedBlackTreeColors.Black;
            }

            // Decrement the count
            base._count--;

            return true;
        }


        /*************************************************************************************************/


        /// <summary>
        /// Insert data item to tree
        /// </summary>
        public override void Insert(TKey key, TValue value)
        {
            var newNode = new RedBlackTreeMapNode<TKey, TValue>(key, value);

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
                    this.Insert(collection[i], default(TValue));
        }

        /// <summary>
        /// Inserts an array of key-value pairs to the tree.
        /// </summary>
        public override void Insert(KeyValuePair<TKey, TValue>[] collection)
        {
            if (collection == null)
                throw new ArgumentNullException();

            if (collection.Length > 0)
                for (int i = 0; i < collection.Length; ++i)
                    this.Insert(collection[i].Key, collection[i].Value);
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
                    this.Insert(collection[i], default(TValue));
        }

        /// <summary>
        /// Inserts a list of key-value pairs to the tree.
        /// </summary>
        public override void Insert(List<KeyValuePair<TKey, TValue>> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();

            if (collection.Count > 0)
                for (int i = 0; i < collection.Count; ++i)
                    this.Insert(collection[i].Key, collection[i].Value);
        }

        /// <summary>
        /// Removes an item from the tree.
        /// </summary>
        public override void Remove(TKey key)
        {
            if (IsEmpty)
                throw new Exception("Tree is empty.");

            // Get the node from the tree
            var node = (RedBlackTreeMapNode<TKey, TValue>)base._findNode(Root, key);

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
            var node = (RedBlackTreeMapNode<TKey, TValue>)base._findMinNode(Root);

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
            var node = (RedBlackTreeMapNode<TKey, TValue>)base._findMaxNode(Root);

            // Invoke the internal remove node method.
            this._remove(node);
        }

    }

}
