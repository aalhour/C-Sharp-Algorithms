using System;

namespace Algorithms.Strings
{
    /// <summary>
    /// The Edit Distance computation algorithm.
    /// Uses a custom class for receiving the costs.
    /// </summary>
    public static class EditDistance
    {
        /// <summary>
        /// Computes the Minimum Edit Distance between two strings.
        /// </summary>
        public static Int64 GetMinDistance(string source, string destination, EditDistanceCostsMap<Int64> distances)
        {
            // Validate parameters and TCost.
            if (source == null || destination == null || distances == null)
                throw new ArgumentNullException("Some of the parameters are null.");
            if (source == destination)
                return 0;

            // Dynamic Programming 3D Table
            long[,] dynamicTable = new long[source.Length + 1, destination.Length + 1];

            // Initialize table
            for (int i = 0; i <= source.Length; ++i)
                dynamicTable[i, 0] = i;

            for (int i = 0; i <= destination.Length; ++i)
                dynamicTable[0, i] = i;

            // Compute min edit distance cost
            for (int i = 1; i <= source.Length; ++i)
            {
                for (int j = 1; j <= destination.Length; ++j)
                {
                    if (source[i - 1] == destination[j - 1])
                    {
                        dynamicTable[i, j] = dynamicTable[i - 1, j - 1];
                    }
                    else
                    {
                        long insert = dynamicTable[i, j - 1] + distances.InsertionCost;
                        long delete = dynamicTable[i - 1, j] + distances.DeletionCost;
                        long substitute = dynamicTable[i - 1, j - 1] + distances.SubstitutionCost;

                        dynamicTable[i, j] = Math.Min(insert, Math.Min(delete, substitute));
                    }
                }
            }

            // Get min edit distance cost
            return dynamicTable[source.Length, destination.Length];
        }

        /// <summary>
        /// Overloaded method for 32-bits Integer Distances
        /// </summary>
        public static Int32 GetMinDistance(string source, string destination, EditDistanceCostsMap<Int32> distances)
        {
            // Validate parameters and TCost.
            if (source == null || destination == null || distances == null)
                throw new ArgumentNullException("Some of the parameters are null.");
            var longDistance = new EditDistanceCostsMap<long>(
                insertionCost: Convert.ToInt64(distances.InsertionCost),
                deletionCost: Convert.ToInt64(distances.DeletionCost),
                substitutionCost: Convert.ToInt64(distances.InsertionCost));

            return Convert.ToInt32(EditDistance.GetMinDistance(source, destination, longDistance));
        }

        /// <summary>
        /// Overloaded method for 16-bits Integer Distances
        /// </summary>
        public static Int16 GetMinDistance(string source, string destination, EditDistanceCostsMap<Int16> distances)
        {
            // Validate parameters and TCost.
            if (source == null || destination == null || distances == null)
                throw new ArgumentNullException("Some of the parameters are null.");
            var longDistance = new EditDistanceCostsMap<long>(
                insertionCost: Convert.ToInt64(distances.InsertionCost),
                deletionCost: Convert.ToInt64(distances.DeletionCost),
                substitutionCost: Convert.ToInt64(distances.InsertionCost));

            return Convert.ToInt16(EditDistance.GetMinDistance(source, destination, longDistance));
        }
    }

}
