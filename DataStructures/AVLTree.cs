using System;
using System.Collections.Generic;

using DataStructures.Interfaces;
using DataStructures.Helpers;

namespace DataStructures
{
    public class AVLTree<T> : IAVLTree<T> where T : IComparable<T>
    {
        private int _count { get; set; }
        private AVLTreeNode<T> _root { get; set; }


        public AVLTree()
        {
            _count = 0;
            _root = new AVLTreeNode<T>();
        }


        public int Count()
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty()
        {
            throw new NotImplementedException();
        }

        public void Insert(T value)
        {
            throw new NotImplementedException();
        }

        public void Delete(T value)
        {
            throw new NotImplementedException();
        }

        public T Find(T value)
        {
            throw new NotImplementedException();
        }

        public T FindMin()
        {
            throw new NotImplementedException();
        }

        public T FindMax()
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

        public T[] FindAll(Predicate<T> searchPredicate)
        {
            throw new NotImplementedException();
        }

        public void Traverse(Action<T> action)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }


    public class AVLTreeNode<T> : IComparable<AVLTreeNode<T>> where T : IComparable<T>
    {
        public T Value { get; set; }
        public int Height { get; set; }
        public AVLTreeNode<T> Parent { get; set; }
        public AVLTreeNode<T> Left { get; set; }
        public AVLTreeNode<T> Right { get; set; }

        /// <summary>
        /// CONSTRUCTORS
        /// </summary>
        public AVLTreeNode() : this(default(T), 0, null, null, null) { }
        public AVLTreeNode(T value) : this(value, 0, null, null, null) { }
        public AVLTreeNode(T value, int height, AVLTreeNode<T> parent, AVLTreeNode<T> left, AVLTreeNode<T> right)
        {
            this.Value = value;
            this.Height = height;
            this.Parent = parent;
            this.Left = left;
            this.Right = right;
        }

        public int CompareTo(AVLTreeNode<T> other)
        {
            if (other == null)
                return -1;

            return this.Value.CompareTo(other.Value);
        }
    }
}
