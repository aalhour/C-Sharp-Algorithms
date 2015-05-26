
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
<<<<<<< HEAD:DataStructures/Interfaces/IAVLTree.cs
        void ForEach(System.Action<T> action);
=======
>>>>>>> cbdc49b56864f95ab1a525af249e169b23d70105:DataStructures/Trees/IAVLTree.cs
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
<<<<<<< HEAD:DataStructures/Interfaces/IAVLTree.cs
		void ForEach(System.Action<K> action);
=======
>>>>>>> cbdc49b56864f95ab1a525af249e169b23d70105:DataStructures/Trees/IAVLTree.cs
        void Clear();
    }
}
