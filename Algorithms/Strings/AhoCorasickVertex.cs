using System.Collections.Generic;

namespace Algorithms.Strings
{
	internal class AhoCorasickVertex
	{
		/// <summary>
		/// A flag indicating whether our vertex is the source string.
		/// </summary>
		public bool IsPattern;

		/// <summary>
		/// The number (Value) of the vertex to which we arrive by symbol (Key).
		/// </summary>
		public readonly SortedDictionary<char, int> NextVertex;

		/// <summary>
		///	Remembering the transition of the automaton.
		/// </summary>
		public readonly SortedDictionary<char, int> AutoMove;

		/// <summary>
		/// The suffix link of the vertex X is a pointer to the vertex Y,
		/// such that the string Y is the largest own suffix of the string X, or,
		/// if there is no such vertex in the tree, then the pointer to the root.
		/// In particular, a link from the root leads to it.
		/// </summary>
		public int SuffLink;

		/// <summary>
		/// "Good" suffix link.
		/// </summary>
		public int GoodSuffLink;

		/// <summary>
		/// parrent vertex in a tree.
		/// </summary>
		public readonly int Parent;

		/// <summary>
		/// Symbol on the vertex.
		/// </summary>
		public readonly char Symbol;

		/// <summary>
		/// For tests.
		/// </summary>
		public string Str;

		/// <summary>
		/// Create a vertex by initializing the variables and setting the parrent and symbol.
		/// </summary>
		/// <param name="parent">Number of the parrent</param>
		/// <param name="symbol">Symbol on the vertex in the tree.</param>
		public AhoCorasickVertex(int parent, char symbol)
		{
			IsPattern = false;
			NextVertex = new SortedDictionary<char, int>();
			AutoMove = new SortedDictionary<char, int>();

			Parent = parent;
			Symbol = symbol;

			GoodSuffLink = -1;             // initially - no suffix flink.
			SuffLink = -1;              // initially - no suffix link.
		}
	}
}
