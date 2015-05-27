
namespace DataStructures.Trees
{
    public interface IAVLTree<T> where T : System.IComparable<T>
    {
        int Count();
        bool IsEmpty();
        void Insert(T value);
        void RemoveMin();
        void RemoveMax();
        void Remove(T value);
        T FindMin();
        T FindMax();
        T FindSuccessor(T value);
        T FindPredecessor(T value);
        T Find(T value);
        T[] FindAll(System.Predicate<T> searchPredicate);
        void Clear();
    }


    public interface IAVLTree<K, V> where K : System.IComparable<K>
    {
        int Count();
        bool IsEmpty();
        void Insert(K key, V value);
        void RemoveMin();
        void RemoveMax();
        void Remove(K key);
        K Find(K key);
        K FindMin();
        K FindMax();
        K FindSuccessor(K key);
        K FindPredecessor(K key);
        K[] FindAll(System.Predicate<K> searchPredicate);
        void Clear();
    }
}
