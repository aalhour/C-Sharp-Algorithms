using System;

namespace DataStructures
{
	/// <summary>
	/// This interface should be implemented alongside the IGraph interface.
	/// </summary>
	public interface IWeightedGraph<T> where T : IComparable<T>
	{
		/// <summary>
		/// Connects two vertices together with a weight, in the direction: first->second.
		/// </summary>
		bool AddEdge(T source, T destination, int weight);

		/// <summary>
		/// Updates the edge weight from source to destination.
		/// </summary>
		bool UpdateEdgeWeight(T source, T destination, int weight);

		/// <summary>
		/// Returns the edge weight from source to destination.
		/// </summary>
		int GetEdgeWeight(T source, T destination);

		/// <summary>
		/// Returns the neighbours of a vertex as a dictionary of nodes-to-weights.
		/// </summary>
		System.Collections.Generic.Dictionary<T, int> NeighboursMap(T vertex);
	}
}

