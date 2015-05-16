# C-Sharp-Algorithms
Implementations of Data Structures and Algorithms in C#

I am writing this organized collection of classes as part of my preparation for technical interviews. This is for educational purposes only. This solution-library doesn't aim to replace any well-written data structure and algorithms library out there.


### Data Structures:
* Singly-Linked List (SLinkedList.cs).
* Doubly-Linked List (DLinkedList.cs).
* ArrayList (ArrayList.cs). An arrays-based list. Implements auto-resizing.
* Stack (Stack.cs). Using my ArrayList implementation.
* Queue (Queue.cs). Using my ArrayList implementation.


### Algorithms
* InsertionSort (InsertionSorter.cs)
  * Implements insertion sort for System.List, and my ArrayList as extention methods. Supports comparers.
    ```
    List<int> list1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
    list1.InsertionSort();
    
    ArrayList<string> list2 = new ArrayList<string>();
    list2.Add("one");
    list2.Add("two");
    list2.Add("three");
    list2.InsertionSort();
    ```
