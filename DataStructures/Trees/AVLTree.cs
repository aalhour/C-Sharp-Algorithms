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


        private void _leftRotate(AVLTreeNode<T> currentNode)
        {

        }

        private void _rightRotate(AVLTreeNode<T> currentNode)
        {

        }

        private void _rebalance(AVLTreeNode<T> currentNode)
        {

        }

        public new int Count()
        {
            return _count;
        }

        public new bool IsEmpty()
        {
            return (_count == 0);
        }

        public override void Insert(T value)
        {
            var newNode = new AVLTreeNode<T>() { Value = value };
            base._insertNode(Root, newNode);
        }

        public override void Remove(T value)
        {
            var node = (AVLTreeNode<T>) base._findNode(Root, value);
            base._remove(node);
        }

        public override T Find(T value)
        {
            throw new NotImplementedException();
        }

        public override T FindMin()
        {
            throw new NotImplementedException();
        }

        public override T FindMax()
        {
            throw new NotImplementedException();
        }

        public T FindSuccessor(T value)
        {
            throw new NotImplementedException();
        }

        public T FindPredecessor(T value)
        {
            throw new NotImplementedException();
        }

        public override List<T> FindAll(Predicate<T> searchPredicate)
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            this._count = 0;
            this._root = null;
        }
    }

}
