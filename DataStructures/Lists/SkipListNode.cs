using System;

namespace DataStructures.Lists
{
    public class SkipListNode<T> : IComparable<SkipListNode<T>> where T : IComparable<T>
    {
        /// <summary>
        /// CONSTRUCTORS
        /// </summary>
        public SkipListNode(T value, int level)
        {
            if (level < 0)
                throw new ArgumentOutOfRangeException("Invalid value for level.");

            Value = value;
            Forwards = new SkipListNode<T>[level];
        }

        /// <summary>
        /// Get and set node's value
        /// </summary>
        public virtual T Value { get; private set; }

        /// <summary>
        /// Get and set node's forwards links
        /// </summary>
        public virtual SkipListNode<T>[] Forwards { get; private set; }

        /// <summary>
        /// Return level of node.
        /// </summary>
        public virtual int Level
        {
            get { return Forwards.Length; }
        }

        /// <summary>
        /// IComparable method implementation
        /// </summary>
        public int CompareTo(SkipListNode<T> other)
        {
            if (other == null)
                return -1;
            
            return this.Value.CompareTo(other.Value);
        }
    }
}
