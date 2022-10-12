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
            _minDegree = minDegree;
            _count = 0;
        }

        public BTreeNode<T> Root => _root;

        /// <summary>
        /// Inserts an item to the tree.
        /// </summary>
        public void Insert(T value)
        {
            if (_root == null)
            {
                _root = new BTreeNode<T>(_minDegree);
                _root.Keys.Add(value);
            }
            else
            {
                if (_root.Keys.Count >= 2*_minDegree-1)
                {
                    var newRoot = new BTreeNode<T>(_minDegree);
                    newRoot.Children.Add(_root);
                    newRoot.SplitChild(0, _root);

                    // Which of the 2 children should hold the new value?
                    var i = 0;
                    if (value.CompareTo(newRoot.Keys[0]) > 0)
                    {
                        i++;
                    }
                    newRoot.Children[i].InsertNonFull(value);

                    _root = newRoot;
                }
                else
                {
                    _root.InsertNonFull(value);
                }
            }
        }

        /// <summary>
        /// Finds the Node that holds the given value.
        /// </summary>
        public BTreeNode<T> Search(T value)
        {
            if (_root == null)
                return null;

            return _root.Search(value);
        }

        /// <summary>
        /// Removes an item from the tree
        /// </summary>
        public void Remove(T item)
        {
            if (_root == null)
            {
                Console.WriteLine("The B Tree is empty.");
                return;
            }

            _root.Remove(item);
            if (_root.Keys.Count == 0)
            {
                if (_root.IsLeaf)
                {
                    _root = null;
                }
                else
                {
                  _root = Root.Children[0];
                }
            }
        }
    }

    public class BTreeNode<T> : IComparable<BTreeNode<T>> where T : IComparable<T>
    {
        private int _degree;

        public BTreeNode(int degree)
        {
            _degree = degree;
            Children = new List<BTreeNode<T>>(degree*2-1);
            Keys = new List<T>(degree*2-1);
        }

        public BTreeNode<T> Parent { get; set; }

        public List<T> Keys { set; get; }

        public List<BTreeNode<T>> Children { set; get; }

        public bool IsLeaf => (Children.Count == 0);

        /// <summary>
        /// A utility function that returns the index of the first key
        /// that is greater than or equal to k.
        /// </summary>
        public int FindKey(T value)
        {
            return Keys.FindLastIndex(searching => value.CompareTo(searching) > 0) + 1;
        }

        public void Remove(T value)
        {
            var index = FindKey(value);

            // We found the key!
            if (index < Keys.Count && value.CompareTo(Keys[index]) == 0)
            {
                if (IsLeaf)
                    RemoveFromLeaf(index);
                else
                    RemoveFromNonLeaf(index);
            }
            else
            {
                if (IsLeaf)
                    return;

                var didMergeLast = (index == Children.Count);

                if (Children[index].Keys.Count < _degree)
                    Fill(index);

                // If the last child has been merged, it must have merged with the previous 
                // child and so we recurse on the (idx-1)th child. Else, we recurse on the 
                // (idx)th child which now has atleast t keys 
                if (didMergeLast && index > Children.Count) 
                    Children[index-1].Remove(value); 
                else
                    Children[index].Remove(value); 
            }
        }

        /// <summary>
        /// Removes the key at index from this leaf node.
        /// </summary>
        public void RemoveFromLeaf(int index)
        {
            Keys.RemoveAt(index);
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
            if (Children[index].Keys.Count >= _degree)
            {
                var pred = GetPredecessor(index);
                Keys[index] = pred;
                Children[index].Remove(pred);
            }
            // If the child has less than this._degree keys, examine the child on 
            // the other side. If it has atleast this._degree keys, find the successor
            // of the key in the subtree rooted at our next child. Replace the key with
            // the successor. Recursively delete the successor in the next child.
            else if (Children[index+1].Keys.Count >= _degree)
            {
                var succ = GetSuccessor(index);
                Keys[index] = succ;
                Children[index+1].Remove(succ);
            }
            // If both the previous child and the next child has less than this._degree 
            // keys, merge our key and all of the next child into the previous child.
            // Now the previous child contains 2 * this._degree - 1 keys. Recursively 
            // delete our key from the previous child. 
            else
            {
                Merge(index);
                Children[index].Remove(Keys[index]);
            }
        }

        /// <summary>
        /// Fills the Child at index, which has fewer than this._degree-1 keys.
        /// </summary>
        private void Fill(int index)
        {
            // If the previous child has keys to spare, take them.
            if (index != 0 && Children[index-1].Keys.Count >= _degree)
                BorrowFromPrevious(index);
            // If the next child has keys to spare, take them.
            else if (index != Keys.Count && Children[index].Keys.Count >= _degree)
                BorrowFromNext(index);
            // Merge this sibling with the next, or previous in the event that this
            // is the last child.
            else
            {
                if (index != Children.Count-1)
                    Merge(index);
                else
                    Merge(index-1);
            }
        }

        /// <summary>
        /// Gets the highest value in the tree rooted at the child at index.
        /// </summary>
        private T GetPredecessor(int index)
        {
            var node = Children[index];
            while (!node.IsLeaf)
                node = node.Children[node.Children.Count-1];
            return node.Keys[node.Keys.Count-1];
        }

        /// <summary>
        /// Gets the lowest value in the tree rooted at the child at index+1.
        /// </summary>
        private T GetSuccessor(int index)
        {
            var node = Children[index+1];
            while (!node.IsLeaf)
                node = node.Children[0];
            return node.Keys[0];
        }

        /// <summary>
        /// Merges the child at index with the child at index+1.
        /// </summary>
        private void Merge(int index)
        {
            var child = Children[index];
            var sibling = Children[index+1];

            // Add our key and the siblings keys to the child.
            child.Keys.Insert(_degree-1, Keys[index]);
            Keys.RemoveAt(index);
            child.Keys.AddRange(sibling.Keys);

            // Now move the children.
            if (!child.IsLeaf)
                child.Children.AddRange(sibling.Children);

            Children.RemoveAt(index+1);
        }

        /// <summary>
        /// Pulls a key from the previous sibling and inserts it in the child
        /// at index.
        /// </summary>
        private void BorrowFromPrevious(int index)
        {
            var child = Children[index];
            var sibling = Children[index-1];

            // Rotate keys: sibling > parent > child
            child.Keys.Insert(0, Keys[index-1]);
            Keys[index-1] = sibling.Keys[sibling.Keys.Count-1];
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
            var child = Children[index];
            var sibling = Children[index+1];
            
            // Rotate keys sibling > parent > child
            child.Keys.Add(Keys[index]);
            Keys[index] = sibling.Keys[0];
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
            var found = Keys.FindIndex(searching => value.CompareTo(searching) == 0);
            if (found != -1)
                return this;
            if (found == -1 && IsLeaf)
                return null;
            
            // Find the child 
            found = Keys.FindLastIndex(searching => value.CompareTo(searching) > 0) + 1;
            return Children[found].Search(value);
        }

        /// <summary>
        /// Assumes value can be inserted. Callers should verify this.Keys has
        /// enough space.
        /// </summary>
        public void InsertNonFull(T value)
        {
            if (IsLeaf)
            {
                // Find where the new key should be inserted.
                var i = Keys.FindLastIndex(
                    delegate(T compare)
                    {
                        return value.CompareTo(compare) > 0;
                    }
                ) + 1;
                Keys.Insert(i, value);
            }
            else
            {
                // Find which child to explore
                var i = Keys.FindLastIndex(
                    delegate(T compare)
                    {
                        return value.CompareTo(compare) > 0;
                    }
                ) + 1;

                if (Children[i].Keys.Count >= 2*_degree-1)
                {
                    SplitChild(i, Children[i]);

                    // One of the new nodes will need to have our value.
                    if (value.CompareTo(Keys[i]) > 0)
                        i++;
                }
                Children[i].InsertNonFull(value);
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
            Children.Insert(i+1, node);

            // One of the keys of child will move up to this node.
            Keys.Insert(i, mid);
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        public virtual int CompareTo(BTreeNode<T> other)
        {
            if (other == null)
                return -1;

            if (other.Children.Count != Children.Count)
                return -1;

            return 0;
        }
    }

}
