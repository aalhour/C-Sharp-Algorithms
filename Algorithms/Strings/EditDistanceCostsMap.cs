using System;
using Algorithms.Common;

namespace Algorithms.Strings
{
    /// <summary>
    /// Edit Distance Costs Map.
    /// Helper class used with the EditDistance class.
    /// </summary>
    public class EditDistanceCostsMap<TCost> where TCost : IComparable<TCost>, IEquatable<TCost>
    {
        public TCost DeletionCost { get; set; }
        public TCost InsertionCost { get; set; }
        public TCost SubstitutionCost { get; set; }

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public EditDistanceCostsMap(TCost insertionCost, TCost deletionCost, TCost substitutionCost)
        {
            if (false == default(TCost).IsNumber())
                throw new InvalidOperationException("Invalid cost type TCost. Please choose TCost to be a number.");

            DeletionCost = deletionCost;
            InsertionCost = insertionCost;
            SubstitutionCost = substitutionCost;
        }
    }
}
