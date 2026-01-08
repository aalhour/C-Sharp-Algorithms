using System.Linq;
using Algorithms.Graphs;
using DataStructures.Graphs;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public static class GraphsTopologicalSorterTest
    {
        [Fact]
        public static void Sort_StringDAG_ReturnsTopologicalOrder()
        {
            //
            // DAG: A -> B -> C -> D -> E -> X
            //      A -> X
            //
            var vertices = new[] { "A", "B", "C", "D", "E", "X" };
            var dag = new DirectedSparseGraph<string>();
            dag.AddVertices(vertices);

            dag.AddEdge("A", "B");
            dag.AddEdge("A", "X");
            dag.AddEdge("B", "C");
            dag.AddEdge("C", "D");
            dag.AddEdge("D", "E");
            dag.AddEdge("E", "X");

            var sorted = TopologicalSorter.Sort(dag).ToList();

            // Verify topological order: each vertex comes before its dependents
            Assert.Equal(6, sorted.Count);

            // A must come before B, X
            Assert.True(sorted.IndexOf("A") < sorted.IndexOf("B"));
            Assert.True(sorted.IndexOf("A") < sorted.IndexOf("X"));

            // B must come before C
            Assert.True(sorted.IndexOf("B") < sorted.IndexOf("C"));

            // C must come before D
            Assert.True(sorted.IndexOf("C") < sorted.IndexOf("D"));

            // D must come before E
            Assert.True(sorted.IndexOf("D") < sorted.IndexOf("E"));

            // E must come before X
            Assert.True(sorted.IndexOf("E") < sorted.IndexOf("X"));
        }

        [Fact]
        public static void Sort_IntDAG_ReturnsTopologicalOrder()
        {
            //
            // DAG structure:
            // 7 -> 11, 8
            // 5 -> 11
            // 3 -> 8, 10
            // 11 -> 2, 9, 10
            // 8 -> 9
            //
            var vertices = new[] { 7, 5, 3, 11, 8, 2, 9, 10 };
            var dag = new DirectedSparseGraph<int>();
            dag.AddVertices(vertices);

            dag.AddEdge(7, 11);
            dag.AddEdge(7, 8);
            dag.AddEdge(5, 11);
            dag.AddEdge(3, 8);
            dag.AddEdge(3, 10);
            dag.AddEdge(11, 2);
            dag.AddEdge(11, 9);
            dag.AddEdge(11, 10);
            dag.AddEdge(8, 9);

            var sorted = TopologicalSorter.Sort(dag).ToList();

            Assert.Equal(8, sorted.Count);

            // Verify some topological constraints
            // 7 must come before 11 and 8
            Assert.True(sorted.IndexOf(7) < sorted.IndexOf(11));
            Assert.True(sorted.IndexOf(7) < sorted.IndexOf(8));

            // 5 must come before 11
            Assert.True(sorted.IndexOf(5) < sorted.IndexOf(11));

            // 11 must come before 2, 9, 10
            Assert.True(sorted.IndexOf(11) < sorted.IndexOf(2));
            Assert.True(sorted.IndexOf(11) < sorted.IndexOf(9));
            Assert.True(sorted.IndexOf(11) < sorted.IndexOf(10));

            // 8 must come before 9
            Assert.True(sorted.IndexOf(8) < sorted.IndexOf(9));
        }

        [Fact]
        public static void Sort_SingleVertex_ReturnsSingleVertex()
        {
            var dag = new DirectedSparseGraph<string>();
            dag.AddVertex("A");

            var sorted = TopologicalSorter.Sort(dag).ToList();

            Assert.Single(sorted);
            Assert.Equal("A", sorted[0]);
        }

        [Fact]
        public static void Sort_DisconnectedVertices_ReturnsAllVertices()
        {
            var dag = new DirectedSparseGraph<string>();
            dag.AddVertex("A");
            dag.AddVertex("B");
            dag.AddVertex("C");
            // No edges - all vertices are independent

            var sorted = TopologicalSorter.Sort(dag).ToList();

            Assert.Equal(3, sorted.Count);
            Assert.Contains("A", sorted);
            Assert.Contains("B", sorted);
            Assert.Contains("C", sorted);
        }
    }
}
