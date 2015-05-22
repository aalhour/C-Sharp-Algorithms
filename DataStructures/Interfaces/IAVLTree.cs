
namespace DataStructures.Interfaces
{
    public interface IAVLTree<T> where T : System.IComparable<T>
    {
        int Count();
        bool IsEmpty();
        void Insert(T value);
        void Delete(T value);
        T Find(T value);
        T FindMin();
        T FindMax();
        T FindSuccessor(T value);
        T FindPredecessor(T value);
        T[] FindAll(System.Predicate<T> searchPredicate);
        void Traverse(System.Action<T> action);

        /// <summary>
        /// Clear this tree.
        /// </summary>
        void Clear();
    }


    public interface IAVLTree<K, V> where K : System.IComparable<K>
    {
        int Count();
        bool IsEmpty();
        void Insert(K key, V value);
        void Delete(K key);
        K Find(K key);
        K FindMin();
        K FindMax();
        K FindSuccessor(K key);
        K FindPredecessor(K key);
        K[] FindAll(System.Predicate<K> searchPredicate);
        void Traverse(System.Action<K> action);

        /// <summary>
        /// Clear this tree.
        /// </summary>
        void Clear();
    }
}
