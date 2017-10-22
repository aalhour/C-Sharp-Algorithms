using Algorithms.Graphs;
using DataStructures.Graphs;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTest.AlgorithmsTests
{
    public static class GraphsDijkstraShortestPathsTest
    {
        [Fact]
        public static void DoTest()
        {
            string[] V;
            IEnumerable<WeightedEdge<string>> E;
            DirectedWeightedSparseGraph<string> graph;
            DijkstraShortestPaths<DirectedWeightedSparseGraph<string>, string> dijkstra;

            // Init graph object
            graph = new DirectedWeightedSparseGraph<string>();

            // Init V
            V = new string[6] { "r", "s", "t", "x", "y", "z" };

            // Insert V
            graph.AddVertices(V);
            Assert.True(graph.VerticesCount == V.Length, "Wrong Vertices Count.");

            // Insert E
            var status = graph.AddEdge("r", "s", 7);
            Assert.True(status == true);
            status = graph.AddEdge("r", "t", 6);
            Assert.True(status == true);
            status = graph.AddEdge("s", "t", 5);
            Assert.True(status == true);
            status = graph.AddEdge("s", "x", 9);
            Assert.True(status == true);
            status = graph.AddEdge("t", "x", 10);
            Assert.True(status == true);
            status = graph.AddEdge("t", "y", 7);
            Assert.True(status == true);
            status = graph.AddEdge("t", "z", 5);
            Assert.True(status == true);
            status = graph.AddEdge("x", "y", 2);
            Assert.True(status == true);
            status = graph.AddEdge("x", "z", 4);
            Assert.True(status == true);
            status = graph.AddEdge("y", "z", 1);
            Assert.True(status == true);

            // Get E
            E = graph.Edges;
            Assert.True(graph.EdgesCount == 10, "Wrong Edges Count.");

            // PRINT THE GRAPH
            // [*] DIJKSTRA ON DIRECTED WEIGHTED GRAPH - TEST 01:

            // Graph representation:
            // Init DIJKSTRA
            dijkstra = new DijkstraShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "s");

            Assert.True(dijkstra.HasPathTo("r") == false);
            Assert.True(dijkstra.HasPathTo("z") == true);

            // Get shortest path to Z
            var pathToZ = string.Empty;
            foreach (var node in dijkstra.ShortestPathTo("z"))
            {
                pathToZ = String.Format("{0}({1}) -> ", pathToZ, node);
            }

            pathToZ = pathToZ.TrimEnd(new char[] { ' ', '-', '>' });

            var pathToY = string.Empty;
            foreach (var node in dijkstra.ShortestPathTo("y"))
            {
                pathToY = String.Format("{0}({1}) -> ", pathToY, node);
            }

            pathToY = pathToY.TrimEnd(new char[] { ' ', '-', '>' });

            // Console.WriteLine("Shortest path to node 'y': " + pathToY + "\r\n");

            // Clear the graph and insert new V and E to the instance
            graph.Clear();

            V = new string[] { "A", "B", "C", "D", "E" };

            // Insert new values of V
            graph.AddVertices(V);
            Assert.True(graph.VerticesCount == V.Length, "Wrong Vertices Count.");

            // Insert new value for edges
            status = graph.AddEdge("A", "C", 7);
            Assert.True(status == true);
            status = graph.AddEdge("B", "A", 19);
            Assert.True(status == true);
            status = graph.AddEdge("B", "C", 11);
            Assert.True(status == true);
            status = graph.AddEdge("C", "E", 5);
            Assert.True(status == true);
            status = graph.AddEdge("C", "D", 15);
            Assert.True(status == true);
            status = graph.AddEdge("D", "B", 4);
            Assert.True(status == true);
            status = graph.AddEdge("E", "D", 13);
            Assert.True(status == true);

            Assert.True(graph.EdgesCount == 7, "Wrong Edges Count.");

            // [*] DIJKSTRA ON DIRECTED WEIGHTED GRAPH - TEST 01:

            // Graph representation:
            // Init DIJKSTRA
            dijkstra = new DijkstraShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "A");

            var pathToD = string.Empty;
            foreach (var node in dijkstra.ShortestPathTo("D"))
            {
                pathToD = String.Format("{0}({1}) -> ", pathToD, node);
            }

            pathToD = pathToD.TrimEnd(new char[] { ' ', '-', '>' });

            var vertices = graph.Vertices;
            var dijkstraAllPairs = new DijkstraAllPairsShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph);

            // Dijkstra All Pairs Shortest Paths:
            foreach (var source in vertices)
            {
                foreach (var destination in vertices)
                {
                    var shortestPath = string.Empty;
                    foreach (var node in dijkstraAllPairs.ShortestPath(source, destination))
                        shortestPath = String.Format("{0}({1}) -> ", shortestPath, node);

                    shortestPath = shortestPath.TrimEnd(new char[] { ' ', '-', '>' });

                    // Console.WriteLine("Shortest path from '" + source + "' to '" + destination + "' is: " + shortestPath + "\r\n");
                }
            }

        }

    }

}
