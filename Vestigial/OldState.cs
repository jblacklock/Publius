using System.Collections.Generic;
using System;
using System.Linq;

public class OldState
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
        if (TotalNumberOfVotes == 0)
        {
            throw new System.InvalidOperationException(this.Name + " does not have any votes!");
        }
        if (this.Electors == 0)
        {
            throw new System.InvalidOperationException(this.Name + " does not have any electors!");
        }
        else
        {
            List<KeyValuePair<int, CandidateState>> leftoverVotes = new List<KeyValuePair<int, CandidateState>>();
            int ElectoralVotesAwarded = 0;
            double ElectoralCollegeVotePerCitizenVotes = TotalNumberOfVotes / this.Electors;

            foreach (KeyValuePair<int, CandidateState> entry in Votes)
            {
                int ElectoralVotesEarnedByCandidate = entry.Value.numberOfVotes * this.Electors / TotalNumberOfVotes;
                CandidateElectors.Add(entry.Value.ID, ElectoralVotesEarnedByCandidate);
                ElectoralVotesAwarded += ElectoralVotesEarnedByCandidate;

                
                KeyValuePair<int, CandidateState> CandidateLeftovers = new KeyValuePair<int, CandidateState>(Convert.ToInt32(entry.Value.numberOfVotes % ElectoralCollegeVotePerCitizenVotes), entry.Value);
                Console.WriteLine(entry.Value.Name + " has " + entry.Value.numberOfVotes % ElectoralCollegeVotePerCitizenVotes + " leftover votes in the state of " + this.Name + ": " + entry.Value.numberOfVotes + "%(" + TotalNumberOfVotes + "/" + this.Electors + ")= " + entry.Value.numberOfVotes % ElectoralCollegeVotePerCitizenVotes);
                leftoverVotes.Add(CandidateLeftovers);
            }

            while (ElectoralVotesAwarded < this.Electors)
            {
                leftoverVotes = leftoverVotes.OrderByDescending(p => p.Key).ToList();
                if (leftoverVotes[0].Key > leftoverVotes[1].Key && this.Electors - ElectoralVotesAwarded == 1)
                {
                    CandidateElectors[leftoverVotes[0].Value.ID] += 1;
                    ElectoralVotesAwarded++;
                    Console.WriteLine("The remaining electoral vote goes to " + leftoverVotes[0].Value.Name);
                }
                else
                {
                    //TODO: MEDIUM PRIORITY have code check to see how many candidates tied for this point
                    Console.WriteLine("The state of " + this.Name + " has a tie for an electoral vote between " + leftoverVotes[0].Value.Name + " and " + leftoverVotes[1].Value.Name);
                    Console.WriteLine("This tie must be broken by the people of the state of " + this.Name);
                    Console.WriteLine(ElectoralVotesAwarded + " electoral votes out of " + this.Electors + " were awared to the various candidates.");
                    break;
                }
            }
        }
    }

    public void printElectoralDistrubution()
    {
        foreach (KeyValuePair<int, int> entry in CandidateElectors)
        {
            Console.WriteLine("In " + this.Name + ", " + Votes[entry.Key].Name + " earned " + entry.Value + " electoral college votes out of " + this.Electors);
        }
    }

    public void getElectoralDistrubution(List<Candidate> candidates)
    {
        foreach (Candidate c in candidates)
        {
            if (CandidateElectors.ContainsKey(c.ID))
            {
                c.ElectoralCollegeVotes += CandidateElectors[c.ID];
            }
        }
    }

}