using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DataStructures.Common
{
    public sealed class PrimesList
    {
        //
        // Singleton implementation with an attempted thread-safety using double-check locking
        // internal datastorage singleton container
        private static PrimesList _instance;

        // lock for thread-safety laziness
        private static readonly object Mutex = new object();

        //
        // INSTANCE VARIABLES
        private static string _primesDocPath = string.Empty;
        private readonly static List<int> _primes = new List<int>();

        /// <summary>
        /// Empty private constructor.
        /// </summary>
        private PrimesList() { }

        /// <summary>
        /// Returns the singleton instance of this class.
        /// </summary>
        public static PrimesList Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Mutex)
                    {
                        if (_instance == null)
                        {
                            _instance = new PrimesList();
                            _initializeData();
                        }
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Initializes the primes document path and list.
        /// </summary>
        private static void _initializeData()
        {
            _primesDocPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\PrimesDocument_10K.csv");
            string[] lines = File.ReadAllLines(_primesDocPath);

            foreach (var line in lines)
            {
                // Split the line by commas and convert the collection to a list.
                var numbersAsStrings = line.Split(',').ToList<string>();
                
                // defensive check against empty strings.
                numbersAsStrings.RemoveAll(item => string.IsNullOrEmpty(item) == true);

                if (numbersAsStrings.Count > 0)
                {
                    // cast them into integers and add them to the primes list
                    var numbers = numbersAsStrings.Select(item => Convert.ToInt32(item)).ToList<int>();
                    _primes.AddRange(numbers);
                }
            }
        }

        /// <summary>
        /// Return count of primes.
        /// </summary>
        public int Count
        {
            get { return _primes.Count; }
        }

        /// <summary>
        /// Returns prime number at the specified index.
        /// </summary>
        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= _primes.Count)
                    throw new ArgumentOutOfRangeException();

                return _primes[index];
            }
        }

        /// <summary>
        /// Returns the list of primes
        /// </summary>
        public List<int> GetAll
        {
            get { return _primes; }
        }

        /// <summary>
        /// Copy the primes list to an array, starting from a specified index.
        /// </summary>
        public void CopyTo(int[] array, int index = 0)
        {
            if (array == null)
                array = new int[_primes.Count];

            if(array.Length <= index)
                throw new ArgumentOutOfRangeException();

            int count = array.Length - index;
            int arrayIndex = index;

            if (count - _primes.Count > 0)
                count = _primes.Count;

            for(int i = 0; i < count; i++)
            {
                array[arrayIndex] = _primes[i];
                arrayIndex++;
            }
        }

    }

}
