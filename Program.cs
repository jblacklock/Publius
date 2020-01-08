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
            bool ThereIsAPresidentialCandidateWinner = false;

            //TODO: LOW PRIORITY/ADVANCED FEATURE The following should be determined via a json document
            int TotalNumberOfElectoralCollegeVotes = 538;

            //TODO: LOW PRIORITY/ADVANCED FEATURE The following should be determined via a json document
            int year = 2020;

            JsonReader reader = new JsonReader();

            List<Candidate> candidates = reader.collectCandidates();
            if (candidates.Count == 0)
            {
                throw new System.InvalidOperationException("There are no candidates present in this election");
            }

            // DO NOT DELETE: the lines below is being saved for later
            // while (ThereIsAPresidentialCandidateWinner == false)
            // {

            List<State> states = reader.collectStates();
            if (states.Count == 0)
            {
                throw new System.InvalidOperationException("There are no states present in this election");
            }

            bool isEveryStateReady = false;

            while (isEveryStateReady == false)
            {
                foreach (State s in states)
                {
                    string curFile = "StateVotes/" + s.Name + ".json";
                    if (!File.Exists(curFile))
                    {
                        isEveryStateReady = false;
                        break;
                    }
                    else { isEveryStateReady = true; }
                }
            }

            reader.addVotesToStates(states, candidates);

            foreach (State s in states)
            {
                //Console.WriteLine(s.getVoteInfo());
                s.calculateCandidateElectors();
                //TODO: LOW PRIORITY/REFACTORING consider making the following two lines part of calculateCandidateElectors
                s.printElectoralDistrubution();
                s.getElectoralDistrubution(candidates);
            }


            candidates = candidates.OrderBy(p => p.ElectoralCollegeVotes).ToList();
            Console.WriteLine(candidates.Count);


            if (candidates[candidates.Count - 1].ElectoralCollegeVotes > TotalNumberOfElectoralCollegeVotes / 2 || candidates.Count == 1)
            {
                ThereIsAPresidentialCandidateWinner = true;
                Console.WriteLine("The winner of the " + year + " presidential election is " + candidates[candidates.Count - 1].Name + " with " + candidates[candidates.Count].ElectoralCollegeVotes + " electoral college votes");
            }
            else if (candidates[0].ElectoralCollegeVotes == candidates[candidates.Count - 1].ElectoralCollegeVotes)
            {
                ThereIsAPresidentialCandidateWinner = true;
                Console.WriteLine("There is a tie between the " + candidates.Count + " remaining candidates: ");
                foreach (Candidate c in candidates)
                {
                    Console.Write(c.Name + ", ");
                }
            }
            else
            {
                Candidate loserCandidate = candidates[0];
                if (loserCandidate.ElectoralCollegeVotes == 0)
                {
                    while (loserCandidate.ElectoralCollegeVotes == 0)
                    {
                        Console.WriteLine(loserCandidate.Name + " did not get a single electoral college vote! That's right: " + loserCandidate.ElectoralCollegeVotes);
                        candidates.RemoveAt(0);
                        loserCandidate = candidates[0];
                    }
                }
                else
                {

                    while (candidates[0].ElectoralCollegeVotes == loserCandidate.ElectoralCollegeVotes)
                    {
                        candidates.RemoveAt(0);
                    }
                }
                Console.WriteLine("The following candidates are still in the race:");
                foreach (Candidate c in candidates)
                {
                    Console.WriteLine(c.Name + " with " + c.ElectoralCollegeVotes + " electoral college votes");
                }


            }

            // DO NOT DELETE: the line below is being saved for later
            //}

        }
    }
}
