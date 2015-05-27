# C-SHARP-ALGORITHMS

Implementations of Data Structures and Algorithms in C#.

I started writing this organized collection of classes as part of my preparation for technical interviews. This is for educational purposes only. However, the source code is stable.

This is a .NET solution and it can be opened with both Xmarian (MonoDevelop) and Visual Studio. It has two separate projects: 1) Algorithms, 2) DataStructures. Both of them are class-library projects.

## DATA STRUCTURES:

#### Lists:

 * [Single-Linked List](DataStructures/Lists/SLinkedList.cs).
 * [Double-Linked List](DataStructures/Lists/DLinkedList.cs).
 * [Array List](DataStructures/Lists/ArrayList.cs). A generic arrays-based list. Implements auto-resizing and handles overflow.
 * [Stack](DataStructures/Lists/Stack.cs). Based on my ArrayList\<T\>.
 * [Queue](DataStructures/Lists/Queue.cs). Based on my ArrayList\<T\>.

#### Priority Queues:

 * [Priority Queue](DataStructures/Heaps/PriorityQueue.cs). Based on my MaxHeap\<T\>.
 * [Keyed Priority Queue](DataStructures/Heaps/KeyedPriorityQueue.cs). Based on my MaxHeap\<T\>.

#### Heaps:

 * [Min-Heap](DataStructures/Heaps/MinHeap.cs). Based on my ArrayList\<T\>.
 * [Max-Heap](DataStructures/Heaps/MaxHeap.cs). Based on my ArrayList\<T\>.
 
#### Trees:

 * [Binary Search Tree](DataStructures/Trees/BinarySearchTree.cs).
 * [Augmented BST](DataStructures/Trees/AugmentedBinarySearchTree.cs). Extends the Binary Search Tree class; it keeps track of the Subtrees Size for each node.

## ALGORITHMS:

#### Sorting:
 Sorting algorithms are implemented as an extension method. They support the native Array\<T\>, and List\<T\> classes. They can takes value comparers. Insertion Sort supports my ArrayList\<T\> class.

  * [Insertion Sort](Algorithms/Sorting/InsertionSorter.cs)
  * [Quick Sort](Algorithms/Sorting/QuickSorter.cs)
  * [Merge Sort](Algorithms/Sorting/MergeSorter.cs)
  * [Heap Sort](Algorithms/Sorting/HeapSorter.cs)
  * [BST Sort](Algorithms/Sorting/BinarySearchTreeSorter.cs). Implements an unbalanced binary search tree sort.

    ```
    List<long> list = new List<long> () { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
    
    // The value comparer object. Can be any value comparer that implmenets IComparer.
    var valueComparer = Comparer<long>.Default;
    
    list.InsertionSort (valueComparer);
    list.QuickSort (valueComparer);
    list.MergeSort (valueComparer);
    list.HeapSort (valueComparer);
    list.UnbalancedBSTSort();
    ```
