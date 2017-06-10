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
         *  -constructor
         *  -insert
         *  -delete
         *  -find
         *  -expand
         *  -contract
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

        private int _double_hash(TKey key)
        {
            //container will never be full due to a check in insert

            //initial hash value
            int hash_value = Convert.ToInt32(key) % _size;
            //slot already occupied
            //clean up duplicate code later
            if (_table[hash_value].value != -1)
            {
                //second hash value, offset
                int offset = 1 + (Convert.ToInt32(key) % (_size - 2)); //FOR TESTING: size 13 - 2 == 11
                hash_value = hash_value + offset;

                //checks if out of bounds- if so, wrap around
                if (hash_value > _size)
                {
                    int wrap = hash_value - _size;
                    hash_value = wrap;
                }

                //keep checking if slot is already taken until open stop
                while (_table[hash_value].value != -1)
                {
                    //add the offset again
                    hash_value += offset;
                    //checks if out of bounds- if so, wrap around
                    if (hash_value > _size)
                    {
                        int wrap = hash_value - _size;
                        hash_value = wrap;
                    }
                };
            }
            return hash_value;
        }
        public int insert(TKey key)
        {
            //calculate index
            int index = _double_hash(key);
            //set value and key
            _table[index].key = key;
            _table[index].value = index;
            //increment how many items are in the table
            _inTable++;

            //only for testing purposes
            return index;
        }
    }
}
