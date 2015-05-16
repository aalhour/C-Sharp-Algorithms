using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using DataStructures;

namespace C_Sharp_Algorithms
{
    /// <summary>
    /// Functions that use the classes and use them with dummy data.
    /// These are used as simple functionality tests.
    /// </summary>
    public static class DummyTests
    {
		public static void Test_ArrayList()
		{
			int index = 0;
			DataStructures.ArrayList<long> arrayList = new DataStructures.ArrayList<long> ();

			for (long i = 1; i < 1000000; ++i)
			{
				arrayList.Add (i);
			}

			for (int i = 1000; i < 1100; i++)
			{
				arrayList.RemoveAt (i);
			}

			for (int i = 100000; i < 100100; i++)
			{
				arrayList.Remove (i);
			}

			var allNumbersGreatorThanNineHundK = arrayList.FindAll (item => item > 900000);

			long nineHundK = arrayList.Find (item => item == 900000);

			var indexIfNineHundK = arrayList.FindIndex (item => item == nineHundK);

			index = 900000;
			arrayList.InsertAt (99999, index);
			arrayList.InsertAt (99999, index);
			arrayList.InsertAt (99999, index);
			arrayList.InsertAt (99999, index);
			arrayList.InsertAt (99999, index);

			var allNines = arrayList.FindAll (item => item == 99999);

			bool doesMillionExist = arrayList.Exists (item => item == 1000000);

			bool doesEightsExists = arrayList.Contains (88888);

			arrayList.Reverse ();
		}


        public static void Test_Queue()
        {
            string top;
            DataStructures.Queue<string> queue = new DataStructures.Queue<string>();

            queue.Push("aaa");
            queue.Push("bbb");
            queue.Push("ccc");
            queue.Push("ddd");
            queue.Push("eee");
            queue.Push("fff");
            queue.Push("ggg");
            queue.Push("hhh");

            Console.WriteLine("Queue Elements:\r\n" + queue.ToReadable());

            var array = queue.ToArray();
            var list = queue.ToList();

            queue.Pop();
            queue.Pop();
            queue.Pop(out top);

            Console.WriteLine("Old 3nd-last: " + top + "\r\n");
            Console.WriteLine("Queue Elements:\r\n" + queue.ToReadable());

            queue.Pop();
            queue.Pop();

            Console.WriteLine("Queue Elements:\r\n" + queue.ToReadable());

            var array2 = queue.ToArray();
            var list2 = queue.ToList();

            Console.ReadLine();
        }

        public static void Test_Stack()
        {
            int top;
            DataStructures.Stack<int> stack = new DataStructures.Stack<int>();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);
            stack.Push(6);

            Console.WriteLine("Stack:\r\n" + stack.ToReadable());

            var array = stack.ToArray();
            var list = stack.ToList();

            stack.Pop();
            stack.Pop(out top);

            Console.WriteLine("Old 2nd-last: " + top);
            Console.WriteLine("Stack:\r\n" + stack.ToReadable());

            stack.Pop();
            stack.Pop();

            Console.WriteLine("Stack:\r\n" + stack.ToReadable());

            var array2 = stack.ToArray();
            var list2 = stack.ToList();

            Console.ReadLine();
        }

        public static void Test_DoublyLinkedList()
        {
            int index = 0;
			DLinkedList<string> listOfStrings = new DLinkedList<string>();

            listOfStrings.Append("fst");
            listOfStrings.Append("sec");
            listOfStrings.Append("trd");
            listOfStrings.Append("for");
            listOfStrings.Append("fft");
            listOfStrings.Append("sxt");
            listOfStrings.Append("svn");
            listOfStrings.Append("egt");

            // Print
            Console.WriteLine(listOfStrings.ToReadable());

            // Remove 1st
            listOfStrings.RemoveAt(0);
            Console.WriteLine("Removed 1st:\r\n" + listOfStrings.ToReadable());

            // Remove 5th and 6th
            listOfStrings.RemoveAt(4);
            listOfStrings.RemoveAt(5);
            Console.WriteLine("Removed 5th & 6th:\r\n" + listOfStrings.ToReadable());

            // Remove 4th
            listOfStrings.RemoveAt(3);
            Console.WriteLine("Removed last:\r\n" + listOfStrings.ToReadable());

            // Remove 3rd
            listOfStrings.RemoveAt(2);
            Console.WriteLine("Removed last:\r\n" + listOfStrings.ToReadable());

            // Remove 1st
            listOfStrings.RemoveAt(0);
            Console.WriteLine("Remove 1st:\r\n" + listOfStrings.ToReadable());

            listOfStrings.Prepend("semsem3");
            listOfStrings.Prepend("semsem2");
            listOfStrings.Prepend("semsem1");
            Console.WriteLine("Prepend 3 items:\r\n" + listOfStrings.ToReadable());
            Console.WriteLine("Count: " + listOfStrings.Count);

            listOfStrings.InsertAt("InsertedAtLast1", listOfStrings.Count);
            listOfStrings.InsertAt("InsertedAtLast2", listOfStrings.Count);
            listOfStrings.InsertAt("InsertedAtMiddle", (listOfStrings.Count / 2));
            listOfStrings.InsertAt("InsertedAt 4", 4);
            listOfStrings.InsertAt("InsertedAt 9", 9);
            listOfStrings.InsertAfter("InsertedAfter 11", 11);
            Console.WriteLine("Inserts 3 items At:\r\n" + listOfStrings.ToReadable());

            // Print count
            Console.WriteLine("Count: " + listOfStrings.Count);

            Console.WriteLine();

            index = 0;
            Console.WriteLine("Get At " + index + ": " + listOfStrings.GetAt(index));

            index = (listOfStrings.Count / 2) + 1;
            Console.WriteLine("Get At " + index + ": " + listOfStrings.GetAt(index));

            index = (listOfStrings.Count / 2) + 2;
            Console.WriteLine("Get At " + index + ": " + listOfStrings.GetAt(index));

            index = (listOfStrings.Count - 1);
            Console.WriteLine("Get At " + index + ": " + listOfStrings.GetAt(index));

            Console.WriteLine();

            var firstRange = listOfStrings.GetRange(4, 6);
            Console.WriteLine("GetRange(4, 6):\r\n" + firstRange.ToReadable());

            var secondRange = firstRange.GetRange(4, 10);
            Console.WriteLine("From Previous GetRange(4, 10):\r\n" + secondRange.ToReadable());

			var thirdRange = (new DLinkedList<string>()).GetRange(0, 10);
            Console.WriteLine("Empty List: GetRange(0, 10):\r\n" + thirdRange.ToReadable());

            var arrayVersion = listOfStrings.ToArray();

            Console.ReadLine();
        }

        public static void Test_SinglyLinkedList()
        {
            int index = 0;
			SLinkedList<int> listOfNumbers = new SLinkedList<int>();

            listOfNumbers.Append(10);
            listOfNumbers.Append(124);
            listOfNumbers.Prepend(654);
            listOfNumbers.Prepend(8);
            listOfNumbers.Append(127485693);
            listOfNumbers.Append(34);
            listOfNumbers.Append(823);

            Console.WriteLine(listOfNumbers.ToReadable());

            listOfNumbers.RemoveAt(0);
            Console.WriteLine("Removed 1st:\r\n" + listOfNumbers.ToReadable());

            listOfNumbers.RemoveAt(3);
            listOfNumbers.RemoveAt(4);
            Console.WriteLine("Removed 3rd & 4th:\r\n" + listOfNumbers.ToReadable());

            listOfNumbers.RemoveAt(2);
            Console.WriteLine("Removed 3rd:\r\n" + listOfNumbers.ToReadable());

            listOfNumbers.RemoveAt(2);

            Console.WriteLine("Removed 3rd:\r\n" + listOfNumbers.ToReadable());

            listOfNumbers.RemoveAt(0);
            Console.WriteLine("Remove 1st:\r\n" + listOfNumbers.ToReadable());

            listOfNumbers.Prepend(3);
            listOfNumbers.Prepend(2);
            listOfNumbers.Prepend(1);
            // Print List and Count
            Console.WriteLine(listOfNumbers.ToReadable());
            Console.WriteLine("Count: " + listOfNumbers.Count + "\r\n");

            listOfNumbers.InsertAt(444, listOfNumbers.Count);
            listOfNumbers.InsertAt(555, listOfNumbers.Count);
            listOfNumbers.InsertAt(222, 2);
            Console.WriteLine(listOfNumbers.ToReadable());
            Console.WriteLine("Count: " + listOfNumbers.Count + "\r\n");

            index = 0;
            Console.WriteLine("Get At " + index + ": " + listOfNumbers.GetAt(index));

            index = (listOfNumbers.Count / 2) + 1;
            Console.WriteLine("Get At " + index + ": " + listOfNumbers.GetAt(index));

            index = (listOfNumbers.Count / 2) + 2;
            Console.WriteLine("Get At " + index + ": " + listOfNumbers.GetAt(index));

            index = (listOfNumbers.Count - 1);
            Console.WriteLine("Get At " + index + ": " + listOfNumbers.GetAt(index));

            Console.WriteLine();

            Console.WriteLine("GetRange(0, 3):\r\n" + listOfNumbers.GetRange(0, 3).ToReadable());

            var arrayVersion = listOfNumbers.ToArray();

            // Capture the console.
            Console.ReadLine();
        }
    }
}
