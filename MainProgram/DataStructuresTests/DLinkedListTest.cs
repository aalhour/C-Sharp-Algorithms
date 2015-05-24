using System;
using System.Diagnostics;

using DataStructures;

namespace C_Sharp_Algorithms.DataStructuresTests
{
	public static class DLinkedListTest
	{
		public static void DoTest ()
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
			Debug.Assert (arrayVersion.Length == listOfStrings.Count);
		}
	}
}

