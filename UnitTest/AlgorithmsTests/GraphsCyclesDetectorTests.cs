using Algorithms.Graphs;
using DataStructures.Graphs;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public static class GraphsCyclesDetectorTests
    {
        [Fact]
        public static void DoTest()
        {
            // Init graph object
            var digraphWithCycles = new DirectedSparseGraph<string>();

            // Init V
            var v = new string[6] { "r", "s", "t", "x", "y", "z" };

            // Insert V
            digraphWithCycles.AddVertices(v);

            // Insert E
            digraphWithCycles.AddEdge("r", "s");
            digraphWithCycles.AddEdge("r", "t");
            digraphWithCycles.AddEdge("s", "t");
            digraphWithCycles.AddEdge("s", "x");
            digraphWithCycles.AddEdge("t", "x");
            digraphWithCycles.AddEdge("t", "y");
            digraphWithCycles.AddEdge("t", "z");
            digraphWithCycles.AddEdge("x", "y");
            digraphWithCycles.AddEdge("x", "z");
            digraphWithCycles.AddEdge("y", "z");
            digraphWithCycles.AddEdge("z", "r");
            digraphWithCycles.AddEdge("z", "s");

            var isCyclic = CyclesDetector.IsCyclic<string>(digraphWithCycles);
            Assert.True(isCyclic == true, "Wrong status! The graph has cycles.");

            var cyclicGraph = new UndirectedSparseGraph<string>();

            v = new string[] { "A", "B", "C", "D", "E" };

            // Insert new values of V
            cyclicGraph.AddVertices(v);

            // Insert new value for edges
            cyclicGraph.AddEdge("A", "C");
            cyclicGraph.AddEdge("B", "A");
            cyclicGraph.AddEdge("B", "C");
            cyclicGraph.AddEdge("C", "E");
            cyclicGraph.AddEdge("C", "D");
            cyclicGraph.AddEdge("D", "B");
            cyclicGraph.AddEdge("E", "D");

            isCyclic = CyclesDetector.IsCyclic<string>(cyclicGraph);
            Assert.True(isCyclic == true, "Wrong status! The graph has cycles.");

            var dag = new DirectedSparseGraph<string>();

            v = new string[] { "A", "B", "C", "D", "E", "X" };

            // Insert new values of V
            dag.AddVertices(v);

            // Insert new value for edges
            dag.AddEdge("A", "B");
            dag.AddEdge("A", "X");
            dag.AddEdge("B", "C");
            dag.AddEdge("C", "D");
            dag.AddEdge("D", "E");
            dag.AddEdge("E", "X");

            isCyclic = CyclesDetector.IsCyclic<string>(dag);
            Assert.True(isCyclic == false, "Wrong status! The graph has no cycles.");
        }

    }

}
