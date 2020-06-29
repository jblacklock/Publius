using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

//JsonReader reads the various json docments involved with the election process
public class JsonReader
{

    // where does this method belong?
    // public void addVotesToStates(List<State> states, List<Candidate> candidates)
    // {
    //     foreach (State s in states)
    //     {
    //         string curFile = "StateVotes/"+s.Name + ".json";
    //         if (File.Exists(curFile))
    //         {
    //             using (StreamReader reader = new StreamReader(curFile))
    //             {
    //                 string json = reader.ReadToEnd();
    //                 List<CandidateState> stateCandidate = JsonConvert.DeserializeObject<List<CandidateState>>(json);
    //                 foreach (CandidateState cs in stateCandidate)
    //                 {
    //                     s.addVotesForCandidates(cs);
    //                 }
    //             }
    //         }
    //         else
    //         {
    //             throw new System.InvalidOperationException("Cannot find votes for " + s.Name);
    //         }
    //     }
    // }

    // This belongs to Simulation Master
    public List<State> collectStates()
    {
        using (StreamReader reader = new StreamReader("ElectionCircumstances/States.json"))
        {
            string json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<List<State>>(json);
        }
    }

    // This belongs to Simulation Master 
    public List<Candidate> collectCandidates()
    {
        using (StreamReader reader = new StreamReader("ElectionCircumstances/Candidates.json"))
        {
            string json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Candidate>>(json);
        }
    }


}