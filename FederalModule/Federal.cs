using System.Collections.Generic;
using System;
using System.Linq;

public class Federal
{
    private Dictionary<int, CandidateElectoralCollegeVotes> IDCandidateElectoralCollegeVotes = new Dictionary<int, CandidateElectoralCollegeVotes>();
    private List<FederalState> states;
    private List<Candidate> candidates;
    private Dictionary<int, Candidate> CandidateDictionary;
    private ElectionType electionType;
    private int Round = 0; 
    private int totalElectoralCollegeVotes = 0;
    public Federal(string StatePath = "ElectionCircumstances/States.json", string CandidatePath = "ElectionCircumstances/Candidates.json")
    {
        setStates(StatePath);
        setCandidates(CandidatePath);

        foreach (FederalState state in states)
        {
            totalElectoralCollegeVotes += state.Electors;
        }
        CandidateDictionary = CandidateDictionaryGenerator(candidates);
    }

    private void setCandidates(string candidatePath)
    {
        FederalJsonReader reader = new FederalJsonReader();

        candidates = reader.collectCandidates(candidatePath);
        if (candidates.Count == 0)
        {
            throw new System.InvalidOperationException("There are no candidates present in this election");
        }
        foreach (Candidate c in candidates)
        {
            CandidateElectoralCollegeVotes tempCandidatesVotes = new CandidateElectoralCollegeVotes();
            tempCandidatesVotes.candidate = c;
            IDCandidateElectoralCollegeVotes.Add(c.ID, tempCandidatesVotes);
        }
    }

    private void setStates(string statePath)
    {
        FederalJsonReader reader = new FederalJsonReader();

        states = reader.collectStates(statePath);
        if (states.Count == 0)
        {
            throw new System.InvalidOperationException("There are no states present in this election");
        }
    }

    public ElectionOutcome IsThereAWinner(ElectionType electionType, string ElectoralCollegeVotes = @"ElectoralCollegeVotes\")
    {
        if(electionType == ElectionType.InstantRunoff)
        {
            Round++;
        }
        FederalJsonReader reader = new FederalJsonReader();

        foreach (FederalState state in states)
        {
            KeyValuePair<int, List<KeyValuePair<int, int>>> CandidateAndVotes = reader.ReadStatesResults(state.Name);
            if (CandidateAndVotes.Key <= state.Electors)
            {

                foreach (KeyValuePair<int, int> candidateVote in CandidateAndVotes.Value)
                {

                    IDCandidateElectoralCollegeVotes[candidateVote.Key].SetElcetoralCollegeVotes(candidateVote.Value);
                }
            }
            else
            {
                throw new System.InvalidOperationException(state.Name + " is submitting " + CandidateAndVotes.Key.ToString() + " votes instead of " + state.Electors.ToString());
            }
        }
        int mostElectoralVotes = 0;
        List<CandidateElectoralCollegeVotes> outcomeList = new List<CandidateElectoralCollegeVotes>();
        foreach (KeyValuePair<int, CandidateElectoralCollegeVotes> c in IDCandidateElectoralCollegeVotes)
        {
            outcomeList.Add(c.Value);
            if (c.Value.GetElcetoralCollegeVotes() > mostElectoralVotes)
            {
                mostElectoralVotes = c.Value.GetElcetoralCollegeVotes();
                
            }
        }
        if (mostElectoralVotes > totalElectoralCollegeVotes / 2 || IDCandidateElectoralCollegeVotes.Count == 1)
        // the following if condition is test code, the above if condition is the actual code
        // if (IDCandidateElectoralCollegeVotes.Count == 1)
        {
            int max = IDCandidateElectoralCollegeVotes.Aggregate((l, r) => l.Value.GetElcetoralCollegeVotes() > r.Value.GetElcetoralCollegeVotes() ? l : r).Key;
            ElectionOutcome WinningOutcome = new ElectionOutcome();
            WinningOutcome.ElectionType = electionType;
            WinningOutcome.Winner = IDCandidateElectoralCollegeVotes[max];
            WinningOutcome.outcome = Outcome.ClearWinner;
            if(electionType == ElectionType.InstantRunoff)
            {
            WinningOutcome.Round = Round;
            }
            WinningOutcome.Results = outcomeList; 
            WinningOutcome.totalElectoralCollegeVotes = totalElectoralCollegeVotes;
            return WinningOutcome;
        }
        else
        {
            return checkForTie(outcomeList);
        }
    }

    private ElectionOutcome checkForTie(List<CandidateElectoralCollegeVotes> outcomeList)
    {
        List<int> votes = new List<int>();
        foreach (KeyValuePair<int, CandidateElectoralCollegeVotes> c in IDCandidateElectoralCollegeVotes)
        {
            votes.Add(c.Value.GetElcetoralCollegeVotes());
        }
        if (votes.Distinct().Count() == 1)
        {
            ElectionOutcome TieOutcome = new ElectionOutcome();
            TieOutcome.ElectionType = electionType;
            TieOutcome.outcome = Outcome.Tie;
            if(electionType == ElectionType.InstantRunoff)
            {
            TieOutcome.Round = Round;
            }
            TieOutcome.Results = outcomeList; 
            TieOutcome.totalElectoralCollegeVotes = totalElectoralCollegeVotes;
            return TieOutcome;
        }
        else
        {
            ElectionOutcome NoOutcome = new ElectionOutcome();
            NoOutcome.ElectionType = electionType;
            NoOutcome.outcome = Outcome.Undecided;
            if(electionType == ElectionType.InstantRunoff)
            {
            NoOutcome.Round = Round;
            }
            NoOutcome.Results = outcomeList; 
            NoOutcome.totalElectoralCollegeVotes = totalElectoralCollegeVotes;
            return NoOutcome;
        }
    }

    private Dictionary<int, Candidate> CandidateDictionaryGenerator(List<Candidate> candidates)
    {
        Dictionary<int, Candidate> FinalDictionary = new Dictionary<int, Candidate>();
        foreach (Candidate c in candidates)
        {
            FinalDictionary.Add(c.ID, c);
        }
        return FinalDictionary;
    }

    public List<int> CandidateToRemove()
    {
        List<int> finalList = new List<int>();
        bool candidatesWithZeroVotes = true;

        while (candidatesWithZeroVotes)
        {
            int min = IDCandidateElectoralCollegeVotes.Aggregate((l, r) => l.Value.GetElcetoralCollegeVotes() < r.Value.GetElcetoralCollegeVotes() ? l : r).Key;
            Console.WriteLine(CandidateDictionary[min].Name + " is a loser");
            IDCandidateElectoralCollegeVotes.Remove(min);
            finalList.Add(min);
            int secondMin = IDCandidateElectoralCollegeVotes.Aggregate((l, r) => l.Value.GetElcetoralCollegeVotes() < r.Value.GetElcetoralCollegeVotes() ? l : r).Key;
            if (IDCandidateElectoralCollegeVotes[secondMin].GetElcetoralCollegeVotes() != 0)
            {
                candidatesWithZeroVotes = false;
            }
        }
        return finalList;
    }



}


