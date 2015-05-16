using System;
using System.Collections.Generic;

using DataStructures;

namespace C_Sharp_Algorithms.DataStructuresTests
{
    public static class ArrayListTest
    {
		public static void DoTest()
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

			//arrayList.Reverse ();

			var arrayList2 = new ArrayList<int> ();
			arrayList2.Add (0);
			arrayList2.Add (10);
			arrayList2.Add (20);
			arrayList2.Add (30);
			arrayList2.Add (40);
			arrayList2.Add (50);
			arrayList2.Add (60);
			arrayList2.Add (70);
			arrayList2.Add (80);
			arrayList2.Add (90);
			arrayList2.Add (100);

			Console.WriteLine(arrayList2.ToHumanReadable (addHeader: true));

			var arrayList3 = arrayList.GetRange (0, 100);
			Console.WriteLine(arrayList3.ToHumanReadable (addHeader: true));
		}
    }
}
