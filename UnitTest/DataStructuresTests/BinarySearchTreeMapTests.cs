using System;
using System.Diagnostics;
using System.Collections.Generic;

using DataStructures.Common;
using DataStructures.Trees;

namespace C_Sharp_Algorithms.DataStructuresTests
{
    public static class BinarySearchTreeMapTests
    {
        public static void DoTest()
        {
            // Binary Search Tree Map collection
            var bstMap = new BinarySearchTreeMap<int, string>(allowDuplicates: false);

            // Testing data
            KeyValuePair<int, string>[] values = new KeyValuePair<int, string>[10];

            // Prepare the values array
            for(int i = 1; i <= 10; ++i)
            {
                var keyValPair = new KeyValuePair<int, string>(i, String.Format("Integer: {0}", i));
                values[i - 1] = keyValPair;
            }


            //
            // Test singular insert
            for (int i = 0; i < 10; ++i)
                bstMap.Insert(values[i].Key, values[i].Value);

            Debug.Assert(bstMap.Count == values.Length, "Expected the same number of items.");

            bstMap.Clear();


            //
            // Test collection insert
            bstMap.Insert(values);


            //
            // Test enumeration of key-value pairs is still in oreder
            var enumerator = bstMap.GetInOrderEnumerator();
            for (int i = 0; i < 10; ++i)
            {
                if (enumerator.MoveNext())
                {
                    var curr = enumerator.Current;
                    if (curr.Key != values[i].Key || curr.Value != values[i].Value)
                        throw new Exception();
                }
            }


            //
            // Test against re-shuffled insertions (not like above order)
            bstMap = new BinarySearchTreeMap<int, string>(allowDuplicates: false);

            bstMap.Insert(4, "int4");
            bstMap.Insert(5, "int5");
            bstMap.Insert(7, "int7");
            bstMap.Insert(2, "int2");
            bstMap.Insert(1, "int1");
            bstMap.Insert(3, "int3");
            bstMap.Insert(6, "int6");
            bstMap.Insert(0, "int0");
            bstMap.Insert(8, "int8");
            bstMap.Insert(10, "int10");
            bstMap.Insert(9, "int9");

            Debug.Assert(bstMap.Count == values.Length, "Expected the same number of items.");


            //
            // ASSERT INSERTING DUPLICATES WOULD BREAK
            var insert_duplicate_passed = true;
            try
            {
                // 2 already exists in tree
                bstMap.Insert(2, "int2");
                insert_duplicate_passed = true;
            }
            catch
            {
                insert_duplicate_passed = false;
            }

            Debug.Assert(insert_duplicate_passed == false, "Fail! The tree doesn't allow duplicates");


            //
            // Test find
            Debug.Assert(bstMap.Find(5).Key == 5, "Wrong find result!");
            Debug.Assert(bstMap.FindMin().Key == 0, "Wrong min!");
            Debug.Assert(bstMap.FindMax().Key == 10, "Wrong max!");


            //
            // Assert find raises exception on non-existing elements
            bool threwKeyNotFoundError = false;

            try
            {
                bstMap.Find(999999999);
                threwKeyNotFoundError = false;
            }
            catch(KeyNotFoundException)
            {
                threwKeyNotFoundError = true;
            }

            Debug.Assert(true == threwKeyNotFoundError, "Expected to catch KeyNotFoundException.");


            //
            // PRINT TREE
            Console.WriteLine("*****************************");
            Console.WriteLine(" [*] BINARY SEARCH TREE TREE:\r\n");
            Console.WriteLine("*****************************");
            Console.WriteLine(bstMap.DrawTree());
            Console.WriteLine("\r\n");


            //
            // Assert count
            Debug.Assert(bstMap.Count == 11);


            //
            // Assert existence and nonexistence of some items
            Debug.Assert(bstMap.Contains(1) == true);
            Debug.Assert(bstMap.Contains(3) == true);
            Debug.Assert(bstMap.Contains(999) == false);


            //
            // Do some deletions
            bstMap.Remove(7);
            bstMap.Remove(1);
            bstMap.Remove(3);


            //
            // Assert count
            Debug.Assert(bstMap.Count == 8);


            //
            // Assert nonexistence of previously existing items
            Debug.Assert(bstMap.Contains(1) == false);
            Debug.Assert(bstMap.Contains(3) == false);


            //
            // Remove root key
            var oldRootKey = bstMap.Root.Key;
            bstMap.Remove(bstMap.Root.Key);


            //
            // Assert count
            Debug.Assert(bstMap.Count == 7);


            //
            // Assert nonexistence of old root's key
            Debug.Assert(bstMap.Contains(oldRootKey) == false);


            //
            // PRINT TREE
            Console.WriteLine("*****************************");
            Console.WriteLine(" [*] BINARY SEARCH TREE TREE:\r\n");
            Console.WriteLine("*****************************");
            Console.WriteLine(bstMap.DrawTree(includeValues: true));
            Console.WriteLine("\r\n");

            Console.ReadLine();
        }//end-do-test
    }
}

