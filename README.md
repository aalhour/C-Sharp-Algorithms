# C-Sharp-Algorithms
Implementations of Data Structures and Algorithms in C#

I am writing this organized collection of classes as part of my preparation for technical interviews. This is for educational purposes only. This solution-library doesn't aim to replace any well-written data structure and algorithms library out there.


### Data Structures:
* SLinkedList.cs (Singly-Linked List).
* DLinkedList.cs (Doubly-Linked List).
* ArrayList.cs (Array-Based List. Implements auto-resizing).
* Stack.cs (Stack - LIFO. Using the typed ArrayList implementation).
* Queue.cs (Queue - FIFO. Using the typed ArrayList implementation).


### Algorithms
* InsertionSort.cs
  * Implements Insertion Sort for System.List and my ArrayList data structures. Supports comparers.
    ```
    List<int> list1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
    list1.InsertionSort();
    
    ArrayList<string> list2 = new ArrayList<string>();
    list2.Add("one");
    list2.Add("two");
    list2.Add("three");
    list2.InsertionSort();
    ```
