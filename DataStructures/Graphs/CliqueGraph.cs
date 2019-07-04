using System;
using System.Collections.Generic;
using DataStructures.Lists;

namespace DataStructures.Graphs
{
    /// <summary>
    /// Represents an unweighted undirected graph, modeling with a set of its maximal complete subgraphs of it.
    /// Should be fast in clustered graphs
    /// </summary>
    public class CliqueGraph<T> : IGraph<T> where T : IComparable<T>, IEquatable<T>
    {
        public class Clique : HashSet<T>, IComparable<Clique>
        {
            public Clique()
                : base()
            {
            }

            public Clique(ISet<T> elements)
                : base(elements)
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

        #region Model

        /// <summary>
        /// Vertices of the graph.
        /// </summary>
        ICollection<T> _vertices = new HashSet<T>();

        /// <summary>
        /// A set of cliques minimal with the hability of charaterize the graph.
        /// </summary>
        ISet<Clique> _cliques = new HashSet<Clique>();

        #endregion

        #region Constructors

        public CliqueGraph()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataStructures.Graphs.CliqueGraph`1"/> class.
        /// Copies the model from another graph.
        /// </summary>
        /// <param name="graph">Graph.</param>
        public CliqueGraph(IGraph<T> graph)
            : this(graph.Vertices)
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
        public CliqueGraph(IEnumerable<T> vertices)
            : this()
        {
            if (vertices == null)
            {
                System.Diagnostics.Debug.WriteLine("Cannot initialize an instance of a CliqueGraph with NULL vertices;\ninvoking default constructor.");
            }
            else
            {
                AddVertices(vertices);
            }
        }

        #endregion

        #region Internal

        /// <summary>
        /// Determines if a set of vertices is complete as a subgraph of this
        /// </summary>
        /// <returns><c>true</c>, if the set is a complete subgraph, <c>false</c> otherwise.</returns>
        /// <param name="vertices">A set of vertices of this graph.</param>
        public bool IsComplete(ISet<T> vertices)
        {
            if (!vertices.IsSubsetOf(_vertices))
                throw new Exception("The param in CliqueGraph.IsComplete should be a subset of Vertices");

            /*
	 * vertices is complete iff [vertices]² \subseteq \bigcup_{c \in cliques} [c]²
	 * where [x]² is the set of all subsets of x of cardinality 2.
	*/

            ISet<UnordererPair<T>> H = getPairs(vertices);

            foreach (var clan in _cliques)
            {
                ISet<UnordererPair<T>> exc = getPairs(clan);
                H.ExceptWith(exc);
            }
            return H.Count == 0;
        }

        /// <summary>
        /// Determines if a set of vertices is complete as a subgraph of another graph
        /// </summary>
        /// <returns><c>true</c>, if the set is a complete subgraph, <c>false</c> otherwise.</returns>
        /// <param name="graph">A graph to determine 'completness'</param>
        /// <param name="vertices">A set of vertices of graph.</param>
        static bool IsComplete(IGraph<T> graph, ISet<T> vertices)
        {
            foreach (var x in vertices)
            {
                foreach (var y in vertices)
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
        /// <param name="clan">Clique to expand</param>
        void ExpandToMaximal(Clique clan)
        {
            Clique maximalityChecker; // Temporal clique for checking maximality

            // Expand NewClique to a maximal complete subgraph
            foreach (var z in Vertices)
            {
                if (!clan.Contains(z))
                {
                    maximalityChecker = new Clique(clan);
                    maximalityChecker.Add(z);
                    if (IsComplete(maximalityChecker))
                        clan.Add(z);
                }
            }

            // Destroy every no maximal elements of the graph
            HashSet<Clique> clone = new HashSet<Clique>(_cliques);
            clone.Remove(clan);
            foreach (var c in clone) // Iterate over a clone of _cliques
            {
                if (clan.IsSupersetOf(c))
                    _cliques.Remove(c);
            }

        }

        /// <summary>
        /// Expands a clique to a maximal complete in a given graph
        /// </summary>
        /// <param name="graph">Graph to use to determine maximality.</param>
        /// <param name="clan">Clique to expand.</param>
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
                    if (IsComplete(graph, tempo))
                        clan.Add(z);
                }
            }
        }

        /// <summary>
        /// Some (temporary) class to compare unorderer pairs.
        /// </summary>
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
        /// Return the subsets of cardinality 2 of a given collection. ie [vertices]².
        /// </summary>
        /// <returns>Returns an ISet whose elements are every subset of a given set of cardinality 2.</returns>
        /// <param name="vertices">Collection whose pairs are going to be returned.</param>
        ISet<UnordererPair<T>> getPairs(ICollection<T> vertices)
        {
            T[] arr = new T[vertices.Count];
            ISet<UnordererPair<T>> ret = new System.Collections.Generic.HashSet<UnordererPair<T>>(new PairComparer());
            vertices.CopyTo(arr, 0);
            for (int i = 0; i < vertices.Count; i++)
            {
                for (int j = i + 1; j < vertices.Count; j++)
                {
                    ret.Add(new UnordererPair<T>(arr[i], arr[j]));
                }
            }
            return ret;
        }

        #endregion


        #region IGraph implementation

        /// <summary>
        /// An enumerable collection of all graph edges.
        /// </summary>
        public IEnumerable<IEdge<T>> Edges
        {
            get
            {
                List<UnweightedEdge<T>> returnEdges = new List<UnweightedEdge<T>>();

                foreach (var edge in getEdges())
                {
                    returnEdges.Add(new UnweightedEdge<T>(edge.Item1, edge.Item2));
                    returnEdges.Add(new UnweightedEdge<T>(edge.Item2, edge.Item1));
                }
                return returnEdges;
            }
        }

        /// <summary>
        /// Get all incoming edges to vertex.
        /// </summary>
        public IEnumerable<IEdge<T>> IncomingEdges(T vertex)
        {
            List<UnweightedEdge<T>> incomingEdges = new List<UnweightedEdge<T>>();

            foreach (var c in _cliques)
            {
                if (c.Contains(vertex))
                {
                    foreach (var item in c)
                    {
                        if (!incomingEdges.Exists(x => x.Source.Equals(item)))
                            incomingEdges.Add(new UnweightedEdge<T>(item, vertex));
                    }
                }
            }

            return incomingEdges;

        }

        /// <summary>
        /// Get all outgoing edges from a vertex.
        /// </summary>
        public IEnumerable<IEdge<T>> OutgoingEdges(T vertex)
        {
            List<UnweightedEdge<T>> outgoingEdges = new List<UnweightedEdge<T>>();

            foreach (var c in _cliques)
            {
                if (c.Contains(vertex))
                {
                    foreach (var item in c)
                    {
                        if (!outgoingEdges.Exists(x => x.Destination.Equals(item)))
                            outgoingEdges.Add(new UnweightedEdge<T>(vertex, item));
                    }
                }
            }

            return outgoingEdges;
        }

        /// <summary>
        /// Connects two vertices together.
        /// </summary>
        /// <returns><c>true</c>, if edge was added, <c>false</c> otherwise.</returns>
        /// <param name="firstVertex">First vertex.</param>
        /// <param name="secondVertex">Second vertex.</param>
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

        /// <summary>
        /// Adds a list of vertices to the graph.
        /// </summary>
        /// <param name="collection">Collection.</param>
        public void AddVertices(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentException();

            foreach (var vertex in collection)
            {
                AddVertex(vertex);
            }
        }

        /// <summary>
        /// Adds a list of vertices to the graph.
        /// </summary>
        /// <param name="collection">Collection.</param>
        void IGraph<T>.AddVertices(IList<T> collection)
        {
            AddVertices(collection);
        }

        /// <summary>
        /// Adds a new vertex to graph.
        /// </summary>
        /// <returns><c>true</c>, if vertex was added, <c>false</c> otherwise.</returns>
        /// <param name="vertex">Vertex.</param>
        public bool AddVertex(T vertex)
        {
            bool ret = !_vertices.Contains(vertex);
            _vertices.Add(vertex);
            return ret;
        }

        /// <summary>
        /// Removes the specified vertex from graph.
        /// </summary>
        /// <returns><c>true</c>, if vertex was removed, <c>false</c> otherwise.</returns>
        /// <param name="vertex">Vertex.</param>
        public bool RemoveVertex(T vertex)
        {
            // Remove vertex from set of vertices, return false if nothing was removed.
            if (!_vertices.Remove(vertex))
                return false;

            // Make the cliques consistent
            foreach (var clan in new HashSet<Clique>(_cliques)) // clone _cliques and iterate
            {
                if (clan.Remove(vertex))
                {
                    // if clan was exhausted, remove it;
                    if (clan.Count <= 1)
                    {
                        _cliques.Remove(clan);
                    }
                    else // else make it maximal
                    {
                        ExpandToMaximal(clan);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Determines whether this instance has edge the specified firstVertex secondVertex.
        /// </summary>
        /// <returns><c>true</c> if this instance has edge the specified firstVertex secondVertex; otherwise, <c>false</c>.</returns>
        /// <param name="firstVertex">First vertex.</param>
        /// <param name="secondVertex">Second vertex.</param>
        public bool HasEdge(T firstVertex, T secondVertex)
        {
            ISet<T> edge = new HashSet<T>();
            edge.Add(firstVertex);
            edge.Add(secondVertex);

            // If [edge]² (= edge) is contained in some clan, there is an edge.
            foreach (var clan in _cliques)
            {
                if (clan.IsSupersetOf(edge))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether this graph has the specified vertex.
        /// </summary>
        /// <returns><c>true</c> if this instance has vertex the specified vertex; otherwise, <c>false</c>.</returns>
        /// <param name="vertex">Vertex.</param>
        public bool HasVertex(T vertex)
        {
            return _vertices.Contains(vertex);
        }

        /// <summary>
        /// Returns the neighbours doubly-linked list for the specified vertex.
        /// </summary>
        /// <param name="vertex">Vertex.</param>
        public DataStructures.Lists.DLinkedList<T> Neighbours(T vertex)
        {
            DataStructures.Lists.DLinkedList<T> returnList = new DataStructures.Lists.DLinkedList<T>();

            foreach (var c in _cliques)
            {
                if (c.Contains(vertex))
                {
                    foreach (var item in c)
                    {
                        if (!returnList.Contains(item))
                            returnList.Append(item);
                    }
                }
            }

            return returnList;
        }

        public int Degree(T vertex)
        {
            return Neighbours(vertex).Count;
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

        /// <summary>
        /// Clear this graph.
        /// </summary>
        public void Clear()
        {
            _vertices.Clear();
            _cliques.Clear();
        }

        /// <summary>
        /// Returns true, if graph is directed; false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is directed; otherwise, <c>false</c>.</value>
        public bool IsDirected
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true, if graph is weighted; false otherwise.
        /// </summary>
        /// <value><c>true</c> if this instance is weighted; otherwise, <c>false</c>.</value>
        public bool IsWeighted
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the count of vetices.
        /// </summary>
        /// <value>The vertices count.</value>
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
                return getEdges().Count;
            }
        }

        /// <summary>
        /// Returns the list of edges.
        /// </summary>
        /// <returns></returns>
        ICollection<UnordererPair<T>> getEdges()
        {
            ISet<UnordererPair<T>> H = new HashSet<UnordererPair<T>>();

            foreach (var clan in _cliques)
            {
                ISet<UnordererPair<T>> union = getPairs(clan);
                H.UnionWith(union);
            }
            return H;
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

        /// <summary>
        /// Returns the list of Vertices.
        /// </summary>
        /// <value>The vertices.</value>
        public ICollection<T> Vertices
        {
            get
            {
                return _vertices;
            }
        }

        /// <summary>
        /// Gets the cloud of a collection of vetices.
        /// A cloud of a collection is the union if the neighborhoods of its elements
        /// </summary>
        /// <returns>The cloud.</returns>
        /// <param name="collection">Collection.</param>
        public ISet<T> GetCloud(ISet<T> collection)
        {
            _getCloud(collection, new HashSet<Clique>(_cliques));
            return collection;

        }

        /// <summary>
        /// Gets the cloud of a collection of vetices.
        /// A cloud of a collection is the union if the neighborhoods of its elements
        /// </summary>
        /// <returns>The cloud.</returns>
        /// <param name="collection">Collection.</param>
        /// <param name="useCliques">A set of cliques to use</param>
        private void _getCloud(ISet<T> cloud, ICollection<Clique> useCliques)
        {

            foreach (var clan in new HashSet<Clique>(useCliques))
            {
                if (cloud.Overlaps(clan))
                {
                    cloud.UnionWith(clan);
                    useCliques.Remove(clan);
                }
            }

        }

        /// <summary>
        /// Returns the conext component of a collection
        /// </summary>
        /// <returns>The component.</returns>
        /// <param name="collection">Collection.</param>
        private void _getComponentCollection(ISet<T> collection)
        {
            int count = 0;
            ICollection<Clique> UnusedCliques = new HashSet<Clique>(_cliques);
            while (count < collection.Count)
            {
                count = collection.Count;
                _getCloud(collection, UnusedCliques);
            }
        }

        /// <summary>
        /// Returns the only connected component containing a given vertex.
        /// </summary>
        /// <returns>A collection containing the vertex of a connected component</returns>
        /// <param name="vertex">Vertex.</param>
        public ICollection<T> GetConnectedComponent(T vertex)
        {
            if (!_vertices.Contains(vertex))
                throw new Exception("vertex should be a vertex of this graph.");
            HashSet<T> component = new HashSet<T>();
            component.Add(vertex);
            _getComponentCollection(component);
            return component;
        }

        #endregion

        #region Clique invariants

        /// <summary>
        /// Returns the list of maximal cliques
        /// </summary>
        /// <value>The get cliques.</value>
        public IReadOnlyCollection<Clique> getCliques
        {
            get
            {
                // TODO: getCliques, this does not return all the maximal cliques; 
                // only return enough of them.
                return (IReadOnlyCollection<Clique>)_cliques;
            }
        }

        /// <summary>
        /// Returns the clique number of the current graph.
        /// </summary>
        /// <value>The clique number.</value>
        public int cliqueNumber
        {
            get
            {
                return Pick<Clique>(getMaximumCliques).Count;
            }
        }

        /// <summary>
        /// Returns the collection of the maxium-sized cliques
        /// </summary>
        /// <value>The get maximum cliques.</value>
        public IEnumerable<Clique> getMaximumCliques
        {
            get
            {
                int maxSize = 0;
                ICollection<Clique> maxCliques = new HashSet<Clique>();

                foreach (var clan in getCliques)
                {
                    if (clan.Count > maxSize)
                    {
                        maxCliques.Clear();
                        maxSize = clan.Count;
                    }

                    if (clan.Count == maxSize)
                    {
                        maxCliques.Add(clan);
                    }
                }
                return maxCliques;
            }
        }

        #endregion

        #region Clique methods

        /// <summary>
        /// Determines if a set of vertices is complete as a subgraph of another graph
        /// </summary>
        /// <returns><c>true</c>, if the set is a complete subgraph, <c>false</c> otherwise.</returns>
        /// <param name="certices">A set of vertices of graph.</param>
        public bool isComplete(IEnumerable<T> vertices)
        {
            if (vertices == null)
                throw new ArgumentException();

            foreach (var x in _cliques)
            {
                if (x.IsSupersetOf(vertices))
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
                    if (!clan0.Equals(clan1) && clan0.Overlaps(clan1)) // Equals = SetEquals here since cliques are maximal.
                    {
                        dualGraph.AddEdge(clan0, clan1);
                    }
                }
            }
            return dualGraph;
        }

        /// <summary>
        /// Given a path in a dual graph, it return a corresponding path in this graph
        /// </summary>
        /// <returns>An equivalent path of the clique path.</returns>
        /// <param name="path">Path.</param>
        public IEnumerable<T> ReturnPathFromCliquePath(IEnumerable<Clique> path)
        {
            ArrayList<T> returnPath = new ArrayList<T>();
            IList<Clique> listPath = new List<Clique>(path);
            ISet<T> intersection;

            // Pick any element of each intersection
            for (int i = 0; i < listPath.Count - 1; i++)
            {
                intersection = new HashSet<T>(listPath[i]);
                intersection.IntersectWith(listPath[i + 1]); // intersection is never empty because 'path' should be a path in a dual graph.

                returnPath.Add(CliqueGraph<T>.Pick(intersection));
            }

            return returnPath;
        }

        #endregion

        /// <summary>
        /// Picks any object in a ISet
        /// </summary>
        /// <param name="Set">Set.</param>
        /// <typeparam name="V">The 1st type parameter.</typeparam>
        static V Pick<V>(IEnumerable<V> Set)
        {
            IEnumerator<V> enumerator = ((IEnumerable<V>)Set).GetEnumerator();
            V ret = enumerator.Current;
            enumerator.Dispose();
            return ret;
        }

    }

    internal class UnordererPair<T> : Tuple<T, T>, IEquatable<UnordererPair<T>> where T : IEquatable<T>
    {
        public UnordererPair(T item0, T item1)
            : base(item0, item1)
        {
        }

        #region IEquatable implementation

        bool IEquatable<UnordererPair<T>>.Equals(UnordererPair<T> other)
        {
            return
                (Item1.Equals(other.Item1) && Item2.Equals(other.Item2)) ||
            (Item1.Equals(other.Item2) && Item2.Equals(other.Item1));

        }

        #endregion
    }

}
