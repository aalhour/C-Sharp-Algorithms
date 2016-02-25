using System;
using DataStructures.Graphs;
using System.Collections.Generic;

namespace C_Sharp_Algorithms.DataStructuresTests
{
    public static class CliqueGraphTest
    {
        public const int vertexPerCluster = 10;
        public const int numClusters = 10;
        static CliqueGraph<ComparableTuple> testGraph = new CliqueGraph<ComparableTuple>();
        static IGraph<ComparableTuple> compareGraph;

        static void MakeGraph(IGraph<ComparableTuple> gra)
        {

            for (int i = 0; i < numClusters; i++)
            {
                for (int j = 0; j < vertexPerCluster; j++)
                {
                    gra.AddVertex(new ComparableTuple(i, j));
                }
            }

            for (int i = 0; i < numClusters; i++)
            {
                MakeCluster(gra, i);
                System.Diagnostics.Debug.WriteLine(string.Format("Cluster {0} finished.", i));
            }

            for (int i = 0; i < numClusters; i++)
            {
                for (int j = 0; j < numClusters; j++)
                {
                    gra.AddEdge(new ComparableTuple(i, 0), new ComparableTuple(j, 0));
                }
            }

            System.Diagnostics.Debug.WriteLine(string.Format("Graph connected"));
        }

        static void MakeCluster(IGraph<ComparableTuple> gra, int i)
        {
            for (int j = 0; j < vertexPerCluster; j++)
            {
                for (int k = j; k < vertexPerCluster; k++)
                {
                    gra.AddEdge(new ComparableTuple(i, j), new ComparableTuple(i, k));
                }
            }
        }


        public static void DoTest()
        {
            compareGraph = new UndirectedDenseGraph<ComparableTuple>(numClusters * vertexPerCluster);
            MakeGraph(compareGraph);

            testGraph = new CliqueGraph<ComparableTuple>(compareGraph);
            // ICollection<ComparableTuple> component = testGraph.GetConnectedComponent(new ComparableTuple(0, 0));
            // DataStructures.Lists.DLinkedList<ComparableTuple> neighbor = testGraph.Neighbours(new ComparableTuple(0, 0));

            testGraph.RemoveEdge(new ComparableTuple(0, 0), new ComparableTuple(1, 0));

            IGraph<CliqueGraph<ComparableTuple>.Clique> dualGraph = testGraph.buildDualGraph();

            foreach (var x in dualGraph.Vertices)
            {
                foreach (var y in dualGraph.Neighbours(x))
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("{0}-{1}", x, y));
                }
            }

            // CliqueGraph.Edges test
            foreach (var edge in testGraph.Edges)
            {
                //System.Diagnostics.Debug.WriteLine(string.Format("{0} -> {1}\t", edge.Source, edge.Destination));
            }

            foreach (var edge in testGraph.OutgoingEdges(new ComparableTuple (0,0)))
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} -> {1}\t", edge.Source, edge.Destination));
            }


        }
    }

    class ComparableTuple  : Tuple<int, int>, IComparable<ComparableTuple>, IEquatable<ComparableTuple>
    {
        #region IComparable implementation


        int IComparable<ComparableTuple>.CompareTo(ComparableTuple other)
        {
            int myInt = ToInt;
            int otherInt = other.ToInt;
            return myInt < otherInt ? -1 : (myInt > otherInt ? 1 : 0);
        }

        #endregion

        #region IEquatable implementation

        bool IEquatable<ComparableTuple>.Equals(ComparableTuple other)
        {
            return ToInt == other.ToInt;
        }

        #endregion

        static readonly int multiplier = CliqueGraphTest.numClusters;

        public ComparableTuple(int item1, int item2)
            : base(item1, item2)
        {
			
        }

        int ToInt
        {
            get
            {
                return Item1 * multiplier + Item2;
            }
        }
			

    }
}

