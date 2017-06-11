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
            var OAtable = new OpenAddressingHashTable<int>(13);

            OAtable.insert(79);
            OAtable.insert(72);
            OAtable.insert(98);
            OAtable.insert(14);

            int one = OAtable.search(79);
            int seven = OAtable.search(72);
            int five = OAtable.search(98);
            int nine = OAtable.search(14);
            int nil = OAtable.search(223);

            Debug.Assert(one == 1);
            Debug.Assert(seven == 7);
            Debug.Assert(five == 5);
            Debug.Assert(nine == 9);
            Debug.Assert(nil == -1);

            OAtable.insert(2);
            OAtable.insert(8);
            OAtable.insert(4);
            OAtable.insert(6);

            
        }
    }
}
