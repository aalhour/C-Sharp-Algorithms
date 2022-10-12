using System;
using System.Collections.Generic;

namespace DataStructures.Trees
{
    public class TrieMapNode<TRecord> : IComparable<TrieMapNode<TRecord>>
    {
        // Node key
        public virtual char Key { get; set; }

        // Associated record with this node
        public virtual TRecord Record { get; set; }

        // Is Terminal node flag
        public virtual bool IsTerminal { get; set; }

        // Parent pointer
        public virtual TrieMapNode<TRecord> Parent { get; set; }

        // Dictionary of child-nodes
        public virtual Dictionary<char, TrieMapNode<TRecord>> Children { get; set; }


        /// <summary>
        /// CONSTRUCTORS
        /// </summary>
        public TrieMapNode(char key, TRecord record) : this(key, record, false) { }

        public TrieMapNode(char key, TRecord record, bool isTerminal)
        {
            Key = key;
            Record = record;
            IsTerminal = isTerminal;
            Children = new Dictionary<char, TrieMapNode<TRecord>>();
        }

        /// <summary>
        /// Return the word at this node if the node is terminal; otherwise, return null
        /// </summary>
        public virtual string Word
        {
            get
            {
                if (!IsTerminal)
                    return null;

                var curr = this;
                var stack = new Stack<char>();

                while(curr.Parent != null)
                {
                    stack.Push(curr.Key);
                    curr = curr.Parent;
                }

                return new String(stack.ToArray());
            }

        }

        /// <summary>
        /// Returns an enumerable list of key-value pairs of all the words that start 
        /// with the prefix that maps from the root node until this node.
        /// </summary>
        public virtual IEnumerable<KeyValuePair<String, TRecord>> GetByPrefix()
        {
            if (IsTerminal)
                yield return new KeyValuePair<String, TRecord>(Word, Record);

            foreach (var childKeyVal in Children)
                foreach(var terminalNode in childKeyVal.Value.GetByPrefix())
                    yield return terminalNode;
        }

        /// <summary>
        /// Returns an enumerable collection of terminal child nodes.
        /// </summary>
        public virtual IEnumerable<TrieMapNode<TRecord>> GetTerminalChildren()
        {
            foreach (var child in Children.Values) {
                if(child.IsTerminal)
                    yield return child;

                foreach (var grandChild in child.GetTerminalChildren())
                    if (grandChild.IsTerminal)
                        yield return grandChild;
            }
        }

        /// <summary>
        /// Remove this element upto its parent.
        /// </summary>
        public virtual void Remove()
        {
            IsTerminal = false;

            if(Children.Count == 0 && Parent != null)
            {
                Parent.Children.Remove(Key);

                if (!Parent.IsTerminal)
                    Parent.Remove();
            }
        }

        /// <summary>
        /// IComparer interface implementation
        /// </summary>
        public int CompareTo(TrieMapNode<TRecord> other)
        {
            if (other == null)
                return -1;

            return Key.CompareTo(other.Key);

        }

        /// <summary>
        /// Clears this node instance
        /// </summary>
        public void Clear()
        {
            Children.Clear();
            Children = null;
        }
    }

}
