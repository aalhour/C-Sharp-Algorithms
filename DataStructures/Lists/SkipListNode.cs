using System;

namespace DataStructures.Lists
{
    public class SkipListNode<T> : IComparable<SkipListNode<T>> where T : IComparable<T>
    {
        /// <summary>
        /// Instance variables
        /// </summary>
        private T _value;
        private SkipListNode<T>[] _forwards;

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
        public virtual T Value
        {
            get { return this._value; }
            private set { this._value = value; }
        }

        /// <summary>
        /// Get and set node's forwards links
        /// </summary>
        public virtual SkipListNode<T>[] Forwards
        {
            get { return this._forwards; }
            private set { this._forwards = value; }
        }

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
