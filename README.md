# C# ALGORITHMS

Implementations of Data Structures and Algorithms in C#.

I started writing this organized collection of classes as part of my preparation for technical interviews. This is for educational purposes only. However, the source code is stable.

This is a .NET solution and it can be opened with both Xmarian (MonoDevelop) and Visual Studio. It has two separate projects: 1) Algorithms, 2) DataStructures. Both of them are class-library projects.

## DATA STRUCTURES:

#### Lists:

 * **[Single-Linked List](DataStructures/Lists/SLinkedList.cs).**
 * **[Double-Linked List](DataStructures/Lists/DLinkedList.cs).**
 * **[Array List](DataStructures/Lists/ArrayList.cs).** A generic arrays-based list. Implements auto-resizing and handles overflow.
 * **[Stack](DataStructures/Lists/Stack.cs).** Based on my *ArrayList\<T\>*.
 * **[Queue](DataStructures/Lists/Queue.cs).** Based on my *ArrayList\<T\>*.

#### Priority Queues:

 * **[Priority Queue](DataStructures/Heaps/PriorityQueue.cs).** Based on my *MaxHeap\<T\>*.
 * **[Keyed Priority Queue](DataStructures/Heaps/KeyedPriorityQueue.cs).** Based on my *MaxHeap\<T\>*.

#### Heaps:

 * **[Min-Heap](DataStructures/Heaps/MinHeap.cs).** Based on my *ArrayList\<T\>*.
 * **[Max-Heap](DataStructures/Heaps/MaxHeap.cs).** Based on my *ArrayList\<T\>*.
 
#### Trees:

 * **[Binary Search Tree](DataStructures/Trees/BinarySearchTree.cs).** Standard BST.
 * **[Augmented Binary Search Tree](DataStructures/Trees/AugmentedBinarySearchTree.cs).** A BST that is augmented to keep track of the subtrees-size for each node. Extends the *BinarySearchTree\<T\>* class.
 * **[AVL Tree](DataStructures/Trees/AVLTree.cs).** The self-balancing AVL binary-search tree. Extends the *BinarySearchTree\<T\>* class.

#### Hashing Functions:
 * **[Prime Hashing Family](DataStructures/Hashing/PrimeHashingFamily.cs).** Implements a simple family of hash functions using primes. The functions are initialized by randomly selecting primes. Supports re-generation of functions.
 * **[Universal Hashing Family](DataStructures/Hashing/UniversalHashingFamily.cs).** Implements a family class of simple universal-hashing functions. Supports re-generation of functions. It uses the [Common/PrimesList](DataStructures/Common/PrimesList.cs) helper class.

#### Hash Tables / Dictionaries:

 * **[Chained Hash Table](DataStructures/Dictionaries/ChainedHashTable.cs).** A hash table that implements the **Separate-Chaining** scheme for resolving keys-collisions. It also implements auto-resizing (expansion and contraction).
 * **[Cuckoo Hash Table](DataStructures/Dictionaries/CuckooHashTable.cs).** A hash table that implements the **Cuckoo Hashing** algorithm for resolving keys-collisions. This is a single-table implementation, the source behind this is the work of Mark Allen Weiss, 2014.


## ALGORITHMS:

#### Sorting:
 Sorting algorithms are implemented as an extension method. They support the native Array\<T\>, and List\<T\> classes. They can takes value comparers. Insertion Sort supports my ArrayList\<T\> class.

  * [Insertion Sort](Algorithms/Sorting/InsertionSorter.cs)
  * [Quick Sort](Algorithms/Sorting/QuickSorter.cs)
  * [Merge Sort](Algorithms/Sorting/MergeSorter.cs)
  * [Heap Sort](Algorithms/Sorting/HeapSorter.cs)
  * [BST Sort](Algorithms/Sorting/BinarySearchTreeSorter.cs). Implements an unbalanced binary search tree sort.
  * [Counting Sort](Algorithms/Sorting/CountingSorter.cs). Only sorts arrays of integers.

    ```
    int[] array = new int[] { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0 };
    List<long> list = new List<long> () { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0 };
    
    // The value comparer object. Can be any value comparer that implmenets IComparer.
    var valueComparer = Comparer<long>.Default;
    
    list.InsertionSort (valueComparer);
    list.QuickSort (valueComparer);
    list.MergeSort (valueComparer);
    list.HeapSort (valueComparer);
    list.UnbalancedBSTSort();
    array.CountingSort();
    ```

#### Visualization:
 * [Tree Drawer](DataStructures/Trees/TreeDrawer.cs). Draws any tree that extends my *BinarySearchTree\<T\>* class. It is defined as an extension function.
    ```
    var avlTree = new AVLTree<int>();
    var treeDataList = new List<int>() { 15, 25, 5, 12, 1, 9, 7, -1, 11, 30, 8, 10, 13, 28, 39 };
    avlTree.Insert(treeDataList);
    
    Console.WriteLine( avlTree.DrawTree() );
    
    /***
     ** Drawer output:
     **           .......9......
     **          /              \
     **       .5       ....12...
     **      /  \     /         \
     **    1   7    11      .25.
     **    /\ / \   /\     /    \
     **  -1     8  10    15    30
     **  /\    /\  /\    /\   / \
     **                 13   28 39
     **                 /\   /\ /\
     */
    ```


#### LICENSE

This project is licensed under the [MIT License](LICENSE).
