/***
 * Open Addressing Hash Table
 * 
 * Implementation of Open Addressing Hash Table using
 *      - Linear Probing
 *      - Quadratic Probing
 *      - Double Hashing
 *      
 * Author: Samuel Kenney
 * Created: 6/7/2017
 * Last Modified: 6/11/2017
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

using DataStructures.Common;
using DataStructures.Hashing;
using System.Threading.Tasks;

namespace DataStructures.Dictionaries
{
    /// <summary>
    /// Open Addressing Data Structure
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue">will always be an integer since it maps to an index</typeparam>
    public class OpenAddressingHashTable<TKey>
    {

        /// <summary>
        /// Open Addressing Entry
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        private class OAHashEntry<TKeyEntry>
        {
            public TKey key { get; set; }
            public int value { get; set; }
            public OAHashEntry()
            {
                key = default(TKey);
                value = -1;
            }
        }

        private int _size { get; set; }
        private double _loadFactor { get; set; }
        private int _inTable { get; set; }
        private OAHashEntry<TKey>[] _table { get; set; }


        //---------------for multiple types of hashing-----------------------
        //public enum HashFunction { Linear, Quadratic, Double};
        //private string _funct;
        //private HashFunction _functionInUse;

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        /// 
        public OpenAddressingHashTable(int size) //need to introduce a way for multiple hash functions later
        {
            _size = size;
            _loadFactor = 0.40;
            _inTable = 0;
            _table = new OAHashEntry<TKey>[_size];
            //initialize all values to -1
            for (int i = 0; i < _table.Length; i++)
            {
                //initialize each slot
                _table[i] = new OAHashEntry<TKey>();
            }
        }

        private void _expand()
        {
            //will hold contents of _table to copy over
            OAHashEntry<TKey>[] temp = new OAHashEntry<TKey>[_size];
            temp = _table;
            //double the size and rehash
            _size *= 2;
            OAHashEntry<TKey>[] exp = new OAHashEntry<TKey>[_size];
            for (int i = 0; i < exp.Length; i++)
            {
                //initialize each slot
                exp[i] = new OAHashEntry<TKey>(); 
            }

            _inTable = 0;
            _table = exp;
            //rehash over the newly sized table
            for (int i = 0; i < temp.Length; i++)
            {
                //this should rehash since the size is now doubled so the hashing will be different
                //inserts the key into _table
                insert(temp[i].key);
            }
        }

        private int _double_hash(TKey key, int i)
        {
            int hash_value = 0;
            int second_hash_value = 0;
            int slot = 0;

            //grabs hash value for a string
            if (typeof(TKey) == typeof(string))
            {
                //https://stackoverflow.com/questions/4092393/value-of-type-t-cannot-be-converted-to
                TKey newTkeyString = (TKey)(object)key;
                string newTkeyString2 = (string)(object)newTkeyString;

                //https://stackoverflow.com/questions/400733/how-to-get-ascii-value-of-string-in-c-sharp
                byte[] asciiBytes = Encoding.ASCII.GetBytes(newTkeyString2);

                int string_value = 0;
                foreach (byte bite in asciiBytes)
                {
                    string_value += (int)bite;
                }

                //calculates first hash values
                hash_value = Convert.ToInt32(string_value) % _size;
                //calculate second hash value
                second_hash_value = 1 + (Convert.ToInt32(string_value) % (_size - 1));
            }
            //grabs a hash value for a char
            else if (typeof(TKey) == typeof(char))
            {
                //https://stackoverflow.com/questions/4092393/value-of-type-t-cannot-be-converted-to
                TKey newTkeyChar = (TKey)(object)key;
                char newTkeyChar2 = (char)(object)newTkeyChar;

                int char_value = (int)newTkeyChar2;

                //calculates first hash values
                hash_value = Convert.ToInt32(char_value) % _size;
                //calculate second hash value
                second_hash_value = 1 + (Convert.ToInt32(char_value) % (_size - 1));
            }
            else
            {
                //calculates first hash values
                hash_value = Convert.ToInt32(key) % _size;
                //calculate second hash value
                second_hash_value = 1 + (Convert.ToInt32(key) % (_size - 1));
            }

            //slot index based on first hash value, second hash value as an offset based on a counter, will also guarentee that the slot will be within the range 0 to size
            slot = (hash_value + (i * second_hash_value)) % _size;

            return slot;
        }

        public void insert(TKey key)
        {
            int i = 0;

            //makes sure there are no duplicate keys
            if (contains(key))
            {
                return;
            }

            do
            {
                //calculate index
                int index = _double_hash(key, i);
                if (_table[index].value == -1)
                {
                    //set value and key
                    _table[index].key = key;
                    _table[index].value = index;
                    //increment how many items are in the table
                    _inTable++;
                    break;
                }
                else
                {
                    i++;
                }

            } while (i != _size);

            //every slot is in the table is occupied
            if (_inTable == _size)
            {
                //expand and rehash
                _expand();
            }
        }

        //finds the key
        public int search(TKey key)
        {
            int i = 0;

            do
            {
                //calculate index
                int index = _double_hash(key, i);
                if (IComparable.Equals(_table[index].key, key))
                {
                    return index;
                }
                i++;

            } while (i < _size);

            return -1;
        }
        //returns if the key is in the table
        public bool contains(TKey key)
        {
            if (search(key) != -1)
            {
                return true;
            }
            return false;
        }
    }
}
