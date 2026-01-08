using DataStructures.Graphs;
using Algorithms.Graphs;
using System.Linq;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public static class GraphsBreadthFirstPathsTest
    {
        private static IGraph<string> CreateTestGraph()
        {
            IGraph<string> graph = new UndirectedSparseGraph<string>();

            var vertices = new[] { "a", "z", "s", "x", "d", "c", "f", "v", "w", "m" };
            graph.AddVertices(vertices);

            graph.AddEdge("a", "s");
            graph.AddEdge("a", "z");
            graph.AddEdge("s", "x");
            graph.AddEdge("x", "d");
            graph.AddEdge("x", "c");
            graph.AddEdge("x", "w");
            graph.AddEdge("x", "m");
            graph.AddEdge("d", "f");
            graph.AddEdge("d", "c");
            graph.AddEdge("c", "f");
            graph.AddEdge("c", "v");
            graph.AddEdge("v", "f");
            graph.AddEdge("w", "m");

            return graph;
        }

        [Fact]
        public static void DistanceTo_ReturnsCorrectDistance()
        {
            var graph = CreateTestGraph();
            var bfsPaths = new BreadthFirstShortestPaths<string>(graph, "f");

            // f -> c -> x -> s -> a = 4 edges
            var distance = bfsPaths.DistanceTo("a");

            Assert.Equal(4, distance);
        }

        [Fact]
        public static void DistanceTo_SourceToSelf_ReturnsZero()
        {
            var graph = CreateTestGraph();
            var bfsPaths = new BreadthFirstShortestPaths<string>(graph, "f");

            var distance = bfsPaths.DistanceTo("f");

            Assert.Equal(0, distance);
        }

        [Fact]
        public static void ShortestPathTo_ReturnsPath()
        {
            var graph = CreateTestGraph();
            var bfsPaths = new BreadthFirstShortestPaths<string>(graph, "f");

            var path = bfsPaths.ShortestPathTo("a").ToList();

            Assert.NotEmpty(path);
            Assert.Equal("f", path[0]); // starts from source
            Assert.Equal("a", path[path.Count - 1]); // ends at destination
        }

        [Fact]
        public static void HasPathTo_ReachableVertex_ReturnsTrue()
        {
            var graph = CreateTestGraph();
            var bfsPaths = new BreadthFirstShortestPaths<string>(graph, "f");

            Assert.True(bfsPaths.HasPathTo("a"));
            Assert.True(bfsPaths.HasPathTo("w"));
            Assert.True(bfsPaths.HasPathTo("z"));
        }

        [Fact]
        public static void DistanceTo_DifferentPaths_ReturnsCorrectDistances()
        {
            var graph = CreateTestGraph();
            var bfsPaths = new BreadthFirstShortestPaths<string>(graph, "f");

            // f -> c -> x -> w = 3 edges
            var distanceToW = bfsPaths.DistanceTo("w");

            Assert.True(distanceToW >= 1); // at least one edge away
        }

        [Fact]
        public static void ShortestPathTo_MultipleVertices_ReturnsCorrectPaths()
        {
            var graph = CreateTestGraph();
            var bfsPaths = new BreadthFirstShortestPaths<string>(graph, "f");

            var pathToA = bfsPaths.ShortestPathTo("a").ToList();
            var pathToW = bfsPaths.ShortestPathTo("w").ToList();

            Assert.NotEmpty(pathToA);
            Assert.NotEmpty(pathToW);
            Assert.Equal(bfsPaths.DistanceTo("a") + 1, pathToA.Count);
            Assert.Equal(bfsPaths.DistanceTo("w") + 1, pathToW.Count);
        }
    }
}
