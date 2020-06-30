using System.Collections.Generic;

namespace Algorithms.Strings
{
	/// <summary>
	///	The substring search algorithm implements the search for multiple substrings from the dictionary in a given string.
	/// </summary>
	public class AhoCorasick
	{
		/// <summary>
		/// Tree in which each vertex denotes a row (the root denotes a zero row - $).
		/// We will store the Tree as an array of vertices, where each vertex has its own unique number, and the root has a zero value (root = 0)
		/// </summary>
		private readonly List<AhoCorasickVertex> Tree = new List<AhoCorasickVertex>();

		public AhoCorasick()
		{
			// Add root vertex.
			Tree.Add(new AhoCorasickVertex(0, '$'));
		}

		public void AddPattern(string pattern)
		{
			int num = 0;

			foreach (char ch in pattern.ToCharArray())
			{
				if (!Tree[num].NextVertex.ContainsKey(ch))          // sign of no rib.
				{
					Tree.Add(new AhoCorasickVertex(num, ch));
					Tree[num].NextVertex.Add(ch, Tree.Count - 1);
				}

				num = Tree[num].NextVertex[ch];
			}

			Tree[num].IsPattern = true;
			Tree[num].Str = pattern;
		}

		public void ClearPatterns()
		{
			Tree.Clear();
			// Add root vertex.
			Tree.Add(new AhoCorasickVertex(0, '$'));
		}

		public bool Exist(string pattern)
		{
			int num = 0;
			foreach(var ch in pattern)
			{
				if(!Tree[num].NextVertex.ContainsKey(ch))
				{
					return false;
				}
				num = Tree[num].NextVertex[ch];
			}

			return Tree[num].IsPattern;
		}

		private int GetSuffLink(int index)
		{
			AhoCorasickVertex node = Tree[index];
			if (node.SuffLink == -1)
			{
				node.SuffLink = (index == 0 || node.Parent == 0) ? 0 : GetAutoMove(GetSuffLink(node.Parent), node.Symbol);
			}

			return node.SuffLink;
		}

		/// <summary>
		/// Transition from the state of the automaton are interconnected.
		/// </summary>
		/// <param name="index">Vertex index.</param>
		/// <param name="ch">Transition symbol.</param>
		private int GetAutoMove(int index, char ch)
		{
			AhoCorasickVertex node = Tree[index];
			if (!node.AutoMove.ContainsKey(ch))
			{
				// if there is an vertex with the symbol ch from the current vertex, then we will follow it,
				// otherwise we will follow the suffix link and start recursively from the new vertex.
				int autoMove;
				if (node.NextVertex.ContainsKey(ch))
				{
					autoMove = node.NextVertex[ch];
				}
				else
				{
					autoMove = (index == 0) ? 0 : GetAutoMove(GetSuffLink(index), ch);
				}

				node.AutoMove.Add(ch, autoMove);
			}

			return node.AutoMove[ch];
		}

		private int GetGoodSuffLink(int index)
		{
			AhoCorasickVertex node = Tree[index];
			if (node.GoodSuffLink == -1)
			{
				int slink = GetSuffLink(index);

				if (slink == 0)
				{
					// Suffix link is root vertex.
					node.GoodSuffLink = 0;
				}
				else
				{
					// If flag = true for the vertex by the suffix link, then this is the desired vertex; otherwise, we start recursively from the same vertex.
					node.GoodSuffLink = Tree[slink].IsPattern ? slink : GetGoodSuffLink(slink);
				}
			}

			return node.GoodSuffLink;
		}

		/// <summary>
		/// Walking on "good" suffix links.
		/// </summary>
		/// <param name="index">Current position of the automaton.</param>
		/// <returns>For tests.</returns>
		private List<string> Check(int index)
		{
			List<string> patterns = new List<string>();
			while (index != 0)
			{
				AhoCorasickVertex node = Tree[index];
				if (node.IsPattern)
				{
					patterns.Add(node.Str);
				}

				index = GetGoodSuffLink(index);
			}

			return patterns;
		}

		/// <summary>
		/// Search for all patterns in a string.
		/// </summary>
		/// <param name="line">Line in which the search occurs.</param>
		/// <returns>For tests.</returns>
		public List<string> FindAllOccurrences(string line)
		{
			List<string> occurences = new List<string>();
			int index = 0;

			for (int i = 0; i < line.Length; i++)
			{
				index = GetAutoMove(index, line[i]);
				occurences.AddRange(Check(index));
			}

			return occurences;
		}
	}
}
