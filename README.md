# C-Sharp-Algorithms
Implementations of Data Structures and Algorithms in C#

I am writing this organized collection of classes as part of my preparation for technical interviews. This is for educational purposes only. This solution-library doesn't aim to replace any well-written data structure and algorithms library out there.


### Data Structures:
* Single-Linked List (SLinkedList.cs).
* Double-Linked List (DLinkedList.cs).
* Array List (ArrayList.cs). An arrays-based list. Implements auto-resizing.
* Stack (Stack.cs). Using my ArrayList implementation.
* Queue (Queue.cs). Using my ArrayList implementation.


### Algorithms
* Insertion Sort (InsertionSorter.cs)
  * Implements insertion sort as an extention method for the following collections: System.List, System.Array, and my ArrayList data structure.
  * Supports value comparers.
    ```
    List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
    list.InsertionSort();
    ```
* Quick Sort (QuickSorter.cs)
  * Implements quick sort as an extension method for all classes that implement IList<T>. Which includes, but is not limited to, System.List and System.Array.
    ```
    List<long> list = new List<long> () { 23, 42, 4, 16, 8, 15, 3, 9, 55, 0, 34, 12, 2, 46, 25 };
    list.QuickSort ();
    ```
