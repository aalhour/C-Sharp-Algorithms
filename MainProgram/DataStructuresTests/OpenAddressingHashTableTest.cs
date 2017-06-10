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

            int one = OAtable.insert(79);
            int seven = OAtable.insert(72);
            int five = OAtable.insert(98);
            int nine = OAtable.insert(14);

            Debug.Assert(one == 1);
            Debug.Assert(seven == 7);
            Debug.Assert(five == 5);
            Debug.Assert(nine == 9);
        }
    }
}
