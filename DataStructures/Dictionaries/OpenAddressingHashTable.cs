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
 * Last Modified: 6/7/2017
 * 
 */

using System;
using System.Collections.Generic;

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
            /* You need:
             *  - a key
             *  - a value
             */
            public TKey key { get; set; }
            public int value { get; set; }
            public OAHashEntry()
            {
                key = default(TKey);
                value = -1;
            }
        }
        /* You need:
         *  -size: from user
         *  -defaut size
         *  -load factor
         *  -how many are in your table
         *  -collection of entries
         *  -which hash function you are using: enum and const value
         *      >double hashing function
         */
        private int _size { get; set; }
        private double _loadFactor { get; set; }
        private int _inTable { get; set; }
        private OAHashEntry<TKey>[] _table { get; set; }


        //---------------for multiple types of hashing-----------------------
        //public enum HashFunction { Linear, Quadratic, Double};
        //private string _funct;
        //private HashFunction _functionInUse;

        /* Functions you need: all need to be based on which function you are using (set constant value for the object)
         *  -constructor--DONE
         *  -insert--DONE
         *  -delete --might not need this for this hash table
         *  -find--DONE
         *  -expand--only if full, then rehash
         *  -contract--only if load factor is less than 4
         * 
         */
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
                insert(exp[i].key);
            }
        }

        //might not be needed for double hashing
        private void _contract()
        {
            //will hold contents of _table to copy over
            OAHashEntry<TKey>[] temp = new OAHashEntry<TKey>[_size];
            temp = _table;
            //shrink the size and rehash
            _size /= 2;
            OAHashEntry<TKey>[] con = new OAHashEntry<TKey>[_size];
            for (int i = 0; i < con.Length; i++)
            {
                //initialize each slot
                con[i] = new OAHashEntry<TKey>();
            }

            _inTable = 0;
            _table = con;

            //rehash over the newly sized table
            for (int i = 0; i < _table.Length; i++)
            {
                if (_table[i].value != -1)
                {
                    insert(con[i].key);
                }
            }
        }

        private int _double_hash(TKey key, int i)
        {
            int hash_value;
            int second_hash_value;
            int slot;

            //calculates first hash value
            hash_value = Convert.ToInt32(key) % _size;
            //calculate second hash value
            second_hash_value = 1 + (Convert.ToInt32(key) % (_size - 2)); //FOR TESTING: size 13 - 2 == 11
            //slot index based on first hash value, second hash value as an offset based on a counter
            slot = (hash_value + (i * second_hash_value)) % _size;

            //make sure it is not out of bounds
            if (slot > _size)
            {
                int wrap = slot - _size;
                slot = wrap;
            }

            return slot;
        }

        public void insert(TKey key)
        {
            int i = 0;

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
            if (i == _size)
            {
                //expand and rehash
                _expand();
            }
        }

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
    }
}
