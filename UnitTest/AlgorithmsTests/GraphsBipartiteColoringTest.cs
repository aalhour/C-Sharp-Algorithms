using System;
using Algorithms.Graphs;
using DataStructures.Graphs;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public static class GraphsBipartiteColoringTest
    {
        /// <summary>
        /// Note: The BipartiteColoring constructor throws InvalidOperationException
        /// when the graph contains an odd cycle. This is by design - you must catch
        /// the exception to determine if the graph is not bipartite.
        /// </summary>
        
        [Fact]
        public static void Constructor_OddCycleTriangle_ThrowsInvalidOperationException()
        {
            //
            // Triangle graph (odd cycle):
            //     a --- b
            //      \   /
            //        c
            //
            IGraph<string> graph = new UndirectedSparseGraph<string>();
            graph.AddVertices(new[] { "a", "b", "c" });
            graph.AddEdge("a", "b");
            graph.AddEdge("b", "c");
            graph.AddEdge("c", "a");

            Assert.Throws<InvalidOperationException>(() => 
                new BipartiteColoring<UndirectedSparseGraph<string>, string>(graph));
        }

        [Fact]
        public static void Constructor_ComplexGraphWithOddCycle_ThrowsInvalidOperationException()
        {
            //
            // Graph with multiple components including odd cycle
            //
            IGraph<string> graph = new UndirectedSparseGraph<string>();
            graph.AddVertices(new[] { "a", "b", "c", "d", "e", "f", "s", "v", "x", "y", "z" });

            // Component with edges
            graph.AddEdge("a", "s");
            graph.AddEdge("a", "d");
            graph.AddEdge("s", "x");
            graph.AddEdge("s", "a");
            graph.AddEdge("x", "d");

            // Component with odd cycle: b-c-v forms a path, c-f-b closes it as triangle
            graph.AddEdge("b", "c");
            graph.AddEdge("b", "v");
            graph.AddEdge("c", "f");
            graph.AddEdge("c", "v"); // creates odd cycle
            graph.AddEdge("f", "b");

            graph.AddEdge("y", "z");

            Assert.Throws<InvalidOperationException>(() => 
                new BipartiteColoring<UndirectedSparseGraph<string>, string>(graph));
        }

        [Fact]
        public static void IsBipartite_GraphWithoutOddCycle_ReturnsTrue()
        {
            IGraph<string> graph = new UndirectedSparseGraph<string>();
            graph.AddVertices(new[] { "a", "b", "c", "d", "e", "f", "s", "v", "x", "y", "z" });

            graph.AddEdge("a", "s");
            graph.AddEdge("a", "d");
            graph.AddEdge("s", "x");
            graph.AddEdge("s", "a");
            graph.AddEdge("x", "d");

            graph.AddEdge("b", "c");
            graph.AddEdge("b", "v");
            graph.AddEdge("c", "f");
            // Note: removed edges that created odd cycle

            graph.AddEdge("y", "z");

            var bipartite = new BipartiteColoring<UndirectedSparseGraph<string>, string>(graph);

            Assert.True(bipartite.IsBipartite());
        }

        [Fact]
        public static void ColorOf_BipartiteGraph_ReturnsCorrectColors()
        {
            // Simple bipartite graph - path graph is always bipartite
            //     a --- s --- z
            //
            IGraph<string> graph = new UndirectedSparseGraph<string>();
            graph.AddVertices(new[] { "a", "s", "z" });

            graph.AddEdge("a", "s");
            graph.AddEdge("s", "z");

            var bipartite = new BipartiteColoring<UndirectedSparseGraph<string>, string>(graph);

            Assert.True(bipartite.IsBipartite());

            // Verify coloring - adjacent vertices must have different colors
            var colorA = bipartite.ColorOf("a");
            var colorS = bipartite.ColorOf("s");
            var colorZ = bipartite.ColorOf("z");

            // Adjacent vertices must have different colors
            Assert.NotEqual(colorA, colorS);
            Assert.NotEqual(colorS, colorZ);
            // a and z can have the same color (they're not adjacent)
        }

        [Fact]
        public static void IsBipartite_PathGraph_ReturnsTrue()
        {
            // Path graph is always bipartite
            // a --- b --- c --- d
            IGraph<string> graph = new UndirectedSparseGraph<string>();
            graph.AddVertices(new[] { "a", "b", "c", "d" });
            graph.AddEdge("a", "b");
            graph.AddEdge("b", "c");
            graph.AddEdge("c", "d");

            var bipartite = new BipartiteColoring<UndirectedSparseGraph<string>, string>(graph);

            Assert.True(bipartite.IsBipartite());
        }

        [Fact]
        public static void IsBipartite_EvenCycle_ReturnsTrue()
        {
            // Square (even cycle) is bipartite
            //  a --- b
            //  |     |
            //  d --- c
            IGraph<string> graph = new UndirectedSparseGraph<string>();
            graph.AddVertices(new[] { "a", "b", "c", "d" });
            graph.AddEdge("a", "b");
            graph.AddEdge("b", "c");
            graph.AddEdge("c", "d");
            graph.AddEdge("d", "a");

            var bipartite = new BipartiteColoring<UndirectedSparseGraph<string>, string>(graph);

            Assert.True(bipartite.IsBipartite());
        }

        [Fact]
        public static void Constructor_Pentagon_ThrowsInvalidOperationException()
        {
            // Pentagon (5-cycle, odd) is not bipartite
            IGraph<string> graph = new UndirectedSparseGraph<string>();
            graph.AddVertices(new[] { "a", "b", "c", "d", "e" });
            graph.AddEdge("a", "b");
            graph.AddEdge("b", "c");
            graph.AddEdge("c", "d");
            graph.AddEdge("d", "e");
            graph.AddEdge("e", "a");

            Assert.Throws<InvalidOperationException>(() => 
                new BipartiteColoring<UndirectedSparseGraph<string>, string>(graph));
        }
    }
}
