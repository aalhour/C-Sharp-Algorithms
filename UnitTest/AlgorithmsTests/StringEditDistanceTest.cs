using System;
using System.Diagnostics;

using Algorithms.Strings;

namespace UnitTest.AlgorithmsTests
{
    public static class StringEditDistanceTest
    {
        public static void DoTest()
        {
            short minDistance = 0;

            // Initialize the costs map
            var costs = new EditDistanceCostsMap<short>(insertionCost: 1, substitutionCost: 1, deletionCost: 1);

            string one = "";
            string two = "";
            minDistance = EditDistance.GetMinDistance(one, two, costs);

            // Assert min cost is zero
            Debug.Assert(minDistance == 0);

            one = "I am Ahmad Alhour";
            two = "Ahmad Alhour";
            minDistance = EditDistance.GetMinDistance(one, two, costs);

            // Assert min cost is 5
            Debug.Assert(minDistance == 5);

            one = "I am Ahmad Alhour";
            two = "";
            minDistance = EditDistance.GetMinDistance(one, two, costs);

            // Assert min cost is the length of string one (17 characters)
            Debug.Assert(minDistance == one.Length);

            one = "Thou shalt not kill";
            two = "You should not murder";
            minDistance = EditDistance.GetMinDistance(one, two, costs);

            // Assert min cost is 11
            Debug.Assert(minDistance == 11);

        }

    }

}
