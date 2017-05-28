using System;
using System.Diagnostics;
using System.Collections.Generic;

using Algorithms.Graphs;
using DataStructures.Graphs;
using DataStructures.Lists;

namespace UnitTest.AlgorithmsTests
{
    public static class GraphsDijkstraShortestPathsTest
    {
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
            Debug.Assert(graph.VerticesCount == V.Length, "Wrong Vertices Count.");

            // Insert E
            var status = graph.AddEdge("r", "s", 7);
            Debug.Assert(status == true);
            status = graph.AddEdge("r", "t", 6);
            Debug.Assert(status == true);
            status = graph.AddEdge("s", "t", 5);
            Debug.Assert(status == true);
            status = graph.AddEdge("s", "x", 9);
            Debug.Assert(status == true);
            status = graph.AddEdge("t", "x", 10);
            Debug.Assert(status == true);
            status = graph.AddEdge("t", "y", 7);
            Debug.Assert(status == true);
            status = graph.AddEdge("t", "z", 5);
            Debug.Assert(status == true);
            status = graph.AddEdge("x", "y", 2);
            Debug.Assert(status == true);
            status = graph.AddEdge("x", "z", 4);
            Debug.Assert(status == true);
            status = graph.AddEdge("y", "z", 1);
            Debug.Assert(status == true);

            // Get E
            E = graph.Edges;
            Debug.Assert(graph.EdgesCount == 10, "Wrong Edges Count.");

            //
            // PRINT THE GRAPH
            Console.Write("[*] DIJKSTRA ON DIRECTED WEIGHTED GRAPH - TEST 01:\r\n");

            Console.WriteLine("Graph representation:");
            Console.WriteLine(graph.ToReadable() + "\r\n");

            // Init DIJKSTRA
            dijkstra = new DijkstraShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "s");

            Debug.Assert(dijkstra.HasPathTo("r") == false);
            Debug.Assert(dijkstra.HasPathTo("z") == true);

            // Get shortest path to Z
            var pathToZ = string.Empty;
            foreach (var node in dijkstra.ShortestPathTo("z"))
                pathToZ = String.Format("{0}({1}) -> ", pathToZ, node);
            pathToZ = pathToZ.TrimEnd(new char[] { ' ', '-', '>' });

            Console.WriteLine("Shortest path to node 'z': " + pathToZ + "\r\n");

            var pathToY = string.Empty;
            foreach (var node in dijkstra.ShortestPathTo("y"))
                pathToY = String.Format("{0}({1}) -> ", pathToY, node);
            pathToY = pathToY.TrimEnd(new char[] { ' ', '-', '>' });

            Console.WriteLine("Shortest path to node 'y': " + pathToY + "\r\n");

            Console.WriteLine("*********************************************\r\n");


            /***************************************************************************************/
            
            
            // Clear the graph and insert new V and E to the instance
            graph.Clear();

            V = new string[] { "A", "B", "C", "D", "E" };
            
            // Insert new values of V
            graph.AddVertices(V);
            Debug.Assert(graph.VerticesCount == V.Length, "Wrong Vertices Count.");

            // Insert new value for edges
            status = graph.AddEdge("A", "C", 7);
            Debug.Assert(status == true);
            status = graph.AddEdge("B", "A", 19);
            Debug.Assert(status == true);
            status = graph.AddEdge("B", "C", 11);
            Debug.Assert(status == true);
            status = graph.AddEdge("C", "E", 5);
            Debug.Assert(status == true);
            status = graph.AddEdge("C", "D", 15);
            Debug.Assert(status == true);
            status = graph.AddEdge("D", "B", 4);
            Debug.Assert(status == true);
            status = graph.AddEdge("E", "D", 13);
            Debug.Assert(status == true);

            Debug.Assert(graph.EdgesCount == 7, "Wrong Edges Count.");

            //
            // PRINT THE GRAPH
            Console.Write("[*] DIJKSTRA ON DIRECTED WEIGHTED GRAPH - TEST 01:\r\n");

            Console.WriteLine("Graph representation:");
            Console.WriteLine(graph.ToReadable() + "\r\n");

            // Init DIJKSTRA
            dijkstra = new DijkstraShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph, "A");

            var pathToD = string.Empty;
            foreach (var node in dijkstra.ShortestPathTo("D"))
                pathToD = String.Format("{0}({1}) -> ", pathToD, node);
            pathToD = pathToD.TrimEnd(new char[] { ' ', '-', '>' });
            
            Console.WriteLine("Shortest path from 'A' to 'D': " + pathToD + "\r\n");

            Console.WriteLine("*********************************************\r\n");


            /***************************************************************************************/


            var dijkstraAllPairs = new DijkstraAllPairsShortestPaths<DirectedWeightedSparseGraph<string>, string>(graph);

            var vertices = graph.Vertices;

            Console.WriteLine("Dijkstra All Pairs Shortest Paths: \r\n");

            foreach (var source in vertices)
            {
                foreach (var destination in vertices)
                {
                    var shortestPath = string.Empty;
                    foreach (var node in dijkstraAllPairs.ShortestPath(source, destination))
                        shortestPath = String.Format("{0}({1}) -> ", shortestPath, node);

                    shortestPath = shortestPath.TrimEnd(new char[] { ' ', '-', '>' });

                    Console.WriteLine("Shortest path from '" + source + "' to '" + destination + "' is: " + shortestPath + "\r\n");
                }
            }
            
            Console.ReadLine();
        }

    }

}
