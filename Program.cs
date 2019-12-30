using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Publius
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonReader reader = new JsonReader();

            List<State> states = reader.collectVotes();

            List<Candidate> candidates = reader.collectCandidates();

            // Dictionary<int, Candidate> candidateInfo = new Dictionary<int, Candidate>();
            // Dictionary<int, List<List<int>>> voteTally = new Dictionary<int, List<List<int>>>();
            // foreach (Candidate c in candidates)
            // {
            //     List<List<int>> listOfVotes = new List<List<int>>();
            //     voteTally.Add(c.ID, listOfVotes);
            //     candidateInfo.Add(c.ID, c);
            // }

            // foreach (List<int> vote in state.Votes)
            // {
            //     List<List<int>> temp = voteTally[vote[0]];
            //     temp.Add(vote);
            //     voteTally[vote[0]] = temp;
            // }

            // int leastPopular = voteTally.Keys.First();
            // int leastPopularCount = voteTally[leastPopular].Count;
            // foreach (int id in voteTally.Keys)
            // {
            //     if (voteTally[id].Count < voteTally[leastPopular].Count)
            //     {
            //         leastPopular = id;
            //         leastPopularCount = voteTally[id].Count;
            //     }

            // }
            // Console.WriteLine("better luck next time " + candidateInfo[leastPopular].Name);

            // while (candidateInfo.Count > 1)
            // {
            //     candidateInfo.Remove(leastPopular);
            //     if (voteTally[leastPopular].Count > 0)
            //     {

            //         foreach (List<int> lostVotes in voteTally[leastPopular].ToList())
            //         {
            //             lostVotes.Remove(0);
            //             while (true)
            //             {
            //                 if (lostVotes.Count == 0)
            //                 {
            //                     break;
            //                 }
            //                 if (voteTally.ContainsKey(lostVotes[0]))
            //                 {
            //                     List<List<int>> temp = voteTally[lostVotes[0]];
            //                     temp.Add(lostVotes);
            //                     break;
            //                 }
            //                 else
            //                 {
            //                     lostVotes.Remove(0);
            //                 }
            //             }

            //         }
            //     }
            //     voteTally.Remove(leastPopular);

            //     leastPopular = voteTally.Keys.First();
            //     leastPopularCount = voteTally[leastPopular].Count;
            //     foreach (int id in voteTally.Keys)
            //     {
            //         if (voteTally[id].Count < voteTally[leastPopular].Count)
            //         {
            //             leastPopular = id;
            //             leastPopularCount = voteTally[id].Count;
            //         }

            //     }
            //     Console.WriteLine("The least popular is " + candidateInfo[leastPopular].Name);
            // }
            // Console.WriteLine("All hail " + candidateInfo[candidateInfo.Keys.First()].Name);
        }
    }
}
