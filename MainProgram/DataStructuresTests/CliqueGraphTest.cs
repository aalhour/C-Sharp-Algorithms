using System;
using DataStructures.Graphs;

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
/*
			CliqueGraph<int> gr = new CliqueGraph<int>();
			gr.AddVertex(0);
			gr.AddVertex(1);
			gr.AddVertex(2);

			gr.AddEdge(0, 1);
			gr.AddEdge(0, 2);
			gr.AddEdge(1, 2);
*/
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

