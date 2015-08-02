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

			MakeGraph(testGraph);

			IGraph<CliqueGraph<ComparableTuple>.Clique> newGraph = testGraph.buildDualGraph();

			foreach (var x in newGraph.Vertices)
			{
				foreach (var y in newGraph.Neighbours(x))
				{
					System.Diagnostics.Debug.WriteLine(string.Format("{0}-{1}", x, y));
				}

			}
			//System.Diagnostics.Debug.Assert 

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

		public ComparableTuple(int item1, int item2) : base(item1, item2)
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

