using System.Collections.Generic;

namespace DataStructures.Trees
{
    public interface IBinarySearchTree<T> where T : System.IComparable<T>
    {
        /// Returns the number of elements in the Tree
        int Count();

        // Checks if the tree is empty.
        bool IsEmpty();

        // Returns the height of the tree.
        int Height();

        // Inserts an element to the tree
        void Insert(T item);

        // Removes the min value from tree
        void RemoveMin();

        // Removes the max value from tree
        void RemoveMax();

        // Remove an element from tree
        void Remove(T item);

        // Finds the minimum element.
        T FindMin();

        // Finds the maximum element.
        T FindMax();

        // Find the element in the tree, returns null if not found.
        T Find(T item);

        // Finds all the elements in the tree that match the predicate.
<<<<<<< HEAD:DataStructures/Interfaces/IBinarySearchTree.cs
        List<T> Select(System.Predicate<T> searchPredicate);
=======
        List<T> FindAll(System.Predicate<T> searchPredicate);
>>>>>>> cbdc49b56864f95ab1a525af249e169b23d70105:DataStructures/Trees/IBinarySearchTree.cs

        // Sort the elements in this tree, using in-order traversal, and returns them.
        List<T> Sort();

        T[] ToArray();

        List<T> ToList();

        // Returns an enumerator that visits node in the order: parent, left child, right child
        IEnumerator<T> GetPreOrderEnumerator();

        // Returns an enumerator that visits node in the order: left child, parent, right child
        IEnumerator<T> GetInOrderEnumerator();

        // Returns an enumerator that visits node in the order: left child, right child, parent
        IEnumerator<T> GetPostOrderEnumerator();

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
        void RemoveMin();
        void RemoveMax();
        void Remove(K item);
        K FindMin();
        K FindMax();
<<<<<<< HEAD:DataStructures/Interfaces/IBinarySearchTree.cs
        int Rank(K item);
        List<V> Select(System.Predicate<K> searchPredicate);
=======
        K Find(K item);
        List<V> FindAll(System.Predicate<K> searchPredicate);
>>>>>>> cbdc49b56864f95ab1a525af249e169b23d70105:DataStructures/Trees/IBinarySearchTree.cs
        List<V> Sort();
        void Clear();
    }
}
