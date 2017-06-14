/***
 * Open Addressing Hash Table
 * 
 * Testing of Open Addressing Hash Table using
 *      - Linear Probing (coming soon)
 *      - Quadratic Probing (coming soon)
 *      - Double Hashing
 *      
 * Author: Samuel Kenney
 * Created: 6/7/2017
 * Last Modified: 6/13/2017
 * 
 */

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

            //testing insert for int
            OAtable.insert(79);
            OAtable.insert(72);
            OAtable.insert(98);
            OAtable.insert(14);

            //test search for int
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

            var OAtableString = new OpenAddressingHashTable<string>(7);

            //testing insert for string
            OAtableString.insert("foo");
            OAtableString.insert("bar");
            OAtableString.insert("insert");
            OAtableString.insert("string");

            //test search for string
            int string_two = OAtableString.search("foo");
            int string_one = OAtableString.search("bar");
            int string_three = OAtableString.search("insert");
            int string_five = OAtableString.search("string");

            Debug.Assert(string_two == 2);
            Debug.Assert(string_one == 1);
            Debug.Assert(string_three == 3);
            Debug.Assert(string_five == 5);

            var OAtableChar = new OpenAddressingHashTable<char>(70);

            //test insert for char
            OAtableChar.insert('h');
            OAtableChar.insert('e');
            OAtableChar.insert('l'); 
            OAtableChar.insert('l'); 
            OAtableChar.insert('o');
            OAtableChar.insert('_'); 
            OAtableChar.insert('w'); 
            OAtableChar.insert('o');
            OAtableChar.insert('r');
            OAtableChar.insert('l');
            OAtableChar.insert('d'); 

            //test search for char
            int char_threefour = OAtableChar.search('h');
            int char_threeone = OAtableChar.search('e');
            int char_threeeight = OAtableChar.search('l');
            //l is duplicate
            int char_fourone = OAtableChar.search('o');
            int char_twofive = OAtableChar.search('_');
            int char_fournine = OAtableChar.search('w');
            //o is duplicate
            int char_fourfour = OAtableChar.search('r');
            //l is duplicate
            int char_thirty = OAtableChar.search('d');

            Debug.Assert(char_threefour == 34);
            Debug.Assert(char_threeone == 31);
            Debug.Assert(char_threeeight == 38);
            //l is duplicate
            Debug.Assert(char_fourone == 41);
            Debug.Assert(char_twofive == 25);
            Debug.Assert(char_fournine == 49);
            //o is duplicate
            Debug.Assert(char_fourfour == 44);
            //l is duplicate
            Debug.Assert(char_thirty == 30);

            var OAtablefloat = new OpenAddressingHashTable<float>(7);

            //testing insert for float
            OAtablefloat.insert(79.7f);
            OAtablefloat.insert(72.3f);
            OAtablefloat.insert(98.9f);
            OAtablefloat.insert(14.6f);

            //test search for float
            int three_float = OAtablefloat.search(79.7f);
            int two_float = OAtablefloat.search(72.3f);
            int one_float = OAtablefloat.search(98.9f);
            int five_float = OAtablefloat.search(14.6f);
            int nil_float = OAtablefloat.search(223.0f);

            Debug.Assert(three_float == 3);
            Debug.Assert(two_float == 2);
            Debug.Assert(one_float == 1);
            Debug.Assert(five_float == 5);
            Debug.Assert(nil_float == -1);

        }
    }
}
