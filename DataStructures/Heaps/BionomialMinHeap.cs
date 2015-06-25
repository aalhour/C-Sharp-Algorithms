using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Heaps
{
    public class BionomialMinHeap<T> : IMinHeap<T> where T : IComparable<T>
    {
        public BionomialMinHeap()
        {

        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty()
        {
            throw new NotImplementedException();
        }

        public void Heapify(IList<T> newCollection)
        {
            throw new NotImplementedException();
        }

        public void Insert(T heapKey)
        {
            throw new NotImplementedException();
        }

        public T Peek()
        {
            throw new NotImplementedException();
        }

        public void RemoveMin()
        {
            throw new NotImplementedException();
        }

        public T ExtractMin()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public T[] ToArray()
        {
            throw new NotImplementedException();
        }

        public List<T> ToList()
        {
            throw new NotImplementedException();
        }

        public IMaxHeap<T> ToBinaryMaxHeap()
        {
            throw new NotImplementedException();
        }
    }
}
