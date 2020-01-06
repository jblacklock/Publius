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

            List<State> states = reader.collectStates();

            List<Candidate> candidates = reader.collectCandidates();

            bool isEveryStateReady = false;

            while (isEveryStateReady == false)
            {
                foreach (State s in states)
                {
                    string curFile = s.Name + ".json";
                    if (!File.Exists(curFile))
                    {
                        isEveryStateReady = false;
                        Console.WriteLine(s.Name + " did not yet submit votes");
                        break;
                    }
                    else { isEveryStateReady = true; }
                }
                Console.WriteLine(isEveryStateReady);
            }

            reader.addVotesToStates(states, candidates);

            foreach (State s in states)
            {
                Console.WriteLine(s.getVoteInfo());
                s.calculateCandidateElectors();
                s.printElectoralDistrubution();
            }

        }
    }
}
