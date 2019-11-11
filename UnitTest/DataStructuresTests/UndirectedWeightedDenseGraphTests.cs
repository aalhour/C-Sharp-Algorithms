using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.Graphs;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public class UndirectedWeightedDenseGraphTests
    {
        [Fact]
        public void IsWeighted_Should_AlwaysReturnTrue()
        {
            var graph = new UndirectedWeightedDenseGraph<int>();
            Assert.True(graph.IsWeighted);
        }

        [Fact]
        public void AddEdge_ShouldCreateEdge_WithProvidedWeight()
        {
            var graph = new UndirectedWeightedDenseGraph<string>();
            var vertices = new string[] { "a", "z", "s" };
            graph.AddVertices(vertices);

            Assert.True(graph.VerticesCount == 3);
            Assert.True(graph.Vertices.SequenceEqual(vertices));

            graph.AddEdge("a", "s", 1);
            graph.AddEdge("a", "z", 2);

            Assert.True(graph.Edges.Count() == 2);
            Assert.True(graph.EdgesCount == 2);

            var edgeToS = graph.GetEdge("a", "s");
            Assert.Equal("s", edgeToS.Destination);
            Assert.Equal("a", edgeToS.Source);
            Assert.Equal(1, edgeToS.Weight);

            var edgeToZ = graph.GetEdge("a", "z");
            Assert.Equal("z", edgeToZ.Destination);
            Assert.Equal("a", edgeToZ.Source);
            Assert.Equal(2, edgeToZ.Weight);

            var expectedOutgoingFromA = new List<string>() { "z", "s" };
            Assert.True(graph.OutgoingEdges("a").Select(e => e.Destination).SequenceEqual(expectedOutgoingFromA));
            Assert.Single(graph.OutgoingEdges("s"));
            Assert.Single(graph.OutgoingEdges("z"));

            var expectedIncomming = new List<string>() { "a" };
            Assert.True(graph.IncomingEdges("s").Select(e => e.Source).SequenceEqual(expectedIncomming));
            Assert.True(graph.IncomingEdges("z").Select(e => e.Source).SequenceEqual(expectedIncomming));
            Assert.Equal(2, graph.IncomingEdges("a").Count());
        }

        [Fact]
        public void GetEdge_ShouldThrowException_WhenSearchVertexNotFound()
        {
            var graph = new UndirectedWeightedDenseGraph<string>();
            var vertices = new string[] { "a", "z", "s" };
            graph.AddVertices(vertices);

            graph.AddEdge("a", "s", 1);
            graph.AddEdge("a", "z", 2);

            Assert.Throws<ArgumentOutOfRangeException>(() => graph.GetEdge("a", "A"));
        }

        [Fact]
        public void GetEdge_ShouldReturnNull_WhenEdgeNotFound()
        {
            var graph = new UndirectedWeightedDenseGraph<string>();
            var vertices = new string[] { "a", "z", "s", "b" };
            graph.AddVertices(vertices);

            graph.AddEdge("a", "s", 1);
            graph.AddEdge("a", "z", 2);

            Assert.Null(graph.GetEdge("a", "b"));
        }

        [Fact]
        public void RemoveEdge_ShouldRemoveEdge_AndSetProperties()
        {
            var graph = new UndirectedWeightedDenseGraph<string>();
            var vertices = new string[] { "a", "z", "s" };
            graph.AddVertices(vertices);

            graph.AddEdge("a", "s", 1);
            graph.AddEdge("a", "z", 2);

            Assert.True(graph.Edges.Count() == 2);
            Assert.True(graph.EdgesCount == 2);

            var expectedOutgoingFromA = new List<string>() { "z", "s" };
            Assert.True(graph.OutgoingEdges("a").Select(e => e.Destination).SequenceEqual(expectedOutgoingFromA));
            Assert.True(graph.IncomingEdges("z").Any());

            var result = graph.RemoveEdge("a", "z");
            Assert.True(result);

            Assert.True(graph.Edges.Count() == 1);
            Assert.True(graph.EdgesCount == 1);

            expectedOutgoingFromA = new List<string>() { "s" };
            Assert.True(graph.OutgoingEdges("a").Select(e => e.Destination).SequenceEqual(expectedOutgoingFromA));
            Assert.False(graph.IncomingEdges("z").Any());
        }

        [Fact]
        public void RemoveEdge_ShouldThrowException_WhenVertexNotExists()
        {
            var graph = new UndirectedWeightedDenseGraph<string>();
            var vertices = new string[] { "a", "z", "s" };
            graph.AddVertices(vertices);

            graph.AddEdge("a", "s", 1);
            graph.AddEdge("a", "z", 2);

            Assert.Throws<ArgumentOutOfRangeException>(() => graph.RemoveEdge("a", "A"));
            Assert.Throws<ArgumentOutOfRangeException>(() => graph.RemoveEdge("A", "a"));
        }

        [Fact]
        public void RemoveEdge_ShoudlReturnFalse_WhenEdgeNotExists()
        {
            var graph = new UndirectedWeightedDenseGraph<string>();
            var vertices = new string[] { "a", "z", "s" };
            graph.AddVertices(vertices);

            graph.AddEdge("a", "s", 1);

            Assert.False(graph.RemoveEdge("a", "z"));
        }

        [Fact]
        public void OutgoingEdges_ShouldThrowException_WhenVertexIsNotExists()
        {
            var graph = new UndirectedWeightedDenseGraph<string>();
            var vertices = new string[] { "a", "z", "s" };
            graph.AddVertices(vertices);

            graph.AddEdge("a", "s", 1);
            graph.AddEdge("a", "z", 2);

            Assert.Throws<ArgumentOutOfRangeException>(() => graph.OutgoingEdges("A").Any());
        }

        [Fact]
        public void IncommingEdges_ShouldThrowException_WhenVertexNotExists()
        {
            var graph = new UndirectedWeightedDenseGraph<string>();
            var vertices = new string[] { "a", "z", "s" };
            graph.AddVertices(vertices);

            graph.AddEdge("a", "s", 1);
            graph.AddEdge("a", "z", 2);

            Assert.Throws<ArgumentOutOfRangeException>(() => graph.IncomingEdges("A").Any());
        }

        [Fact]
        public void UpdateEdgeWeight_ShouldUpdateWeight_OfCSpecificEdge()
        {
            var graph = new UndirectedWeightedDenseGraph<string>();
            var vertices = new string[] { "a", "z", "s" };
            graph.AddVertices(vertices);

            graph.AddEdge("a", "s", 1);
            graph.AddEdge("a", "z", 2);
            Assert.Equal(1, graph.GetEdgeWeight("a", "s"));

            graph.UpdateEdgeWeight("a", "s", 10);
            Assert.Equal(10, graph.GetEdgeWeight("a", "s"));
        }

        [Fact]
        public void UpdateEdgeWeight_ShouldThrowException_WhenVertexNotFound()
        {
            var graph = new UndirectedWeightedDenseGraph<string>();
            var vertices = new string[] { "a", "z", "s" };
            graph.AddVertices(vertices);

            graph.AddEdge("a", "s", 1);
            graph.AddEdge("a", "z", 2);

            Assert.Throws<ArgumentOutOfRangeException>(() => graph.UpdateEdgeWeight("A", "a", 10));
            Assert.Throws<ArgumentOutOfRangeException>(() => graph.UpdateEdgeWeight("a", "A", 10));
        }

        [Fact]
        public void UpdateEdgeWeight_ShouldReturnFalse_WhenEdgeNotFound()
        {
            var graph = new UndirectedWeightedDenseGraph<string>();
            var vertices = new string[] { "a", "z", "s" };
            graph.AddVertices(vertices);

            graph.AddEdge("a", "s", 1);
            graph.AddEdge("a", "z", 2);

            Assert.False(graph.UpdateEdgeWeight("z", "s", 10));
        }
    }
}
