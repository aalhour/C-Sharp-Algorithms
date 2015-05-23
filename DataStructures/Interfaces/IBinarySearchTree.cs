
namespace DataStructures
{
    public interface IBinarySearchTree<T> where T : System.IComparable<T>
    {
        /// Returns the number of elements in the Tree
        int Count();

        // Checks if the tree is empty.
        bool IsEmpty();

        // Inserts an element to the tree
        void Insert(T item);

        // Remove an element from tree
        void Remove(T item);

        // Removes the min value from tree
        void RemoveMin();

        // Removes the max value from tree
        void RemoveMax();

        // Finds the minimum element.
        T FindMin();

        // Finds the maximum element.
        T FindMax();

        // Find the element in the tree, returns null if not found.
        T Find(T item);

        // Finds all the elements in the tree that match the predicate.
        ArrayList<T> FindAll(System.Predicate<T> searchPredicate);

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
    /// The itemed version of the Binary Search Tree.
    /// </summary>
    /// <typeparam name="K">Type of items.</typeparam>
    /// <typeparam name="V">Type of records per node.</typeparam>
    public interface IBinarySearchTree<K, V> where K : System.IComparable<K>
    {
        int Count();
        bool IsEmpty();
        void Insert(K item, V value);
        void Remove(K item);
        void RemoveMin();
        void RemoveMax();
        K Find(K item);
        K FindMin();
        K FindMax();
        ArrayList<V> FindAll(System.Predicate<K> searchPredicate);
        void Traverse(System.Action<K> action);
        void BSTSort(out ArrayList<V> outputCollection);
        void BSTSort(out System.Collections.Generic.IList<V> outputCollection);
        void Clear();
    }
}
