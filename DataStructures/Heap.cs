using System;
using System.Collections.Generic;

namespace DataStructures
{
    /// <summary>
    /// Implements the Heap Data Structure.
    /// </summary>
    public class Heap<K, V, P>
    {
        /// <summary>
        /// The Keyed Heap Node object.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <typeparam name="P"></typeparam>
        public class HeapNode<K, V, P>
        {
            public K Key { get; set; }
            public V Value { get; set; }
            public P Priority { get; set; }

            public HeapNode()
            {

            }

            public HeapNode(K key, V value, P priority)
            {
                this.Key = key;
                this.Value = value;
                this.Priority = priority;
            }
        }
    }
}
