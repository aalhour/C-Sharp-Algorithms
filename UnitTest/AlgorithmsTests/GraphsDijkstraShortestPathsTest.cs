using Algorithms.Graphs;
using DataStructures.Graphs;
using System;
using System.Linq;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public class GraphsDijkstraShortestPathsTest
    {
        [Fact]
        public void Constructor_Throw_WhenGraphInNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DijkstraShortestPaths<DirectedWeightedSparseGraph<string>, string>(null, "vertex"));
        }

        [Fact]
        public void Constructor_Throw_WhenSourceVertexIsNull()
        {
            var graph = new DirectedWeightedSparseGraph<string>();
            Assert.Throws<ArgumentNullException>(() => new DijkstraShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, null));
        }

        [Fact]
        public void Constructor_Throw_WhenSourceIsNotPartOfGraph()
        {
            var graph = new DirectedWeightedSparseGraph<string>();
            graph.AddVertex("a");
            graph.AddVertex("b");
            graph.AddVertex("c");
            graph.AddVertex("d");
            Assert.Throws<ArgumentException>(() => new DijkstraShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "x"));
        }

        [Fact]
        public void Constructor_Throw_WhenAnyEdgeWeightIsLessThanZero()
        {
            var graph = new DirectedWeightedSparseGraph<string>();
            graph.AddVertex("a");
            graph.AddVertex("b");

            graph.AddEdge("a", "b", -1);

            Assert.Throws<ArgumentException>(() => new DijkstraShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "a"));
        }

        [Fact]
        public void ShortestPathTo_Throw_WhenDestinationIsNotInGraph()
        {
            var graph = new DirectedWeightedSparseGraph<string>();
            graph.AddVertex("a");
            graph.AddVertex("b");
            graph.AddVertex("c");
            graph.AddVertex("d");

            var dijkstra = new DijkstraShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "a");
            Assert.Throws<ArgumentException>(() => dijkstra.ShortestPathTo("z"));
        }

        [Fact]
        public void ShortestPathTo_ReturnNull_WhenDestinationIsNotAchievable()
        {
            var graph = new DirectedWeightedSparseGraph<string>();
            graph.AddVertex("a");
            graph.AddVertex("b");
            graph.AddVertex("c");
            graph.AddVertex("d");

            graph.AddEdge("a", "b", 1);
            graph.AddEdge("b", "c", 1);
            graph.AddEdge("c", "a", 1);

            var dijkstra = new DijkstraShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "a");
            Assert.Null(dijkstra.ShortestPathTo("d"));
        }

        [Fact]
        public void ShortestPathTo_ReturnSingleVertex_WhenDestinationIsSameAsSource()
        {
            var graph = new DirectedWeightedSparseGraph<string>();
            graph.AddVertex("a");
            graph.AddVertex("b");
            graph.AddVertex("c");
            graph.AddVertex("d");

            graph.AddEdge("a", "b", 1);
            graph.AddEdge("b", "c", 1);
            graph.AddEdge("c", "a", 1);

            var dijkstra = new DijkstraShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "a");
            var result = dijkstra.ShortestPathTo("a");
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("a", result.Single());
        }

        [Fact]
        public void ShortestPathTo_FindShortestPath_WhenThereIsOnlyOnePath()
        {
            var graph = new DirectedWeightedSparseGraph<string>();
            graph.AddVertex("a");
            graph.AddVertex("b");
            graph.AddVertex("c");
            graph.AddVertex("d");

            graph.AddEdge("a", "b", 1);
            graph.AddEdge("b", "c", 1);
            graph.AddEdge("a", "c", 1);
            graph.AddEdge("c", "d", 1);

            var dijkstra = new DijkstraShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "a");
            var result = dijkstra.ShortestPathTo("d");
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Contains("a", result);
            Assert.Contains("c", result);
            Assert.Contains("d", result);
            Assert.Equal(2, dijkstra.DistanceTo("d"));
        }

        [Fact]
        public void ShortestPathTo_FindShortestPath_WhenThereIsPossibleMultiplePaths()
        {
            var graph = new DirectedWeightedSparseGraph<string>();
            graph.AddVertex("a");
            graph.AddVertex("b");
            graph.AddVertex("c");
            graph.AddVertex("d");

            graph.AddEdge("a", "b", 1);
            graph.AddEdge("b", "c", 1);
            graph.AddEdge("c", "a", 1);
            graph.AddEdge("c", "d", 1);
            graph.AddEdge("b", "d", 1);

            var dijkstra = new DijkstraShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "a");
            var result = dijkstra.ShortestPathTo("d");
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Contains("a", result);
            Assert.Contains("b", result);
            Assert.Contains("d", result);
            Assert.Equal(2, dijkstra.DistanceTo("d"));
        }

        [Fact]
        public void ShortestPathTo_FindShortestPath_WhenEdgeHaveDifferentWeight()
        {
            var vertices = new[] { "r", "s", "t", "x", "y", "z" };
            var graph = new DirectedWeightedSparseGraph<string>();
            graph.AddVertices(vertices);

            graph.AddEdge("r", "s", 7);
            graph.AddEdge("r", "t", 6);
            graph.AddEdge("s", "t", 5);
            graph.AddEdge("s", "x", 9);
            graph.AddEdge("t", "x", 10);
            graph.AddEdge("t", "y", 7);
            graph.AddEdge("t", "z", 5);
            graph.AddEdge("x", "y", 2);
            graph.AddEdge("x", "z", 4);
            graph.AddEdge("y", "z", 1);

            var dijkstra = new DijkstraShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "s");
            var shortestToZ = dijkstra.ShortestPathTo("z");
            Assert.NotNull(shortestToZ);
            Assert.Equal(3, shortestToZ.Count());
            Assert.Contains("s", shortestToZ);
            Assert.Contains("t", shortestToZ);
            Assert.Contains("z", shortestToZ);
            Assert.Equal(10, dijkstra.DistanceTo("z"));

            var shortestToY = dijkstra.ShortestPathTo("y");
            Assert.NotNull(shortestToY);
            Assert.Equal(3, shortestToY.Count());
            Assert.Contains("s", shortestToY);
            Assert.Contains("x", shortestToY);
            Assert.Contains("y", shortestToY);
            Assert.Equal(11, dijkstra.DistanceTo("y"));
        }

        [Fact]
        public void HasPathTo_Throw_WhenVertexIsNotInGraph()
        {
            var graph = new DirectedWeightedSparseGraph<string>();
            graph.AddVertex("a");
            graph.AddVertex("b");
            graph.AddVertex("c");

            graph.AddEdge("a", "b", 1);

            var dijkstra = new DijkstraShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "a");
            Assert.Throws<ArgumentException>(() => dijkstra.HasPathTo("z"));
        }

        [Fact]
        public void HasPathTo_ReturnTrue_WhenVertexIsAchievable()
        {
            var graph = new DirectedWeightedSparseGraph<string>();
            graph.AddVertex("a");
            graph.AddVertex("b");
            graph.AddVertex("c");

            graph.AddEdge("a", "b", 1);

            var dijkstra = new DijkstraShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "a");
            Assert.True(dijkstra.HasPathTo("b"));
        }

        [Fact]
        public void HasPathTo_ReturnFalse_WhenVertexIsNotAchievable()
        {
            var graph = new DirectedWeightedSparseGraph<string>();
            graph.AddVertex("a");
            graph.AddVertex("b");
            graph.AddVertex("c");

            graph.AddEdge("a", "b", 1);

            var dijkstra = new DijkstraShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "a");
            Assert.False(dijkstra.HasPathTo("c"));
        }

        [Fact]
        public void DistanceTo_Throw_WhenVertexIsNotInGraph()
        {
            var graph = new DirectedWeightedSparseGraph<string>();
            graph.AddVertex("a");
            graph.AddVertex("b");
            graph.AddVertex("c");

            graph.AddEdge("a", "b", 1);

            var dijkstra = new DijkstraShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "a");
            Assert.Throws<ArgumentException>(() => dijkstra.DistanceTo("z"));
        }

        [Fact]
        public void DistanceTo_ReturnInfinity_WhenVertexIsNotAchievable()
        {
            var graph = new DirectedWeightedSparseGraph<string>();
            graph.AddVertex("a");
            graph.AddVertex("b");
            graph.AddVertex("c");

            graph.AddEdge("a", "b", 1);

            var dijkstra = new DijkstraShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "a");
            Assert.Equal(long.MaxValue, dijkstra.DistanceTo("c"));
        }
    }
}
