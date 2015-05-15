using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using DataStructures;

namespace C_Sharp_Algorithms
{
    /// <summary>
    /// Functions that use the classes and use them with dummy data.
    /// These are used as simple functionality tests.
    /// </summary>
    public static class DummyTests
    {
        public static void Test_SinglyLinkedList()
        {
            SLList<int> listOfNumbers = new SLList<int>();

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

			listOfNumbers.Prepend (3);
			listOfNumbers.Prepend (2);
			listOfNumbers.Prepend (1);
			// Print List and Count
			Console.WriteLine(listOfNumbers.ToReadable());
			Console.WriteLine("Count: " + listOfNumbers.Count + "\r\n");

			listOfNumbers.InsertAt (444, listOfNumbers.Count);
			listOfNumbers.InsertAt (555, listOfNumbers.Count);
            listOfNumbers.InsertAt(222, 2);
			Console.WriteLine(listOfNumbers.ToReadable());
			Console.WriteLine("Count: " + listOfNumbers.Count + "\r\n");

			// Capture the console.
            Console.ReadLine();
        }

        public static void Test_DoublyLinkedList()
        {
            DLList<string> listOfNumbers = new DLList<string>();

            listOfNumbers.Append("fst");
            listOfNumbers.Append("sec");
            listOfNumbers.Append("trd");
            listOfNumbers.Append("for");
            listOfNumbers.Append("fft");
            listOfNumbers.Append("sxt");
            listOfNumbers.Append("svn");
            listOfNumbers.Append("egt");

            // Print
            Console.WriteLine(listOfNumbers.ToReadable());

            // Remove 1st
            listOfNumbers.RemoveAt(0);
            Console.WriteLine("Removed 1st:\r\n" + listOfNumbers.ToReadable());

            // Remove 5th and 6th
            listOfNumbers.RemoveAt(4);
            listOfNumbers.RemoveAt(5);
            Console.WriteLine("Removed 5th & 6th:\r\n" + listOfNumbers.ToReadable());

            // Remove 4th
            listOfNumbers.RemoveAt(3);
            Console.WriteLine("Removed last:\r\n" + listOfNumbers.ToReadable());

            // Remove 3rd
            listOfNumbers.RemoveAt(2);
            Console.WriteLine("Removed last:\r\n" + listOfNumbers.ToReadable());

            // Remove 1st
            listOfNumbers.RemoveAt(0);
            Console.WriteLine("Remove 1st:\r\n" + listOfNumbers.ToReadable());

            // Print count
            Console.WriteLine("Count: " + listOfNumbers.Count);

            Console.ReadLine();
        }
    }
}
