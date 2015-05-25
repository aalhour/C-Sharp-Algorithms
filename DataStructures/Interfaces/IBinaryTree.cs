
namespace DataStructures.Interfaces
{
    public interface IBinaryTree<T> where T : System.IComparable<T>
    {
        // Returns the number of elements in the Tree
        int Count();

        // Checks if the tree is empty.
        bool IsEmpty();

        // Inserts an element to the tree
        void Insert(T key);

        // Deletes an element from tree
        void Delete(T key);

        // Find the element in the tree, returns null if not found.
        T Find(T key);

        // Finds all the elements in the tree that match the predicate.
        T[] FindAll(System.Predicate<T> searchPredicate);

        // Traverses the tree and applies the action to every node.
        void Traverse(System.Action<T> action);

        // Clear this tree.
        void Clear();
    }


    /// <summary>
    /// The keyed version of the Tree.
    /// </summary>
    /// <typeparam name="K">Type of keys.</typeparam>
    /// <typeparam name="V">Type of records per node.</typeparam>
    public interface IBinaryTree<K, V> where K : System.IComparable<K>
    {
        int Count();
        bool IsEmpty();
        void Insert(K key, V value);
        void Delete(K key);
        V Find(K key);
        K[] FindAll(System.Predicate<K> searchPredicate);
        void Traverse(System.Action<K> action);
        void Clear();
    }
}
