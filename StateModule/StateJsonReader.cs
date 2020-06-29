using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

//JsonReader reads the various json docments involved with the election process
public class StateJsonReader
{


    public List<VoterPreferences> collectVotes(State State, string curFile, List<Candidate> candidates)
    {
        if (File.Exists(curFile))
        {
            using (StreamReader reader = new StreamReader(curFile))
            {
                string json = reader.ReadToEnd();
                List<VoterPreferences> stateCandidate = JsonConvert.DeserializeObject<List<VoterPreferences>>(json);
                return stateCandidate;
            }
        }
        else
        {
            throw new System.InvalidOperationException("Cannot find votes for " + State.Name);
        }
    }

    public void writeVoteDocument(List<KeyValuePair<int,int>> candidatesAndElectoralCollegeVotes, State state)
    {
        string jsonString;
        jsonString = JsonConvert.SerializeObject(candidatesAndElectoralCollegeVotes);
        System.IO.File.WriteAllText(@"ElectoralCollegeVotes\"+state.Name+"ElectoralCollegeVotes.json", jsonString);
    }

    public List<Candidate> collectCandidates(string CandidatePath)
    {
        using (StreamReader reader = new StreamReader(CandidatePath))
        {
            string json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Candidate>>(json);
        }
    }


}