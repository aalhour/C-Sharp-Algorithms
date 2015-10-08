using System.Collections.Generic;

namespace AlgorithmsTests.Sorting
{
    public interface ISortingValuesProvider<T>
    {
        List<List<T>> ExpectedListSortedDescending { get; }

        List<List<T>> ExpectedListSortedAscending { get; }

        List<List<T>> ListsToSort { get; set; }
    }
}