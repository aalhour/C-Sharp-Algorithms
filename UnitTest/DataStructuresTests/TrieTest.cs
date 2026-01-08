using System;
using System.Linq;
using System.Collections.Generic;
using DataStructures.Trees;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class TrieTest
    {
        #region Add Tests

        [Fact]
        public static void Add_SingleWord_IncreasesCount()
        {
            var trie = new Trie();
            
            trie.Add("hello");
            
            Assert.Equal(1, trie.Count);
            Assert.False(trie.IsEmpty);
        }

        [Fact]
        public static void Add_MultipleWords_IncreasesCountCorrectly()
        {
            var trie = new Trie();
            
            trie.Add("hello");
            trie.Add("world");
            trie.Add("help");
            
            Assert.Equal(3, trie.Count);
        }

        [Fact]
        public static void Add_DuplicateWord_ThrowsInvalidOperationException()
        {
            var trie = new Trie();
            trie.Add("hello");
            
            Assert.Throws<InvalidOperationException>(() => trie.Add("hello"));
        }

        [Fact]
        public static void Add_EmptyString_ThrowsArgumentException()
        {
            var trie = new Trie();
            
            Assert.Throws<ArgumentException>(() => trie.Add(""));
        }

        [Fact]
        public static void Add_NullString_ThrowsArgumentException()
        {
            var trie = new Trie();
            
            Assert.Throws<ArgumentException>(() => trie.Add(null));
        }

        #endregion

        #region ContainsWord Tests

        [Fact]
        public static void ContainsWord_ExistingWord_ReturnsTrue()
        {
            var trie = new Trie();
            trie.Add("hello");
            trie.Add("world");
            
            Assert.True(trie.ContainsWord("hello"));
            Assert.True(trie.ContainsWord("world"));
        }

        [Fact]
        public static void ContainsWord_NonExistingWord_ReturnsFalse()
        {
            var trie = new Trie();
            trie.Add("hello");
            
            Assert.False(trie.ContainsWord("world"));
        }

        [Fact]
        public static void ContainsWord_PrefixOnly_ReturnsFalse()
        {
            // A prefix that is not a terminal word should return false
            var trie = new Trie();
            trie.Add("hello");
            
            Assert.False(trie.ContainsWord("hel")); // prefix, not a word
        }

        [Fact]
        public static void ContainsWord_EmptyString_ThrowsInvalidOperationException()
        {
            var trie = new Trie();
            
            Assert.Throws<InvalidOperationException>(() => trie.ContainsWord(""));
        }

        #endregion

        #region ContainsPrefix Tests

        [Fact]
        public static void ContainsPrefix_ExistingPrefix_ReturnsTrue()
        {
            var trie = new Trie();
            trie.Add("hello");
            trie.Add("help");
            trie.Add("helicopter");
            
            Assert.True(trie.ContainsPrefix("hel"));
            Assert.True(trie.ContainsPrefix("help"));
            Assert.True(trie.ContainsPrefix("hello"));
        }

        [Fact]
        public static void ContainsPrefix_NonExistingPrefix_ReturnsFalse()
        {
            var trie = new Trie();
            trie.Add("hello");
            
            Assert.False(trie.ContainsPrefix("world"));
            Assert.False(trie.ContainsPrefix("helloworld")); // extends beyond word
        }

        [Fact]
        public static void ContainsPrefix_EmptyString_ThrowsInvalidOperationException()
        {
            var trie = new Trie();
            
            Assert.Throws<InvalidOperationException>(() => trie.ContainsPrefix(""));
        }

        #endregion

        #region SearchByPrefix Tests

        [Fact]
        public static void SearchByPrefix_ExistingPrefix_ReturnsMatchingWords()
        {
            var trie = new Trie();
            trie.Add("hello");
            trie.Add("help");
            trie.Add("helicopter");
            trie.Add("world");
            
            var results = trie.SearchByPrefix("hel").ToList();
            
            Assert.Equal(3, results.Count);
            Assert.Contains("hello", results);
            Assert.Contains("help", results);
            Assert.Contains("helicopter", results);
        }

        [Fact]
        public static void SearchByPrefix_ExactWord_ReturnsOnlyThatWord()
        {
            var trie = new Trie();
            trie.Add("hello");
            trie.Add("help");
            
            var results = trie.SearchByPrefix("hello").ToList();
            
            Assert.Single(results);
            Assert.Equal("hello", results[0]);
        }

        [Fact]
        public static void SearchByPrefix_NonExistingPrefix_ReturnsNull()
        {
            var trie = new Trie();
            trie.Add("hello");
            
            var results = trie.SearchByPrefix("world");
            
            Assert.Null(results);
        }

        [Fact]
        public static void SearchByPrefix_EmptyString_ThrowsInvalidOperationException()
        {
            var trie = new Trie();
            
            Assert.Throws<InvalidOperationException>(() => trie.SearchByPrefix(""));
        }

        #endregion

        #region Remove Tests

        [Fact]
        public static void Remove_ExistingWord_DecreasesCount()
        {
            var trie = new Trie();
            trie.Add("hello");
            trie.Add("world");
            
            trie.Remove("hello");
            
            Assert.Equal(1, trie.Count);
            Assert.False(trie.ContainsWord("hello"));
            Assert.True(trie.ContainsWord("world"));
        }

        [Fact]
        public static void Remove_NonExistingWord_ThrowsKeyNotFoundException()
        {
            var trie = new Trie();
            trie.Add("hello");
            
            Assert.Throws<KeyNotFoundException>(() => trie.Remove("world"));
        }

        [Fact]
        public static void Remove_PrefixOnly_ThrowsKeyNotFoundException()
        {
            // Removing a prefix that is not a terminal word should fail
            var trie = new Trie();
            trie.Add("hello");
            
            Assert.Throws<KeyNotFoundException>(() => trie.Remove("hel"));
        }

        [Fact]
        public static void Remove_EmptyString_ThrowsArgumentException()
        {
            var trie = new Trie();
            
            Assert.Throws<ArgumentException>(() => trie.Remove(""));
        }

        [Fact]
        public static void Remove_DoesNotAffectOtherWords()
        {
            var trie = new Trie();
            trie.Add("hello");
            trie.Add("help");
            trie.Add("helicopter");
            
            trie.Remove("help");
            
            Assert.Equal(2, trie.Count);
            Assert.True(trie.ContainsWord("hello"));
            Assert.False(trie.ContainsWord("help"));
            Assert.True(trie.ContainsWord("helicopter"));
        }

        #endregion

        #region Clear Tests

        [Fact]
        public static void Clear_RemovesAllWords()
        {
            var trie = new Trie();
            trie.Add("hello");
            trie.Add("world");
            trie.Add("help");
            
            trie.Clear();
            
            Assert.Equal(0, trie.Count);
            Assert.True(trie.IsEmpty);
        }

        #endregion

        #region Enumerator Tests

        [Fact]
        public static void GetEnumerator_ReturnsAllWords()
        {
            var trie = new Trie();
            var words = new List<string> { "hello", "world", "help", "helicopter" };
            foreach (var word in words)
            {
                trie.Add(word);
            }
            
            var enumeratedWords = new List<string>();
            var enumerator = trie.GetEnumerator();
            while (enumerator.MoveNext())
            {
                enumeratedWords.Add(enumerator.Current);
            }
            
            Assert.Equal(words.Count, enumeratedWords.Count);
            foreach (var word in words)
            {
                Assert.Contains(word, enumeratedWords);
            }
        }

        #endregion

        #region Integration Tests

        [Fact]
        public static void IntegrationTest_HowToMakeWords()
        {
            //
            // Test with "How to make" prefix words
            //
            var trie = new Trie();

            var prefix_howTo = "How to make";
            var word_howToSand = prefix_howTo + " a sandwitch";
            var word_howToRobot = prefix_howTo + " a robot";
            var word_howToOmelet = prefix_howTo + " an omelet";
            var word_howToProp = prefix_howTo + " a proposal";
            var listOfHow = new List<string>() { word_howToSand, word_howToRobot, word_howToOmelet, word_howToProp };

            trie.Add(word_howToOmelet);
            trie.Add(word_howToSand);
            trie.Add(word_howToRobot);
            trie.Add(word_howToProp);

            Assert.Equal(4, trie.Count);
            Assert.False(trie.ContainsWord(prefix_howTo)); // prefix only, not a word
            Assert.True(trie.ContainsPrefix(prefix_howTo));
            Assert.True(trie.ContainsPrefix(word_howToSand));

            var someHowToWords = trie.SearchByPrefix(prefix_howTo).ToList();
            Assert.Equal(listOfHow.Count, someHowToWords.Count);
        }

        [Fact]
        public static void IntegrationTest_ActWords()
        {
            //
            // Test with "act" prefix words
            //
            var trie = new Trie();

            var prefix_act = "act";
            var word_acts = prefix_act + "s";
            var word_actor = prefix_act + "or";
            var word_acting = prefix_act + "ing";
            var word_actress = prefix_act + "ress";
            var word_active = prefix_act + "ive";

            trie.Add(word_actress);
            trie.Add(word_active);
            trie.Add(word_acting);
            trie.Add(word_acts);
            trie.Add(word_actor);

            Assert.Equal(5, trie.Count);

            // Assert there are only two words under the prefix "acti" -> active, & acting
            var actiWords = trie.SearchByPrefix("acti").ToList();
            Assert.Equal(2, actiWords.Count);
            Assert.Contains(word_acting, actiWords);
            Assert.Contains(word_active, actiWords);

            // "acto" is not a word (but is a prefix to "actor")
            Assert.False(trie.ContainsWord("acto"));
            Assert.True(trie.ContainsPrefix("acto"));

            // Remove "acting" and verify
            trie.Remove(word_acting);
            Assert.Equal(4, trie.Count);
            
            actiWords = trie.SearchByPrefix("acti").ToList();
            Assert.Single(actiWords);
            Assert.Contains(word_active, actiWords);
        }

        #endregion
    }
}
