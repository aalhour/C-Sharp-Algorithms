using System.Collections.Generic;

namespace C_Sharp_Algorithms.AlgorithmsTests.Sorting.ValuesProvider
{
    public interface ISortingValuesProvider<T>
    {
        List<List<T>> ExpectedListsSortedDescending { get; }

        List<List<T>> ExpectedListsSortedAscending { get; }

        List<List<T>> ListsToSort { get; set; }

        bool IsListToSortSortedAscending();
        bool IsListToSortSortedDescending();
    }
}