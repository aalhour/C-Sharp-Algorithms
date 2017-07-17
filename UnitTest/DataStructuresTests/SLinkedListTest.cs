using DataStructures.Lists;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class SLinkedListTest
    {
        [Fact]
        public static void DoTest()
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

            listOfNumbers1.RemoveAt(0);
            listOfNumbers1.RemoveAt(3);
            listOfNumbers1.RemoveAt(4);
            listOfNumbers1.RemoveAt(2);
            listOfNumbers1.RemoveAt(2);
            listOfNumbers1.RemoveAt(0);

            listOfNumbers1.Prepend(3);
            listOfNumbers1.Prepend(2);
            listOfNumbers1.Prepend(1);

            // Print List and Count

            listOfNumbers1.InsertAt(444, listOfNumbers1.Count);
            listOfNumbers1.InsertAt(555, listOfNumbers1.Count);
            listOfNumbers1.InsertAt(222, 2);
            
            index = (listOfNumbers1.Count - 1);

            var arrayVersion = listOfNumbers1.ToArray();
            Assert.True(arrayVersion.Length == listOfNumbers1.Count);

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

            Assert.True(intArray[0] == 0 && intArray[intArray.Length - 1] == 55, "Wrong sorting!");
        }
    }
}

