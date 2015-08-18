using System;
using System.Diagnostics;

using DataStructures.Lists;

namespace C_Sharp_Algorithms.DataStructuresTests
{
    public static class SkipListTest
    {
        public static void DoTest()
        {
            var skipList = new SkipList<int>();
            skipList.Add(20);
            skipList.Add(10);
            skipList.Add(5);
            skipList.Add(11);
            skipList.Add(1);

            // Get enumarator
            var enumerator = skipList.GetEnumerator();

            Console.WriteLine(" [*] Skip-List elements:");
            Console.Write("..... ");
            while (enumerator.MoveNext())
                Console.Write("{0} -> ", enumerator.Current);

            Console.WriteLine("\r\n===================================\r\n");

            int min = default(int);
            if (skipList.TryDeleteMin(out min))
                Console.WriteLine("Removed min from SkipList, and it was: {0}", min);

            Console.WriteLine("\r\n===================================\r\n");

            // Reload enumarator
            enumerator = skipList.GetEnumerator();

            Console.WriteLine(" [*] Skip-List elements:");
            Console.Write("..... ");
            while (enumerator.MoveNext())
                Console.Write("{0} -> ", enumerator.Current);

            Console.WriteLine("\r\n===================================\r\n");

            skipList.Clear();

            for (int i = 100; i >= 50; --i)
                skipList.Add(i);

            for (int i = 0; i <= 35; ++i)
                skipList.Add(i);

            // Reload enumarator
            enumerator = skipList.GetEnumerator();

            Console.WriteLine(" [*] Skip-List elements:");
            Console.Write("..... ");
            while (enumerator.MoveNext())
                Console.Write("{0} -> ", enumerator.Current);

            Console.WriteLine("\r\nSkipList Count = {0}", skipList.Count);

            Console.WriteLine("\r\n===================================\r\n");

            for (int i = -15; i <= 0; ++i)
                skipList.Add(i);

            for (int i = -15; i >= -35; --i)
                skipList.Add(i);

            // Reload enumarator
            enumerator = skipList.GetEnumerator();

            Console.WriteLine(" [*] Skip-List elements:");
            Console.Write("..... ");
            while (enumerator.MoveNext())
                Console.Write("{0} -> ", enumerator.Current);

            Debug.Assert(skipList.Count == 124);

            Console.WriteLine("\r\nSkipList Count = {0}", skipList.Count);

            Console.WriteLine("\r\n===================================\r\n");

            skipList.Clear();

            for (int i = 100; i >= 0; --i)
                skipList.Add(i);

            // Reload enumarator
            enumerator = skipList.GetEnumerator();

            Console.WriteLine(" [*] Skip-List elements:");
            Console.Write("..... ");
            while (enumerator.MoveNext())
                Console.Write("{0} -> ", enumerator.Current);

            Debug.Assert(skipList.Count == 101);

            Console.WriteLine("\r\nSkipList Count = {0}", skipList.Count);

            Console.WriteLine("\r\n===================================\r\n");

            Console.ReadLine();
        }
    }
}
