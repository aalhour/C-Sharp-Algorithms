using System;
using System.Diagnostics;

using DataStructures.Lists;

namespace C_Sharp_Algorithms.DataStructuresTests
{
	public static class QueueTest
	{
		public static void DoTest ()
		{
			string top;
			Queue<string> queue = new Queue<string>();

			queue.Enqueue("aaa");
			queue.Enqueue("bbb");
			queue.Enqueue("ccc");
			queue.Enqueue("ddd");
			queue.Enqueue("eee");
			queue.Enqueue("fff");
			queue.Enqueue("ggg");
			queue.Enqueue("hhh");

			Console.WriteLine("Queue Elements:\r\n" + queue.ToHumanReadable());

			var array = queue.ToArray();
			Debug.Assert (array.Length == 8, "Wrong size.");

			queue.Pop();
			queue.Pop();
			top = queue.Dequeue();

			Console.WriteLine("Old 3nd-last: " + top + "\r\n");
			Console.WriteLine("Queue Elements:\r\n" + queue.ToHumanReadable());

			queue.Pop();
			queue.Pop();

			Console.WriteLine("Queue Elements:\r\n" + queue.ToHumanReadable());

			var array2 = queue.ToArray();
			Debug.Assert (array2.Length == 3, "Wrong size.");
		}
	}
}

