using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsTests.Sorting
{
    public abstract class SortingValuesProvider<T> : ISortingValuesProvider<T>
    {
        public List<List<T>> ExpectedListsSortedDescending { get; set; }
        public List<List<T>> ExpectedListsSortedAscending { get; set; }
        public List<List<T>> ListsToSort { get; set; }
        public bool IsListToSortSortedAscending()
        {
            return this.CompareListList(this.ExpectedListsSortedAscending);
        }

        public bool IsListToSortSortedDescending()
        {
            return this.CompareListList(this.ExpectedListsSortedDescending);
        }

        private bool CompareListList(IList<List<T>> expectedLists)
        {
            bool findMismatch = this.ListsToSort.Count != expectedLists.Count;
            int i = 0;
            while (!findMismatch && i < this.ListsToSort.Count)
            {
                findMismatch = !this.ListsToSort[i].SequenceEqual(expectedLists[i]);
                i++;
            }
            return !findMismatch;
        }
    }
}
