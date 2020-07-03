using System.Collections.Generic;
using System;
using System.Linq;

public class CandidateElectoralCollegeVotes
{
    public Candidate candidate {get; set;}
    private int ElectotalCollegeVotes = 0;

    public int GetElcetoralCollegeVotes ()
    {
        return ElectotalCollegeVotes;
    }
    public void SetElcetoralCollegeVotes (int newElectoralCollegeVotes)
    {
        if(newElectoralCollegeVotes < 0)
        {
            throw new System.InvalidOperationException("Candidates cannot recieve negative electoral college votes (no matter how unpopular they are)");
        }
        ElectotalCollegeVotes += newElectoralCollegeVotes;
        // Console.WriteLine(candidate.Name + " has " + ElectotalCollegeVotes.ToString()+ " electoral college votes");
    }

    public void clearElectoralCollegeVotes ()
    {
        ElectotalCollegeVotes = 0;
    }
}