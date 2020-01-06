using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class JsonReader
{

    public void addVotesToStates(List<State> states, List<Candidate> candidates)
    {
        foreach (State s in states)
        {
            string curFile = s.Name + ".json";
            if (File.Exists(curFile))
            {
                using (StreamReader reader = new StreamReader(curFile))
                {
                    string json = reader.ReadToEnd();
                    Console.WriteLine(json);
                    List<CandidateState> stateCandidate = JsonConvert.DeserializeObject<List<CandidateState>>(json);
                    Console.WriteLine("Got past deserialization process");
                    foreach (CandidateState cs in stateCandidate)
                    {
                        Console.WriteLine(cs.Name);
                        s.addVotesForCandidates(cs);
                    }
                }
            }
            else
            {
                throw new System.InvalidOperationException("Cannot find votes for " + s.Name);
            }
        }
    }

    public List<State> collectStates()
    {
        using (StreamReader reader = new StreamReader("States.json"))
        {
            string json = reader.ReadToEnd();
            Console.WriteLine(json);
            return JsonConvert.DeserializeObject<List<State>>(json);
        }
    }

    public List<Candidate> collectCandidates()
    {
        using (StreamReader reader = new StreamReader("candidates.json"))
        {
            string json = reader.ReadToEnd();
            Console.WriteLine(json);
            return JsonConvert.DeserializeObject<List<Candidate>>(json);
        }
    }


}