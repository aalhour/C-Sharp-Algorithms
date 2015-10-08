using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsTests.Sorting
{
    public class SortingIntValuesProvider : SortingValuesProvider<int>
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="T:System.Object"/>.
        /// </summary>
        public SortingIntValuesProvider()
        {
            this.ListsToSort= new List<List<int>>
            {
                new List<int>{0,2},
                new List<int>{2,1},
                new List<int>{2,1,3},
                new List<int>{0,1,2,3,4,5,6,7,8,9},
                new List<int>{9,8,7,6,5,4,3,2,1},
                new List<int>{0,5,9,4,3,8,56,91,56,87,5,25,46,31},
                new List<int>{100,59,91,4,33,9,516,1,516,87,205,25,6,31,45,20,197,203,9}
            };
            base.ExpectedListSortedAscending = this.ListsToSort.Select(list => list.OrderBy(i => i).ToList()).ToList();
            base.ExpectedListSortedDescending = this.ListsToSort.Select(list => list.OrderByDescending(i => i).ToList()).ToList();
        }
    }
}
