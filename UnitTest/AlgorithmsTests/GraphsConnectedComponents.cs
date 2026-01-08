using Algorithms.Graphs;
using DataStructures.Graphs;
using System.Linq;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public static class GraphsConnectedComponentsTest
    {
        [Fact]
        public static void Compute_FindsAllConnectedComponents()
        {
            //
            // Graph with 4 connected components:
            //
            // Component #1: (e) - isolated vertex
            // Component #2: a--s--x--d (with a--d)
            // Component #3: b--c--f--v (with b--v, c--v)
            // Component #4: y--z
            //
            var graph = new UndirectedSparseGraph<string>();

            var verticesSet1 = new string[] { "a", "b", "c", "d", "e", "f", "s", "v", "x", "y", "z" };
            graph.AddVertices(verticesSet1);

            // Connected Component #1 - vertex "e" is isolated (no edges)

            // Connected Component #2
            graph.AddEdge("a", "s");
            graph.AddEdge("a", "d");
            graph.AddEdge("s", "x");
            graph.AddEdge("x", "d");

            // Connected Component #3
            graph.AddEdge("b", "c");
            graph.AddEdge("b", "v");
            graph.AddEdge("c", "f");
            graph.AddEdge("c", "v");
            graph.AddEdge("f", "b");

            // Connected Component #4
            graph.AddEdge("y", "z");

            // Compute connected components
            var connectedComponents = ConnectedComponents.Compute<string>(graph);
            connectedComponents = connectedComponents.OrderBy(item => item.Count).ToList();

            Assert.Equal(4, connectedComponents.Count);

            // Component with isolated vertex (e)
            Assert.Single(connectedComponents[0]);
            Assert.Equal("e", connectedComponents[0][0]);

            // Component with y and z
            Assert.Equal(2, connectedComponents[1].Count);
            Assert.Contains("y", connectedComponents[1]);
            Assert.Contains("z", connectedComponents[1]);

            // Two components with 4 vertices each
            Assert.Equal(4, connectedComponents[2].Count);
            Assert.Equal(4, connectedComponents[3].Count);
        }

        [Fact]
        public static void Compute_SingleComponent_WhenAllConnected()
        {
            var graph = new UndirectedSparseGraph<string>();
            graph.AddVertices(new[] { "a", "b", "c" });
            graph.AddEdge("a", "b");
            graph.AddEdge("b", "c");

            var components = ConnectedComponents.Compute<string>(graph);

            Assert.Single(components);
            Assert.Equal(3, components[0].Count);
        }

        [Fact]
        public static void Compute_AllIsolated_EachVertexIsComponent()
        {
            var graph = new UndirectedSparseGraph<string>();
            graph.AddVertices(new[] { "a", "b", "c" });
            // No edges - all vertices are isolated

            var components = ConnectedComponents.Compute<string>(graph);

            Assert.Equal(3, components.Count);
            Assert.All(components, c => Assert.Single(c));
        }
    }
}
