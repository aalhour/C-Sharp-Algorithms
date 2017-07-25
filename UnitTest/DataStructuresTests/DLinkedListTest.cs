using DataStructures.Lists;
using System;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class DLinkedListTest
    {
        [Fact]
        public static void DoTest()
        {
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

            // Remove 1st
            listOfStrings.RemoveAt(0);
            Assert.True(listOfStrings[0] == "fst", "Wrong first element.");

            // Remove 4th
            listOfStrings.RemoveAt(4);
            Console.WriteLine("Remove At 4:\r\n" + listOfStrings.ToReadable());
            Assert.True(listOfStrings[4] == "sxt", "Wrong 4th element.");

            // Remove 5th and 6th
            // Note that after removing 5th, the old element at index 6 becomes at index 5.
            listOfStrings.RemoveAt(5);
            listOfStrings.RemoveAt(5);
            Assert.True(listOfStrings[4] == "sxt", "Wrong element at index 5.");
            Assert.True(listOfStrings.Count < 6, "Wrong element at index 6. There must be no element at index 5.");

            // Remove 3rd
            listOfStrings.RemoveAt(listOfStrings.Count - 1);
            Assert.True(listOfStrings[3] == "for", "Wrong element at index 3.");

            // Remove 1st
            listOfStrings.RemoveAt(0);
            Assert.True(listOfStrings[0] == "sec", "Wrong element at index 0.");

            listOfStrings.Prepend("semsem3");
            listOfStrings.Prepend("semsem2");
            listOfStrings.Prepend("semsem1");

            listOfStrings.InsertAt("InsertedAtLast1", listOfStrings.Count);
            listOfStrings.InsertAt("InsertedAtLast2", listOfStrings.Count);
            listOfStrings.InsertAt("InsertedAtMiddle", (listOfStrings.Count / 2));
            listOfStrings.InsertAt("InsertedAt 4", 4);
            listOfStrings.InsertAt("InsertedAt 9", 9);
            listOfStrings.InsertAfter("InsertedAfter 11", 11);

            // Test the remove item method
            listOfStrings.Remove("trd");

            listOfStrings.Remove("InsertedAt 9");
            var arrayVersion = listOfStrings.ToArray();
            Assert.True(arrayVersion.Length == listOfStrings.Count);

            /****************************************************************************************/

            var stringsIterators = listOfStrings.GetEnumerator();
            Assert.True(stringsIterators.Current == listOfStrings[0], "Wrong enumeration.");
            if (stringsIterators.MoveNext() == true)
            {
                Assert.True(stringsIterators.Current == listOfStrings[1], "Wrong enumeration.");
            }

            stringsIterators.Dispose();
            Assert.True(listOfStrings != null && listOfStrings.Count > 0, "Enumartor has side effects!");

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
            Assert.True(intArray[0] == 0 && intArray[intArray.Length - 1] == 55, "Wrong sorting!");
        }
    }
}

