using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataStrcutres;

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

            var first = listOfNumbers.Append(10);
            var second = listOfNumbers.Append(124);
            var third = listOfNumbers.Append(654);
            var fourth = listOfNumbers.Append(8);
            var fifth = listOfNumbers.Append(127485693);
            var sixth = listOfNumbers.Append(34);
            var seventh = listOfNumbers.Append(823);

            Console.WriteLine(listOfNumbers.ToReadable());

            listOfNumbers.Remove(first);
            listOfNumbers.Remove(first);
            listOfNumbers.Remove(first);

            Console.WriteLine("Removed 1st: " + listOfNumbers.ToReadable());

            listOfNumbers.Remove(fourth);
            listOfNumbers.Remove(fifth);

            Console.WriteLine("Removed 4th & 5th: " + listOfNumbers.ToReadable());

            listOfNumbers.Remove(seventh);

            Console.WriteLine("Removed 7th: " + listOfNumbers.ToReadable());

            listOfNumbers.Remove(first);

            Console.WriteLine("Try remove old 1st: " + listOfNumbers.ToReadable());

            Console.ReadLine();
        }
    }
}
