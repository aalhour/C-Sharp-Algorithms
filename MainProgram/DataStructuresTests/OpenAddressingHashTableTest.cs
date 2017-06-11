using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using DataStructures.Dictionaries;

namespace C_Sharp_Algorithms.DataStructuresTests
{
    class OpenAddressingHashTableTest
    {
        public static void DoTest()
        {
            var OAtable = new OpenAddressingHashTable<int>(7);

            //testing insert
            OAtable.insert(79);
            OAtable.insert(72);
            OAtable.insert(98);
            OAtable.insert(14);

            //test search
            int two = OAtable.search(79);
            int three = OAtable.search(72);
            int zero = OAtable.search(98);
            int six = OAtable.search(14);
            int nil = OAtable.search(223);

            Debug.Assert(two == 2);
            Debug.Assert(three == 3);
            Debug.Assert(zero == 0);
            Debug.Assert(six == 6);
            Debug.Assert(nil == -1);

            //test contains
            Debug.Assert(OAtable.contains(14));
            Debug.Assert(OAtable.contains(98));
            Debug.Assert(OAtable.contains(72));
            Debug.Assert(!OAtable.contains(5));

            //test expand
            OAtable.insert(858);
            OAtable.insert(456);
            OAtable.insert(24);
            //expand and rehash after this insertion-- new size: 14
            OAtable.insert(872234);

            //check each new value since the table was rehashed
            int nine = OAtable.search(79);
            int two_expand = OAtable.search(72);
            int zero_expand = OAtable.search(98);
            int six_expand = OAtable.search(14);
            int four_expand = OAtable.search(858);
            int eight = OAtable.search(456);
            int ten = OAtable.search(24);
            int five_expand = OAtable.search(872234);

            Debug.Assert(nine == 9);
            Debug.Assert(two_expand == 2);
            Debug.Assert(zero_expand == 0);
            Debug.Assert(six_expand == 6);
            Debug.Assert(four_expand == 4);
            Debug.Assert(eight == 8);
            Debug.Assert(ten == 10);
            Debug.Assert(five_expand == 5);
        }
    }
}
