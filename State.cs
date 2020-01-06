using System.Collections.Generic;
using System;

public class State
{
    public string Name { get; set; }
    public string Abbreviation { get; set; }

    //The first int of Votes represents the candidate's ID
    //The second int of Votes represnets the number of votes which that candidate has 
    private Dictionary<int, CandidateState> Votes = new Dictionary<int, CandidateState>();
    
    private Dictionary<int, int> CandidateElectors = new Dictionary<int, int>();
    public int Electors { get; set; }
    public int TotalNumberOfVotes = 0;

    public void addVotesForCandidates(CandidateState cs)
    {
        Votes.Add(cs.ID, cs);
        TotalNumberOfVotes += cs.numberOfVotes;
    }

    public string getVoteInfo()
    {
        string finalString = "";
        foreach (KeyValuePair<int, CandidateState> entry in Votes)
        {
            finalString += entry.Value.Name + ": " + entry.Value.numberOfVotes + " ";
        }
        return finalString;
    }

    public void calculateCandidateElectors()
    {
         Dictionary<int, int> leftovers = new Dictionary<int, int>();

        foreach (KeyValuePair<int, CandidateState> entry in Votes)
        {
            if (TotalNumberOfVotes == 0)
            {
                throw new System.InvalidOperationException(this.Name+" does not have any votes!");
            }
            else
            {
                Console.WriteLine(entry.Value.numberOfVotes+"/"+TotalNumberOfVotes);
                CandidateElectors.Add(entry.Value.ID, (entry.Value.numberOfVotes*this.Electors/TotalNumberOfVotes));
                //This does not yet deal with leftover votes
            }
        }
    }

    public void printElectoralDistrubution(){
        foreach(KeyValuePair<int, int> entry in CandidateElectors){
            Console.WriteLine("In "+ this.Name+", "+Votes[entry.Key].Name+" earned "+ entry.Value+" electoral college votes out of "+this.Electors);
        }
    }

}