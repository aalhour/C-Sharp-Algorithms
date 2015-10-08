using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsTests.Sorting
{
    public abstract class SortingValuesProvider<T> : ISortingValuesProvider<T>
    {
        public List<List<T>> ExpectedListSortedDescending { get; set; }
        public List<List<T>> ExpectedListSortedAscending { get; set; }
        public List<List<T>> ListsToSort { get; set; }
        public bool IsListToSortSortedAscending()
        {
            return ListsToSort.SequenceEqual(ExpectedListSortedAscending);
        }

        public bool IsListToSortSortedDescending()
        {
            return ListsToSort.SequenceEqual(ExpectedListSortedDescending);
        }
    }
}
