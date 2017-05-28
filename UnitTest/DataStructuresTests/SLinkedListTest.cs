using System;
using System.Diagnostics;

using DataStructures.Lists;

namespace C_Sharp_Algorithms.DataStructuresTests
{
	public static class SLinkedListTest
	{
		public static void DoTest ()
		{
			int index = 0;
			SLinkedList<int> listOfNumbers1 = new SLinkedList<int>();

			listOfNumbers1.Append(10);
			listOfNumbers1.Append(124);
			listOfNumbers1.Prepend(654);
			listOfNumbers1.Prepend(8);
			listOfNumbers1.Append(127485693);
			listOfNumbers1.Append(34);
			listOfNumbers1.Append(823);

			Console.WriteLine(listOfNumbers1.ToReadable());

			listOfNumbers1.RemoveAt(0);
			Console.WriteLine("Removed 1st:\r\n" + listOfNumbers1.ToReadable());

			listOfNumbers1.RemoveAt(3);
			listOfNumbers1.RemoveAt(4);
			Console.WriteLine("Removed 3rd & 4th:\r\n" + listOfNumbers1.ToReadable());

			listOfNumbers1.RemoveAt(2);
			Console.WriteLine("Removed 3rd:\r\n" + listOfNumbers1.ToReadable());

			listOfNumbers1.RemoveAt(2);

			Console.WriteLine("Removed 3rd:\r\n" + listOfNumbers1.ToReadable());

			listOfNumbers1.RemoveAt(0);
			Console.WriteLine("Remove 1st:\r\n" + listOfNumbers1.ToReadable());

			listOfNumbers1.Prepend(3);
			listOfNumbers1.Prepend(2);
			listOfNumbers1.Prepend(1);
			// Print List and Count
			Console.WriteLine(listOfNumbers1.ToReadable());
			Console.WriteLine("Count: " + listOfNumbers1.Count + "\r\n");

			listOfNumbers1.InsertAt(444, listOfNumbers1.Count);
			listOfNumbers1.InsertAt(555, listOfNumbers1.Count);
			listOfNumbers1.InsertAt(222, 2);
			Console.WriteLine(listOfNumbers1.ToReadable());
			Console.WriteLine("Count: " + listOfNumbers1.Count + "\r\n");

			index = 0;
			Console.WriteLine("Get At " + index + ": " + listOfNumbers1.GetAt(index));

			index = (listOfNumbers1.Count / 2) + 1;
			Console.WriteLine("Get At " + index + ": " + listOfNumbers1.GetAt(index));

			index = (listOfNumbers1.Count / 2) + 2;
			Console.WriteLine("Get At " + index + ": " + listOfNumbers1.GetAt(index));

			index = (listOfNumbers1.Count - 1);
			Console.WriteLine("Get At " + index + ": " + listOfNumbers1.GetAt(index));

			Console.WriteLine();

			Console.WriteLine("GetRange(0, 3):\r\n" + listOfNumbers1.GetRange(0, 3).ToReadable());

			var arrayVersion = listOfNumbers1.ToArray();
			Debug.Assert (arrayVersion.Length == listOfNumbers1.Count);

            /************************************************************************************/

            var listOfNumbers2 = new SLinkedList<int>();

            listOfNumbers2.Append(23);
            listOfNumbers2.Append(42);
            listOfNumbers2.Append(4);
            listOfNumbers2.Append(16);
            listOfNumbers2.Append(8);
            listOfNumbers2.Append(15);
            listOfNumbers2.Append(9);
            listOfNumbers2.Append(55);
            listOfNumbers2.Append(0);
            listOfNumbers2.Append(34);
            listOfNumbers2.Append(12);
            listOfNumbers2.Append(2);

            listOfNumbers2.SelectionSort();

            var intArray = listOfNumbers2.ToArray();

            Debug.Assert(intArray[0] == 0 && intArray[intArray.Length - 1] == 55, "Wrong sorting!");
		}
	}
}

