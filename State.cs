using System.Collections.Generic;
using System;

public class State
{
    public string Name { get; set; }
    public string Abbreviation { get; set; }

    //The first int of Votes represents the candidate's ID
    //The second int of Votes represnets the number of votes which that candidate has 
    private Dictionary<int, CandidateState> Votes = new Dictionary<int, CandidateState>();
    public int Electors { get; set; }

    public void addVotesForCandidates(CandidateState cs){
        Votes.Add(cs.ID,cs);
    }

    public string getVoteInfo(){
        string finalString = "";
        foreach(KeyValuePair<int, CandidateState> entry in Votes){
            finalString += entry.Value.Name+": "+entry.Value.numberOfVotes+" ";
        }
        return finalString;
    }

}