
namespace DataStructures
{
    public interface IBinarySearchTree<T> where T : System.IComparable<T>
    {
        /// Returns the number of elements in the Tree
        int Count();

        // Checks if the tree is empty.
        bool IsEmpty();

        // Inserts an element to the tree
        void Insert(T key);

        // Deletes an element from tree
        void Delete(T key);

        // Finds the minimum element.
        T FindMin();

        // Finds the maximum element.
        T FindMax();

        // Find the element in the tree, returns null if not found.
        T Find(T key);

        // Finds all the elements in the tree that match the predicate.
        ArrayList<T>[] FindAll(System.Predicate<T> searchPredicate);

        // Traverses the tree and applies the action to every node.
        void Traverse(System.Action<T> action);

        // Sort the elements in this tree, using BinarySearchTree Sorting
        // ... and add them to the passed outputCollection
        void BSTSort(out ArrayList<T> outputCollection);
        void BSTSort(out System.Collections.Generic.IList<T> outputCollection);

        // Clear this tree.
        void Clear();
    }

    /// <summary>
    /// The keyed version of the Binary Search Tree.
    /// </summary>
    /// <typeparam name="K">Type of keys.</typeparam>
    /// <typeparam name="V">Type of records per node.</typeparam>
    public interface IBinarySearchTree<K, V> where K : System.IComparable<K>
    {
        int Count();
        bool IsEmpty();
        void Insert(K key, V value);
        void Delete(K key);
        K Find(K key);
        K FindMin();
        K FindMax();
        ArrayList<V> FindAll(System.Predicate<K> searchPredicate);
        void Traverse(System.Action<K> action);
        void BSTSort(out ArrayList<V> outputCollection);
        void BSTSort(out System.Collections.Generic.IList<V> outputCollection);
        void Clear();
    }
}
