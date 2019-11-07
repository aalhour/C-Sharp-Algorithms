using System.Collections.Generic;
using Xunit;
using Algorithms.Search;

namespace UnitTest.AlgorithmsTests
{
    public static class BinarySearcherTest
    {


        [Fact]
        public static void BinarySearchTest()
        {
            //list of ints
            IList<int> list = new List<int> { 9, 3, 7, 1, 6, 10 };
            IList<int> sortedList = new List<int> { 1, 3, 6, 7, 9, 10 };
            BinarySearcher<int> intSearcher = new BinarySearcher<int>(list, Comparer<int>.Default);
            int numToSearch = 6;
            int itemIndex = intSearcher.BinarySearch(numToSearch);
            int expectedIndex = sortedList.IndexOf(numToSearch);

            Assert.Equal(expectedIndex, itemIndex);
            Assert.Equal(numToSearch, intSearcher.Current);

            numToSearch = 20;
            int itemNotExists = intSearcher.BinarySearch(numToSearch);
            Assert.Equal(-1, itemNotExists);

            intSearcher.Dispose();

            //list of strings
            IList<string> animals = new List<string> {"lion", "cat", "tiger", "bee", "sparrow"};
            IList<string> sortedAnimals = new List<string> { "bee", "cat", "lion", "sparrow", "tiger" };
            BinarySearcher<string> strSearcher = new BinarySearcher<string>(animals, Comparer<string>.Default);
            string itemToSearch = "bee";
            int actualIndex = strSearcher.BinarySearch(itemToSearch);
            int expectedAnimalIndex = sortedAnimals.IndexOf(itemToSearch);

            Assert.Equal(expectedAnimalIndex, actualIndex);
            Assert.Equal(itemToSearch, strSearcher.Current);

            strSearcher.Dispose();
        }


        [Fact]
        public static void NullCollectionExceptionTest()
        {
            IList<int> list = null;
            Assert.Throws<System.NullReferenceException>(() => new BinarySearcher<int>(list, Comparer<int>.Default));
        }

    }
}
