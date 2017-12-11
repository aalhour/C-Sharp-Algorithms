using DataStructures.Lists;
using System;
using System.Diagnostics;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class ArrayListTest
    {
        [Fact]
        public static void DoTest()
        {
            int index = 0;
            var arrayList = new ArrayList<long>();

            for (long i = 1; i < 1000000; ++i)
            {
                arrayList.Add(i);
            }

            for (int i = 1000; i < 1100; i++)
            {
                arrayList.RemoveAt(i);
            }

            for (int i = 100000; i < 100100; i++)
            {
                arrayList.Remove(i);
            }

            var allNumbersGreatorThanNineHundK = arrayList.FindAll(item => item > 900000);
            Assert.True(allNumbersGreatorThanNineHundK.Count > 0, "Count check failed!");

            long nineHundK = arrayList.Find(item => item == 900000);

            var indexIfNineHundK = arrayList.FindIndex(item => item == nineHundK);
            Assert.True(indexIfNineHundK != -1, "Wrong index!");

            index = 900000;
            arrayList.InsertAt(99999, index);
            arrayList.InsertAt(99999, index);
            arrayList.InsertAt(99999, index);
            arrayList.InsertAt(99999, index);
            arrayList.InsertAt(99999, index);

            var allNines = arrayList.FindAll(item => item == 99999);
            Assert.True(allNines.Count == 6, "Wrong result!");

            bool doesMillionExist = arrayList.Exists(item => item == 1000000);
            Assert.False(doesMillionExist, "Wrong result!");

            bool doesEightsExists = arrayList.Contains(88888);
            Assert.True(doesEightsExists, "Wrong result!");

            //arrayList.Reverse ();

            var arrayList2 = new ArrayList<int>();
            arrayList2.Add(0);
            arrayList2.Add(10);
            arrayList2.Add(20);
            arrayList2.Add(30);
            arrayList2.Add(40);
            arrayList2.Add(50);
            arrayList2.Add(60);
            arrayList2.Add(70);
            arrayList2.Add(80);
            arrayList2.Add(90);
            arrayList2.Add(100);

            // Console.WriteLine(arrayList2.ToHumanReadable(addHeader: true));

            //var arrayList3 = arrayList.GetRange(0, 100);
            //Console.WriteLine(arrayList3.ToHumanReadable(addHeader: true));


            /****************************************************************/


            arrayList = new ArrayList<long>();
            arrayList.AddRepeatedly(11, 32);
            Assert.True(arrayList.Count == 32, "Wrong array size.");
        }
    }
}
