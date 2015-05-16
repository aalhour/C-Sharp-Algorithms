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

			queue.Push("aaa");
			queue.Push("bbb");
			queue.Push("ccc");
			queue.Push("ddd");
			queue.Push("eee");
			queue.Push("fff");
			queue.Push("ggg");
			queue.Push("hhh");

			Console.WriteLine("Queue Elements:\r\n" + queue.ToHumanReadable());

			var array = queue.ToArray();

			queue.Pop();
			queue.Pop();
			queue.Pop(out top);

			Console.WriteLine("Old 3nd-last: " + top + "\r\n");
			Console.WriteLine("Queue Elements:\r\n" + queue.ToHumanReadable());

			queue.Pop();
			queue.Pop();

			Console.WriteLine("Queue Elements:\r\n" + queue.ToHumanReadable());

			var array2 = queue.ToArray();
		}
	}
}

