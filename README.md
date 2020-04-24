```

                                          o---o    |   |                                 
                                         /       --O---O--                               
                                        O          |   |                                 
                                         \       --O---O--                               
                                          o---o    |   |                                 


              O    o       o--o    o--o   o---o   o-O-o  o--O--o  o   o  o     o   o--o 
             / \   |      o       o    o  |   |     |       |     |   |  |\   /|  |     
            o---o  |      |  o-o  |    |  O--Oo     |       |     O---O  | \o/ |   o--o 
            |   |  |      o    |  o    o  |  \      |       |     |   |  |     |      | 
            o   o  O---o   o--o    o--o   o   \o  o-O-o     o     o   o  o     o  o---o 

```

<p align="center">
  <a href="LICENSE" alt="License">
    <img src="https://img.shields.io/github/license/aalhour/C-Sharp-Algorithms?style=flat-square&color=blue" />
  </a>
  <a href="https://travis-ci.org/aalhour/C-Sharp-Algorithms" alt="Build">
    <img src="https://img.shields.io/travis/aalhour/C-Sharp-Algorithms?style=flat-square&color=blue" />
  </a>
  <a href="https://github.com/aalhour/C-Sharp-Algorithms/graphs/contributors" alt="Contributors">
    <img src="https://img.shields.io/github/contributors/aalhour/C-Sharp-Algorithms?style=flat-square&color=blue" />
  </a>
  <a href="https://github.com/aalhour/C-Sharp-Algorithms/pulse" alt="Activity">
    <img src="https://img.shields.io/github/commit-activity/m/aalhour/C-Sharp-Algorithms?style=flat-square&color=blue" />
  </a>
</p>

##
### WHAT IS C# ALGORITHMS?

A plug-and-play class-library project of standard Data Structures and Algorithms, written in C#. It contains **75+** Data Structures and Algorithms, designed as Object-Oriented isolated components. Even though this project started for educational purposes, the implemented Data Structures and Algorithms are standard, efficient, stable and tested.

##
### BACK STORY

This project originally started out as an interview preparation project. However, after receiving a great amount of positive responses on [reddit](https://redd.it/3etf9f), and noticing excitement from a few [GitHubers](https://github.com/aalhour/C-Sharp-Algorithms#contributors) to contribute furthermore to it, the project took on a different meaning. So, I decided to keep maintaining it as a reference for data structures and algorithm implementations in C# as well as my own research side-project under these topics.

##
### DESCRIPTION

#### Solution Hierarchy:

This is a C#.NET solution-project, and it contains three subprojects:

  1. [Algorithms](Algorithms): A class library project. Contains the Algorithms implementations
  2. [Data Structures](DataStructures): A class library project. Contains the Data Structures implementations
  3. [UnitTest](UnitTest): Unit-testing project for the Algorithms and Data Structures

#### Requirements:

  1. .NET Core >= 2.0
  2. XUnit

#### A Note to Contributors:

If you wish to contribute to C# ALGORITHMS, then please make sure you check out the [Contribution Guidelines](.github/CONTRIBUTING.md) first.

##
### DATA STRUCTURES

#### Linear:

  * [Skip List](DataStructures/Lists/SkipList.cs)
  * [Array List](DataStructures/Lists/ArrayList.cs)
  * [Stack](DataStructures/Lists/Stack.cs)
  * [Queue](DataStructures/Lists/Queue.cs)
  * [Single-Linked List](DataStructures/Lists/SLinkedList.cs)
  * [Double-Linked List](DataStructures/Lists/DLinkedList.cs)

#### Circular:
  
  * [Circular Buffer](DataStructures/Lists/CircularBuffer.cs)

#### Heaps:

  * [Binary-Min Heap](DataStructures/Heaps/BinaryMinHeap.cs)
  * [Binary-Max Heap](DataStructures/Heaps/BinaryMaxHeap.cs)
  * [Binomial-Min Heap](DataStructures/Heaps/BinomialMinHeap.cs)
 
#### Priority Queues:

  * [Min-Priority Queue](DataStructures/Heaps/MinPriorityQueue.cs)
  * [Key-value Priority Queue](DataStructures/Heaps/KeyedPriorityQueue.cs)
 
#### Hashing Functions:

  * [Prime Hashing Family](DataStructures/Hashing/PrimeHashingFamily.cs)
  * [Universal Hashing Family](DataStructures/Hashing/UniversalHashingFamily.cs)

#### Hash Tables:

  * [Chained Hash Table](DataStructures/Dictionaries/ChainedHashTable.cs)
  * [Cuckoo Hash Table](DataStructures/Dictionaries/CuckooHashTable.cs)
  * [Open-Addressing Hash Table](DataStructures/Dictionaries/OpenAddressingHashTable.cs)

#### Sorted Collections (Tree-based):

  * [Sorted List](DataStructures/SortedCollections/SortedList.cs)
  * [Sorted Dictionary](DataStructures/SortedCollections/SortedDictionary.cs)

#### Trees:

  
  * Basic Search Trees:
    + [Binary Search Tree](DataStructures/Trees/BinarySearchTree.cs)
      * [Map version](DataStructures/Trees/BinarySearchTreeMap.cs) _(supports key-value pairing; nodes indexed by keys)_
    + [\(Augmented\) Binary Search Tree](DataStructures/Trees/AugmentedBinarySearchTree.cs)
    + [Ternary Search Tree](DataStructures/Trees/TernarySearchTree.cs)  
  * Self-Balancing Trees:
    + [AVL Tree](DataStructures/Trees/AVLTree.cs)
    + [B-Tree](DataStructures/Trees/BTree.cs)
    + [Red-Black Tree](DataStructures/Trees/RedBlackTree.cs)
      * [Map version](DataStructures/Trees/RedBlackTreeMap.cs) _(supports key-value pairing; nodes indexed by keys)_
  * Prefix Trees:
    + [Trie](DataStructures/Trees/Trie.cs)
    + [Trie Map](DataStructures/Trees/TrieMap.cs) _(associative prefix tree; complete words are keys to records)_
 
#### Graphs:

  * Undirected Graphs:
    + [Clique Graphs](DataStructures/Graphs/CliqueGraph.cs)
    + [Undirected Sparse Graph](DataStructures/Graphs/UndirectedSparseGraph.cs)
    + [Undirected Dense Graph](DataStructures/Graphs/UndirectedDenseGraph.cs)
  * Undirected Weighted Graphs:
    + [Undirected Weighted Sparse Graph](DataStructures/Graphs/UndirectedWeightedSparseGraph.cs)
    + [Undirected Weighted Dense Graph](DataStructures/Graphs/UndirectedWeightedDenseGraph.cs)
  * Directed Graphs:
    + [Directed Sparse Graph](DataStructures/Graphs/DirectedSparseGraph.cs)
    + [Directed Dense Graph](DataStructures/Graphs/DirectedDenseGraph.cs)
  * Directed Weighted Graphs:
    + [Directed Weighted Sparse Graph](DataStructures/Graphs/DirectedWeightedSparseGraph.cs)
    + [Directed Weighted Dense Graph](DataStructures/Graphs/DirectedWeightedDenseGraph.cs)


##
### ALGORITHMS

#### Sorting:

  * [Bubble Sort](Algorithms/Sorting/BubbleSorter.cs)
  * [Bucket Sort](Algorithms/Sorting/BucketSorter.cs)
  * [BST Sort](Algorithms/Sorting/BinarySearchTreeSorter.cs)
  * [Comb Sort](Algorithms/Sorting/CombSorter.cs)
  * [Counting Sort](Algorithms/Sorting/CountingSorter.cs)
  * [Cycle Sort](Algorithms/Sorting/CycleSorter.cs)
  * [Gnome Sort](Algorithms/Sorting/GnomeSorter.cs)
  * [Heap Sort](Algorithms/Sorting/HeapSorter.cs)
  * [Insertion Sort](Algorithms/Sorting/InsertionSorter.cs)
  * [LSD Radix Sort](Algorithms/Sorting/LSDRadixSorter.cs)
  * [Merge Sort](Algorithms/Sorting/MergeSorter.cs)
  * [Selection Sort](Algorithms/Sorting/SelectionSorter.cs)
  * [Shell Sort](Algorithms/Sorting/ShellSorter.cs)
  * [OddEven Sort](Algorithms/Sorting/OddEvenSorter.cs)
  * [PigeonHole Sort](Algorithms/Sorting/PigeonHoleSorter.cs)
  * [Quick Sort](Algorithms/Sorting/QuickSorter.cs)

#### Searching:

  * [Binary Search](Algorithms/Search/BinarySearcher.cs)

#### Graphs:

  * Graph Search:
    + [Depth-First Searcher](Algorithms/Graphs/DepthFirstSearcher.cs)
    + [Breadth-First Searcher](Algorithms/Graphs/BreadthFirstSearcher.cs)
  * Shortest Paths:
    + [Breadth-First SPs](Algorithms/Graphs/BreadthFirstShortestPaths.cs)
    + [Bellman-Ford SPs](Algorithms/Graphs/BellmanFordShortestPaths.cs)
    + [Dijkstra SPs](Algorithms/Graphs/DijkstraShortestPaths.cs)
    + [Dijkstra All-Pairs SPs](Algorithms/Graphs/DijkstraAllPairsShortestPaths.cs)
  * DFS Applications:
    + [Cycles Detector](Algorithms/Graphs/CyclesDetector.cs)
    + [Topological Sorter](Algorithms/Graphs/TopologicalSorter.cs)
  * BFS Applications:
    + [Connected Components](Algorithms/Graphs/ConnectedComponents.cs)
    + [Bipartite Graphs Coloring](Algorithms/Graphs/BipartiteColoring.cs)

#### Trees:

  * [Recursive Binary Tree Walker](Algorithms/Trees/BinaryTreeRecursiveWalker.cs)
    + Methods: PrintAll, ForEach, Contains and BinarySearch. Traversal Modes: Preorder, Inorder & Postorder

#### Strings:

  * [Permutations and Anagrams](Algorithms/Strings/Permutations.cs)
  * [Edit Distance](Algorithms/Strings/EditDistance.cs)
    + Uses a generic custom class for passing costs: [EditDistanceCostsMap\<T\>](Algorithms/Strings/EditDistanceCostsMap.cs)

#### Numeric:

  * [Binomial Coefficients](Algorithms/Numeric/BinomialCoefficients.cs)
  * [Catalan Numbers](Algorithms/Numeric/CatalanNumbers.cs)
  * [Greatest Common Divisor](Algorithms/Numeric/GreatestCommonDivisor.cs)

#### Visualization:

  * [Tree Drawer](DataStructures/Trees/TreeDrawer.cs)


##
### CONTRIBUTORS

<a href="https://github.com/aalhour/C-Sharp-Algorithms/graphs/contributors">
  <img src="https://contributors-img.firebaseapp.com/image?repo=aalhour/C-Sharp-Algorithms" />
</a>

<br />
<!-- Made with [contributors-img](https://contributors-img.firebaseapp.com). -->

##
### LICENSE

This project is licensed under the [MIT License](LICENSE).
