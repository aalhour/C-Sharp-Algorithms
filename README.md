# C# ALGORITHMS

### Implementations of Data Structures and Algorithms in C#.

I started writing this organized collection of classes as part of my preparation for technical interviews. This is for educational purposes only. However, the source code is stable.

This is a .NET solution and it can be opened with both Xamarin Studio (MonoDevelop) and Visual Studio. It has two separate projects: 1) Algorithms, 2) DataStructures. Both of them are class-library projects.

The third project is called MainProgram and it has all the tests for all the implemented algorithms and data structures. It has two main directories:
 * [Algorithms Tests](MainProgram/AlgorithmsTests).
 * [Data Structures Tests](MainProgram/DataStructuresTests).


## Data Structures

#### Lists:

 * **[Single-Linked List](DataStructures/Lists/SLinkedList.cs).**
 * **[Double-Linked List](DataStructures/Lists/DLinkedList.cs).**
 * **[Array List](DataStructures/Lists/ArrayList.cs).** A generic arrays-based list. Implements auto-resizing and handles overflow.
 * **[Stack](DataStructures/Lists/Stack.cs).** Based on my *ArrayList\<T\>*.
 * **[Queue](DataStructures/Lists/Queue.cs).** Based on my *ArrayList\<T\>*.

#### Priority Queues:

 * **[Min-Priority Queue](DataStructures/Heaps/MinPriorityQueue.cs).** Based on my *MinHeap\<T\>*.
 * **[Keyed Priority Queue](DataStructures/Heaps/KeyedPriorityQueue.cs).** Based on my *MaxHeap\<T\>*.

#### Heaps:

 * **[Binary Min-Heap](DataStructures/Heaps/BinaryMinHeap.cs).** Uses the *ArrayList\<T\>* class.
 * **[Binary Max-Heap](DataStructures/Heaps/BinaryMaxHeap.cs).** Uses the *ArrayList\<T\>* class.
 * **[Binomial Min-Heap](DataStructures/Heaps/BinomialMinHeap.cs).** Uses the *ArrayList\<T\>* class as a collection of connected BinomialNode lists. The BinomialNode is a private class inside the Heap data structure class.
 
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

#### Graphs:
 * **Undirected Graphs:**
  * **[Undirected Sparse Graph](DataStructures/Graphs/UndirectedSparseGraph.cs).** An adjacency-list graph representation. Implemented using a Dictionary. The nodes are inserted as keys, and the neighbors of every node are implemented as a doubly-linked list of nodes. This class implements the [IGraph\<T\>](DataStructures/Graphs/IGraph.cs) interface.
  * **[Undirected Dense Graph](DataStructures/Graphs/UndirectedDenseGraph.cs).** An incidence-matrix graph representation. Implemented using a two dimensional boolean array. This class implements the [IGraph\<T\>](DataStructures/Graphs/IGraph.cs) interface.
 
 * **Directed Graphs / Digraphs:** 
  * **[Directed Sparse Graph](DataStructures/Graphs/DirectedSparseGraph.cs).** An adjacency-list digraph representation. Follows almost the same implementation details of the Undirected version, except for managing the directed edges. Implements the [IGraph\<T\>](DataStructures/Graphs/IGraph.cs) interface.
  * **[Directed Dense Graph](DataStructures/Graphs/DirectedDenseGraph.cs).** An incidence-matrix digraph representation. Follows almost the same implementation details of the Undirected version, except for managing the directed edges. Implements the [IGraph\<T\>](DataStructures/Graphs/IGraph.cs) interface.
 
 * **Directed Weighted Graphs / Weighted Digraphs:**
  * **[Directed Weighted Sparse Graph](DataStructures/Graphs/DirectedWeightedSparseGraph.cs).** An adjacency-list weighted digraph representation. Shares a good deal of implemention details with the Directed Sparse version (DirectedSparseGraph\<T\>). Edges are instances of [WeightedEdge\<T\>](DataStructures/Graphs/WeightedEdge.cs) class. Implements both interfaces: [IGraph\<T\>](DataStructures/Graphs/IGraph.cs) and [IWeightedGraph\<T\>](DataStructures/Graphs/IWeightedGraph.cs).
  * **[Directed Weighted Dense Graph](DataStructures/Graphs/DirectedWeightedDenseGraph.cs).** An adjacency-matrix weighted digraph representation. Inherits and extends Directed Dense verion (DirectedDenseGraph\<T\>). Implements the [IWeightedGraph\<T\>](DataStructures/Graphs/IWeightedGraph.cs) interface.


## Algorithms

#### Sorting:
 Sorting algorithms are implemented as an extension method. They support the native Array\<T\>, and List\<T\> classes. They can takes value comparers. Insertion Sort supports my ArrayList\<T\> class.

  * **[Insertion Sort](Algorithms/Sorting/InsertionSorter.cs).**
  * **[Quick Sort](Algorithms/Sorting/QuickSorter.cs).**
  * **[Merge Sort](Algorithms/Sorting/MergeSorter.cs).**
  * **[Heap Sort](Algorithms/Sorting/HeapSorter.cs).**
  * **[BST Sort](Algorithms/Sorting/BinarySearchTreeSorter.cs).** Implements an unbalanced binary search tree sort.
  * **[Counting Sort](Algorithms/Sorting/CountingSorter.cs).** Only sorts arrays of integers.

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

#### Graphs:
 * **[Depth-First Searcher](Algorithms/Graphs/DepthFirstSearcher.cs).** Implements the *Depth-First Search* algorithm in two ways: Iterative and Recursive. Provides multiple functions for traversing graphs: PrintAll(), VisitAll(Action\<T\> forEachFunc), FindFirstMatch(Predicate\<T\> match). The VisitAll() applies a function to every graph node. The FindFirstMatch() function searches the graph for a predicate match.
 * **[Breadth-First Searcher](Algorithms/Graphs/BreadthFirstSearcher.cs).** Implements the the *Breadth-First Search* algorithm. Provides multiple functions for traversing graphs: PrintAll(), VisitAll(Action\<T\> forEachFunc), FindFirstMatch(Predicate\<T\> match). The VisitAll() applies a function to every graph node. The FindFirstMatch() function searches the graph for a predicate match.
 * **[Breadth-First Shortest Paths](Algorithms/Graphs/BreadthFirstShortestPaths.cs).** Calculates Shortest-Paths for Unweighted Graphs using the *Breadth-First Search* algorithm. It provides the capability to find shortest-paths from a single-source and multiple-sources, in addition to looking up reachable and unreachable nodes.
 * **[Dijkstra's Shortest Paths](Algorithms/Graphs/DijkstraShortestPaths.cs).** Calculates Dijkstra's Shortest-Paths for Directed Weighted Graphs from a single-source to all destinations. This class provides the same API as the *BreadthFirstShortestPaths\<T\>*.
 * **[Dijksta's All-Pairs Shortest Paths](Algorithms/Graphs/DijkstraAllPairsShortestPaths.cs).** Calculates Dijktra's shortest paths for all pairs of vertices in a graph. This is a wrapper class that applies single-source dijkstra shortest paths (DijkstraShortestPaths\<TG, TV\>) to all vertices of a graph and saves the results in a dictionary index by the vertices.
 * **[Cycles Detector](Algorithms/Graphs/CyclesDetector.cs).** Detects if a given graph is cyclic. Supports directed and undirected graphs.
 * **[Topological Sorter](Algorithms/Graphs/TopologicalSorter.cs).** Calculates one topological sorting of a *DAG* (Directed Acyclic Graph). This class depends on the *CyclesDetector* static class.


#### Numeric:
 * **[Catalan Numbers](Algorithms/Numeric/CatalanNumbers.cs).** A class that calculates the catalan numbers. A dynamic-programming solution.

#### Visualization:
 * **[Tree Drawer](DataStructures/Trees/TreeDrawer.cs).** Draws any tree that extends my *BinarySearchTree\<T\>* class. It is defined as an extension method.
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


## License

This project is licensed under the [MIT License](LICENSE).
