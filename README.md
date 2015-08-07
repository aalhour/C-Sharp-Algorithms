# C# ALGORITHMS

###Implementations of Data Structures and Algorithms in C#.

## Description:

Originally started out as an interview-preparation project, May 2015.

#### Project Hierarchy:

This is a C#.NET solution-project, and it contains three subprojects:

 1. [Algorithms](Algorithms): A class library project. Contains the Algorithms implementations.
 2. [Data Structures](DataStructures): A class library project. Contains the Data Structures implementations.
 3. [Main Program](MainProgram): Contains tests for the data structures and algorithms projects.


## Requirements:
 * C# 5.
 * .NET 4.5.


## Data Structures:

#### Lists:
 * [Single-Linked List](DataStructures/Lists/SLinkedList.cs).
 * [Double-Linked List](DataStructures/Lists/DLinkedList.cs).
 * [Array List](DataStructures/Lists/ArrayList.cs).
 * [Stack](DataStructures/Lists/Stack.cs).
 * [Queue](DataStructures/Lists/Queue.cs).

#### Heaps:
 * [Binary-Min Heap](DataStructures/Heaps/BinaryMinHeap.cs).
 * [Binary-Max Heap](DataStructures/Heaps/BinaryMaxHeap.cs).
 * [Binomial-Min Heap](DataStructures/Heaps/BinomialMinHeap.cs).
 
#### Priority Queues:
 * [Min-Priority Queue](DataStructures/Heaps/MinPriorityQueue.cs).
 * [Keyed Priority Queue](DataStructures/Heaps/KeyedPriorityQueue.cs).
 
#### Hashing Functions:
 * [Prime Hashing Family](DataStructures/Hashing/PrimeHashingFamily.cs).
 * [Universal Hashing Family](DataStructures/Hashing/UniversalHashingFamily.cs).

#### Hash Tables:
 * [Chained Hash Table](DataStructures/Dictionaries/ChainedHashTable.cs).
 * [Cuckoo Hash Table](DataStructures/Dictionaries/CuckooHashTable.cs).

#### Trees:
 * [AVL Tree](DataStructures/Trees/AVLTree.cs).
 * [Binary Search Tree](DataStructures/Trees/BinarySearchTree.cs).
 * [Augmented Binary Search Tree](DataStructures/Trees/AugmentedBinarySearchTree.cs).
 
#### Graphs:
 * **Undirected Graphs:**
   * [Clique Graphs](DataStructures/Graphs/CliqueGraph.cs).
   * [Undirected Sparse Graph](DataStructures/Graphs/UndirectedSparseGraph.cs).
   * [Undirected Dense Graph](DataStructures/Graphs/UndirectedDenseGraph.cs).
 
 * **Directed Graphs:** 
   * [Directed Sparse Graph](DataStructures/Graphs/DirectedSparseGraph.cs).
   * [Directed Dense Graph](DataStructures/Graphs/DirectedDenseGraph.cs).

 * **Directed Weighted Graphs:**
   * [Directed Weighted Sparse Graph](DataStructures/Graphs/DirectedWeightedSparseGraph.cs).
   * [Directed Weighted Dense Graph](DataStructures/Graphs/DirectedWeightedDenseGraph.cs).


## Algorithms

#### Sorting:
 * [Insertion Sort](Algorithms/Sorting/InsertionSorter.cs).
 * [Quick Sort](Algorithms/Sorting/QuickSorter.cs).
 * [Merge Sort](Algorithms/Sorting/MergeSorter.cs).
 * [Heap Sort](Algorithms/Sorting/HeapSorter.cs).
 * [BST Sort](Algorithms/Sorting/BinarySearchTreeSorter.cs).
 * [Counting Sort](Algorithms/Sorting/CountingSorter.cs).

#### Graphs:
 * [Depth-First Searcher](Algorithms/Graphs/DepthFirstSearcher.cs).
 * [Breadth-First Searcher](Algorithms/Graphs/BreadthFirstSearcher.cs).
 * [Breadth-First Shortest Paths](Algorithms/Graphs/BreadthFirstShortestPaths.cs).
 * [Dijkstra's Shortest Paths](Algorithms/Graphs/DijkstraShortestPaths.cs).
 * [Dijksta's All-Pairs Shortest Paths](Algorithms/Graphs/DijkstraAllPairsShortestPaths.cs).
 * [Cycles Detector](Algorithms/Graphs/CyclesDetector.cs).
 * [Topological Sorter](Algorithms/Graphs/TopologicalSorter.cs).

#### Strings:
 * [Permutations](Algorithms/Strings/Permutations.cs).

#### Numeric:
 * [Catalan Numbers](Algorithms/Numeric/CatalanNumbers.cs).

#### Visualization:
 * [Tree Drawer](DataStructures/Trees/TreeDrawer.cs).


## Contributors
 * [Edgar Carballo Domínguez](https://github.com/karv).


## License
This project is licensed under the [MIT License](LICENSE).
