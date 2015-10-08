using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsTests.Sorting.ValuesProvider
{
    public class SortingStringValuesProvider : SortingValuesProvider<string>
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="T:System.Object"/>.
        /// </summary>
        public SortingStringValuesProvider()
        {
            this.ListsToSort = new List<List<string>>
            {
                new List<string>{"a","bc"},
                new List<string>{"z","a"},
                new List<string>{"zz","ab","e"},
                new List<string>{"ad","er","ft","gd","tu","uv","uz","ya","yc","zzzz"},
                new List<string>{"zzzz","yc","ya","uz","uv","tu","gd","ft","er","ad"},
                new List<string>{"qsdf","qqd","rt","fdf","abt","qsdqsd","fko","aqf","zuiver","unit","test","qsdqsd","","555"},
                new List<string>{"ret","yaop","rbuiop","testing","algorithm","sorting","sort me","zergik","zf","daran:!edl","hey","hola","something","anotherone","unit","unit","test","algorithm"}
            };
            base.ExpectedListsSortedAscending = this.ListsToSort.Select(list => list.OrderBy(i => i).ToList()).ToList();
            base.ExpectedListsSortedDescending = this.ListsToSort.Select(list => list.OrderByDescending(i => i).ToList()).ToList();
        }
    }
}
