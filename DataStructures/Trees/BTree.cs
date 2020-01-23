using System;
using System.Collections.Generic;

namespace DataStructures.Trees
{
    /// <summary>
    /// B Tree Data Structure. Heavily influenced by Geeks for Geeks article.
    /// https://www.geeksforgeeks.org/b-tree-set-1-introduction-2/
    /// </summary>
    public class BTree<T> where T : IComparable<T>
    {
        private int _count { get; set; }
        private BTreeNode<T> _root { get; set; }
        private int _minDegree;

        /// <summary>
        /// CONSTRUCTOR.
        /// </summary>
        public BTree(int minDegree) {
            this._minDegree = minDegree;
            this._count = 0;
        }

        public BTreeNode<T> Root
        {
            get { return this._root; }
        }

        /// <summary>
        /// Inserts an item to the tree.
        /// </summary>
        public void Insert(T value)
        {
            if (this._root == null)
            {
                this._root = new BTreeNode<T>(this._minDegree);
                this._root.Keys.Add(value);
            }
            else
            {
                if (this._root.Keys.Count >= 2*this._minDegree-1)
                {
                    var newRoot = new BTreeNode<T>(this._minDegree);
                    newRoot.Children.Add(this._root);
                    newRoot.SplitChild(0, this._root);

                    // Which of the 2 children should hold the new value?
                    var i = 0;
                    if (value.CompareTo(newRoot.Keys[0]) > 0)
                    {
                        i++;
                    }
                    newRoot.Children[i].InsertNonFull(value);

                    this._root = newRoot;
                }
                else
                {
                    this._root.InsertNonFull(value);
                }
            }
        }

        /// <summary>
        /// Finds the Node that holds the given value.
        /// </summary>
        public BTreeNode<T> Search(T value)
        {
            if (this._root == null)
                return null;

            return this._root.Search(value);
        }

        /// <summary>
        /// Removes an item from the tree
        /// </summary>
        public void Remove(T item)
        {
            if (this._root == null)
            {
                Console.WriteLine("The B Tree is empty.");
                return;
            }

            this._root.Remove(item);
            if (this._root.Keys.Count == 0)
            {
                if (this._root.IsLeaf)
                {
                    this._root = null;
                }
                else
                {
                  this._root = this.Root.Children[0];
                }
            }
        }
    }

    public class BTreeNode<T> : System.IComparable<BTreeNode<T>> where T : System.IComparable<T>
    {
        private BTreeNode<T> _parent;
        private List<BTreeNode<T>> _children;
        private int _degree;
        private List<T> _keys;

        public BTreeNode(int degree)
        {
            this._degree = degree;
            this._children = new List<BTreeNode<T>>(degree*2-1);
            this._keys = new List<T>(degree*2-1);
        }

        public BTreeNode<T> Parent
        {
            get { return this._parent; }
            set { this._parent = value; }
        }

        public List<T> Keys
        {
            set { this._keys = value; }
            get { return this._keys; }
        }

        public List<BTreeNode<T>> Children
        {
            set { this._children = value; }
            get { return this._children; }
        }

        public bool IsLeaf
        {
            get { return (this._children.Count == 0); }
        }

        /// <summary>
        /// A utility function that returns the index of the first key
        /// that is greater than or equal to k.
        /// </summary>
        public int FindKey(T value)
        {
            return this.Keys.FindLastIndex(searching => value.CompareTo(searching) > 0) + 1;
        }

        public void Remove(T value)
        {
            var index = this.FindKey(value);

            // We found the key!
            if (index < this.Keys.Count && value.CompareTo(this.Keys[index]) == 0)
            {
                if (this.IsLeaf)
                    this.RemoveFromLeaf(index);
                else
                    this.RemoveFromNonLeaf(index);
            }
            else
            {
                if (this.IsLeaf)
                    return;

                var didMergeLast = (index == this.Children.Count);

                if (this.Children[index].Keys.Count < this._degree)
                    this.Fill(index);

                // If the last child has been merged, it must have merged with the previous 
                // child and so we recurse on the (idx-1)th child. Else, we recurse on the 
                // (idx)th child which now has atleast t keys 
                if (didMergeLast && index > this.Children.Count) 
                    this.Children[index-1].Remove(value); 
                else
                    this.Children[index].Remove(value); 
            }
        }

        /// <summary>
        /// Removes the key at index from this leaf node.
        /// </summary>
        public void RemoveFromLeaf(int index)
        {
            this.Keys.RemoveAt(index);
        }

        /// <summary>
        /// Removes the key at index from this non-leaf node.
        /// </summary>
        public void RemoveFromNonLeaf(int index)
        {
            // If the child that precedes our key has atleast this._degree keys, 
            // find the predecessor of our key in the subtree rooted at the child 
            // at index. Replace our key by it's pred. Recursively delete pred in
            // the list of children.
            if (this.Children[index].Keys.Count >= this._degree)
            {
                var pred = this.GetPredecessor(index);
                this.Keys[index] = pred;
                this.Children[index].Remove(pred);
            }
            // If the child has less than this._degree keys, examine the child on 
            // the other side. If it has atleast this._degree keys, find the successor
            // of the key in the subtree rooted at our next child. Replace the key with
            // the successor. Recursively delete the successor in the next child.
            else if (this.Children[index+1].Keys.Count >= this._degree)
            {
                var succ = this.GetSuccessor(index);
                this.Keys[index] = succ;
                this.Children[index+1].Remove(succ);
            }
            // If both the previous child and the next child has less than this._degree 
            // keys, merge our key and all of the next child into the previous child.
            // Now the previous child contains 2 * this._degree - 1 keys. Recursively 
            // delete our key from the previous child. 
            else
            {
                this.Merge(index);
                this.Children[index].Remove(this.Keys[index]);
            }
        }

        /// <summary>
        /// Fills the Child at index, which has fewer than this._degree-1 keys.
        /// </summary>
        private void Fill(int index)
        {
            // If the previous child has keys to spare, take them.
            if (index != 0 && this.Children[index-1].Keys.Count >= this._degree)
                this.BorrowFromPrevious(index);
            // If the next child has keys to spare, take them.
            else if (index != this.Keys.Count && this.Children[index].Keys.Count >= this._degree)
                this.BorrowFromNext(index);
            // Merge this sibling with the next, or previous in the event that this
            // is the last child.
            else
            {
                if (index != this.Children.Count-1)
                    this.Merge(index);
                else
                    this.Merge(index-1);
            }
        }

        /// <summary>
        /// Gets the highest value in the tree rooted at the child at index.
        /// </summary>
        private T GetPredecessor(int index)
        {
            var node = this.Children[index];
            while (!node.IsLeaf)
                node = node.Children[node.Children.Count-1];
            return node.Keys[node.Keys.Count-1];
        }

        /// <summary>
        /// Gets the lowest value in the tree rooted at the child at index+1.
        /// </summary>
        private T GetSuccessor(int index)
        {
            var node = this.Children[index+1];
            while (!node.IsLeaf)
                node = node.Children[0];
            return node.Keys[0];
        }

        /// <summary>
        /// Merges the child at index with the child at index+1.
        /// </summary>
        private void Merge(int index)
        {
            var child = this.Children[index];
            var sibling = this.Children[index+1];

            // Add our key and the siblings keys to the child.
            child.Keys.Insert(this._degree-1, this.Keys[index]);
            this.Keys.RemoveAt(index);
            child.Keys.AddRange(sibling.Keys);

            // Now move the children.
            if (!child.IsLeaf)
                child.Children.AddRange(sibling.Children);

            this.Children.RemoveAt(index+1);
        }

        /// <summary>
        /// Pulls a key from the previous sibling and inserts it in the child
        /// at index.
        /// </summary>
        private void BorrowFromPrevious(int index)
        {
            var child = this.Children[index];
            var sibling = this.Children[index-1];

            // Rotate keys: sibling > parent > child
            child.Keys.Insert(0, this.Keys[index-1]);
            this.Keys[index-1] = sibling.Keys[sibling.Keys.Count-1];
            sibling.Keys.RemoveAt(sibling.Keys.Count-1);

            // Rotate children, if its not a leaf node
            if (!child.IsLeaf)
            {
                child.Children.Insert(0, sibling.Children[sibling.Keys.Count-1]);
                sibling.Children.RemoveAt(sibling.Keys.Count-1);
            }
        }

        /// <summary>
        /// Pulls a key from the next sibling and inserts it in the child
        /// at index.
        /// </summary>
        private void BorrowFromNext(int index)
        {
            var child = this.Children[index];
            var sibling = this.Children[index+1];
            
            // Rotate keys sibling > parent > child
            child.Keys.Add(this.Keys[index]);
            this.Keys[index] = sibling.Keys[0];
            sibling.Keys.RemoveAt(0);

            // Rotate children if it's not a leaf node
            if (!child.IsLeaf)
            {
                child.Children.Add(sibling.Children[0]);
                sibling.Children.RemoveAt(0);
            }
        }

        /// <summary>
        /// Finds the Node that holds the given value.
        /// </summary>
        public BTreeNode<T> Search(T value)
        {
            var found = this.Keys.FindIndex(searching => value.CompareTo(searching) == 0);
            if (found != -1)
                return this;
            if (found == -1 && this.IsLeaf)
                return null;
            
            // Find the child 
            found = this.Keys.FindLastIndex(searching => value.CompareTo(searching) > 0) + 1;
            return this.Children[found].Search(value);
        }

        /// <summary>
        /// Assumes value can be inserted. Callers should verify this.Keys has
        /// enough space.
        /// </summary>
        public void InsertNonFull(T value)
        {
            if (this.IsLeaf)
            {
                // Find where the new key should be inserted.
                var i = this.Keys.FindLastIndex(
                    delegate(T compare)
                    {
                        return value.CompareTo(compare) > 0;
                    }
                ) + 1;
                this.Keys.Insert(i, value);
            }
            else
            {
                // Find which child to explore
                var i = this.Keys.FindLastIndex(
                    delegate(T compare)
                    {
                        return value.CompareTo(compare) > 0;
                    }
                ) + 1;

                if (this.Children[i].Keys.Count >= 2*this._degree-1)
                {
                    this.SplitChild(i, this.Children[i]);

                    // One of the new nodes will need to have our value.
                    if (value.CompareTo(this.Keys[i]) > 0)
                        i++;
                }
                this.Children[i].InsertNonFull(value);
            }
        }

        /// <summary>
        /// Splits the given child in 2
        /// </summary>
        public void SplitChild(int i, BTreeNode<T> child)
        {
            // Create a new Node which will store this._degree - 1 keys.
            var node = new BTreeNode<T>(child._degree);

            // Copy the keys over, leaving out the middle key
            var mid = child.Keys[child._degree-1];
            node.Keys = child.Keys.GetRange(child._degree, child._degree-1);
            child.Keys = child.Keys.GetRange(0, child._degree-1);

            // Copy the children over
            if (!child.IsLeaf)
            {
                node.Children = child.Children.GetRange(child._degree, child._degree);
                child.Children = child.Children.GetRange(0, child._degree);
            }

            // Insert the new node in this list of children.
            // Keep in mind the index i is for the keys, not children.
            this.Children.Insert(i+1, node);

            // One of the keys of child will move up to this node.
            this.Keys.Insert(i, mid);
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        public virtual int CompareTo(BTreeNode<T> other)
        {
            if (other == null)
                return -1;

            if (other.Children.Count != this.Children.Count)
                return -1;

            return 0;
        }
    }

}
