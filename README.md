# C-Sharp-Algorithms
Implementations of Data Structures and Algorithms in C#

I am writing this organized collection of classes as part of my preparation for technical interviews. This is for educational purposes only. This solution-library doesn't aim to replace any well-written data structure and algorithms library out there.




## Data Structures:
* Single-Linked List (SLinkedList.cs).
* Double-Linked List (DLinkedList.cs).
* Array List (ArrayList.cs). An arrays-based list. Implements auto-resizing.
* Stack (Stack.cs). Using my ArrayList implementation.
* Queue (Queue.cs). Using my ArrayList implementation.




## Algorithms:

#### Sorting:
 * Insertion Sort (InsertionSorter.cs)
   * Implements insertion sort as an extention method. Supports the system Array<T>, List<T>, and my ArrayList<T>. Supports value comparers.
 
 * Quick Sort (QuickSorter.cs)
   * Implements quick sort as an extension method. Supports the system Array<T>, and List<T>. Supports value comparers.
  
 * Merge Sort (MergeSorter.cs)
   * Implements merge sort as an extension method. Supports the system Array<T>, and List<T>. Supports value comparers.
    
    ```
    List<long> list = new List<long> () { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
    
    // The value comparer object. Can be any value comparer that implmenets IComparer.
    var valueComparer = Comparer<long>.Default;
    
    list.InsertionSort (valueComparer);
    list.QuickSort (valueComparer);
    list.MergeSort (valueComparer);
    ```
