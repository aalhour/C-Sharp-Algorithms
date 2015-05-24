using System;
using System.Diagnostics;

using DataStructures;

namespace C_Sharp_Algorithms.DataStructuresTests
{
	public static class StackTest
	{
		public static void DoTest ()
		{
			int top;
			DataStructures.Stack<int> stack = new DataStructures.Stack<int>();

			stack.Push(1);
			stack.Push(2);
			stack.Push(3);
			stack.Push(4);
			stack.Push(5);
			stack.Push(6);

			Console.WriteLine("Stack:\r\n" + stack.ToHumanReadable());

			var array = stack.ToArray();
			Debug.Assert (array.Length == stack.Count, "Wrong size!");

			stack.Pop();
			stack.Pop(out top);

			Console.WriteLine("Old 2nd-last: " + top);
			Console.WriteLine("Stack:\r\n" + stack.ToHumanReadable());

			stack.Pop();
			stack.Pop();

			Console.WriteLine("Stack:\r\n" + stack.ToHumanReadable());

			var array2 = stack.ToArray();
			Debug.Assert (array2.Length == stack.Count, "Wrong size!");
		}
	}
}

