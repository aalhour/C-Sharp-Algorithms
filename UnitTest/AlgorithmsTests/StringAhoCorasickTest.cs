using Algorithms.Strings;

using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace UnitTest.AlgorithmsTests
{
	public static class StringAhoCorasickTest
	{
		[Fact]
		public static void DoTest()
		{
			AhoCorasick alg = new AhoCorasick();

			// Initialize patterns

			alg.AddPattern("a");
			alg.AddPattern("b");
			alg.AddPattern("c");
			alg.AddPattern("d");
			alg.AddPattern("aa");

			List<string> foundPatterns = alg.FindAllOccurrences("caaab");

			Assert.True(foundPatterns.Count == 7);
			Assert.True(foundPatterns.Where(q => q.Equals("c")).Count() == 1);
			Assert.True(foundPatterns.Where(q => q.Equals("a")).Count() == 3);
			Assert.True(foundPatterns.Where(q => q.Equals("aa")).Count() == 2);
			Assert.True(foundPatterns.Where(q => q.Equals("b")).Count() == 1);
			alg.ClearPatterns();

			alg.AddPattern("test1");
			alg.AddPattern("test2");
			alg.AddPattern("test3");
			alg.AddPattern("test33");
			alg.AddPattern("verybigtest");

			foundPatterns = alg.FindAllOccurrences("testtest1test1122test22test3549798test3656test333354654sdjkhbfabvdskhjfbashjdvbfjhksdbahjfvhusgdabvfhjsdvfgsdkhjvkverybigtesthdsagfhkgasdkhfverybigtestsdhgfjhkgsdfgk");

			Assert.True(foundPatterns.Count == 9);
			Assert.True(foundPatterns.Where(q => q.Equals("test1")).Count() == 2);
			Assert.True(foundPatterns.Where(q => q.Equals("test2")).Count() == 1);
			Assert.True(foundPatterns.Where(q => q.Equals("test3")).Count() == 3);
			Assert.True(foundPatterns.Where(q => q.Equals("test33")).Count() == 1);
			Assert.True(foundPatterns.Where(q => q.Equals("verybigtest")).Count() == 2);
		}
	}
}
