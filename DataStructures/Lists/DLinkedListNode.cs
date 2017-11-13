using System;

namespace DataStructures.Lists
{
    /// <inheritdoc />
    /// <summary>
    /// The Doubly-Linked List Node class.
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public class DLinkedListNode<T> : IComparable<DLinkedListNode<T>> where T : IComparable<T>
    {
        public DLinkedListNode() : this(default(T)) { }
        public DLinkedListNode(T dataItem) : this(dataItem, null, null) { }
        public DLinkedListNode(T dataItem, DLinkedListNode<T> next, DLinkedListNode<T> previous)
        {
            Data = dataItem;
            Next = next;
            Previous = previous;
        }

        public virtual T Data { get; set; }

        public virtual DLinkedListNode<T> Next { get; set; }

        public virtual DLinkedListNode<T> Previous { get; set; }

        public int CompareTo(DLinkedListNode<T> other)
        {
            if (other == null) return -1;

            return this.Data.CompareTo(other.Data);
        }
    }
}