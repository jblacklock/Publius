using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

//JsonReader reads the various json docments involved with the election process
public class FederalJsonReader
{
    public List<FederalState> collectStates(string statePath)
    {
        using (StreamReader reader = new StreamReader(statePath))
        {
            string json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<List<FederalState>>(json);
        }
    }

    // This belongs to Simulation Master 
    public List<Candidate> collectCandidates(string candidatePath)
    {
        using (StreamReader reader = new StreamReader(candidatePath))
        {
            string json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Candidate>>(json);
        }
    }

    public KeyValuePair<int, List<KeyValuePair<int, int>>> ReadStatesResults(string stateName)
    {
        int totalElectoralCollegeVotes = 0;
        List<CandidateReport> CandidatesAndVotes;
        List<KeyValuePair<int, int>> x = new List<KeyValuePair<int, int>>();
        using (StreamReader reader = new StreamReader("ElectoralCollegeVotes/" + stateName + "ElectoralCollegeVotes.json"))
        {
            string json = reader.ReadToEnd();
            CandidatesAndVotes = JsonConvert.DeserializeObject<List<CandidateReport>>(json);
            foreach (CandidateReport c in CandidatesAndVotes)
            {
                totalElectoralCollegeVotes += c.Value;
                x.Add(new KeyValuePair<int, int>(c.Key, c.Value));
                // Console.WriteLine("The candidate with an ID of " + c.Key + " has earned " + c.Value + " electoral college votes from the great state of " + stateName);
            }
        }
        return new KeyValuePair<int, List<KeyValuePair<int, int>>>(totalElectoralCollegeVotes, x);

    }
}