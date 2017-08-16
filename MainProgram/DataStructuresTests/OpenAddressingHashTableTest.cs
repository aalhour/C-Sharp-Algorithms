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
            var OAtable = new OpenAddressingHashTable<int, int>(7);

            //testing Add for int
            OAtable.Add(79, 1);
            OAtable.Add(72, 2);
            OAtable.Add(98, 3);
            OAtable.Add(14, 4);

            var one = OAtable[79];
            Debug.Assert(one == 1);
            var six = OAtable[72];
            Debug.Assert(six == 2);
            var three = OAtable[98];
            Debug.Assert(three == 3);
            var four = OAtable[14];
            Debug.Assert(four == 4);

            //test search for int
            int two_search = OAtable.search(79);
            int three_search = OAtable.search(72);
            int zero = OAtable.search(98);
            int six_search = OAtable.search(14);
            int nil = OAtable.search(223);

            Debug.Assert(two_search == 2);
            Debug.Assert(three_search == 3);
            Debug.Assert(zero == 0);
            Debug.Assert(six_search == 6);
            Debug.Assert(nil == -1);

            //test ContainsKey
            Debug.Assert(OAtable.ContainsKey(14));
            Debug.Assert(OAtable.ContainsKey(98));
            Debug.Assert(OAtable.ContainsKey(72));
            Debug.Assert(!OAtable.ContainsKey(5));

            //test expand
            OAtable.Add(858, 5);
            OAtable.Add(456, 6);
            OAtable.Add(24, 7);
            //expand and rehash after this insertion-- new size: 14
            OAtable.Add(872234, 8);

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

            var five = OAtable[858];
            Debug.Assert(five == 5);
            var six_1 = OAtable[456];
            Debug.Assert(six_1 == 6);
            var seven = OAtable[24];
            Debug.Assert(seven == 7);
            var eight_2 = OAtable[872234];
            Debug.Assert(eight_2 == 8);


            var OAtableString = new OpenAddressingHashTable<string, string>(7);

            //testing Add for string
            OAtableString.Add("foo", "oof");
            OAtableString.Add("bar", "rab");
            OAtableString.Add("insert", "tresni");
            OAtableString.Add("string", "gnirts");

            var oof = OAtableString["foo"];
            Debug.Assert(oof == "oof");
            var rab = OAtableString["bar"];
            Debug.Assert(rab == "rab");
            var tresni = OAtableString["insert"];
            Debug.Assert(tresni == "tresni");
            var gnirts = OAtableString["string"];
            Debug.Assert(gnirts == "gnirts");


            //test search for string
            int string_two = OAtableString.search("foo");
            int string_one = OAtableString.search("bar");
            int string_three = OAtableString.search("insert");
            int string_five = OAtableString.search("string");

            Debug.Assert(string_two == 2);
            Debug.Assert(string_one == 1);
            Debug.Assert(string_three == 3);
            Debug.Assert(string_five == 5);

            var OAtableChar = new OpenAddressingHashTable<char, char>(70);

            //test Add for char
            OAtableChar.Add('h', 'h');
            OAtableChar.Add('e', 'e');
            OAtableChar.Add('l', 'l');
            OAtableChar.Add('l', 'l');
            OAtableChar.Add('o', 'o');
            OAtableChar.Add('_', '_');
            OAtableChar.Add('w', 'w');
            OAtableChar.Add('o', 'o');
            OAtableChar.Add('r', 'r');
            OAtableChar.Add('l', 'l');
            OAtableChar.Add('d', 'd');
            
            var h = OAtableChar['h'];
            Debug.Assert(h == 'h');
            var w = OAtableChar['w'];
            Debug.Assert(w == 'w');
            var l = OAtableChar['l'];
            Debug.Assert(l == 'l');
            var d = OAtableChar['d'];
            Debug.Assert(d == 'd');

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

            //test remove, clear, count, rehash and TryGetValue
            var OAtable_2 = new OpenAddressingHashTable<int, int>(7);

            //testing Add for int
            OAtable_2.Add(79, 1);
            OAtable_2.Add(72, 2);
            OAtable_2.Add(98, 3);
            OAtable_2.Add(14, 4);

            var one_2 = OAtable_2[79];
            Debug.Assert(one_2 == 1);
            var six_2 = OAtable_2[72];
            Debug.Assert(six_2 == 2);
            var three_2 = OAtable_2[98];
            Debug.Assert(three_2 == 3);
            var four_2 = OAtable_2[14];
            Debug.Assert(four_2 == 4);

            int count = OAtable_2.Count();
            Debug.Assert(count == 4);

            Debug.Assert(OAtable_2.Remove(72));

            count = OAtable_2.Count();
            Debug.Assert(count == 3);

            Debug.Assert(!OAtable_2.Remove(500));

            Debug.Assert(OAtable_2.Remove(79));

            count = OAtable_2.Count();
            Debug.Assert(count == 2);

            OAtable_2.Clear();

            count = OAtable_2.Count();
            Debug.Assert(count == 0);

            //test KeyValuePair<> for Add, Remove, and Contains
            var OAtable_3 = new OpenAddressingHashTable<int, int>(7);
            var item = new KeyValuePair<int, int>(79, 10);
            OAtable_3.Add(item);
            var item1 = new KeyValuePair<int, int>(72, 11);
            OAtable_3.Add(item1);
            var item2 = new KeyValuePair<int, int>(98, 12);
            OAtable_3.Add(item2);
            var item3 = new KeyValuePair<int, int>(14, 13);
            OAtable_3.Add(item3);
            var item4 = new KeyValuePair<int, int>(200, 50);

            var ten_3 = OAtable_3[79];
            Debug.Assert(ten == 10);
            var eleven = OAtable_3[72];
            Debug.Assert(eleven == 11);
            var twelve = OAtable_3[98];
            Debug.Assert(twelve == 12);
            var thirteen = OAtable_3[14];
            Debug.Assert(thirteen == 13);

            Debug.Assert(OAtable_3.Contains(item2));
            Debug.Assert(!OAtable_3.Contains(item4));

            OAtable_3.Remove(item1);
            count = OAtable_3.Count();
            Debug.Assert(count == 3);
            

        }
    }
}
