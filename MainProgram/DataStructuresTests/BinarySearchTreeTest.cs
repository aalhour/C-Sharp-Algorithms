using System;
using System.Diagnostics;

using DataStructures;

namespace C_Sharp_Algorithms
{
	public static class BinarySearchTreeTest
	{
		public static void DoTest ()
		{
			BinarySearchTree<int> binarySearchTree = new BinarySearchTree<int> ();

			binarySearchTree.Insert (15);
			binarySearchTree.Insert (25);
			binarySearchTree.Insert (5);
			binarySearchTree.Insert (12);
			binarySearchTree.Insert (1);
			binarySearchTree.Insert (16);
			binarySearchTree.Insert (20);
			binarySearchTree.Insert (9);
			binarySearchTree.Insert (9);
			binarySearchTree.Insert (7);
			binarySearchTree.Insert (7);
			binarySearchTree.Insert (-1);
			binarySearchTree.Insert (11);
			binarySearchTree.Insert (19);
			binarySearchTree.Insert (30);
			binarySearchTree.Insert (8);
			binarySearchTree.Insert (10);
			binarySearchTree.Insert (13);
			binarySearchTree.Insert (28);
			binarySearchTree.Insert (39);


			//
			// Test removing an element with subtrees
			try
			{
				// doesn't exist!
				binarySearchTree.Remove (1000);
			}
			catch(Exception)
			{
				// does exist!
				binarySearchTree.Remove (25);
			}

			//
			// Test min & max
			var min = binarySearchTree.FindMin ();
			Debug.Assert(min == -1, "Min is wrong.");

			var max = binarySearchTree.FindMax ();
			Debug.Assert(max == 39, "Max is wrong.");

			// Remove min & max
			binarySearchTree.RemoveMin ();
			binarySearchTree.RemoveMax ();

			min = binarySearchTree.FindMin ();
			Debug.Assert(min == 1, "Min is wrong.");

			max = binarySearchTree.FindMax ();
			Debug.Assert(max == 30, "Max is wrong.");

			// Remove min twice
			binarySearchTree.RemoveMin ();
			binarySearchTree.RemoveMin ();

			min = binarySearchTree.FindMin ();
			Debug.Assert(min == 7, "Min is wrong.");

			// Remove max thrice
			binarySearchTree.RemoveMax ();
			binarySearchTree.RemoveMax ();
			binarySearchTree.RemoveMax ();

			max = binarySearchTree.FindMax ();
			Debug.Assert(max == 20, "Max is wrong.");
		}
	}
}

