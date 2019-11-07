using System.Collections.Generic;
using Xunit;
using Algorithms.Search;

namespace UnitTest.AlgorithmsTests
{
    public static class BinarySearcherTest
    {
        [Fact]
        public static void IntBinarySearchTest()
        {
            //list of ints
            IList<int> list = new List<int> { 9, 3, 7, 1, 6, 10 };
            IList<int> sortedList = new List<int> { 1, 3, 6, 7, 9, 10 };
            int numToSearch = 6;
            BinarySearcher<int> intSearcher = new BinarySearcher<int>(list, Comparer<int>.Default);
            intSearcher.SearchFor(numToSearch);
            int itemIndex = intSearcher.BinarySearch();
            int expectedIndex = sortedList.IndexOf(numToSearch);

            Assert.Equal(expectedIndex, itemIndex);
            Assert.Equal(numToSearch, intSearcher.Current);

            numToSearch = 20;
            intSearcher.SearchFor(numToSearch);
            int itemNotExists = intSearcher.BinarySearch();
            Assert.Equal(-1, itemNotExists);
        }

        [Fact]
        public static void StringBinarySearchTest()
        {
            //list of strings
            IList<string> animals = new List<string> { "lion", "cat", "tiger", "bee", "sparrow" };
            IList<string> sortedAnimals = new List<string> { "bee", "cat", "lion", "sparrow", "tiger" };
            string itemToSearch = "bee";
            BinarySearcher<string> strSearcher = new BinarySearcher<string>(animals, Comparer<string>.Default);
            strSearcher.SearchFor(itemToSearch);
            int actualIndex = strSearcher.BinarySearch();
            int expectedAnimalIndex = sortedAnimals.IndexOf(itemToSearch);

            Assert.Equal(expectedAnimalIndex, actualIndex);
            Assert.Equal(itemToSearch, strSearcher.Current);

            itemToSearch = "shark";
            strSearcher.SearchFor(itemToSearch);
            int itemNotExists = strSearcher.BinarySearch();
            Assert.Equal(-1, itemNotExists);
        }

        [Fact]
        public static void MoveNextTest()
        {
            IList<int> items = new List<int> {3, 5, 2, 6, 1, 4};
            BinarySearcher<int> searcher = new BinarySearcher<int>(items, Comparer<int>.Default);
            searcher.SearchFor(1);
            IList<int> leftEnumeratedValues = new List<int> {3, 2, 1};
            int i = 0;
            while (searcher.MoveNext())
            {
                Assert.Equal(leftEnumeratedValues[i++], searcher.Current);
            }

            searcher.SearchFor(6);
            IList<int> rightEnumeratedValues = new List<int> {3, 5, 6};
            i = 0;
            while (searcher.MoveNext())
            {
                Assert.Equal(rightEnumeratedValues[i++], searcher.Current);
            }

        }

        [Fact]
        public static void NullCollectionExceptionTest()
        {
            IList<int> list = null;
            Assert.Throws<System.NullReferenceException>(() => new BinarySearcher<int>(list, Comparer<int>.Default));
        }
    }
}