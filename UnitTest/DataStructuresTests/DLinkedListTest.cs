using System;
using System.Diagnostics;

using DataStructures.Lists;

namespace C_Sharp_Algorithms.DataStructuresTests
{
	public static class DLinkedListTest
	{
		public static void DoTest ()
		{
			int index = 0;
			DLinkedList<string> listOfStrings = new DLinkedList<string>();

            listOfStrings.Append("zero");
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
			Console.WriteLine("Remove At 0:\r\n" + listOfStrings.ToReadable());
            Debug.Assert(listOfStrings[0] == "fst", "Wrong first element.");

            // Remove 4th
            listOfStrings.RemoveAt(4);
            Console.WriteLine("Remove At 4:\r\n" + listOfStrings.ToReadable());
            Debug.Assert(listOfStrings[4] == "sxt", "Wrong 4th element.");

			// Remove 5th and 6th
            // Note that after removing 5th, the old element at index 6 becomes at index 5.
			listOfStrings.RemoveAt(5);
			listOfStrings.RemoveAt(5);
			Console.WriteLine("Remove At 5 & 6:\r\n" + listOfStrings.ToReadable());
            Debug.Assert(listOfStrings[4] == "sxt", "Wrong element at index 5.");
            Debug.Assert(listOfStrings.Count < 6, "Wrong element at index 6. There must be no element at index 5.");

			// Remove 3rd
            listOfStrings.RemoveAt(listOfStrings.Count - 1);
			Console.WriteLine("Removed last:\r\n" + listOfStrings.ToReadable());
            Debug.Assert(listOfStrings[3] == "for", "Wrong element at index 3.");

			// Remove 1st
			listOfStrings.RemoveAt(0);
			Console.WriteLine("Remove 0th:\r\n" + listOfStrings.ToReadable());
            Debug.Assert(listOfStrings[0] == "sec", "Wrong element at index 0.");

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

			// Test the remove item method
			listOfStrings.Remove ("trd");
			Console.WriteLine("Removed item 'trd':\r\n" + listOfStrings.ToReadable());

			listOfStrings.Remove ("InsertedAt 9");
			Console.WriteLine("Removed item 'InsertedAt 9':\r\n" + listOfStrings.ToReadable());

			// Print count
			Console.WriteLine("Count: " + listOfStrings.Count);

			Console.WriteLine();

			index = 0;
			Console.WriteLine("Get At " + index + ": " + listOfStrings[index]);

			index = (listOfStrings.Count / 2) + 1;
			Console.WriteLine("Get At " + index + ": " + listOfStrings[index]);

			index = (listOfStrings.Count / 2) + 2;
			Console.WriteLine("Get At " + index + ": " + listOfStrings[index]);

			index = (listOfStrings.Count - 1);
			Console.WriteLine("Get At " + index + ": " + listOfStrings[index]);

			Console.WriteLine();

			var firstRange = listOfStrings.GetRange(4, 6);
			Console.WriteLine("GetRange(4, 6):\r\n" + firstRange.ToReadable());

			var secondRange = firstRange.GetRange(4, 10);
			Console.WriteLine("From Previous GetRange(4, 10):\r\n" + secondRange.ToReadable());

			var thirdRange = (new DLinkedList<string>()).GetRange(0, 10);
			Console.WriteLine("Empty List: GetRange(0, 10):\r\n" + thirdRange.ToReadable());

			var arrayVersion = listOfStrings.ToArray();
			Debug.Assert (arrayVersion.Length == listOfStrings.Count);



            /****************************************************************************************/

            var stringsIterators = listOfStrings.GetEnumerator();

            Debug.Assert(stringsIterators.Current == listOfStrings[0], "Wrong enumeration.");

            if (stringsIterators.MoveNext() == true)
            {
                Debug.Assert(stringsIterators.Current == listOfStrings[1], "Wrong enumeration.");
            }

            stringsIterators.Dispose();

            Debug.Assert(listOfStrings != null && listOfStrings.Count > 0, "Enumartor has side effects!");

            /****************************************************************************************/

            var listOfNumbers = new DLinkedList<int>();

            listOfNumbers.Append(23);
            listOfNumbers.Append(42);
            listOfNumbers.Append(4);
            listOfNumbers.Append(16);
            listOfNumbers.Append(8);
            listOfNumbers.Append(15);
            listOfNumbers.Append(9);
            listOfNumbers.Append(55);
            listOfNumbers.Append(0);
            listOfNumbers.Append(34);
            listOfNumbers.Append(12);
            listOfNumbers.Append(2);

            listOfNumbers.SelectionSort();

            var intArray = listOfNumbers.ToArray();

            Debug.Assert(intArray[0] == 0 && intArray[intArray.Length - 1] == 55, "Wrong sorting!");
		}
	}
}

