using System;
using System.Diagnostics;

using DataStructures;

namespace C_Sharp_Algorithms.DataStructuresTests
{
	public static class SLinkedListTest
	{
		public static void DoTest ()
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
			Debug.Assert (arrayVersion.Length == listOfNumbers.Count);
		}
	}
}

