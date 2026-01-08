using System;
using DataStructures.Graphs;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class CliqueGraphTest
    {
        public const int VertexPerCluster = 10;
        public const int NumClusters = 10;

        [Fact]
        public static void Constructor_FromExistingGraph_CreatesCliqueGraph()
        {
            var sourceGraph = CreateClusteredGraph();

            var cliqueGraph = new CliqueGraph<ComparableTuple>(sourceGraph);

            Assert.NotNull(cliqueGraph);
            Assert.True(cliqueGraph.VerticesCount > 0);
        }

        [Fact]
        public static void RemoveEdge_RemovesEdgeFromGraph()
        {
            var sourceGraph = CreateClusteredGraph();
            var cliqueGraph = new CliqueGraph<ComparableTuple>(sourceGraph);

            var v1 = new ComparableTuple(0, 0);
            var v2 = new ComparableTuple(1, 0);
            var hadEdgeBefore = cliqueGraph.HasEdge(v1, v2);

            cliqueGraph.RemoveEdge(v1, v2);
            var hasEdgeAfter = cliqueGraph.HasEdge(v1, v2);

            Assert.True(hadEdgeBefore);
            Assert.False(hasEdgeAfter);
        }

        [Fact]
        public static void BuildDualGraph_CreatesDualGraph()
        {
            var sourceGraph = CreateClusteredGraph();
            var cliqueGraph = new CliqueGraph<ComparableTuple>(sourceGraph);

            var dualGraph = cliqueGraph.buildDualGraph();

            Assert.NotNull(dualGraph);
            Assert.True(dualGraph.VerticesCount > 0);
        }

        [Fact]
        public static void Edges_ReturnsAllEdges()
        {
            var sourceGraph = CreateClusteredGraph();
            var cliqueGraph = new CliqueGraph<ComparableTuple>(sourceGraph);

            var edgeCount = 0;
            foreach (var edge in cliqueGraph.Edges)
            {
                edgeCount++;
            }

            Assert.True(edgeCount > 0);
        }

        [Fact]
        public static void OutgoingEdges_ReturnsEdgesFromVertex()
        {
            var sourceGraph = CreateClusteredGraph();
            var cliqueGraph = new CliqueGraph<ComparableTuple>(sourceGraph);
            var vertex = new ComparableTuple(0, 0);

            var outgoingCount = 0;
            foreach (var edge in cliqueGraph.OutgoingEdges(vertex))
            {
                Assert.Equal(vertex.Item1, edge.Source.Item1);
                Assert.Equal(vertex.Item2, edge.Source.Item2);
                outgoingCount++;
            }

            Assert.True(outgoingCount > 0);
        }

        private static IGraph<ComparableTuple> CreateClusteredGraph()
        {
            var graph = new UndirectedDenseGraph<ComparableTuple>(NumClusters * VertexPerCluster);

            // Add vertices
            for (int i = 0; i < NumClusters; i++)
            {
                for (int j = 0; j < VertexPerCluster; j++)
                {
                    graph.AddVertex(new ComparableTuple(i, j));
                }
            }

            // Create clusters (complete subgraphs)
            for (int i = 0; i < NumClusters; i++)
            {
                for (int j = 0; j < VertexPerCluster; j++)
                {
                    for (int k = j; k < VertexPerCluster; k++)
                    {
                        graph.AddEdge(new ComparableTuple(i, j), new ComparableTuple(i, k));
                    }
                }
            }

            // Connect clusters through first vertex of each
            for (int i = 0; i < NumClusters; i++)
            {
                for (int j = 0; j < NumClusters; j++)
                {
                    graph.AddEdge(new ComparableTuple(i, 0), new ComparableTuple(j, 0));
                }
            }

            return graph;
        }
    }

    class ComparableTuple : Tuple<int, int>, IComparable<ComparableTuple>, IEquatable<ComparableTuple>
    {
        private static readonly int Multiplier = CliqueGraphTest.NumClusters;

        public ComparableTuple(int item1, int item2) : base(item1, item2) { }

        private int ToInt => Item1 * Multiplier + Item2;

        public int CompareTo(ComparableTuple other)
        {
            return ToInt.CompareTo(other?.ToInt ?? 0);
        }

        public bool Equals(ComparableTuple other)
        {
            return other != null && ToInt == other.ToInt;
        }
    }
}
