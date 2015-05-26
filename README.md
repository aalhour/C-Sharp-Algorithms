# C-SHARP-ALGORITHMS

## DESCRIPTION:
Implementations of Data Structures and Algorithms in C#

I started writing this organized collection of classes as part of my preparation for technical interviews. This is for educational purposes only. This solution-library doesn't aim to replace any well-written data structure and algorithms library out there. However, the source code is stable.

## DATA STRUCTURES:

#### LISTS:

 * **Single-Linked List** (SLinkedList.cs).
 * **Double-Linked List** (DLinkedList.cs).
 * **Array List** (ArrayList.cs). A generic arrays-based list. Implements auto-resizing.
 * **Stack** (Stack.cs). Based on my ArrayList\<T\>.
 * **Queue** (Queue.cs). Based on my ArrayList\<T\>.
 * **Priority Queue** (PriorityQueue.cs). Based on my MaxHeap\<T\>.
 * **Keyed Priority Queue** (KeyedPriorityQueue.cs). Based on my MaxHeap\<T\>.

#### HEAPS:

 * **Min-Heap** (MinHeap.cs). Based on my ArrayList\<T\>.
 * **Max-Heap** (MaxHeap.cs). Based on my ArrayList\<T\>.
 
#### TREES:

 * **Binary Search Tree** (BinarySearchTree.cs).

## ALGORITHMS:

#### SORTING:
Sorting algorithms are implemented as an extension method. They support the native Array\<T\>, and List\<T\> classes. They can takes value comparers. Insertion Sort supports my ArrayList\<T\> class.

  * **Insertion Sort** (InsertionSorter.cs)
  * **Quick Sort** (QuickSorter.cs)
  * **Merge Sort** (MergeSorter.cs)
  * **Heap Sort** (HeapSorter.cs)
  * **BST Sort** (BinarySearchTreeSorter.cs). Implements an unbalanced binary search tree sort.

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
