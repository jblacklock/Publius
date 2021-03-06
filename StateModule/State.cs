using System.Collections.Generic;
using System;
using System.Linq;

public class State
{
    public string Name { get; set; }
    public string Abbreviation { get; set; }
    public int Electors { get; set; }
    private Dictionary<int, List<List<int>>> OrganizedVotes;

    public void calculateCandidateElectors(ElectoralCollegeDistributionType distributionType, string CandidatePath, string StateVotePath)
    {
        StateJsonReader reader = new StateJsonReader();
        List<Candidate> candidates = reader.collectCandidates(CandidatePath);
        if (candidates.Count == 0)
        {
            throw new System.InvalidOperationException("There are no candidates present in this election");
        }
        List<VoterPreferences> Votes = collectTheVotes(candidates, StateVotePath);
        OrganizedVotes = OrganizeVotes(candidates, Votes);
        Votes.Clear();
        Dictionary<int, int> ElctoralCollegeVotes = DistributeElectoralCollegePoints(distributionType);
        // TestCode
        // Dictionary<int, Candidate> cans = CandidateDictionaryGenerator(candidates);
        // foreach (KeyValuePair<int, int> cPair in ElctoralCollegeVotes)
        // {
        //     Console.WriteLine(cPair.Key + " won " + cPair.Value + " votes in the great state of " + Name);
        // }
        GenerateVoteDocument(ElctoralCollegeVotes);
    }

    public void removeCandidate(ElectoralCollegeDistributionType distributionType, List<int> candidateIDList)
    {
        foreach (int candidateID in candidateIDList)
        {
            List<List<int>> VotesToRedistribute = OrganizedVotes[candidateID];
            OrganizedVotes.Remove(candidateID);
            foreach (List<int> votes in VotesToRedistribute)
            {
                while (true)
                {
                    if (votes.Count > 0)
                    {
                        int NextCandidate = votes[0];
                        List<int> NewAlteredList = votes;
                        NewAlteredList.RemoveAt(0);
                        if (OrganizedVotes.ContainsKey(NextCandidate))
                        {
                            OrganizedVotes[NextCandidate].Add(NewAlteredList);
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            Dictionary<int, int> ElctoralCollegeVotes = DistributeElectoralCollegePoints(distributionType);
            GenerateVoteDocument(ElctoralCollegeVotes);
        }
    }

    private void GenerateVoteDocument(Dictionary<int, int> ElectoralCollegeVotes)
    {
        var jsonList = new List<KeyValuePair<int, int>>();
        foreach (KeyValuePair<int, int> ecv in ElectoralCollegeVotes)
        {
            KeyValuePair<int, int> tempKVP = new KeyValuePair<int, int>(ecv.Key, ecv.Value);
            jsonList.Add(tempKVP);
        }
        StateJsonReader reader = new StateJsonReader();
        reader.writeVoteDocument(jsonList, this);
    }

    private Dictionary<int, int> DistributeElectoralCollegePoints(ElectoralCollegeDistributionType distributionType)
    {
        int TotalNumberofVotes = 0;
        foreach (KeyValuePair<int, List<List<int>>> c in OrganizedVotes)
        {
            TotalNumberofVotes += c.Value.Count;
        }
        int remainingElectors = Electors;
        Dictionary<int, int> FinalDict = new Dictionary<int, int>();

        if (distributionType == ElectoralCollegeDistributionType.ProportionalVote)
        {
            List<KeyValuePair<Decimal, int>> remainders = new List<KeyValuePair<Decimal, int>>();
            foreach (KeyValuePair<int, List<List<int>>> c in OrganizedVotes)
            {
                int firstInt = c.Key;
                Decimal a = c.Value.Count;
                Decimal b = TotalNumberofVotes;
                Decimal secondDouble = (a / b) * Electors;
                int secondInt = Convert.ToInt32(Math.Floor(secondDouble));
                FinalDict.Add(firstInt, secondInt);
                remainingElectors -= secondInt;
                KeyValuePair<decimal, int> remainderToAdd = new KeyValuePair<decimal, int>((secondDouble % 1.0m), c.Key);
                remainders.Add(remainderToAdd);
            }
            List<KeyValuePair<Decimal, int>> newRemainders = remainders.OrderByDescending(x => x.Key).ToList();
            while (remainingElectors > 0)
            {
                int currentCandidateID = newRemainders[0].Value;
                FinalDict[currentCandidateID] += 1;
                newRemainders.RemoveAt(0);
                remainingElectors -= 1;
            }
        }
        else
        {
            KeyValuePair<int, int> IDAndVotesOfMostPopularCandidate = new KeyValuePair<int, int>(-1, -1);
            foreach (KeyValuePair<int, List<List<int>>> c in OrganizedVotes)
            {
                if (c.Value.Count > IDAndVotesOfMostPopularCandidate.Value)
                {
                    IDAndVotesOfMostPopularCandidate = new KeyValuePair<int, int>(c.Key, c.Value.Count);
                }
            }
            foreach (KeyValuePair<int, List<List<int>>> c in OrganizedVotes)
            { 
                if(c.Key != IDAndVotesOfMostPopularCandidate.Key)
                {
                    FinalDict.Add(c.Key, 0);
                }
                else
                {
                    FinalDict.Add(c.Key, Electors);
                }
            }
        }
        return FinalDict;
    }

    public void calculateCandidateElectors(ElectoralCollegeDistributionType distributionType)
    {
        calculateCandidateElectors(distributionType, "ElectionCircumstances/Candidates.json", "StateVotes/" + Name + ".json");
    }



    private Dictionary<int, List<List<int>>> OrganizeVotes(List<Candidate> candidates, List<VoterPreferences> Votes)
    {
        Dictionary<int, List<List<int>>> FinalDictionary = new Dictionary<int, List<List<int>>>();
        foreach (Candidate candidate in candidates)
        {
            FinalDictionary.Add(candidate.ID, new List<List<int>>());
        }
        foreach (VoterPreferences vote in Votes)
        {
            bool voteDone = false;
            while (voteDone == false)
            {
                int CurrentVote = vote.GetTopPick();
                if (FinalDictionary.ContainsKey(CurrentVote))
                {
                    List<int> AlteredPreference = vote.GetAlteredPreferences();
                    if (AlteredPreference.Count >= 0)
                    {
                        FinalDictionary[CurrentVote].Add(AlteredPreference);
                    }
                    voteDone = true;
                }
                else
                {
                    if (vote.GetLength() <= 1)
                    {
                        voteDone = true;
                    }
                    else
                    {
                        vote.RemoveTopPick();
                    }
                }
            }
        }
        return FinalDictionary;
    }

    private List<VoterPreferences> collectTheVotes(List<Candidate> Candidates, string StateVotePath)
    {
        StateJsonReader reader = new StateJsonReader();
        return reader.collectVotes(this, StateVotePath, Candidates);
    }
}