# C# ALGORITHMS [![](https://travis-ci.org/aalhour/C-Sharp-Algorithms.svg?branch=master)](https://travis-ci.org/aalhour/C-Sharp-Algorithms)

#### Implementations of Data Structures and Algorithms in C#.

## Description

Originally started out as an interview-preparation project, May 2015.

**Project Hierarchy:**

This is a C#.NET solution-project, and it contains three subprojects:

 1. [Algorithms](Algorithms): A class library project. Contains the Algorithms implementations.
 2. [Data Structures](DataStructures): A class library project. Contains the Data Structures implementations.
 3. [Main Program](MainProgram): Contains tests for the data structures and algorithms projects.


## Requirements
 * C# 5.
 * .NET 4.5.

Note: The projects where tested on Xamarin Studio (MonoDevelop) on OSX 10, and Visual Studio on Windows 7. All of the project compile successfully. All tests in the MainProgram project pass.


## Data Structures

### Lists:
 * [Single-Linked List](DataStructures/Lists/SLinkedList.cs).
 * [Double-Linked List](DataStructures/Lists/DLinkedList.cs).
 * [Array List](DataStructures/Lists/ArrayList.cs).
 * [Stack](DataStructures/Lists/Stack.cs).
 * [Queue](DataStructures/Lists/Queue.cs).
 * [Skip List](DataStructures/Lists/SkipList.cs).

### Heaps:
 * [Binary-Min Heap](DataStructures/Heaps/BinaryMinHeap.cs).
 * [Binary-Max Heap](DataStructures/Heaps/BinaryMaxHeap.cs).
 * [Binomial-Min Heap](DataStructures/Heaps/BinomialMinHeap.cs).
 
### Priority Queues:
 * [Min-Priority Queue](DataStructures/Heaps/MinPriorityQueue.cs).
 * [Keyed Priority Queue](DataStructures/Heaps/KeyedPriorityQueue.cs).
 
### Hashing Functions:
 * [Prime Hashing Family](DataStructures/Hashing/PrimeHashingFamily.cs).
 * [Universal Hashing Family](DataStructures/Hashing/UniversalHashingFamily.cs).

### Hash Tables:
 * [Chained Hash Table](DataStructures/Dictionaries/ChainedHashTable.cs).
 * [Cuckoo Hash Table](DataStructures/Dictionaries/CuckooHashTable.cs).

### Trees:
 * [Trie](DataStructures/Trees/Trie.cs), and [Trie Map](DataStructures/Trees/TrieMap.cs).
 * [AVL Tree](DataStructures/Trees/AVLTree.cs).
 * [Binary Search Tree](DataStructures/Trees/BinarySearchTree.cs).
 * [Augmented Binary Search Tree](DataStructures/Trees/AugmentedBinarySearchTree.cs).
 
### Graphs:
**Undirected Graphs:**
 * [Clique Graphs](DataStructures/Graphs/CliqueGraph.cs).
 * [Undirected Sparse Graph](DataStructures/Graphs/UndirectedSparseGraph.cs).
 * [Undirected Dense Graph](DataStructures/Graphs/UndirectedDenseGraph.cs).
 
**Directed Graphs:** 
 * [Directed Sparse Graph](DataStructures/Graphs/DirectedSparseGraph.cs).
 * [Directed Dense Graph](DataStructures/Graphs/DirectedDenseGraph.cs).

**Directed Weighted Graphs:**
 * [Directed Weighted Sparse Graph](DataStructures/Graphs/DirectedWeightedSparseGraph.cs).
 * [Directed Weighted Dense Graph](DataStructures/Graphs/DirectedWeightedDenseGraph.cs).

=================================================================

## Algorithms

### Sorting:
 * [Insertion Sort](Algorithms/Sorting/InsertionSorter.cs).
 * [Quick Sort](Algorithms/Sorting/QuickSorter.cs).
 * [Merge Sort](Algorithms/Sorting/MergeSorter.cs).
 * [Heap Sort](Algorithms/Sorting/HeapSorter.cs).
 * [BST Sort](Algorithms/Sorting/BinarySearchTreeSorter.cs).
 * [Counting Sort](Algorithms/Sorting/CountingSorter.cs).
 * [LSD Radix Sort](Algorithms/Sorting/LSDRadixSorter.cs).

### Graphs:
**Graph Search:**
 * [Depth-First Searcher](Algorithms/Graphs/DepthFirstSearcher.cs).
 * [Breadth-First Searcher](Algorithms/Graphs/BreadthFirstSearcher.cs).

**Shortest Paths:**
 * [Breadth-First SPs](Algorithms/Graphs/BreadthFirstShortestPaths.cs).
 * [Bellman-Ford SPs](Algorithms/Graphs/BellmanFordShortestPaths.cs).
 * [Dijkstra SPs](Algorithms/Graphs/DijkstraShortestPaths.cs).
 * [Dijkstra All-Pairs SPs](Algorithms/Graphs/DijkstraAllPairsShortestPaths.cs).

**DFS Applications:**
 * [Cycles Detector](Algorithms/Graphs/CyclesDetector.cs).
 * [Topological Sorter](Algorithms/Graphs/TopologicalSorter.cs).

**BFS Applications:**
 * [Connected Components](Algorithms/Graphs/ConnectedComponents.cs).

### Strings:
 * [Permutations and Anagrams](Algorithms/Strings/Permutations.cs).
 * [Edit Distance](Algorithms/Strings/EditDistance.cs).
  * Uses a generic custom class for passing costs: [EditDistanceCostsMap\<T\>](Algorithms/Strings/EditDistanceCostsMap.cs).

### Numeric:
 * [Catalan Numbers](Algorithms/Numeric/CatalanNumbers.cs).

### Visualization:
 * [Tree Drawer](DataStructures/Trees/TreeDrawer.cs).

=================================================================

## Contributors
 * [Edgar Carballo Domínguez](https://github.com/karv).


## License
This project is licensed under the [MIT License](LICENSE).
