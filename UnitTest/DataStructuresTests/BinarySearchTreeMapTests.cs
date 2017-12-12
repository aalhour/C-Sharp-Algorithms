using DataStructures.Trees;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class BinarySearchTreeMapTests
    {
        [Fact]
        public static void DoTest()
        {
            // Binary Search Tree Map collection
            var bstMap = new BinarySearchTreeMap<int, string>(allowDuplicates: false);

            // Testing data
            KeyValuePair<int, string>[] values = new KeyValuePair<int, string>[10];

            // Prepare the values array
            for (int i = 1; i <= 10; ++i)
            {
                var keyValPair = new KeyValuePair<int, string>(i, String.Format("Integer: {0}", i));
                values[i - 1] = keyValPair;
            }

            // Test singular insert
            for (int i = 0; i < 10; ++i)
                bstMap.Insert(values[i].Key, values[i].Value);

            Assert.True(bstMap.Count == values.Length, "Expected the same number of items.");

            bstMap.Clear();

            // Test collection insert
            bstMap.Insert(values);

            bool passed = true;
            // Test enumeration of key-value pairs is still in oreder
            var enumerator = bstMap.GetInOrderEnumerator();
            for (int i = 0; i < 10; ++i)
            {
                if (enumerator.MoveNext())
                {
                    var curr = enumerator.Current;
                    if (curr.Key != values[i].Key || curr.Value != values[i].Value)
                    {
                        passed = false;
                        break;
                    }
                }
            }
            Assert.True(passed);


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

            Assert.True(bstMap.Count == values.Length + 1, "Expected the same number of items.");

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

            Assert.False(insert_duplicate_passed, "Fail! The tree doesn't allow duplicates");

            // Test find
            Assert.True(bstMap.Find(5).Key == 5, "Wrong find result!");
            Assert.True(bstMap.FindMin().Key == 0, "Wrong min!");
            Assert.True(bstMap.FindMax().Key == 10, "Wrong max!");

            // Assert find raises exception on non-existing elements
            bool threwKeyNotFoundError = false;

            try
            {
                bstMap.Find(999999999);
                threwKeyNotFoundError = false;
            }
            catch (KeyNotFoundException)
            {
                threwKeyNotFoundError = true;
            }

            Assert.True(threwKeyNotFoundError, "Expected to catch KeyNotFoundException.");

            // Assert count
            Assert.True(bstMap.Count == 11);

            // Assert existence and nonexistence of some items
            Assert.True(bstMap.Contains(1));
            Assert.True(bstMap.Contains(3));
            Assert.False(bstMap.Contains(999));

            // Do some deletions
            bstMap.Remove(7);
            bstMap.Remove(1);
            bstMap.Remove(3);

            // Assert count
            Assert.True(bstMap.Count == 8);

            // Assert nonexistence of previously existing items
            Assert.False(bstMap.Contains(1));
            Assert.False(bstMap.Contains(3));

            // Remove root key
            var oldRootKey = bstMap.Root.Key;
            bstMap.Remove(bstMap.Root.Key);

            // Assert count
            Assert.True(bstMap.Count == 7);

            // Assert nonexistence of old root's key
            Assert.False(bstMap.Contains(oldRootKey));

        }//end-do-test
    }
}

