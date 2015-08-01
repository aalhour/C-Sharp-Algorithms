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


		class Clique : HashSet<T>
		{
			public Clique() : base()
			{
			}

			public Clique(ISet<T> elementos) : base(elementos)
			{
			}
		}

		ICollection<T> _nodos = new HashSet<T>();

		ISet<Clique> cliques = new HashSet<Clique>();

		public CliqueGraph()
		{
		}

		#region Interno Técnico

		/// <summary>
		/// Revisa si un conjunto de nodos es completo.
		/// </summary>
		/// <returns><c>true</c>, if completo was esed, <c>false</c> otherwise.</returns>
		/// <param name="conj">Conj.</param>
		bool EsCompleto(ISet<T> conj)
		{
			ISet<UnordererPair<T>> H = getPares(conj);


			foreach (var clan in cliques)
			{
				ISet<UnordererPair<T>> exc = getPares(clan);
				H.ExceptWith(exc);
			}
			return H.Count == 0;
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
			HashSet<Clique> clone = new HashSet<Clique>(cliques);
			clone.Remove(clan);
			foreach (var c in clone)
			{
				if (clan.IsSupersetOf(c))
					cliques.Remove(c);
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
		ISet<UnordererPair<T>> getPares(ICollection<T> conj)
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
			cliques.Add(NewClique);

			_nodos.Add(firstVertex);
			_nodos.Add(secondVertex);

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

			foreach (var clan in new HashSet<Clique>(cliques))  //Iterating over a clone of cliques
			{
				if (clan.IsSupersetOf(removing))
				{
					// clan should be eliminated from cliques and replaced by maximal refinements
					cliques.Remove(clan);

					splitting = new Clique(clan);
					splitting.Remove(firstVertex);
					cliques.Add(splitting);
					ExpandToMaximal(splitting);

					splitting = new Clique(clan);
					splitting.Remove(secondVertex);
					cliques.Add(splitting);
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
			bool ret = !_nodos.Contains(vertex);
			_nodos.Add(vertex);
			return ret;
		}

		public bool RemoveVertex(T vertex)
		{
			// Remove vertex from set of vertices
			if (!_nodos.Remove(vertex))
				return false;

			// Make the cliques consistent
			foreach (var clan in new HashSet<Clique> (cliques))
			{
				if (clan.Remove(vertex))
				{
					// if clan was exhausted, remove it;
					// else make it maximal
					if (clan.Count <= 0)
					{
						cliques.Remove(clan);
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

			foreach (var clan in cliques)
			{
				if (clan.IsSupersetOf(edge))
					return true;
			}
			return false;
		}

		public bool HasVertex(T vertex)
		{
			return _nodos.Contains(vertex);
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
			_nodos.Clear();
			cliques.Clear();
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
				return _nodos.Count;
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
				return _nodos;
			}
		}

		public ICollection<T> Vertices
		{
			get
			{
				return _nodos;
			}
		}

		#endregion

		#region IGrafica implementation

		public System.Collections.Generic.ICollection<T> WeakNeighbours(T nodo)
		{
			HashSet<T> ret = new HashSet<T>();
			foreach (var c in cliques)
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
		public bool esCompleto(IEnumerable<T> nods)
		{
			foreach (var x in cliques)
			{
				if (x.IsSupersetOf(nods))
					return true;
			}
			return false;
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