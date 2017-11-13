using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DataStructures.Common
{
    /// <summary>
    /// Provides a list of the first 10,000 primes.
    /// This class is a singleton, and read the primes from the file @"Data\PrimesList_10K.csv".
    /// </summary>
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
        private static readonly List<int> Primes = new List<int>();

        // Picked the HashPrime to be (101) because it is prime, and if the ‘hashSize - 1’ is not a multiple of this HashPrime, which is 
        // enforced in _getUpperBoundPrime, then expand function has the potential of being every value from 1 to hashSize - 1. 
        // The choice is largely arbitrary.
        public const int HASH_PRIME = 101;


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
                            InitializeData();
                        }
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Initializes the primes document path and list.
        /// </summary>
        private static void InitializeData()
        {
            _primesDocPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data/PrimesDocument_10K.csv");
            var lines = File.ReadAllLines(_primesDocPath);

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
                    Primes.AddRange(numbers);
                }
            }
        }

        /// <summary>
        /// Return count of primes.
        /// </summary>
        public int Count => Primes.Count;

        /// <summary>
        /// Returns prime number at the specified index.
        /// </summary>
        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= Primes.Count)
                    throw new ArgumentOutOfRangeException();

                return Primes[index];
            }
        }

        /// <summary>
        /// Checks if a number is a Prime Number.
        /// </summary>
        public bool IsPrime(int candidate)
        {
            if ((candidate & 1) != 0)
            {
                int limit = (int)Math.Sqrt(candidate);

                for (int divisor = 3; divisor <= limit; divisor += 2)
                {
                    if ((candidate % divisor) == 0)
                        return false;
                }

                return true;
            }

            return (candidate == 2);
        }

        /// <summary>
        /// Returns the next biggest prime number.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public int GetNextPrime(int number)
        {
            if (number < 0)
                throw new ArgumentException("Number should be greater than or equal to 0.");

            for (int i = 0; i < Primes.Count; i++)
            {
                if (i >= number)
                    return i;
            }

            // Outside of our predefined table. Compute the prime the hard way. 
            for (int i = (number | 1); i < Int32.MaxValue; i += 2)
            {
                if (IsPrime(i) && ((i - 1) % HASH_PRIME != 0))
                    return i;
            }

            return number;
        }

        /// <summary>
        /// Returns the next minimum prime number.
        /// </summary>
        public int GetPreviousPrime(int number)
        {
            if (number < 0)
                throw new ArgumentException("Number should be greater than or equal to 0.");

            for (int i = 0; i < Primes.Count; i++)
            {
                if (Primes[i] >= number)
                    return Primes[i];
            }

            // Outside of our predefined table. Compute the prime the hard way. 
            for (int i = (number | 1); i < Int32.MaxValue; i += 2)
            {
                if (IsPrime(i) && ((i - 1) % HASH_PRIME != 0))
                    return i;
            }

            return number;
        }

        /// <summary>
        /// Returns the list of primes
        /// </summary>
        public List<int> GetAll
        {
            get { return Primes; }
        }

        /// <summary>
        /// Copy the primes list to an array, starting from a specified index.
        /// </summary>
        public void CopyTo(int[] array, int index = 0)
        {
            if (array == null)
                array = new int[Primes.Count];

            if (array.Length <= index)
                throw new ArgumentOutOfRangeException();

            int count = array.Length - index;
            int arrayIndex = index;

            if (count - Primes.Count > 0)
                count = Primes.Count;

            for (int i = 0; i < count; i++)
            {
                array[arrayIndex] = Primes[i];
                arrayIndex++;
            }
        }

    }

}
