using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    /// <summary>
    /// The Stack (LIFO) Data Structure.
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public class Stack<T>
    {
        /// <summary>
        /// Instance variables.
        /// </summary>
        private DLList<T> Collection { get; set; }
        public int Count { get { return Collection.Count; } }


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public Stack()
        {
            Collection = new DLList<T>();
        }


        /// <summary>
        /// Checks whether the stack is empty.
        /// </summary>
        /// <returns>True if stack is empty, false otherwise.</returns>
        public bool IsEmpty()
        {
            return Collection.IsEmpty();
        }


        /// <summary>
        /// Returns the top element in the stack.
        /// </summary>
        public T Top
        {
            get
            {
                try
                {
                    return Collection.First;
                }
                catch (Exception)
                {
                    throw new Exception("Stack is empty.");
                }
            }
        }


        /// <summary>
        /// Inserts an element at the top of the stack.
        /// </summary>
        /// <param name="dataItem">Element to be inserted.</param>
        public void Push(T dataItem)
        {
            Collection.Prepend(dataItem);
        }


        /// <summary>
        /// Removes the top element from stack.
        /// </summary>
        public void Pop()
        {
            if (Count > 0)
            {
                Collection.RemoveAt(0);
            }
            else
            {
                throw new Exception("Stack is empty.");
            }
        }


        /// <summary>
        /// Given a variable top, assign the Top Element's value to it and then remove it from stack.
        /// </summary>
        /// <param name="top">Varialbe to hold the Top Element value.</param>
        public void Pop(out T top)
        {
            if (Count > 0)
            {
                top = Top;
                Collection.RemoveAt(0);
            }
            else
            {
                throw new Exception("Stack is empty.");
            }
        }


        /// <summary>
        /// Returns an array version of this stack.
        /// </summary>
        /// <returns>System.Array of stack elements.</returns>
        public T[] ToArray()
        {
            return Collection.ToArray();
        }


        /// <summary>
        /// Returns a System.List version of this stack.
        /// </summary>
        /// <returns>System.List of elements.</returns>
        public List<T> ToList()
        {
            return Collection.ToList();
        }


        /// <summary>
        /// Returns a readable, multi-line print-out (string) of the stack.
        /// </summary>
        /// <returns>String</returns>
        public string ToReadable()
        {
            return Collection.ToReadable();
        }

    }

}
