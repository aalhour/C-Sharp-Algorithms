
namespace DataStructures.Interfaces
{
    public interface ITree<T> where T : System.IComparable<T>
    {
        /// <summary>
        /// Returns the number of elements in the Tree
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Checks if the tree is empty.
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();

        /// <summary>
        /// Inserts an element to the tree
        /// </summary>
        /// <param name="key">Value.</param>
        void Insert(T key);

        /// <summary>
        /// Deletes an element from tree
        /// </summary>
        /// <param name="key">Value.</param>
        void Delete(T key);

        /// <summary>
        /// Find the element in the tree, returns null if not found.
        /// </summary>
        /// <param name="key">Value.</param>
        /// <returns></returns>
        T Find(T key);

        /// <summary>
        /// Finds all the elements in the tree that match the predicate.
        /// </summary>
        /// <param name="searchPredicate">Predicate</param>
        /// <returns>Array of elements</returns>
        T[] FindAll(System.Predicate<T> searchPredicate);

        /// <summary>
        /// Traverses the tree and applies the action to every node.
        /// </summary>
        /// <param name="action">Action to apply.</param>
        void Traverse(System.Action<T> action);

        /// <summary>
        /// Clear this tree.
        /// </summary>
        void Clear();
    }


    public interface ITree<K, V> where K : System.IComparable<K>
    {
        /// <summary>
        /// Returns the number of elements in the Tree
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Checks if the tree is empty.
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();

        /// <summary>
        /// Inserts a key and a value to the tree
        /// </summary>
        /// <param name="key">Value.</param>
        /// <param name="value"></param>
        void Insert(K key, V value);

        /// <summary>
        /// Deletes an element from tree
        /// </summary>
        /// <param name="key">Key of element.</param>
        void Delete(K key);

        /// <summary>
        /// Find the element in the tree, returns null if not found.
        /// </summary>
        /// <param name="key">Value.</param>
        /// <returns>The value associated with the key.</returns>
        V Find(K key);

        /// <summary>
        /// Finds all the elements in the tree that have a matching key to the predicate.
        /// </summary>
        /// <param name="searchPredicate">Predicate</param>
        /// <returns>Array of elements</returns>
        K[] FindAll(System.Predicate<K> searchPredicate);

        /// <summary>
        /// Traverses the tree and applies the action to every node.
        /// </summary>
        /// <param name="action">Action to apply.</param>
        void Traverse(System.Action<K> action);

        /// <summary>
        /// Clear this tree.
        /// </summary>
        void Clear();
    }
}
