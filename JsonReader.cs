using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class JsonReader
{

    public List<State> collectVotes()
    {
        using (StreamReader reader = new StreamReader("firstVotes.json"))
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