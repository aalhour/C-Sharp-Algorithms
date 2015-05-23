using System;
using System.Collections.Generic;

using DataStructures.Interfaces;
using DataStructures.Helpers;

namespace DataStructures
{
    /// <summary>
    /// Implements a generic Binary Search Tree data structure.
    /// </summary>
    /// <typeparam name="T">Type of elements.</typeparam>
    public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable<T>
    {
        /// <summary>
        /// TREE INSTANCE VARIABLES
        /// </summary>
        /// <returns></returns>
        private BSTNode<T> _root { get; set; }
        private int _count { get; set; }


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public BinarySearchTree()
        {
            _root = null;
            _count = 0;
        }


        /// <summary>
        /// Calculates the tree height from a specific node, recursively.
        /// </summary>
        /// <param name="node">Node</param>
        /// <returns>Height of node's longest subtree</returns>
        private int _getTreeHeight(BSTNode<T> node)
        {
            if (node == null)
                return 0;
            else if (node.Left == null && node.Right == null)
                return 1;

            if (node.Left != null && node.Right != null)
            {
                if (node.Left.SubtreeSize > node.Right.SubtreeSize)
                    node = node.Left;
                else
                    node = node.Right;
            }
            else if (node.Left != null)
            {
                node = node.Left;
            }
            else if (node.Right != null)
            {
                node = node.Right;
            }

            return (1 + _getTreeHeight(node));
        }


        /// <summary>
        /// Returns the Subtrees size for a tree node if node exists; otherwise 0 (left and right nodes of leafs).
        /// This is used in the recursive function UpdateSubtreeSize.
        /// </summary>
        /// <returns>The size.</returns>
        /// <param name="node">BST Node.</param>
        private int _subtreeSize(BSTNode<T> node)
        {
            if (node == null)
                return 0;
            else
                return node.SubtreeSize;
        }


        /// <summary>
        /// Updates the Subtree Size of a tree node.
        /// Used in recusively calculating the Subtrees Sizes of nodes.
        /// </summary>
        /// <param name="node">BST Node.</param>
        private void _updateSubtreeSize(BSTNode<T> node)
        {
            if (node == null)
                return;

            node.SubtreeSize = _subtreeSize(node.Left) + _subtreeSize(node.Right) + 1;
            node = node.Parent;
            _updateSubtreeSize(node);
        }


        /// <summary>
        /// Returns the min-node in a subtree.
        /// Used in the recusive _remove function.
        /// </summary>
        /// <returns>The minimum-valued tree node.</returns>
        /// <param name="node">The tree node with subtree(s).</param>
        private BSTNode<T> _findMinNode(BSTNode<T> node)
        {
            var currentNode = node;

            while (currentNode.Left != null)
                currentNode = currentNode.Left;

            return currentNode;
        }


        /// <summary>
        /// Returns the max-node in a subtree.
        /// Used in the recusive _remove function.
        /// </summary>
        /// <returns>The maximum-valued tree node.</returns>
        /// <param name="node">The tree node with subtree(s).</param>
        private BSTNode<T> _findMaxNode(BSTNode<T> node)
        {
            var currentNode = node;

            while (currentNode.Right != null)
                currentNode = currentNode.Right;

            return currentNode;
        }


        /// <summary>
        /// Replaces the node's value from it's parent node object with the newValue.
        /// Used in the recusive _remove function.
        /// </summary>
        /// <param name="BSTNode">BST node.</param>
        /// <param name="newValue">New value.</param>
        private void _replaceNodeInParent(BSTNode<T> node, BSTNode<T> newNode = null)
        {
            if (node.Parent != null)
            {
                if (node == node.Parent.Left)
                {
                    node.Parent.Left = newNode;
                }
                else
                {
                    node.Parent.Right = newNode;
                }
            }

            if (newNode != null)
                newNode.Parent = node.Parent;
        }


        /// <summary>
        /// /// A private method used in the public Remove function.
        /// Removes a given tree node from the tree.
        /// Handles nodes with sub-trees.
        /// </summary>
        /// <param name="node">Tree node to delete.</param>
        /// <param name="node">Tree node to delete.</param>
        private void _remove(BSTNode<T> node, T item)
        {
            var parent = node.Parent;

            if (node.Left != null && node.Right != null) //if both children are present
            {
                var successor = node.Right;
                node.Value = successor.Value;
                _remove(successor, successor.Value);
            }
            else if (node.Left != null) //if the node has only a *left* child
            {
                _replaceNodeInParent(node, node.Left);
                _updateSubtreeSize(parent);
                _count--;

            }
            else if (node.Right != null) //if the node has only a *right* child
            {
                _replaceNodeInParent(node, node.Right);
                _updateSubtreeSize(parent);
                _count--;
            }
            else //this node has no children
            {
                _replaceNodeInParent(node, null);
                _updateSubtreeSize(parent);
                _count--;
            }
        }


        /// <summary>
        /// In-order traversal of the subtrees of a node. Returns every node it vists.
        /// </summary>
        /// <param name="currentNode">Node to traverse the tree from.</param>
        private void _inOrderTraverse(BSTNode<T> currentNode, ref List<T> list)
        {
            if (currentNode == null)
                return;

            // call the left child
            _inOrderTraverse(currentNode.Left, ref list);

            // visit node
            list.Add(currentNode.Value);

            // call the right child
            _inOrderTraverse(currentNode.Right, ref list);
        }


        /// <summary>
        /// In-order traversal of the subtrees of a node, and applies an action to the value of every visited node.
        /// </summary>
        /// <param name="currentNode">Node to traverse the tree from.</param>
        /// <param name="action">Action to apply to every node's value.</param>
        private void _inOrderTraverse(BSTNode<T> currentNode, Action<T> action)
        {
            if (currentNode == null)
                return;

            // call the left child
            _inOrderTraverse(currentNode.Left, action);

            // visit node
            action(currentNode.Value);

            // call the right child
            _inOrderTraverse(currentNode.Right, action);
        }


        /// <summary>
        /// Implements pre-order traversal of the subtrees of a given node. Applies an action to every visited node (value).
        /// </summary>
        /// <param name="currentNode">Node to traverse from.</param>
        /// <param name="action">Action.</param>
        private void _preOrderTraverse(BSTNode<T> currentNode, Action<T> action)
        {
            if (currentNode == null)
                return;

            // visit node
            action(currentNode.Value);

            // call the left child
            _preOrderTraverse(currentNode.Left, action);

            // call the right child
            _preOrderTraverse(currentNode.Right, action);
        }


        /// <summary>
        /// Implements post-order traversal of the subtrees of a given node. Applies an action to every visited node (value).
        /// </summary>
        /// <param name="currentNode">Node to traverse from.</param>
        /// <param name="action">Action.</param>
        private void _postOrderTraverse(BSTNode<T> currentNode, Action<T> action)
        {
            if (currentNode == null)
                return;

            // call the left child
            _postOrderTraverse(currentNode.Left, action);

            // call the right child
            _postOrderTraverse(currentNode.Right, action);

            // visit node
            action(currentNode.Value);
        }


        private string _getEdgesForNode(BSTNode<T> node)
        {
            return null;
        }


        /// <summary>
        /// Prints the subtrees from a given node.
        /// </summary>
        /// <param name="currentNode">Node</param>
        private void _printTree(BSTNode<T> currentNode, Queue<BSTNode<T>> queue = null)
        {
            if (currentNode == null)
                return;

            if (queue == null)
                queue = new Queue<BSTNode<T>>();

            int leftPad = _getTreeHeight(currentNode);

            // handle root
            if (currentNode.Parent == null)
            {
                Console.WriteLine(new String(' ', leftPad) + currentNode.Value);

                queue.Push(currentNode.Left);
                queue.Push(currentNode.Right);

                _printTree(currentNode.Left, queue);
            }
            else if (!queue.IsEmpty)
            {
                Queue<BSTNode<T>> subQueue = new Queue<BSTNode<T>>();

                string spaceBetweenNodes = new String(' ', 2);
                string treeLevel = String.Format("{0}", new String(' ', leftPad + 3));

                //
                // Print the tree edges
                for (int i = 1; i <= queue.Count; ++i)
                {
                    if (i % 2 == 0)
                        treeLevel = String.Format("{0}\\{1}", treeLevel, spaceBetweenNodes);
                    else
                        treeLevel = String.Format("{0}/{1}", treeLevel, spaceBetweenNodes);
                }

                Console.WriteLine(treeLevel);

                //
                // Print the tree nodes
                spaceBetweenNodes = new String(' ', 4);
                treeLevel = String.Format("{0}", new String(' ', leftPad - 1));

                while (!queue.IsEmpty)
                {
                    var top = queue.Top;
                    queue.Pop();

                    if (top == null)
                    {
                        treeLevel = String.Format("{0}{1}{2}", treeLevel, spaceBetweenNodes, "");
                        subQueue.Push(null);
                        subQueue.Push(null);
                    }
                    else
                    {
                        treeLevel = String.Format("{0}{1}{2}", treeLevel, spaceBetweenNodes, top.Value);
                        subQueue.Push(top.Left);
                        subQueue.Push(top.Right);
                    }
                }

                Console.WriteLine(treeLevel);

                _printTree(subQueue.Top, subQueue);
            }
        }


        /// <summary>
        /// A recursive private method. Used in the public FindAll(predicate) functions.
        /// Implements in-order traversal to find all the matching elements in a subtree.
        /// </summary>
        /// <param name="searchPredicate"></param>
        private void _findAll(BSTNode<T> currentNode, Predicate<T> match, ref List<T> list)
        {
            if (currentNode == null)
                return;

            // call the left child
            _findAll(currentNode.Left, match, ref list);

            if (match(currentNode.Value)) // match
            {
                list.Add(currentNode.Value);
            }

            // call the right child
            _findAll(currentNode.Right, match, ref list);
        }


        /// <summary>
        /// Return the number of elements in this tree
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _count;
        }


        /// <summary>
        /// Checks if tree is empty.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return (_count == 0);
        }


        /// <summary>
        /// Returns the height of the tree.
        /// </summary>
        /// <returns>Hight</returns>
        public int Height()
        {
            var currentNode = _root;
            return _getTreeHeight(currentNode);
        }


        /// <summary>
        /// Inserts an element to the tree
        /// </summary>
        /// <param name="item">Item to insert</param>
        public void Insert(T item)
        {

            if (IsEmpty())
            {
                _root = new BSTNode<T>()
                {
                    Value = item,
                    SubtreeSize = 1
                };

                _count++;
            }
            else
            {
                var currentNode = _root;
                var newNode = new BSTNode<T>(item);

                // 
                // Get the currentNode to refer to the appropriate node.
                while (true)
                {
                    if (item.IsLessThan(currentNode.Value))
                    {
                        if (currentNode.Left == null)
                        {
                            newNode.Parent = currentNode;
                            currentNode.Left = newNode;
                            _count++;
                            break;
                        }

                        currentNode = currentNode.Left;
                    }
                    else
                    {
                        if (currentNode.Right == null)
                        {
                            newNode.Parent = currentNode;
                            currentNode.Right = newNode;
                            _count++;
                            break;
                        }

                        currentNode = currentNode.Right;
                    }
                }//end-while


                //
                // Update the subtrees-sizes
                var node = newNode.Parent;
                _updateSubtreeSize(node);

            }//end-else
        }


        /// <summary>
        /// Deletes an element from the tree
        /// </summary>
        /// <param name="item">item to remove.</param>
        public void Remove(T item)
        {
            if (IsEmpty())
            {
                throw new Exception("Tree is empty.");
            }

            var currentNode = _root;

            while (currentNode != null)
            {
                if (item.IsEqualTo(currentNode.Value))
                {
                    break;
                }
                else if (item.IsLessThan(currentNode.Value))
                {
                    currentNode = currentNode.Left;
                }
                else if (item.IsGreaterThan(currentNode.Value))
                {
                    currentNode = currentNode.Right;
                }
            }

            //
            // If the element was found, remove it.
            if (currentNode != null)
            {
                _remove(currentNode, item);
            }
            else
            {
                throw new Exception("Item was not found.");
            }
        }


        /// <summary>
        // Removes the min value from tree.
        /// </summary>
        public void RemoveMin()
        {
            BSTNode<T> parent = null;
            var currentNode = _root;

            //
            // Keep going left
            currentNode = _findMinNode(currentNode);

            //
            // Remove the node
            if (currentNode.Right != null)
            {
                parent = currentNode.Parent;
                var right = currentNode.Right;

                right.Parent = parent;
                parent.Left = right;
                _count--;
            }
            else
            {
                parent = currentNode.Parent;
                parent.Left = null;
                _count--;
            }

            //
            // Update the subtrees-sizes
            _updateSubtreeSize(parent);
        }


        /// <summary>
        /// Removes the max value from tree.
        /// </summary>
        public void RemoveMax()
        {
            BSTNode<T> parent = null;
            var currentNode = _root;

            //
            // Keep going right
            currentNode = _findMaxNode(currentNode);

            //
            // Remove the node
            if (currentNode.Left != null)
            {
                parent = currentNode.Parent;
                var left = currentNode.Left;

                left.Parent = parent;
                parent.Right = left;
                _count--;
            }
            else
            {
                parent = currentNode.Parent;
                parent.Right = null;
                _count--;
            }

            //
            // Update the subtrees-sizes
            _updateSubtreeSize(parent);
        }


        /// <summary>
        /// Finds the minimum in tree 
        /// </summary>
        /// <returns>Min</returns>
        public T FindMin()
        {
            var currentNode = _root;
            return _findMinNode(currentNode).Value;
        }


        /// <summary>
        /// Finds the maximum in tree 
        /// </summary>
        /// <returns>Max</returns>
        public T FindMax()
        {
            var currentNode = _root;
            return _findMaxNode(currentNode).Value;
        }


        /// <summary>
        /// Find the item in the tree. Throws an exception if not found.
        /// </summary>
        /// <param name="item">Item to find.</param>
        /// <returns>Item.</returns>
        public T Find(T item)
        {
            var currentNode = _root;

            //
            // Attempt to find the item
            while (currentNode.Left != null || currentNode.Right != null)
            {
                if (item.IsEqualTo(currentNode.Value))
                {
                    break;
                }
                else if (currentNode.Left != null && item.IsLessThan(currentNode.Value))
                {
                    currentNode = currentNode.Left;
                }
                else if (currentNode.Right != null && item.IsGreaterThan(currentNode.Value))
                {
                    currentNode = currentNode.Right;
                }
            }

            //
            // Return the item if found; otherwise, throw an exception.
            if (item.IsEqualTo(currentNode.Value))
            {
                return currentNode.Value;
            }
            else
            {
                throw new Exception("Item was not found.");
            }
        }


        /// <summary>
        /// Given a predicate function, find all the elements that match it.
        /// </summary>
        /// <param name="searchPredicate">The search predicate</param>
        /// <returns>ArrayList<T> of elements.</returns>
        public List<T> FindAll(Predicate<T> searchPredicate)
        {
            var currentNode = _root;
            var list = new List<T>();
            _findAll(currentNode, searchPredicate, ref list);

            return list;
        }


        /// <summary>
        /// Traverses the tree and applies the action to every node.
        /// </summary>
        /// <param name="action">Action to apply to every node's value.</param>
        public void Traverse(Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException("Null actions are not allowed.");

            var currentNode = _root;
            _inOrderTraverse(currentNode, action);
        }


        /// <summary>
        /// Sort the elements in this tree, using in-order traversal, and returns them.
        /// </summary>
        public List<T> BSTSort()
        {
            var currentNode = _root;
            var list = new List<T>();

            _inOrderTraverse(currentNode, ref list);

            return list;
        }


        public void VisualizeTree()
        {
            //
            // NOT FULLY IMPLEMENTED YET!
            throw new NotImplementedException();

            if (IsEmpty())
            {
                Console.WriteLine("<Empty Tree>");
                return;
            }

            var currentNode = _root;
            _printTree(currentNode);
        }


        /// <summary>
        /// Clears all elements from tree.
        /// </summary>
        public void Clear()
        {
            _root = null;
            _count = 0;
        }

    }//end-of-binary-search-tree


    /// <summary>
    /// The binary search tree node.
    /// </summary>
    public class BSTNode<T> : IComparable<BSTNode<T>> where T : IComparable<T>
    {
        public T Value { get; set; }
        public int SubtreeSize { get; set; }
        public BSTNode<T> Parent { get; set; }
        public BSTNode<T> Left { get; set; }
        public BSTNode<T> Right { get; set; }

        /// <summary>
        /// CONSTRUCTORS
        /// </summary>
        public BSTNode() : this(default(T), 0, null, null, null) { }
        public BSTNode(T value) : this(value, 0, null, null, null) { }
        public BSTNode(T value, int subTreeSize, BSTNode<T> parent, BSTNode<T> left, BSTNode<T> right)
        {
            this.Value = value;
            this.SubtreeSize = 0;
            this.Parent = parent;
            this.Left = left;
            this.Right = right;
        }

        // 
        // IComparable CompareTo implementation
        public int CompareTo(BSTNode<T> other)
        {
            if (other == null) return -1;

            return this.Value.CompareTo(other.Value);
        }
    }//end-of-bstnode
}
