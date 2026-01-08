using System.Collections.Generic;
using System.Linq;
using DataStructures.Trees;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class TrieMapTest
    {
        #region Add and Count Tests

        [Fact]
        public static void Add_SingleWord_IncreasesCount()
        {
            var trieMap = new TrieMap<int>();

            trieMap.Add("hello", 42);

            Assert.Equal(1, trieMap.Count);
        }

        [Fact]
        public static void Add_MultipleWords_TracksCount()
        {
            var trieMap = new TrieMap<int>();

            trieMap.Add("hello", 1);
            trieMap.Add("world", 2);
            trieMap.Add("help", 3);

            Assert.Equal(3, trieMap.Count);
        }

        #endregion

        #region ContainsWord Tests

        [Fact]
        public static void ContainsWord_ExistingWord_ReturnsTrue()
        {
            var trieMap = new TrieMap<int>();
            trieMap.Add("hello", 42);

            Assert.True(trieMap.ContainsWord("hello"));
        }

        [Fact]
        public static void ContainsWord_NonExistingWord_ReturnsFalse()
        {
            var trieMap = new TrieMap<int>();
            trieMap.Add("hello", 42);

            Assert.False(trieMap.ContainsWord("world"));
        }

        [Fact]
        public static void ContainsWord_PrefixOnly_ReturnsFalse()
        {
            var trieMap = new TrieMap<int>();
            trieMap.Add("hello", 42);

            Assert.False(trieMap.ContainsWord("hel"));
        }

        #endregion

        #region ContainsPrefix Tests

        [Fact]
        public static void ContainsPrefix_ExistingPrefix_ReturnsTrue()
        {
            var trieMap = new TrieMap<int>();
            trieMap.Add("hello", 42);
            trieMap.Add("help", 43);

            Assert.True(trieMap.ContainsPrefix("hel"));
            Assert.True(trieMap.ContainsPrefix("hello"));
        }

        [Fact]
        public static void ContainsPrefix_NonExistingPrefix_ReturnsFalse()
        {
            var trieMap = new TrieMap<int>();
            trieMap.Add("hello", 42);

            Assert.False(trieMap.ContainsPrefix("world"));
        }

        #endregion

        #region SearchByPrefix Tests

        [Fact]
        public static void SearchByPrefix_ReturnsMatchingWords()
        {
            var trieMap = new TrieMap<int>();
            trieMap.Add("active", 65);
            trieMap.Add("acting", 34);
            trieMap.Add("actor", 32);
            trieMap.Add("acts", 81);

            var actiWords = trieMap.SearchByPrefix("acti").Select(item => item.Key).ToList();

            Assert.Equal(2, actiWords.Count);
            Assert.Contains("acting", actiWords);
            Assert.Contains("active", actiWords);
        }

        [Fact]
        public static void SearchByPrefix_HowToWords_ReturnsAll()
        {
            var trieMap = new TrieMap<int>();
            const string prefix = "How to make";
            var words = new List<string>
            {
                prefix + " a sandwitch",
                prefix + " a robot",
                prefix + " an omelet",
                prefix + " a proposal"
            };

            for (int i = 0; i < words.Count; i++)
            {
                trieMap.Add(words[i], i);
            }

            var results = trieMap.SearchByPrefix(prefix).ToList();

            Assert.Equal(words.Count, results.Count);
        }

        #endregion

        #region SearchByWord Tests

        [Fact]
        public static void SearchByWord_ReturnsAssociatedValue()
        {
            var trieMap = new TrieMap<int>();
            trieMap.Add("actress", 82);
            trieMap.Add("proposal", 19);

            trieMap.SearchByWord("actress", out int actressRecord);
            trieMap.SearchByWord("proposal", out int proposalRecord);

            Assert.Equal(82, actressRecord);
            Assert.Equal(19, proposalRecord);
        }

        #endregion

        #region Remove Tests

        [Fact]
        public static void Remove_ExistingWord_DecreasesCount()
        {
            var trieMap = new TrieMap<int>();
            trieMap.Add("acting", 34);
            trieMap.Add("active", 65);

            trieMap.Remove("acting");

            Assert.Equal(1, trieMap.Count);
            Assert.False(trieMap.ContainsWord("acting"));
            Assert.True(trieMap.ContainsWord("active"));
        }

        [Fact]
        public static void Remove_NonTerminalPrefix_ThrowsKeyNotFoundException()
        {
            var trieMap = new TrieMap<int>();
            trieMap.Add("actor", 32);

            // "acto" is a prefix but not a terminal word
            Assert.Throws<KeyNotFoundException>(() => trieMap.Remove("acto"));
            Assert.Equal(1, trieMap.Count);
        }

        [Fact]
        public static void Remove_NonExistingWord_ThrowsKeyNotFoundException()
        {
            var trieMap = new TrieMap<int>();
            trieMap.Add("hello", 1);

            Assert.Throws<KeyNotFoundException>(() => trieMap.Remove("world"));
        }

        #endregion

        #region Enumerator Tests

        [Fact]
        public static void GetEnumerator_ReturnsAllWords()
        {
            var trieMap = new TrieMap<int>();
            var words = new List<string> { "active", "acting", "actor", "acts" };

            for (int i = 0; i < words.Count; i++)
            {
                trieMap.Add(words[i], i);
            }

            var enumeratedWords = new List<string>();
            var enumerator = trieMap.GetEnumerator();
            while (enumerator.MoveNext())
            {
                enumeratedWords.Add(enumerator.Current.Key);
            }

            Assert.Equal(trieMap.Count, enumeratedWords.Count);
            foreach (var word in words)
            {
                Assert.Contains(word, enumeratedWords);
            }
        }

        #endregion

        #region Integration Test

        [Fact]
        public static void IntegrationTest_FullWorkflow()
        {
            var trieMap = new TrieMap<int>();

            // Add "How to make" words
            const string prefixHowTo = "How to make";
            var howToWords = new List<string>
            {
                prefixHowTo + " a sandwitch",
                prefixHowTo + " a robot",
                prefixHowTo + " an omelet",
                prefixHowTo + " a proposal"
            };
            trieMap.Add(howToWords[0], 11);
            trieMap.Add(howToWords[1], 15);
            trieMap.Add(howToWords[2], 7);
            trieMap.Add(howToWords[3], 19);

            // Add "act" words
            var actWords = new List<string> { "acts", "actor", "acting", "actress", "active" };
            trieMap.Add("actress", 82);
            trieMap.Add("active", 65);
            trieMap.Add("acting", 34);
            trieMap.Add("acts", 81);
            trieMap.Add("actor", 32);

            Assert.Equal(9, trieMap.Count);

            // Prefix is not a word
            Assert.False(trieMap.ContainsWord(prefixHowTo));
            Assert.True(trieMap.ContainsPrefix(prefixHowTo));

            // "acto" is prefix but not word
            Assert.False(trieMap.ContainsWord("acto"));
            Assert.True(trieMap.ContainsPrefix("acto"));

            // Get values
            trieMap.SearchByWord("actress", out int actressVal);
            Assert.Equal(82, actressVal);

            // Remove and verify
            trieMap.Remove("acting");
            Assert.Equal(8, trieMap.Count);

            var actiWords = trieMap.SearchByPrefix("acti").Select(item => item.Key).ToList();
            Assert.Single(actiWords);
            Assert.Contains("active", actiWords);
        }

        #endregion
    }
}
