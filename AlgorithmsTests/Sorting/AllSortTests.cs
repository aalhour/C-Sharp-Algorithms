using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Sorting;
using AlgorithmsTests.Sorting.ValuesProvider;
using NUnit.Framework;

namespace AlgorithmsTests.Sorting
{
    [TestFixture]
    public class AllSortTests
    {
        private ISortingValuesProvider<int> _sortingIntValuesProvider;
        private ISortingValuesProvider<string> _sortingStringValuesProvider;

        [SetUp]
        public void SetUp()
        {
            this._sortingIntValuesProvider=new SortingIntValuesProvider();
            this._sortingStringValuesProvider=new SortingStringValuesProvider();
        }
        [Test]
        [Category("ManyAsserts")] //This method does many assertions and does not tell where the test fail
        public void AllSortAscendingIntTests()
        {
            List<Action> sortActions = new List<Action>
            {
                () =>_sortingIntValuesProvider.ListsToSort.ForEach(l=>l.BubbleSortAscending(Comparer<int>.Default)),
                () =>_sortingIntValuesProvider.ListsToSort.ForEach(l=>l.BucketSortAscending()),
                () =>_sortingIntValuesProvider.ListsToSort.ForEach(l=>l.CombSortAscending(Comparer<int>.Default)),
                () =>_sortingIntValuesProvider.ListsToSort.ForEach(l=>l.CountingSort()),
                () =>_sortingIntValuesProvider.ListsToSort.ForEach(l=>l.CycleSortAscending(Comparer<int>.Default)),
                () =>_sortingIntValuesProvider.ListsToSort.ForEach(l=>l.GnomeSortAscending(Comparer<int>.Default)),
                () =>_sortingIntValuesProvider.ListsToSort.ForEach(l=>l.HeapSortAscending(Comparer<int>.Default)),
                () =>_sortingIntValuesProvider.ListsToSort.ForEach(l=>l.InsertionSort(Comparer<int>.Default)),
                () =>_sortingIntValuesProvider.ListsToSort = _sortingIntValuesProvider.ListsToSort.Select(l=>l.MergeSort(Comparer<int>.Default)).ToList(), //Hack there because the merge sort does not modify the original list but return another one
                () =>_sortingIntValuesProvider.ListsToSort.ForEach(l=>l.OddEvenSortAscending(Comparer<int>.Default)),
                () =>_sortingIntValuesProvider.ListsToSort.ForEach(l=>l.PigeonHoleSortAscending()),
                () =>_sortingIntValuesProvider.ListsToSort.ForEach(l=>l.QuickSort(Comparer<int>.Default)),
                () =>_sortingIntValuesProvider.ListsToSort.ForEach(l=>l.SelectionSortAscending(Comparer<int>.Default)),
                () =>_sortingIntValuesProvider.ListsToSort.ForEach(l=>l.ShellSortAscending(Comparer<int>.Default))
            };
            foreach (var sortAction in sortActions) {
                this.SetUp();
                sortAction.Invoke();
                Assert.IsTrue(_sortingIntValuesProvider.IsListToSortSortedAscending());
            }
        }


        [Test]
        [Category("ManyAsserts")] //This method does many assertions and does not tell where the test fail
        public void AllDescendingIntTests()
        {
            List<Action> sortActions = new List<Action>
            {
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.BubbleSortDescending(Comparer<int>.Default)),
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.BucketSortDescending()),
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.CombSortDescending(Comparer<int>.Default)),
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.CycleSortDescending(Comparer<int>.Default)),
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.GnomeSortDescending(Comparer<int>.Default)),
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.HeapSortDescending(Comparer<int>.Default)),
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.OddEvenSortDescending(Comparer<int>.Default)),
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.PigeonHoleSortDescending()),
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.SelectionSortDescending(Comparer<int>.Default)),
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.ShellSortDescending(Comparer<int>.Default))
            };
            foreach (var sortAction in sortActions)
            {
                this.SetUp();
                sortAction.Invoke();
                Assert.IsTrue(_sortingIntValuesProvider.IsListToSortSortedDescending());
            }
        }

        [Test]
        [Category("ManyAsserts")] //This method does many assertions and does not tell where the test fail
        public void AllSortAscendingStringTests()
        {
            List<Action> sortActions = new List<Action>
            {
                () =>_sortingStringValuesProvider.ListsToSort.ForEach(l=>l.BubbleSortAscending(Comparer<string>.Default)),
                () =>_sortingStringValuesProvider.ListsToSort.ForEach(l=>l.CombSortAscending(Comparer<string>.Default)),
                () =>_sortingStringValuesProvider.ListsToSort.ForEach(l=>l.CycleSortAscending(Comparer<string>.Default)),
                () =>_sortingStringValuesProvider.ListsToSort.ForEach(l=>l.GnomeSortAscending(Comparer<string>.Default)),
                () =>_sortingStringValuesProvider.ListsToSort.ForEach(l=>l.HeapSortAscending(Comparer<string>.Default)),
                () =>_sortingStringValuesProvider.ListsToSort.ForEach(l=>l.InsertionSort(Comparer<string>.Default)),
                () =>_sortingStringValuesProvider.ListsToSort = _sortingStringValuesProvider.ListsToSort.Select(l=>l.MergeSort(Comparer<string>.Default)).ToList(), //Hack there because the merge sort does not modify the original list but return another one
                () =>_sortingStringValuesProvider.ListsToSort.ForEach(l=>l.OddEvenSortAscending(Comparer<string>.Default)),
                () =>_sortingStringValuesProvider.ListsToSort.ForEach(l=>l.QuickSort(Comparer<string>.Default)),
                () =>_sortingStringValuesProvider.ListsToSort.ForEach(l=>l.SelectionSortAscending(Comparer<string>.Default)),
                () =>_sortingStringValuesProvider.ListsToSort.ForEach(l=>l.ShellSortAscending(Comparer<string>.Default))
            };
            foreach (var sortAction in sortActions)
            {
                this.SetUp();
                sortAction.Invoke();
                Assert.IsTrue(_sortingStringValuesProvider.IsListToSortSortedAscending());
            }
        }


        [Test]
        [Category("ManyAsserts")] //This method does many assertions and does not tell where the test fail
        public void AllDescendingStringTests()
        {
            List<Action> sortActions = new List<Action>
            {
                () =>this._sortingStringValuesProvider.ListsToSort.ForEach(l=>l.BubbleSortDescending(Comparer<string>.Default)),
                () =>this._sortingStringValuesProvider.ListsToSort.ForEach(l=>l.CombSortDescending(Comparer<string>.Default)),
                () =>this._sortingStringValuesProvider.ListsToSort.ForEach(l=>l.CycleSortDescending(Comparer<string>.Default)),
                () =>this._sortingStringValuesProvider.ListsToSort.ForEach(l=>l.GnomeSortDescending(Comparer<string>.Default)),
                () =>this._sortingStringValuesProvider.ListsToSort.ForEach(l=>l.HeapSortDescending(Comparer<string>.Default)),
                () =>this._sortingStringValuesProvider.ListsToSort.ForEach(l=>l.OddEvenSortDescending(Comparer<string>.Default)),
                () =>this._sortingStringValuesProvider.ListsToSort.ForEach(l=>l.SelectionSortDescending(Comparer<string>.Default)),
                () =>this._sortingStringValuesProvider.ListsToSort.ForEach(l=>l.ShellSortDescending(Comparer<string>.Default))
            };
            foreach (var sortAction in sortActions)
            {
                this.SetUp();
                sortAction.Invoke();
                Assert.IsTrue(_sortingStringValuesProvider.IsListToSortSortedDescending());
            }
        }
    }
}
