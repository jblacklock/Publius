using System.Collections.Generic;
using System;
using System.Linq;

public class CandidatePreferences
{
    public Candidate candidate {get; set;}
    
    private List<CandidateIDAndPercents> CanidateIDsAndPercentages;

    private decimal FirstProbability;

    private decimal percentTotal = 0;

    public CandidatePreferences(Candidate cand, List<CandidateIDAndPercents> canList, decimal firstChoiceProbability)
    {
        candidate = cand;
        CanidateIDsAndPercentages = canList;
        FirstProbability = firstChoiceProbability;
    }
    public bool setCandidateSecondChoiceProbability(int ID, decimal percent)
    {
        if(percent + percentTotal > 1 || percent < 0)
        {
            return false;
        }
        else
        {
            CandidateIDAndPercents tempCand = new CandidateIDAndPercents(ID, percent);
            CanidateIDsAndPercentages.Add(tempCand);
            percentTotal += percent;
            return true;
        }
    } 
}

public class CandidateIDAndPercents 
{
    public int ID {get; set;}
    public decimal secondChoiceProbability {get; set;}

    public CandidateIDAndPercents(int id, decimal second)
    {
        ID = id;
        second = secondChoiceProbability;
    }
}