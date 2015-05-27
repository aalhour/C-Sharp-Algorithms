using System;
using System.Collections.Generic;

using DataStructures.Interfaces;
using DataStructures.Helpers;

namespace DataStructures
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
        private int _count { get; set; }
        private AVLTreeNode<T> _root { get; set; }

		public new AVLTreeNode<T> Root
		{
			get { return this._root; }
			set { this._root = value; }
		}

        public AVLTree()
        {
            _count = 0;
            _root = new AVLTreeNode<T>();
        }


		protected override void _insertNode (BSTNode<T> currentNode, BSTNode<T> newNode)
		{
			base._insertNode (currentNode, newNode);
		}

		protected override bool _remove (BSTNode<T> node)
		{
			return base._remove (node);
		}

		private void RotateSingleLeft (AVLTree<T> node)
		{
		}

		private void RotateSingleRight (AVLTree<T> node)
		{
		}

		private void RotateDoubleLeft (AVLTree<T> node)
		{
		}

		private void RotateDoubleRight (AVLTree<T> node)
		{
		}

		public override void Insert(T value)
        {
            throw new NotImplementedException();
        }

		public override void Remove (T item)
		{
			throw new NotImplementedException();
		}

		public T FindSuccessor(T value)
		{
			if (IsEmpty ())
				throw new Exception ("Tree is empty.");
			
			var node = (AVLTreeNode<T>)_findNode (_root, value);

			if (node.HasRightChild)
				return ((AVLTreeNode<T>)_findMinNode (node.RightChild)).Value;

			var current = node;
			while(current.Parent != null && current.IsRightChild)
				current = current.Parent;
			
			return current.Value;

			throw new Exception ("No successor was found.");
		}

		public T FindPredecessor(T value)
		{
			if (IsEmpty ())
				throw new Exception ("Tree is empty.");

			return default(T);
		}

		public override List<T> Sort ()
		{
			return base.Sort ();
		}

    }

}
