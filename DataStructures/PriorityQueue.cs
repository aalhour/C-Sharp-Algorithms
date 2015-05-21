using System;
using System.Collections.Generic;

namespace DataStructures
{
    /// <summary>
    /// 
    /// WORK IN PROGRESS.
    /// 
    /// Implements the Priority Queue Data Structure.
    /// </summary>
    public class PriorityQueue<K, V, P>
    {
        /// <summary>
        /// The Keyed Heap Node object.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <typeparam name="P"></typeparam>
        public class PriorityQueueNode<K, V, P>
        {
            public K Key { get; set; }
            public V Value { get; set; }
            public P Priority { get; set; }

            public PriorityQueueNode() : this(default(K), default(V), default(P)) 
            {
                // Empty constructor.
            }

            public PriorityQueueNode(K key, V value, P priority)
            {
                this.Key = key;
                this.Value = value;
                this.Priority = priority;
            }
        }//end-of-node-class


        /// <summary>
        /// Instance variables
        /// </summary>
        private int _size { get; set; }
        private ArrayList<PriorityQueueNode<K, V, P>> _collection { get; set; }


        //
        // Inserts a new item with a priority
        public void InsertWithPriority(V dataItem, P priority)
        {
            throw new NotImplementedException();
        }


        //
        // Remove the highest priority element and return it
        public V PullHighestPriority()
        {
            throw new NotImplementedException();

            /*var elementWithHighestPriority = PeekAtHighestPriority();
            Remove(elementWithHighestPriority);

            return elementWithHighestPriority;*/
        }


        //
        // Returns the highest priority element
        public V PeekAtHighestPriority()
        {
            throw new NotImplementedException();
        }


        //
        // Removes the first-matched element that has the value = dataItem
        public void Remove(V dataItem)
        {
            throw new NotImplementedException();
        }

    }

}
