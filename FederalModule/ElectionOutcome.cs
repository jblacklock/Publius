using System.Collections.Generic;
using System;
using System.Linq;

public class ElectionOutcome
{
    public CandidateElectoralCollegeVotes Winner { get; set; }
    public Outcome outcome { get; set; }
    public List<CandidateElectoralCollegeVotes> Results { get; set; }
    public int Round { get; set; }
    public ElectionType ElectionType { get; set; }
    public int totalElectoralCollegeVotes {get; set;}

    public string GetElectionOutcome()
    {
        string finalString = "";
        if(ElectionType == ElectionType.InstantRunoff)
        {
            finalString += "ROUND " + Round.ToString() + " => ";
        }
        List<KeyValuePair<Candidate, int>> resultsList = new List<KeyValuePair<Candidate, int>>();
        foreach (CandidateElectoralCollegeVotes c in Results)
        {
            resultsList.Add(new KeyValuePair<Candidate, int>(c.candidate, c.GetElcetoralCollegeVotes()));
        }
        List<KeyValuePair<Candidate, int>> orderedResultsList = resultsList.OrderByDescending(x => x.Value).ToList();


        if (outcome == Outcome.Undecided)
        {
            finalString += "The election is still undecided. Currently, the distribution of the electoral college votes for round " + Round.ToString() + " is as follows: ";
        }
        else if (outcome == Outcome.ClearWinner)
        {
            finalString += Winner.candidate.Name + " won the presidential election with " + Winner.GetElcetoralCollegeVotes().ToString() + "/"+ totalElectoralCollegeVotes + " electoral college votes. The final breakdown of the electoral college votes is as follows: ";
        }
        else
        {
            int i = 0;
            int tracker = orderedResultsList[i].Value;
            List<String> NameOfTiedCandidates = new List<string>();
            while (tracker == orderedResultsList[i].Value)
            {
                NameOfTiedCandidates.Add(orderedResultsList[i].Key.Name);
                i++;
            }
            finalString += "The election ended in a " + NameOfTiedCandidates.Count +" way tie between the following candidates: ";
            finalString += String.Join(",", NameOfTiedCandidates);
            finalString += ". The breakdown of the electoral college votes are as follows:";
        }
        foreach (KeyValuePair<Candidate, int> c in orderedResultsList)
        {
            finalString += "\n" + c.Key.Name + ": " + c.Value;
        }

        return finalString;
    }
}

