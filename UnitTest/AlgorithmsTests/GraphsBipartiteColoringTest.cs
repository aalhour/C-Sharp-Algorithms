using System;
using System.Diagnostics;

using Algorithms.Graphs;
using DataStructures.Graphs;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public static class GraphsBipartiteColoringTest
    {
        [Fact]
        public static void DoTest()
        {
            // The graph
            IGraph<string> graph = new UndirectedSparseGraph<string>();

            // The bipartite wrapper
            BipartiteColoring<UndirectedSparseGraph<string>, string> bipartiteGraph;

            // The status for checking bipartiteness
            bool initBipartiteStatus;


            // Prepare the graph for the first case of testing
            _initializeFirstCaseGraph(ref graph);

            // Test initializing the bipartite
            // This initialization must fail. The graph contains an odd cycle
            initBipartiteStatus = false;
            bipartiteGraph = null;

            try
            {
                bipartiteGraph = new BipartiteColoring<UndirectedSparseGraph<string>, string>(graph);
                initBipartiteStatus = bipartiteGraph.IsBipartite();
            }
            catch
            {
                initBipartiteStatus = false;
            }

            Debug.Assert(initBipartiteStatus == false, "Graph should not be bipartite.");


            /************************************************************/


            //
            // Prepare the graph for the second case of testing
            _initializeSecondCaseGraph(ref graph);

            //
            // Test initializing the bipartite
            // This initialization must fail. The graph contains an odd cycle
            bipartiteGraph = null;
            initBipartiteStatus = false;

            try
            {
                bipartiteGraph = new BipartiteColoring<UndirectedSparseGraph<string>, string>(graph);
                initBipartiteStatus = bipartiteGraph.IsBipartite();
            }
            catch
            {
                initBipartiteStatus = false;
            }

            Debug.Assert(initBipartiteStatus == false, "Graph should not be bipartite.");


            //
            // Remove Odd Cycle and try to initialize again.
            initBipartiteStatus = true;
            graph.RemoveEdge("c", "v");
            graph.RemoveEdge("f", "b");

            //
            // This initialization must pass. The graph doesn't contain any odd cycle
            try
            {
                bipartiteGraph = new BipartiteColoring<UndirectedSparseGraph<string>, string>(graph);
                initBipartiteStatus = bipartiteGraph.IsBipartite();
            }
            catch
            {
                initBipartiteStatus = false;
            }

            Debug.Assert(initBipartiteStatus == true, "Graph should be bipartite.");

            Debug.Assert(bipartiteGraph.ColorOf("a") == BipartiteColor.Red);
            Debug.Assert(bipartiteGraph.ColorOf("s") == BipartiteColor.Blue);
            Debug.Assert(bipartiteGraph.ColorOf("b") == BipartiteColor.Red);
            Debug.Assert(bipartiteGraph.ColorOf("f") == BipartiteColor.Red);
            Debug.Assert(bipartiteGraph.ColorOf("z") == BipartiteColor.Blue);
        }


        //
        // Second Case Initialization
        private static void _initializeFirstCaseGraph(ref IGraph<string> graph)
        {
            // Clear the graph
            graph.Clear();

            //
            // Add vertices
            var verticesSet = new string[] { "a", "b", "c" };
            graph.AddVertices (verticesSet);

            // 
            // Add Edges
            graph.AddEdge("a", "b");
            graph.AddEdge("b", "c");
            graph.AddEdge("c", "a");

        }


        //
        // Second Case Initialization
        private static void _initializeSecondCaseGraph(ref IGraph<string> graph)
        {
            // Clear the graph
            graph.Clear();

            //
            // Add vertices
            var verticesSet = new string[] { "a", "b", "c", "d", "e", "f", "s", "v", "x", "y", "z" };
            graph.AddVertices (verticesSet);

            //
            // Add edges

            // Connected Component #1
            // the vertex "e" won't be connected to any other vertex

            // Connected Component #2
            graph.AddEdge("a", "s");
            graph.AddEdge("a", "d");
            graph.AddEdge("s", "x");
            graph.AddEdge("s", "a");
            graph.AddEdge("x", "d");

            // Connected Component #3
            graph.AddEdge("b", "c");
            graph.AddEdge("b", "v");
            graph.AddEdge("c", "f");
            graph.AddEdge("c", "v");
            graph.AddEdge("f", "b");

            // Connected Component #4
            graph.AddEdge("y", "z");
        }

    }

}

