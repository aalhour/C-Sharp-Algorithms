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


		private int SubtreeSize(BSTNode<T> node)
		{
			if (node == null)
				return 0;
			else
				return node.SubtreeSize;
		}


		private void UpdateSubtreeSize(BSTNode<T> node)
		{
			node.SubtreeSize = SubtreeSize (node.Left) + SubtreeSize (node.Right) + 1;
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
				while(true)
				{
					if(item.IsLessThanOrEqualTo(currentNode.Value))
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
				var node = newNode;

				while (node != null)
				{
					UpdateSubtreeSize (node);
					node = node.Parent;
				}//end-while

            }//end-else
        }


        /// <summary>
        /// Deletes an element from the tree
        /// </summary>
        /// <param name="item">item to remove.</param>
        public void Remove(T item)
        {
			if (IsEmpty ())
			{
				throw new Exception ("Tree is empty.");
			}

			var currentNode = _root;

			while (currentNode != null)
			{
				if(item.IsEqualTo(currentNode.Value))
				{
					break;
				}
				else if (item.IsLessThan(currentNode.Value))
				{
					currentNode = currentNode.Left;
				}
				else if(item.IsGreaterThan(currentNode.Value))
				{
					currentNode = currentNode.Right;
				}
			}

			//
			// If the element was found, remove it.
			if (currentNode != null)
			{
				
			}
			else
			{
				throw new Exception ("Item was not found.");
			}
        }


        /// <summary>
        /// Finds the minimum in tree 
        /// </summary>
        /// <returns>Min</returns>
        public T FindMin()
        {
			var currentNode = _root;

			while (currentNode.Left != null)
			{
				currentNode = currentNode.Left;
			}

			return currentNode.Value;
        }


        /// <summary>
        /// Finds the maximum in tree 
        /// </summary>
        /// <returns>Max</returns>
        public T FindMax()
        {
			var currentNode = _root;

			while (currentNode.Right != null)
			{
				currentNode = currentNode.Right;
			}

			return currentNode.Value;
        }


		public void RemoveMin()
		{
			BSTNode<T> parent = null;
			var currentNode = _root;

			//
			// Keep going left
			while (currentNode.Left != null)
			{
				currentNode = currentNode.Left;
			}

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
			while (parent != null)
			{
				UpdateSubtreeSize (parent);
				parent = parent.Parent;
			}
		}


		public void RemoveMax()
		{
			BSTNode<T> parent = null;
			var currentNode = _root;

			//
			// Keep going right
			while (currentNode.Right != null)
			{
				currentNode = currentNode.Right;
			}

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
			while (parent != null)
			{
				UpdateSubtreeSize (parent);
				parent = parent.Parent;
			}
		}


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
				else if(currentNode.Left != null && item.IsLessThan(currentNode.Value))
				{
					currentNode = currentNode.Left;
				}
				else if(currentNode.Right != null && item.IsGreaterThan(currentNode.Value))
				{
					currentNode = currentNode.Right;
				}
			}

			//
			// Return the item if found; otherwise, throw an exception.
			if (item.IsEqualTo (currentNode.Value))
			{
				return currentNode.Value;
			}
			else
			{
				throw new Exception ("Item was not found.");
			}
        }


        public ArrayList<T> FindAll(Predicate<T> searchPredicate)
        {
            throw new NotImplementedException();
        }


        public void Traverse(Action<T> action)
        {
            throw new NotImplementedException();
        }


        public void BSTSort(out ArrayList<T> outputCollection)
        {
            throw new NotImplementedException();
        }


        public void BSTSort(out IList<T> outputCollection)
        {
            throw new NotImplementedException();
        }


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
