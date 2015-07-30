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
			Debug.Assert(queue.Count == 8);

			Console.WriteLine("Queue Elements:\r\n" + queue.ToHumanReadable());

			var array = queue.ToArray();
			Debug.Assert (array.Length == 8, "Wrong size.");

			queue.Dequeue();
			queue.Dequeue();
			top = queue.Dequeue();
			Debug.Assert (top == "ccc");

			Console.WriteLine("Old 3nd-last: " + top + "\r\n");
			Console.WriteLine("Queue Elements:\r\n" + queue.ToHumanReadable());

			queue.Dequeue();
			queue.Dequeue();
			Debug.Assert (queue.Top == "fff");

			Console.WriteLine("Queue Elements:\r\n" + queue.ToHumanReadable());

			var array2 = queue.ToArray();
			Debug.Assert (array2.Length == 3, "Wrong size.");
		}
	}
}

