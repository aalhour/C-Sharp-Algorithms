/***
 * Trie Map.
 * 
 * This is an implementation of a Trie where complete words (words that end with terminal nodes) have associated records of any type.
 * This version of Trie uses the custom generic class TrieMapNode<TRecord> for its nodes.
 * 
 * This class implements the IDictionary and IEnumerable interfaces.
 */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using DataStructures.Common;

namespace DataStructures.Trees
{
    /// <summary>
    /// The Trie Map Data Structure (a.k.a Prefix Tree).
    /// </summary>
    /// <typeparam name="TRecord">The type of records attached to words</typeparam>
    
    public class TrieMap<TRecord> : IDictionary<String, TRecord>, IEnumerable<KeyValuePair<String, TRecord>>
    {
        private int _count { get; set; }
        private TrieMapNode<TRecord> _root { get; set; }
        private EqualityComparer<TRecord> _recordsComparer = EqualityComparer<TRecord>.Default;

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public TrieMap()
        {
            _count = 0;
            _root = new TrieMapNode<TRecord>(' ', default(TRecord), false);
        }

        /// <summary>
        /// Return count of words.
        /// </summary>
        public int Count
        {
            get { return _count; }
        }

        /// <summary>
        /// Checks if element is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return _count == 0; }
        }

        /// <summary>
        /// Add word to trie
        /// </summary>
        public void Add(string word, TRecord record)
        {
            if (string.IsNullOrEmpty(word))
                throw new ArgumentException("Word is empty or null.");

            var current = _root;

            for (int i = 0; i < word.Length; ++i)
            {
                if (!current.Children.ContainsKey(word[i]))
                {
                    var newTrieNode = new TrieMapNode<TRecord>(word[i], default(TRecord));
                    newTrieNode.Parent = current;
                    current.Children.Add(word[i], newTrieNode);
                }

                current = current.Children[word[i]];
            }

            if (current.IsTerminal)
                throw new InvalidOperationException("Word already exists in Trie.");

            ++_count;
            current.IsTerminal = true;
            current.Record = record;
        }

        /// <summary>
        /// Updates a terminal word with a new record. Throws an exception if word was not found or if it is not a terminal word.
        /// </summary>
        public void UpdateWord(string word, TRecord newRecord)
        {
            if (string.IsNullOrEmpty(word))
                throw new InvalidOperationException("Word is either null or empty.");

            var current = _root;

            for (int i = 0; i < word.Length; ++i)
            {
                if (!current.Children.ContainsKey(word[i]))
                    throw new KeyNotFoundException("Word doesn't belong to trie.");

                current = current.Children[word[i]];
            }

            if (!current.IsTerminal)
                throw new KeyNotFoundException("Word doesn't belong to trie.");

            current.Record = newRecord;
        }

        /// <summary>
        /// Removes a word from the trie.
        /// </summary>
        public void Remove(string word)
        {
            if (string.IsNullOrEmpty(word))
                throw new ArgumentException("Word is empty or null.");

            var current = _root;

            for (int i = 0; i < word.Length; ++i)
            {
                if (!current.Children.ContainsKey(word[i]))
                    throw new KeyNotFoundException("Word doesn't belong to trie.");

                current = current.Children[word[i]];
            }

            if (!current.IsTerminal)
                throw new KeyNotFoundException("Word doesn't belong to trie.");

            --_count;
            current.Remove();
        }

        /// <summary>
        /// Checks whether the trie has a specific word.
        /// </summary>
        public bool ContainsWord(string word)
        {
            TRecord record;
            return this.SearchByWord(word, out record);
        }

        /// <summary>
        /// Checks whether the trie has a specific prefix.
        /// </summary>
        public bool ContainsPrefix(string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
                throw new InvalidOperationException("Prefix is either null or empty.");

            var current = _root;

            for (int i = 0; i < prefix.Length; ++i)
            {
                if (!current.Children.ContainsKey(prefix[i]))
                    return false;

                current = current.Children[prefix[i]];
            }

            return true;
        }

        /// <summary>
        /// Searchs the trie for a word and returns the associated record, if found; otherwise returns false.
        /// </summary>
        public bool SearchByWord(string word, out TRecord record)
        {
            if (string.IsNullOrEmpty(word))
                throw new InvalidOperationException("Word is either null or empty.");

            record = default(TRecord);
            var current = _root;

            for (int i = 0; i < word.Length; ++i)
            {
                if (!current.Children.ContainsKey(word[i]))
                    return false;

                current = current.Children[word[i]];
            }

            if (!current.IsTerminal)
                return false;

            record = current.Record;
            return true;
        }

        /// <summary>
        /// Searches the entire trie for words that has a specific prefix.
        /// </summary>
        public IEnumerable<KeyValuePair<String, TRecord>> SearchByPrefix(string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
                throw new InvalidOperationException("Prefix is either null or empty.");

            var current = _root;

            for (int i = 0; i < prefix.Length; ++i)
            {
                if (!current.Children.ContainsKey(prefix[i]))
                    return null;

                current = current.Children[prefix[i]];
            }

            return current.GetByPrefix();
        }

        /// <summary>
        /// Clears this insance.
        /// </summary>
        public void Clear()
        {
            _count = 0;
            _root.Clear();
            _root = new TrieMapNode<TRecord>(' ', default(TRecord), false);
        }


        #region IDictionary implementation
        bool ICollection<KeyValuePair<string, TRecord>>.IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Checks whether a specific key exists in trie as a word (terminal word).
        /// </summary>
        bool IDictionary<string, TRecord>.ContainsKey(string key)
        {
            TRecord record;
            return SearchByWord(key, out record);
        }

        /// <summary>
        /// Return all terminal words in trie.
        /// </summary>
        ICollection<string> IDictionary<string, TRecord>.Keys
        {
            get
            {
                var collection = new List<string>(Count);

                var terminalNodes = _root.GetTerminalChildren();
                foreach (var node in terminalNodes)
                    collection.Add(node.Word);

                return collection;
            }
        }

        /// <summary>
        /// Return all the associated records to terminal words.
        /// </summary>
        ICollection<TRecord> IDictionary<string, TRecord>.Values
        {
            get
            {
                var collection = new List<TRecord>(Count);

                var terminalNodes = _root.GetTerminalChildren();
                foreach (var node in terminalNodes)
                    collection.Add(node.Record);

                return collection;
            }
        }

        /// <summary>
        /// Tries to get the associated record of a terminal word from trie. Returns false if key was not found.
        /// </summary>
        bool IDictionary<string, TRecord>.TryGetValue(string key, out TRecord value)
        {
            return SearchByWord(key, out value);
        }

        /// <summary>
        /// Checks whether a specific word-record pair exists in trie. The key of item must be a terminal word not a prefix.
        /// </summary>
        bool ICollection<KeyValuePair<string, TRecord>>.Contains(KeyValuePair<string, TRecord> item)
        {
            TRecord record;
            var status = SearchByWord(item.Key, out record);
            return (status == true && _recordsComparer.Equals(item.Value, record));
        }

        void ICollection<KeyValuePair<string, TRecord>>.CopyTo(KeyValuePair<string, TRecord>[] array, int arrayIndex)
        {
            var tempArray = _root.GetTerminalChildren()
                .Select<TrieMapNode<TRecord>, KeyValuePair<String, TRecord>>(item => new KeyValuePair<String, TRecord>(item.Word, item.Record))
                .ToArray();

            Array.Copy(tempArray, 0, array, arrayIndex, Count);
        }

        /// <summary>
        /// Get/Set the associated record of a terminal word in trie.
        /// </summary>
        TRecord IDictionary<string, TRecord>.this[string key]
        {
            get
            {
                TRecord record;
                if (SearchByWord(key, out record))
                    return record;
                else
                    throw new KeyNotFoundException();
            }
            set
            {
                UpdateWord(key, value);
            }
        }

        void ICollection<KeyValuePair<string, TRecord>>.Add(KeyValuePair<string, TRecord> item)
        {
            this.Add(item.Key, item.Value);
        }

        /// <summary>
        /// Remove a word from trie.
        /// </summary>
        bool IDictionary<string, TRecord>.Remove(string key)
        {
            try
            {
                this.Remove(word: key);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Removes a word from trie.
        /// </summary>
        bool ICollection<KeyValuePair<string, TRecord>>.Remove(KeyValuePair<string, TRecord> item)
        {
            try
            {
                this.Remove(word: item.Key);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion IDictionary implementation


        #region IEnumerable implementation
        public IEnumerator<KeyValuePair<string, TRecord>> GetEnumerator()
        {
            return _root.GetTerminalChildren()
                    .Select<TrieMapNode<TRecord>, KeyValuePair<String, TRecord>>(item => new KeyValuePair<String, TRecord>(item.Word, item.Record))
                    .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion IEnumerable implementation
    }

}
