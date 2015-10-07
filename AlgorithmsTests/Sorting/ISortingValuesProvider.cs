using System.Collections.Generic;

namespace AlgorithmsTests.Sorting
{
    public interface ISortingValuesProvider
    {
        List<List<int>> ExpectedListSortedDescending { get; }

        List<List<int>> ExpectedListSortedAscending { get; }

        List<List<int>> ListsToSort { get; set; }
    }
}