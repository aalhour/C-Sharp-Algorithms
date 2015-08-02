using System;
using System.Collections.Generic;
using DataStructures.Graphs;

namespace DataStructures.Graphs
{
	/// <summary>
	/// Representa una gráfica modelada como conjunto de sus subgráficas completas maximales
	/// </summary>
	public class CliqueGraph<T>: IGraph<T>
		where T : IComparable<T>, IEquatable<T>
	{
		public class Clique : HashSet<T>, IComparable<Clique>
		{
			public Clique() : base()
			{
			}

			public Clique(ISet<T> elementos) : base(elementos)
			{
			}

			#region IComparable implementation

			int IComparable<Clique>.CompareTo(Clique other)
			{
				throw new NotImplementedException();
			}

			#endregion

			public override string ToString()
			{
				string ret = "{";
				foreach (var x in this)
				{
					ret += x.ToString() + " ";
				}
				ret += "}";
				return ret;
			}
		}

		ICollection<T> _vertices = new HashSet<T>();

		ISet<Clique> _cliques = new HashSet<Clique>();

		public CliqueGraph()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DataStructures.Graphs.CliqueGraph`1"/> class.
		/// Copies the model from another graph.
		/// </summary>
		/// <param name="graph">Graph.</param>
		public CliqueGraph(IGraph<T> graph) : this(graph.Vertices)
		{
			foreach (var startVert in Vertices)
			{
				foreach (var endVert in graph.Neighbours(startVert))
				{
					if (!HasEdge(startVert, endVert))
					{
						// Add vortex
						Clique newClan = new Clique();
						newClan.Add(startVert);
						newClan.Add(endVert);

						ExpandToMaximal(graph, newClan);
						_cliques.Add(newClan);
					}
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DataStructures.Graphs.CliqueGraph`1"/> class.
		/// </summary>
		/// <param name="vertices">Initial vertices of the graph</param>
		public CliqueGraph(IEnumerable<T> vertices) : this()
		{
			AddVertices(vertices);
		}

		#region Interno Técnico

		/// <summary>
		/// Revisa si un conjunto de nodos es completo.
		/// </summary>
		/// <returns><c>true</c>, if completo was esed, <c>false</c> otherwise.</returns>
		/// <param name="conj">Conj.</param>
		bool EsCompleto(ISet<T> conj)
		{
			ISet<UnordererPair<T>> H = getPairs(conj);


			foreach (var clan in _cliques)
			{
				ISet<UnordererPair<T>> exc = getPairs(clan);
				H.ExceptWith(exc);
			}
			return H.Count == 0;
		}

		static bool EsCompleto(IGraph<T> graph, ISet<T> conj)
		{
			foreach (var x in conj)
			{
				foreach (var y in conj)
				{
					if (!graph.HasEdge(x, y))
						return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Expands a clique to a maximal complete
		/// </summary>
		/// <param name="clan">Clan.</param>
		void ExpandToMaximal(Clique clan)
		{
			Clique tempo; // Temporal clique for checking maximality
			// Expand NewClique to a maximal complete subgraph
			foreach (var z in Vertices)
			{
				if (!clan.Contains(z))
				{
					tempo = new Clique(clan);
					tempo.Add(z);
					if (EsCompleto(tempo))
						clan.Add(z);
				}
			}

			// Destroy every no maximal elements of 
			HashSet<Clique> clone = new HashSet<Clique>(_cliques);
			clone.Remove(clan);
			foreach (var c in clone)
			{
				if (clan.IsSupersetOf(c))
					_cliques.Remove(c);
			}

		}

		static void ExpandToMaximal(IGraph<T> graph, Clique clan)
		{
			Clique tempo; // Temporal clique for checking maximality
			// Expand NewClique to a maximal complete subgraph
			foreach (var z in graph.Vertices)
			{
				if (!clan.Contains(z))
				{
					tempo = new Clique(clan);
					tempo.Add(z);
					if (EsCompleto(graph, tempo))
						clan.Add(z);
				}
			}
		}

		class PairComparer : IEqualityComparer<UnordererPair<T>>
		{
			#region IEqualityComparer implementation

			bool IEqualityComparer<UnordererPair<T>>.Equals(UnordererPair<T> x, UnordererPair<T> y)
			{
				return ((IEquatable<UnordererPair<T>>)x).Equals(y);
			}

			int IEqualityComparer<UnordererPair<T>>.GetHashCode(UnordererPair<T> obj)
			{
				return obj.Item1.GetHashCode() + obj.Item2.GetHashCode();
			}

			#endregion
			
		}

		/// <summary>
		/// Return the subsets of cardinality 2 of a given collection
		/// </summary>
		/// <returns></returns>
		/// <param name="conj"></param>
		ISet<UnordererPair<T>> getPairs(ICollection<T> conj)
		{
			T[] arr = new T[conj.Count];
			ISet<UnordererPair<T>> ret = new System.Collections.Generic.HashSet<UnordererPair<T>>(new PairComparer());
			conj.CopyTo(arr, 0);
			for (int i = 0; i < conj.Count; i++)
			{
				for (int j = i + 1; j < conj.Count; j++)
				{
					ret.Add(new UnordererPair<T>(arr[i], arr[j]));
				}
			}
			return ret;
		}

		#endregion

		#region IGraph implementation

		public bool AddEdge(T firstVertex, T secondVertex)
		{
			if (HasEdge(firstVertex, secondVertex))
				return false;
			Clique NewClique = new Clique();  // The new clique that contains the edge (firstVertex, secondVertex)
			_cliques.Add(NewClique);

			_vertices.Add(firstVertex);
			_vertices.Add(secondVertex);

			NewClique.Add(firstVertex);
			NewClique.Add(secondVertex);

			ExpandToMaximal(NewClique);
			return true;
		}

		/// <summary>
		/// Deletes an edge, if exists, between two vertices.
		/// </summary>
		/// <returns><c>true</c>, if edge was removed, <c>false</c> otherwise.</returns>
		/// <param name="firstVertex">First vertex.</param>
		/// <param name="secondVertex">Second vertex.</param>
		public bool RemoveEdge(T firstVertex, T secondVertex)
		{
			bool ret = false;
			Clique splitting;
			Clique removing = new Clique();
			removing.Add(firstVertex);
			removing.Add(secondVertex);

			foreach (var clan in new HashSet<Clique>(_cliques))  //Iterating over a clone of cliques
			{
				if (clan.IsSupersetOf(removing))
				{
					// clan should be eliminated from cliques and replaced by maximal refinements
					_cliques.Remove(clan);

					splitting = new Clique(clan);
					splitting.Remove(firstVertex);
					_cliques.Add(splitting);
					ExpandToMaximal(splitting);

					splitting = new Clique(clan);
					splitting.Remove(secondVertex);
					_cliques.Add(splitting);
					ExpandToMaximal(splitting);

					ret = true;  // return true when finished
				}
			}
			return ret;
		}

		public void AddVertices(IEnumerable<T> collection)
		{
			foreach (var vertex in collection)
			{
				AddVertex(vertex);
			}
		}

		void IGraph<T>.AddVertices(IList<T> collection)
		{
			AddVertices(collection);
		}

		public bool AddVertex(T vertex)
		{
			bool ret = !_vertices.Contains(vertex);
			_vertices.Add(vertex);
			return ret;
		}

		public bool RemoveVertex(T vertex)
		{
			// Remove vertex from set of vertices
			if (!_vertices.Remove(vertex))
				return false;

			// Make the cliques consistent
			foreach (var clan in new HashSet<Clique> (_cliques))
			{
				if (clan.Remove(vertex))
				{
					// if clan was exhausted, remove it;
					// else make it maximal
					if (clan.Count <= 0)
					{
						_cliques.Remove(clan);
					}
					else
					{
						ExpandToMaximal(clan);
					}
				}
			}
			return true;
		}

		public bool HasEdge(T firstVertex, T secondVertex)
		{
			ISet<T> edge = new HashSet<T>();
			edge.Add(firstVertex);
			edge.Add(secondVertex);

			foreach (var clan in _cliques)
			{
				if (clan.IsSupersetOf(edge))
					return true;
			}
			return false;
		}

		public bool HasVertex(T vertex)
		{
			return _vertices.Contains(vertex);
		}

		public DataStructures.Lists.DLinkedList<T> Neighbours(T vertex)
		{
			throw new NotImplementedException();
		}

		public int Degree(T vertex)
		{
			return WeakNeighbours(vertex).Count;
		}

		public string ToReadable()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<T> DepthFirstWalk()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<T> DepthFirstWalk(T startingVertex)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<T> BreadthFirstWalk()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<T> BreadthFirstWalk(T startingVertex)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			_vertices.Clear();
			_cliques.Clear();
		}

		public bool IsDirected
		{
			get
			{
				return false;
			}
		}

		public bool IsWeighted
		{
			get
			{
				return false;
			}
		}

		public int VerticesCount
		{
			get
			{
				return _vertices.Count;
			}
		}

		public int EdgesCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Returns the list of Vertices.
		/// </summary>
		/// <value>The vertices.</value>
		IEnumerable<T> IGraph<T>.Vertices
		{
			get
			{
				return _vertices;
			}
		}

		public ICollection<T> Vertices
		{
			get
			{
				return _vertices;
			}
		}

		#endregion

		#region IGrafica implementation

		public System.Collections.Generic.ICollection<T> WeakNeighbours(T nodo)
		{
			HashSet<T> ret = new HashSet<T>();
			foreach (var c in _cliques)
			{
				if (c.Contains(nodo))
					ret.UnionWith(c);
			}
			return ret;
		}

		#endregion


		/// <summary>
		/// Revisa si un conjunto de nodos es completo en la gráfica
		/// </summary>
		/// <returns><c>true</c>, if completo was esed, <c>false</c> otherwise.</returns>
		/// <param name="nods">Nods.</param>
		public bool isComplete(IEnumerable<T> nods)
		{
			foreach (var x in _cliques)
			{
				if (x.IsSupersetOf(nods))
					return true;
			}
			return false;
		}

		/// <summary>
		/// Builds the graph of cliques of this graph
		/// </summary>
		/// <returns>The dual graph.</returns>
		public IGraph<Clique> buildDualGraph()
		{
			IGraph<Clique> dualGraph = new UndirectedDenseGraph<Clique>((uint)VerticesCount);
			foreach (var clan in _cliques)
			{
				dualGraph.AddVertex(clan);
			}
			foreach (var clan0 in _cliques)
			{
				foreach (var clan1 in _cliques)
				{
					if (!clan0.Equals(clan1) && clan0.Overlaps(clan1))
					{
						dualGraph.AddEdge(clan0, clan1);
					}
				}
			}
			return dualGraph;
		}

	}

	internal class UnordererPair<T> : Tuple<T, T>, IEquatable<UnordererPair<T>>
		where T:IEquatable<T>
	{
		public UnordererPair(T item0, T item1) : base(item0, item1)
		{
		}

		#region IEquatable implementation

		bool IEquatable<UnordererPair<T>>.Equals(UnordererPair<T> other)
		{
			return 
			(Item1.Equals(other.Item1) && Item2.Equals(other.Item2)) ||
			(Item1.Equals(other.Item2) && Item2.Equals(other.Item1));	
		
		}
		/*
		public override bool Equals(object obj)
		{
			if (obj is UnordererPair<T>)
				return ((UnordererPair<T>)obj).Equals(this);
			return base.Equals(obj);
		}
*/

		#endregion
	}

}