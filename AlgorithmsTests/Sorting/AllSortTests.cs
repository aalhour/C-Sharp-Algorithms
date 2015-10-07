using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Sorting;
using NUnit.Framework;

namespace AlgorithmsTests.Sorting
{
    [TestFixture]
    public class AllSortTests
    {
        private ISortingValuesProvider _sortingIntValuesProvider;

        [SetUp]
        public void SetUp()
        {
            this._sortingIntValuesProvider=new SortingIntValuesProvider();
        }

        [Test]
        [Category("ManyAsserts")] //This method does many assertions and does not tell where the test fail
        public void AllSortAscendingTests()
        {
            List<Action> sortActions = new List<Action>
            {
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.BubbleSortAscending(Comparer<int>.Default)),
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.BucketSortAscending()),
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.CombSortAscending(Comparer<int>.Default)),
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.CountingSort()),
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.CycleSortAscending(Comparer<int>.Default)),
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.GnomeSortAscending(Comparer<int>.Default)),
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.HeapSortAscending(Comparer<int>.Default)),
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.InsertionSort(Comparer<int>.Default)),
                () =>this._sortingIntValuesProvider.ListsToSort = this._sortingIntValuesProvider.ListsToSort.Select(l=>l.MergeSort(Comparer<int>.Default)).ToList(), //Hack there because the merge sort does not modify the original list but return another one
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.OddEvenSortAscending(Comparer<int>.Default)),
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.PigeonHoleSortAscending()),
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.QuickSort(Comparer<int>.Default)),
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.SelectionSortAscending(Comparer<int>.Default)),
                () =>this._sortingIntValuesProvider.ListsToSort.ForEach(l=>l.ShellSortAscending(Comparer<int>.Default))
            };
            foreach (var sortAction in sortActions) {
                this.SetUp();
                sortAction.Invoke();
                CollectionAssert.AreEqual(this._sortingIntValuesProvider.ExpectedListSortedAscending, this._sortingIntValuesProvider.ListsToSort,sortAction.ToString());
            }
        }


        [Test]
        [Category("ManyAsserts")] //This method does many assertions and does not tell where the test fail
        public void AllDescendingTests()
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
                CollectionAssert.AreEqual(this._sortingIntValuesProvider.ExpectedListSortedDescending, this._sortingIntValuesProvider.ListsToSort, sortAction.ToString());
            }
        }
    }
}
