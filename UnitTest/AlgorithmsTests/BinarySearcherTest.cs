using System.Collections.Generic;
using Xunit;
using Algorithms.Search;

namespace UnitTest.AlgorithmsTests
{
    public static class BinarySearcherTest
    {


        [Fact]
        public static void MergeSortTest()
        {
            //a list of int
            IList<int> list = new List<int> {9, 3, 7, 1, 6, 10};
          
            IList<int> sortedList = BinarySearcher.MergeSort<int>(list);
            IList<int> expectedList = new List<int> { 1, 3, 6, 7, 9, 10 };

            Assert.Equal(expectedList, sortedList);

            //a list of strings
            IList<string> animals = new List<string> {"lion", "cat", "tiger", "bee"};
            IList<string> sortedAnimals = BinarySearcher.MergeSort<string>(animals);
            IList<string> expectedAnimals = new List<string> {"bee", "cat", "lion", "tiger"};
            
            Assert.Equal(expectedAnimals, sortedAnimals);

        }


        [Fact]
        public static void BinarySearchTest()
        {
            //list of ints
            IList<int> list = new List<int> { 9, 3, 7, 1, 6, 10 };
            IList<int> sortedList = BinarySearcher.MergeSort<int>(list);

            int itemIndex = BinarySearcher.BinarySearch<int>(list, 6);
            int expectedIndex = sortedList.IndexOf(6);

            Assert.Equal(expectedIndex, itemIndex);

            //list of strings
            IList<string> animals = new List<string> {"lion", "cat", "tiger", "bee", "sparrow"};
            IList<string> sortedAnimals = BinarySearcher.MergeSort<string>(animals);

            int actualIndex = BinarySearcher.BinarySearch<string>(animals, "cat");
            int expectedAnimalIndex = sortedAnimals.IndexOf("cat");

            Assert.Equal(expectedAnimalIndex, actualIndex);

        }


        [Fact]
        public static void NullCollectionExceptionTest()
        {
            IList<int> list = null;
            Assert.Throws<System.NullReferenceException>(() => BinarySearcher.BinarySearch<int>(list,0));
        }

    }
}
