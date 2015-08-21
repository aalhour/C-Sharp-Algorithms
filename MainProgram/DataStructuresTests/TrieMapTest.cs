using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

using DataStructures.Trees;

namespace C_Sharp_Algorithms.DataStructuresTests
{
    public static class TrieMapTest
    {
        public static void DoTest()
        {
            var trieMap = new TrieMap<int>();

            // Insert some how to words
            var prefix_howTo = "How to make";

            var word_howToSand = prefix_howTo + " a sandwitch";
            var word_howToRobot = prefix_howTo + " a robot";
            var word_howToOmelet = prefix_howTo + " an omelet";
            var word_howToProp = prefix_howTo + " a proposal";
            var listOfHow = new List<string>() { word_howToSand, word_howToRobot, word_howToOmelet, word_howToProp };
            
            trieMap.Add(word_howToOmelet, 7);
            trieMap.Add(word_howToSand, 11);
            trieMap.Add(word_howToRobot, 15);
            trieMap.Add(word_howToProp, 19);

            // Count of words = 4
            Debug.Assert(trieMap.Count == 4);

            // Insert some dictionary words
            var prefix_act = "act";

            var word_acts = prefix_act + "s";
            var word_actor = prefix_act + "or";
            var word_acting = prefix_act + "ing";
            var word_actress = prefix_act + "ress";
            var word_active = prefix_act + "ive";
            var listOfActWords = new List<string>() { word_acts, word_actor, word_acting, word_actress, word_active };

            trieMap.Add(word_actress, 82);
            trieMap.Add(word_active, 65);
            trieMap.Add(word_acting, 34);
            trieMap.Add(word_acts, 81);
            trieMap.Add(word_actor, 32);
            
            // Count of words = 9
            Debug.Assert(trieMap.Count == 9);



            //
            // ASSERT THE WORDS IN TRIE.

            // Search for a word that doesn't exist
            Debug.Assert(false == trieMap.ContainsWord(prefix_howTo));

            // Search for prefix
            Debug.Assert(true == trieMap.ContainsPrefix(prefix_howTo));

            // Search for a prefix using a word
            Debug.Assert(true == trieMap.ContainsPrefix(word_howToSand));

            // Get all words that start with the how-to prefix
            var someHowToWords = trieMap.SearchByPrefix(prefix_howTo).ToList();
            Debug.Assert(someHowToWords.Count == listOfHow.Count);

            // Assert there are only two words under the prefix "acti" -> active, & acting
            var someActiWords = trieMap.SearchByPrefix("acti").Select(item => item.Key).ToList<string>();
            Debug.Assert(someActiWords.Count == 2);
            Debug.Assert(someActiWords.Contains(word_acting));
            Debug.Assert(someActiWords.Contains(word_active));

            // Assert that "acto" is not a word
            Debug.Assert(trieMap.ContainsWord("acto") == false);



            //
            // TEST GETTING VALUES ASSOCIATED TO WORDS

            int actressRecord;
            trieMap.SearchByWord(word_actress, out actressRecord);
            Debug.Assert(actressRecord == 82);

            int howToProposeRequests;
            trieMap.SearchByWord(word_howToProp, out howToProposeRequests);
            Debug.Assert(howToProposeRequests == 19);



            //
            // TEST DELETING SOMETHINGS

            // Removing a prefix should fail
            var removing_acto_fails = false;
            try
            {
                // try removing a non-terminal word
                trieMap.Remove("acto");
                removing_acto_fails = false;
            }
            catch
            {
                // if exception occured then code works, word doesn't exist.
                removing_acto_fails = true;
            }

            Debug.Assert(removing_acto_fails == true);
            Debug.Assert(trieMap.Count == 9);

            // Removing a word should work
            var removing_acting_passes = false;
            try
            {
                // try removing a non-terminal word
                trieMap.Remove(word_acting);
                removing_acting_passes = true;
            }
            catch
            {
                // if exception occured then code DOESN'T work, word does exist.
                removing_acting_passes = false;
            }

            Debug.Assert(removing_acting_passes == true);
            Debug.Assert(trieMap.Count == 8);

            someActiWords = trieMap.SearchByPrefix("acti").Select(item => item.Key).ToList<string>();
            Debug.Assert(someActiWords.Count == 1);
            Debug.Assert(someActiWords.Contains(word_active));

            
            //
            // TEST ENUMERATOR

            var enumerator = trieMap.GetEnumerator();
            var allWords = new List<string>();
            while (enumerator.MoveNext())
                allWords.Add(enumerator.Current.Key);

            // Assert size
            Debug.Assert(allWords.Count == trieMap.Count);

            // Assert each element
            foreach (var word in allWords)
                Debug.Assert(listOfActWords.Contains(word) || listOfHow.Contains(word));


            Console.WriteLine("Testing is finished.");

            Console.ReadLine();
        }
    }
}
