using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsTests.Sorting.ValuesProvider
{
    public abstract class SortingValuesProvider<T> : ISortingValuesProvider<T>
    {
        public List<List<T>> ExpectedListsSortedDescending { get; set; }
        public List<List<T>> ExpectedListsSortedAscending { get; set; }
        public List<List<T>> ListsToSort { get; set; }
        public bool IsListToSortSortedAscending()
        {
            return this.IsListToSortSorted(this.ExpectedListsSortedAscending);
        }

        public bool IsListToSortSortedDescending()
        {
            return this.IsListToSortSorted(this.ExpectedListsSortedDescending);
        }

        private bool IsListToSortSorted(IList<List<T>> expectedSortedLists)
        {
            bool findMismatch = this.ListsToSort.Count != expectedSortedLists.Count;
            int i = 0;
            while (!findMismatch && i < this.ListsToSort.Count)
            {
                findMismatch = !this.ListsToSort[i].SequenceEqual(expectedSortedLists[i]);
                i++;
            }
            return !findMismatch;
        }
    }
}
