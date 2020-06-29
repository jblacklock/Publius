using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Publius
{
    class SimulationMaster
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

            List<State> states = reader.collectStates();
            if (states.Count == 0)
            {
                throw new System.InvalidOperationException("There are no states present in this election");
            }

            foreach (State s in states)
            {
                s.calculateCandidateElectors();
            }

            // test code
            foreach (State s in states)
            {
                s.removeCandidate(3);
            }
        }
    }
}