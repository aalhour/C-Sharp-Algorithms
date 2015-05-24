using System;
using DataStructures;

namespace C_Sharp_Algorithms.DataStructuresTests
{
	public static class QueueTest
	{
		public static void DoTest ()
		{
			string top;
			DataStructures.Queue<string> queue = new DataStructures.Queue<string>();

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

			queue.Pop();
			queue.Pop();
			top = queue.Dequeue();

			Console.WriteLine("Old 3nd-last: " + top + "\r\n");
			Console.WriteLine("Queue Elements:\r\n" + queue.ToHumanReadable());

			queue.Pop();
			queue.Pop();

			Console.WriteLine("Queue Elements:\r\n" + queue.ToHumanReadable());

			var array2 = queue.ToArray();
		}
	}
}

