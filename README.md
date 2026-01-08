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
  <strong>A plug-and-play library of classic data structures and algorithms in C#</strong>
</p>

<p align="center">
  <a href="https://github.com/aalhour/C-Sharp-Algorithms/actions"><img src="https://img.shields.io/github/actions/workflow/status/aalhour/C-Sharp-Algorithms/build_and_test.yml?style=for-the-badge&logo=github&label=build" alt="Build Status" /></a>
  <a href="https://github.com/aalhour/C-Sharp-Algorithms/releases"><img src="https://img.shields.io/github/v/release/aalhour/C-Sharp-Algorithms?style=for-the-badge&logo=github" alt="Release" /></a>
  <a href="LICENSE"><img src="https://img.shields.io/github/license/aalhour/C-Sharp-Algorithms?style=for-the-badge" alt="License" /></a>
  <a href="https://github.com/aalhour/C-Sharp-Algorithms/stargazers"><img src="https://img.shields.io/github/stars/aalhour/C-Sharp-Algorithms?style=for-the-badge&logo=github" alt="Stars" /></a>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet" alt=".NET 10" />
  <img src="https://img.shields.io/badge/tests-623%20passing-brightgreen?style=flat-square" alt="Tests" />
  <img src="https://img.shields.io/badge/data%20structures-35+-blue?style=flat-square" alt="Data Structures" />
  <img src="https://img.shields.io/badge/algorithms-40+-blue?style=flat-square" alt="Algorithms" />
</p>

---

## ⚡ Quick Start

```bash
# Clone the repository
git clone https://github.com/aalhour/C-Sharp-Algorithms.git
cd C-Sharp-Algorithms

# Build and test
dotnet build
dotnet test
```

**Requirements:** [.NET 10.0 SDK](https://dotnet.microsoft.com/download) or later

---

## 📖 About

This project started as interview prep and evolved into a comprehensive reference implementation of classic computer science data structures and algorithms. Every component is:

- **Educational** — Clear, readable implementations with documentation
- **Tested** — 623+ unit tests ensuring correctness
- **Modular** — Use only what you need

### Project Structure

| Project | Description |
|---------|-------------|
| [`Algorithms`](Algorithms) | Sorting, searching, graph algorithms, and more |
| [`DataStructures`](DataStructures) | Lists, trees, heaps, hash tables, graphs |
| [`UnitTest`](UnitTest) | Comprehensive test coverage |

---

## 📦 Data Structures

<details>
<summary><strong>Lists & Collections</strong></summary>

| Structure | Description |
|-----------|-------------|
| [ArrayList](DataStructures/Lists/ArrayList.cs) | Dynamic array with auto-resizing |
| [Stack](DataStructures/Lists/Stack.cs) | LIFO collection |
| [Queue](DataStructures/Lists/Queue.cs) | FIFO collection |
| [SLinkedList](DataStructures/Lists/SLinkedList.cs) | Singly-linked list |
| [DLinkedList](DataStructures/Lists/DLinkedList.cs) | Doubly-linked list |
| [SkipList](DataStructures/Lists/SkipList.cs) | Probabilistic balanced structure |
| [CircularBuffer](DataStructures/Lists/CircularBuffer.cs) | Fixed-size circular queue |

</details>

<details>
<summary><strong>Heaps & Priority Queues</strong></summary>

| Structure | Description |
|-----------|-------------|
| [BinaryMinHeap](DataStructures/Heaps/BinaryMinHeap.cs) | Min-heap using binary tree |
| [BinaryMaxHeap](DataStructures/Heaps/BinaryMaxHeap.cs) | Max-heap using binary tree |
| [BinomialMinHeap](DataStructures/Heaps/BinomialMinHeap.cs) | Binomial heap (min) |
| [MinPriorityQueue](DataStructures/Heaps/MinPriorityQueue.cs) | Priority queue (min) |
| [KeyedPriorityQueue](DataStructures/Heaps/KeyedPriorityQueue.cs) | Key-value priority queue |

</details>

<details>
<summary><strong>Hash Tables</strong></summary>

| Structure | Description |
|-----------|-------------|
| [ChainedHashTable](DataStructures/Dictionaries/ChainedHashTable.cs) | Separate chaining collision resolution |
| [CuckooHashTable](DataStructures/Dictionaries/CuckooHashTable.cs) | Cuckoo hashing |
| [OpenScatterHashTable](DataStructures/Dictionaries/OpenScatterHashTable.cs) | Linear probing |
| [OpenAddressingHashTable](DataStructures/Dictionaries/OpenAddressingHashTable.cs) | Open addressing with double hashing |

**Hashing Functions:** [PrimeHashingFamily](DataStructures/Hashing/PrimeHashingFamily.cs) ・ [UniversalHashingFamily](DataStructures/Hashing/UniversalHashingFamily.cs)

</details>

<details>
<summary><strong>Trees</strong></summary>

**Search Trees**
| Structure | Description |
|-----------|-------------|
| [BinarySearchTree](DataStructures/Trees/BinarySearchTree.cs) | Classic BST ([Map version](DataStructures/Trees/BinarySearchTreeMap.cs)) |
| [AugmentedBinarySearchTree](DataStructures/Trees/AugmentedBinarySearchTree.cs) | BST with subtree counts |
| [TernarySearchTree](DataStructures/Trees/TernarySearchTree.cs) | For string keys |

**Self-Balancing Trees**
| Structure | Description |
|-----------|-------------|
| [AVLTree](DataStructures/Trees/AVLTree.cs) | Height-balanced BST |
| [RedBlackTree](DataStructures/Trees/RedBlackTree.cs) | Color-balanced BST ([Map version](DataStructures/Trees/RedBlackTreeMap.cs)) |
| [BTree](DataStructures/Trees/BTree.cs) | B-tree for disk-based storage |

**Prefix Trees**
| Structure | Description |
|-----------|-------------|
| [Trie](DataStructures/Trees/Trie.cs) | Prefix tree for strings |
| [TrieMap](DataStructures/Trees/TrieMap.cs) | Associative prefix tree |

</details>

<details>
<summary><strong>Graphs</strong></summary>

| Type | Sparse | Dense |
|------|--------|-------|
| **Undirected** | [UndirectedSparseGraph](DataStructures/Graphs/UndirectedSparseGraph.cs) | [UndirectedDenseGraph](DataStructures/Graphs/UndirectedDenseGraph.cs) |
| **Undirected Weighted** | [UndirectedWeightedSparseGraph](DataStructures/Graphs/UndirectedWeightedSparseGraph.cs) | [UndirectedWeightedDenseGraph](DataStructures/Graphs/UndirectedWeightedDenseGraph.cs) |
| **Directed** | [DirectedSparseGraph](DataStructures/Graphs/DirectedSparseGraph.cs) | [DirectedDenseGraph](DataStructures/Graphs/DirectedDenseGraph.cs) |
| **Directed Weighted** | [DirectedWeightedSparseGraph](DataStructures/Graphs/DirectedWeightedSparseGraph.cs) | [DirectedWeightedDenseGraph](DataStructures/Graphs/DirectedWeightedDenseGraph.cs) |

Also: [CliqueGraph](DataStructures/Graphs/CliqueGraph.cs)

</details>

<details>
<summary><strong>Sorted Collections</strong></summary>

| Structure | Description |
|-----------|-------------|
| [SortedList](DataStructures/SortedCollections/SortedList.cs) | Always-sorted list |
| [SortedDictionary](DataStructures/SortedCollections/SortedDictionary.cs) | Sorted key-value store |

</details>

---

## 🔧 Algorithms

<details>
<summary><strong>Sorting</strong> (16 algorithms)</summary>

| Algorithm | Type | Complexity |
|-----------|------|------------|
| [QuickSort](Algorithms/Sorting/QuickSorter.cs) | Divide & Conquer | O(n log n) avg |
| [MergeSort](Algorithms/Sorting/MergeSorter.cs) | Divide & Conquer | O(n log n) |
| [HeapSort](Algorithms/Sorting/HeapSorter.cs) | Selection | O(n log n) |
| [InsertionSort](Algorithms/Sorting/InsertionSorter.cs) | Insertion | O(n²) |
| [SelectionSort](Algorithms/Sorting/SelectionSorter.cs) | Selection | O(n²) |
| [BubbleSort](Algorithms/Sorting/BubbleSorter.cs) | Exchange | O(n²) |
| [ShellSort](Algorithms/Sorting/ShellSorter.cs) | Insertion | O(n log² n) |
| [CombSort](Algorithms/Sorting/CombSorter.cs) | Exchange | O(n²) |
| [CountingSort](Algorithms/Sorting/CountingSorter.cs) | Non-comparison | O(n + k) |
| [LSD RadixSort](Algorithms/Sorting/LSDRadixSorter.cs) | Non-comparison | O(nk) |
| [BucketSort](Algorithms/Sorting/BucketSorter.cs) | Distribution | O(n + k) |
| [BSTSort](Algorithms/Sorting/BinarySearchTreeSorter.cs) | Tree-based | O(n log n) |
| [CycleSort](Algorithms/Sorting/CycleSorter.cs) | In-place | O(n²) |
| [GnomeSort](Algorithms/Sorting/GnomeSorter.cs) | Exchange | O(n²) |
| [OddEvenSort](Algorithms/Sorting/OddEvenSorter.cs) | Exchange | O(n²) |
| [PigeonHoleSort](Algorithms/Sorting/PigeonHoleSorter.cs) | Distribution | O(n + k) |

</details>

<details>
<summary><strong>Graph Algorithms</strong></summary>

**Traversal**
- [Depth-First Search](Algorithms/Graphs/DepthFirstSearcher.cs)
- [Breadth-First Search](Algorithms/Graphs/BreadthFirstSearcher.cs)

**Shortest Paths**
- [Dijkstra](Algorithms/Graphs/DijkstraShortestPaths.cs) — Single-source, non-negative weights
- [Dijkstra All-Pairs](Algorithms/Graphs/DijkstraAllPairsShortestPaths.cs) — All pairs shortest paths
- [Bellman-Ford](Algorithms/Graphs/BellmanFordShortestPaths.cs) — Handles negative weights
- [BFS Shortest Paths](Algorithms/Graphs/BreadthFirstShortestPaths.cs) — Unweighted graphs

**Applications**
- [Cycle Detection](Algorithms/Graphs/CyclesDetector.cs)
- [Topological Sort](Algorithms/Graphs/TopologicalSorter.cs)
- [Connected Components](Algorithms/Graphs/ConnectedComponents.cs)
- [Bipartite Coloring](Algorithms/Graphs/BipartiteColoring.cs)

</details>

<details>
<summary><strong>Trees, Strings & Numeric</strong></summary>

**Tree Traversal**
- [Recursive Walker](Algorithms/Trees/BinaryTreeRecursiveWalker.cs) — Preorder, Inorder, Postorder
- [Iterative Walker](Algorithms/Trees/BinaryTreeIterativeWalker.cs) — Stack-based traversal

**String Algorithms**
- [Permutations & Anagrams](Algorithms/Strings/Permutations.cs)
- [Edit Distance](Algorithms/Strings/EditDistance.cs) (Levenshtein)

**Numeric**
- [Binomial Coefficients](Algorithms/Numeric/BinomialCoefficients.cs)
- [Catalan Numbers](Algorithms/Numeric/CatalanNumbers.cs)
- [Greatest Common Divisor](Algorithms/Numeric/GreatestCommonDivisor.cs)
- [Sieve of Eratosthenes](Algorithms/Numeric/SieveOfEratosthenes.cs)
- [Sieve of Atkin](Algorithms/Numeric/SieveOfAtkin.cs)

**Visualization**
- [Tree Drawer](DataStructures/Trees/TreeDrawer.cs)

</details>

<details>
<summary><strong>Searching</strong></summary>

- [Binary Search](Algorithms/Search/BinarySearcher.cs)

</details>

---

## 🚀 Roadmap

See [TODO.md](TODO.md) for planned additions. Highlights:

- **Data Structures:** Bloom Filters, Fibonacci Heaps, Disjoint Sets, Suffix Trees
- **Algorithms:** A* Search, Minimum Spanning Trees, String Matching (KMP, Boyer-Moore)

---

## 🤝 Contributing

Contributions welcome! Please read the [Contribution Guidelines](.github/CONTRIBUTING.md) first.

<a href="https://github.com/aalhour/C-Sharp-Algorithms/graphs/contributors">
  <img src="https://contributors-img.firebaseapp.com/image?repo=aalhour/C-Sharp-Algorithms" />
</a>

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).
