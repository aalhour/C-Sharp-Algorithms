using System;
using System.Collections;
using System.Collections.Generic;

using DataStructures.Interfaces;
using DataStructures.Helpers;

namespace DataStructures
{
	/// <summary>
	/// The binary search tree node.
	/// </summary>
	public class BSTNode<T> : IComparable<BSTNode<T>> where T : IComparable<T>
	{
		private T _value;
		private int _subtreeSize = 0;
		private BSTNode<T> _parent;
		private BSTNode<T> _left;
		private BSTNode<T> _right;


		public BSTNode() : this(default(T), 0, null, null, null) { }
		public BSTNode(T value) : this(value, 0, null, null, null) { }
		public BSTNode(T value, int subTreeSize, BSTNode<T> parent, BSTNode<T> left, BSTNode<T> right)
		{
			Value = value;
			SubtreeSize = subTreeSize;
			Parent = parent;
			LeftChild = left;
			RightChild = right;
		}

		public virtual T Value
		{
			get { return this._value; }
			set { this._value = value; }
		}

		// Rank of node
		public virtual int SubtreeSize
		{
			get { return this._subtreeSize; }
			set { this._subtreeSize = value; }
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
		public virtual int CompareTo(BSTNode<T> other)
		{
			if (other == null)
				return -1;

			return this.Value.CompareTo(other.Value);
		}
	}//end-of-bstnode


	/******************************************************************************/


    /// <summary>
    /// Implements a generic Binary Search Tree data structure.
    /// </summary>
    /// <typeparam name="T">Type of elements.</typeparam>
    public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable<T>
    {
		/// <summary>
		/// Specifies the mode of travelling through the tree.
		/// </summary>
		public enum TraversalMode
		{
			InOrder = 0,
			PreOrder = 1,
			PostOrder = 2
		}


        /// <summary>
        /// TREE INSTANCE VARIABLES
        /// </summary>
        /// <returns></returns>
        private BSTNode<T> _root { get; set; }
        private int _count { get; set; }

		public virtual BSTNode<T> Root
		{
			get { return this._root; }
			set { this._root = value; }
		}

		public BinarySearchTree()
        {
            _root = null;
            _count = 0;
        }


		/// <summary>
		/// Replaces the node's value from it's parent node object with the newValue.
		/// Used in the recusive _remove function.
		/// </summary>
		/// <param name="node">BST node.</param>
		/// <param name="newNode">New value.</param>
		protected void _replaceNodeInParent(BSTNode<T> node, BSTNode<T> newNode = null)
		{
			if (node.Parent != null)
			{
				if (node.IsLeftChild)
					node.Parent.LeftChild = newNode;
				else
					node.Parent.RightChild = newNode;
			}

			if (newNode != null)
				newNode.Parent = node.Parent;
		}

		/// <summary>
		/// Remove the specified node.
		/// </summary>
		/// <param name="node">Node.</param>
		/// <returns>>True if removed successfully; false if node wasn't found.</returns>
		protected virtual bool _remove(BSTNode<T> node)
		{
			if (node == null)
				return false;
			
			var parent = node.Parent;

			if (node.ChildrenCount == 2) // if both children are present
			{
				var successor = node.RightChild;
				node.Value = successor.Value;
				return (true && _remove(successor));
			}
			else if (node.HasLeftChild) // if the node has only a LEFT child
			{
				_replaceNodeInParent(node, node.LeftChild);
				_updateSubtreeSize(parent);
				_count--;

			}
			else if (node.HasRightChild) // if the node has only a RIGHT child
			{
				_replaceNodeInParent(node, node.RightChild);
				_updateSubtreeSize(parent);
				_count--;
			}
			else //this node has no children
			{
				_replaceNodeInParent(node, null);
				_updateSubtreeSize(parent);
				_count--;
			}

			return true;
		}

		/// <summary>
		/// Inserts a node inside the subtree of another.
		/// </summary>
		/// <param name="currentNode">Current node to insert afters.</param>
		/// <param name="newNode">New node to be inserted.</param>
		protected virtual void _insertNode(BSTNode<T> currentNode, BSTNode<T> newNode)
		{
			if (currentNode == null)
				return;
			
			if (newNode.Value.IsLessThan(currentNode.Value))
			{
				if (currentNode.HasLeftChild == false)
				{
					newNode.Parent = currentNode;
					currentNode.LeftChild = newNode;
					_count++;
					return;
				}

				_insertNode(currentNode.LeftChild, newNode);
			}
			else
			{
				if (currentNode.HasRightChild == false)
				{
					newNode.Parent = currentNode;
					currentNode.RightChild = newNode;
					_count++;
					return;
				}

				_insertNode(currentNode.RightChild, newNode);
			}
		}

		/// <summary>
        /// Returns the Subtrees size for a tree node if node exists; otherwise 0 (left and right nodes of leafs).
        /// This is used in the recursive function UpdateSubtreeSize.
        /// </summary>
        /// <returns>The size.</returns>
        /// <param name="node">BST Node.</param>
		protected int _subtreeSize(BSTNode<T> node)
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
		protected void _updateSubtreeSize(BSTNode<T> node)
        {
            if (node == null)
                return;

			node.SubtreeSize = _subtreeSize(node.LeftChild) + _subtreeSize(node.RightChild) + 1;

			_updateSubtreeSize(node.Parent);
        }

		/// <summary>
		/// Calculates the tree height from a specific node, recursively.
		/// </summary>
		/// <param name="node">Node</param>
		/// <returns>Height of node's longest subtree</returns>
		protected int _getTreeHeight(BSTNode<T> node)
		{
			if (node == null || node.HasChildren == false)
				return 0;

			if (node.ChildrenCount == 2) // it has both a right child and a left child
			{
				if (node.LeftChild.SubtreeSize > node.RightChild.SubtreeSize)
					return (1 + _getTreeHeight(node.LeftChild));
				else
					return (1 + _getTreeHeight(node.RightChild));
			}
			else if (node.HasLeftChild)
			{
				return (1 + _getTreeHeight(node.LeftChild));
			}
			else if (node.HasRightChild)
			{
				return (1 + _getTreeHeight(node.RightChild));
			}

			// return-functions-fix
			return 0;
		}

		/// <summary>
        /// Finds a node inside another node's subtrees, given it's value.
        /// </summary>
        /// <param name="currentNode">Node to start search from.</param>
        /// <param name="item">Search value</param>
        /// <returns>Node if found; otherwise null</returns>
		protected BSTNode<T> _findNode(BSTNode<T> currentNode, T item)
        {
            if (currentNode == null)
                return null;

            if (item.IsEqualTo(currentNode.Value))
            {
                return currentNode;
            }
			else if (currentNode.HasLeftChild && item.IsLessThan(currentNode.Value))
            {
				return _findNode(currentNode.LeftChild, item);
            }
			else if (currentNode.HasRightChild && item.IsGreaterThan(currentNode.Value))
            {
				return _findNode(currentNode.RightChild, item);
            }

            // Return-functions-fix
            return null;
        }

		/// <summary>
        /// Returns the min-node in a subtree.
        /// Used in the recusive _remove function.
        /// </summary>
        /// <returns>The minimum-valued tree node.</returns>
        /// <param name="node">The tree node with subtree(s).</param>
		protected BSTNode<T> _findMinNode(BSTNode<T> node)
        {
            var currentNode = node;

			while (currentNode.HasLeftChild)
                currentNode = currentNode.LeftChild;

            return currentNode;
        }

		/// <summary>
        /// Returns the max-node in a subtree.
        /// Used in the recusive _remove function.
        /// </summary>
        /// <returns>The maximum-valued tree node.</returns>
        /// <param name="node">The tree node with subtree(s).</param>
		protected BSTNode<T> _findMaxNode(BSTNode<T> node)
        {
            var currentNode = node;

			while (currentNode.HasRightChild)
                currentNode = currentNode.RightChild;

            return currentNode;
        }

		/// <summary>
        /// A recursive private method. Used in the public FindAll(predicate) functions.
        /// Implements in-order traversal to find all the matching elements in a subtree.
        /// </summary>
		/// <param name="currentNode">Node to start searching from.</param>
        /// <param name="match"></param>
		protected void _findAll(BSTNode<T> currentNode, Predicate<T> match, ref List<T> list)
        {
            if (currentNode == null)
                return;

            // call the left child
            _findAll(currentNode.LeftChild, match, ref list);

            if (match(currentNode.Value)) // match
            {
                list.Add(currentNode.Value);
            }

            // call the right child
            _findAll(currentNode.RightChild, match, ref list);
        }

		/// <summary>
		/// In-order traversal of the subtrees of a node. Returns every node it vists.
		/// </summary>
		/// <param name="currentNode">Node to traverse the tree from.</param>
		/// <param name="list">List to add elements to.</param>
        private void _inOrderTraverse(BSTNode<T> currentNode, ref List<T> list)
        {
            if (currentNode == null)
                return;

            // call the left child
            _inOrderTraverse(currentNode.LeftChild, ref list);

            // visit node
            list.Add(currentNode.Value);

            // call the right child
            _inOrderTraverse(currentNode.RightChild, ref list);
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
            if (IsEmpty())
                return 0;

            var currentNode = _root;
            return _getTreeHeight(currentNode);
        }

		/// <summary>
        /// Inserts an element to the tree
        /// </summary>
        /// <param name="item">Item to insert</param>
        public virtual void Insert(T item)
        {
			if (IsEmpty())
            {
                _root = new BSTNode<T>() { Value = item };
				_updateSubtreeSize (_root);
                _count++;
            }
            else
            {
                var currentNode = _root;
                var newNode = new BSTNode<T>(item);

				// Insert node recursively starting from the root.
				_insertNode (currentNode, newNode);

                // Update the subtree-size for the newNode's parent.
				_updateSubtreeSize(newNode.Parent);

            }//end-else
        }

        /// <summary>
        /// Deletes an element from the tree
        /// </summary>
        /// <param name="item">item to remove.</param>
		public virtual void Remove(T item)
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");
			
			var node = _findNode (_root, item);
			bool status = _remove (node);

            // If the element was found, remove it.
			if (status == false)
				throw new Exception("Item was not found.");
        }

        /// <summary>
        /// Removes the min value from tree.
        /// </summary>
		public virtual void RemoveMin()
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");

            //BSTNode<T> parent = null;
			var node = _findMinNode(_root);
			var parent = node.Parent;
			_remove (node);

            // Update the subtrees-sizes
			_updateSubtreeSize(parent);
        }

        /// <summary>
        /// Removes the max value from tree.
        /// </summary>
		public virtual void RemoveMax()
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");

			var node = _findMaxNode(_root);
			var parent = node.Parent;
			_remove (node);

            // Update the subtrees-sizes
            _updateSubtreeSize(parent);
        }

		/// <summary>
		/// Clears all elements from tree.
		/// </summary>
		public virtual void Clear()
		{
			_root = null;
			_count = 0;
		}

        /// <summary>
        /// Finds the minimum in tree 
        /// </summary>
        /// <returns>Min</returns>
		public virtual T FindMin()
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");
			
			return _findMinNode(_root).Value;
        }

        /// <summary>
        /// Finds the maximum in tree 
        /// </summary>
        /// <returns>Max</returns>
		public virtual T FindMax()
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");
			
			return _findMaxNode(_root).Value;
        }

        /// <summary>
        /// Find the item in the tree. Throws an exception if not found.
        /// </summary>
        /// <param name="item">Item to find.</param>
        /// <returns>Item.</returns>
		public virtual T Find(T item)
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");
			
			var node = _findNode(_root, item);

            if (node != null)
                return node.Value;
            else
                throw new Exception("Item was not found.");
        }

        /// <summary>
        /// Returns the rank of the specified element
        /// </summary>
        /// <param name="item">Tree element</param>
        /// <returns>Rank(item) if found; otherwise throws an exception.</returns>
		public virtual int Rank(T item)
        {
			var node = _findNode(_root, item);

            if (node == null)
                throw new Exception("Item was not found.");
            else
				return _subtreeSize(node.LeftChild);
        }

        /// <summary>
        /// Given a predicate function, find all the elements that match it.
        /// </summary>
        /// <param name="searchPredicate">The search predicate</param>
        /// <returns>ArrayList<T> of elements.</returns>
		public virtual List<T> Select(Predicate<T> searchPredicate)
        {
            var list = new List<T>();
			_findAll(_root, searchPredicate, ref list);

            return list;
        }

        /// <summary>
        /// Sort the elements in this tree, using in-order traversal, and returns them.
        /// </summary>
		public virtual List<T> Sort()
        {
            var list = new List<T>();
			_inOrderTraverse(_root, ref list);

            return list;
        }

		/// <summary>
		/// Returns an array of nodes' values.
		/// </summary>
		/// <returns>The array.</returns>
		public virtual T[] ToArray()
		{
			// the array version of binary search tree is the sorted arrangement of nodes.
			return Sort ().ToArray ();
		}

		/// <summary>
		/// Returns a list of the nodes' value.
		/// </summary>
		public virtual List<T> ToList()
		{
			// the list version of binary search tree is the sorted arrangement of nodes.
			return Sort ();
		}


		/// <summary>
		/// Returns an enumerator that visits node in the order: parent, left child, right child
		/// </summary>
		public virtual IEnumerator<T> GetPreOrderEnumerator()
		{
			return new BinarySearchTreePreOrderEnumerator(this);
		}

		/// <summary>
		/// Returns an enumerator that visits node in the order: left child, parent, right child
		/// </summary>
		public virtual IEnumerator<T> GetInOrderEnumerator()
		{
			return new BinarySearchTreeInOrderEnumerator(this);
		}

		/// <summary>
		/// Returns an enumerator that visits node in the order: left child, right child, parent
		/// </summary>
		public virtual IEnumerator<T> GetPostOrderEnumerator()
		{
			return new BinarySearchTreePostOrderEnumerator(this);
		}


		/*********************************************************************/


		/// <summary>
		/// Returns an preorder-traversal enumerator for the tree values
		/// </summary>
		internal class BinarySearchTreePreOrderEnumerator : IEnumerator<T>
		{
			private BSTNode<T> current;
			private BinarySearchTree<T> tree;
			internal Queue<BSTNode<T>> traverseQueue;

			public BinarySearchTreePreOrderEnumerator(BinarySearchTree<T> tree)
			{
				this.tree = tree;

				//Build queue
				traverseQueue = new Queue<BSTNode<T>>();
				visitNode(this.tree.Root);
			}

			private void visitNode(BSTNode<T> node)
			{
				if (node == null)
					return;
				else
				{
					traverseQueue.Enqueue(node);
					visitNode(node.LeftChild);
					visitNode(node.RightChild);
				}
			}

			public T Current
			{
				get { return current.Value; }
			}

			object IEnumerator.Current
			{
				get { return Current; }
			}

			public void Dispose()
			{
				current = null;
				tree = null;
			}

			public void Reset()
			{
				current = null;
			}

			public bool MoveNext()
			{
				if (traverseQueue.Count > 0)
					current = traverseQueue.Dequeue();
				else
					current = null;

				return (current != null);
			}
		}


		/// <summary>
		/// Returns an inorder-traversal enumerator for the tree values
		/// </summary>
		internal class BinarySearchTreeInOrderEnumerator : IEnumerator<T>
		{
			private BSTNode<T> current;
			private BinarySearchTree<T> tree;
			internal Queue<BSTNode<T>> traverseQueue;

			public BinarySearchTreeInOrderEnumerator(BinarySearchTree<T> tree)
			{
				this.tree = tree;

				//Build queue
				traverseQueue = new Queue<BSTNode<T>>();
				visitNode(this.tree.Root);
			}

			private void visitNode(BSTNode<T> node)
			{
				if (node == null)
					return;
				else
				{
					visitNode(node.LeftChild);
					traverseQueue.Enqueue(node);
					visitNode(node.RightChild);
				}
			}

			public T Current
			{
				get { return current.Value; }
			}

			object IEnumerator.Current
			{
				get { return Current; }
			}

			public void Dispose()
			{
				current = null;
				tree = null;
			}

			public void Reset()
			{
				current = null;
			}

			public bool MoveNext()
			{
				if (traverseQueue.Count > 0)
					current = traverseQueue.Dequeue();
				else
					current = null;

				return (current != null);
			}
		}

		/// <summary>
		/// Returns a postorder-traversal enumerator for the tree values
		/// </summary>
		internal class BinarySearchTreePostOrderEnumerator : IEnumerator<T>
		{
			private BSTNode<T> current;
			private BinarySearchTree<T> tree;
			internal Queue<BSTNode<T>> traverseQueue;

			public BinarySearchTreePostOrderEnumerator(BinarySearchTree<T> tree)
			{
				this.tree = tree;

				//Build queue
				traverseQueue = new Queue<BSTNode<T>>();
				visitNode(this.tree.Root);
			}

			private void visitNode(BSTNode<T> node)
			{
				if (node == null)
					return;
				else
				{
					visitNode(node.LeftChild);
					visitNode(node.RightChild);
					traverseQueue.Enqueue(node);
				}
			}

			public T Current
			{
				get { return current.Value; }
			}

			object IEnumerator.Current
			{
				get { return Current; }
			}

			public void Dispose()
			{
				current = null;
				tree = null;
			}

			public void Reset()
			{
				current = null;
			}

			public bool MoveNext()
			{
				if (traverseQueue.Count > 0)
					current = traverseQueue.Dequeue();
				else
					current = null;

				return (current != null);
			}
		}

    }//end-of-binary-search-tree

}
