using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Publius
{
    class SimulationMaster
    {
        static void Main(string[] args)
        {
            bool ThereIsAPresidentialCandidateWinner = false;

            //TODO: LOW PRIORITY/ADVANCED FEATURE The following should be determined via a json document
            int TotalNumberOfElectoralCollegeVotes = 538;

            //TODO: LOW PRIORITY/ADVANCED FEATURE The following should be determined via a json document
            int year = 2020;

            JsonReader reader = new JsonReader();

            List<Candidate> candidates = reader.collectCandidates();
            if (candidates.Count == 0)
            {
                throw new System.InvalidOperationException("There are no candidates present in this election");
            }

            List<State> states = reader.collectStates();
            if (states.Count == 0)
            {
                throw new System.InvalidOperationException("There are no states present in this election");
            }

            // Candidate Trump = new Candidate();
            // Trump.Name = "Trump";
            // Trump.ID = 1;
            // Candidate Obama = new Candidate();
            // Obama.Name = "Obama";
            // Obama.ID = 2;
            // Candidate Brown = new Candidate();
            // Brown.Name = "Brown";
            // Brown.ID = 3;
            // Candidate Lincoln = new Candidate();
            // Lincoln.Name = "Lincoln";
            // Lincoln.ID = 4;
            // Candidate Jesse = new Candidate();
            // Jesse.Name = "Jesse";
            // Jesse.ID = 5;
            // Candidate Luke = new Candidate();
            // Luke.Name = "Luke";
            // Luke.ID = 6;
            // CandidateIDAndPercents TObama = new CandidateIDAndPercents(Obama.ID, 0);
            // CandidateIDAndPercents TBrown = new CandidateIDAndPercents(Brown.ID, 0.5m);
            // CandidateIDAndPercents TLincoln = new CandidateIDAndPercents(Lincoln.ID, 0.5m);
            // CandidateIDAndPercents TJesse = new CandidateIDAndPercents(Jesse.ID, 0);
            // CandidateIDAndPercents TLuke = new CandidateIDAndPercents(Luke.ID, 0);
            // List<CandidateIDAndPercents> TrumpsList = new List<CandidateIDAndPercents> {TObama, TBrown, TLincoln, TJesse, TLuke};
            // CandidatePreferences TrumpPreferences = new CandidatePreferences(Trump, TrumpsList, 0.2m);

            // CandidateIDAndPercents OTrump = new CandidateIDAndPercents(Trump.ID, 0);
            // CandidateIDAndPercents OBrown = new CandidateIDAndPercents(Brown.ID, 0.2m);
            // CandidateIDAndPercents OLincoln = new CandidateIDAndPercents(Lincoln.ID, 0.5m);
            // CandidateIDAndPercents OJesse = new CandidateIDAndPercents(Jesse.ID, 0);
            // CandidateIDAndPercents OLuke = new CandidateIDAndPercents(Luke.ID, 0.3m);
            // List<CandidateIDAndPercents> ObamasList = new List<CandidateIDAndPercents> {OTrump, OBrown, OLincoln, OJesse, OLuke};
            // CandidatePreferences ObamaPreferences = new CandidatePreferences(Obama, ObamasList, 0.2m);
            
            // CandidateIDAndPercents BTrump = new CandidateIDAndPercents(Trump.ID, 0.7m);
            // CandidateIDAndPercents BObama = new CandidateIDAndPercents(Obama.ID, 0);
            // CandidateIDAndPercents BLincoln = new CandidateIDAndPercents(Lincoln.ID, 0.3m);
            // CandidateIDAndPercents BJesse = new CandidateIDAndPercents(Jesse.ID, 0);
            // CandidateIDAndPercents BLuke = new CandidateIDAndPercents(Luke.ID, 0);
            // List<CandidateIDAndPercents> BrownsList = new List<CandidateIDAndPercents> {BObama, BTrump, BLincoln, BJesse, BLuke};
            // CandidatePreferences BrownPreferences = new CandidatePreferences(Brown, BrownsList, 0.2m);
            
            // CandidateIDAndPercents LObama = new CandidateIDAndPercents(Obama.ID, 0.2m);
            // CandidateIDAndPercents LBrown = new CandidateIDAndPercents(Brown.ID, 0.2m);
            // CandidateIDAndPercents LTrump = new CandidateIDAndPercents(Trump.ID, 0.2m);
            // CandidateIDAndPercents LJesse = new CandidateIDAndPercents(Jesse.ID, 0.2m);
            // CandidateIDAndPercents LLuke = new CandidateIDAndPercents(Luke.ID, 0.2m);
            // List<CandidateIDAndPercents> LincolnsList = new List<CandidateIDAndPercents> {LObama, LBrown, LTrump, LJesse, LLuke};
            // CandidatePreferences LincolnPreferences = new CandidatePreferences(Lincoln, LincolnsList, 0.2m);
            
            // CandidateIDAndPercents JObama = new CandidateIDAndPercents(Obama.ID, 0);
            // CandidateIDAndPercents JBrown = new CandidateIDAndPercents(Brown.ID, 0.5m);
            // CandidateIDAndPercents JLincoln = new CandidateIDAndPercents(Lincoln.ID, 0.5m);
            // CandidateIDAndPercents JTrump = new CandidateIDAndPercents(Trump.ID, 0);
            // CandidateIDAndPercents JLuke = new CandidateIDAndPercents(Luke.ID, 0);
            // List<CandidateIDAndPercents> JessesList = new List<CandidateIDAndPercents> {JObama, JBrown, JLincoln, JTrump, JLuke};
            // CandidatePreferences JessePreferences = new CandidatePreferences(Jesse, JessesList, 0.1m);
            
            // CandidateIDAndPercents LuObama = new CandidateIDAndPercents(Obama.ID, 0.2m);
            // CandidateIDAndPercents LuBrown = new CandidateIDAndPercents(Brown.ID, 0.2m);
            // CandidateIDAndPercents LuLincoln = new CandidateIDAndPercents(Lincoln.ID, 0.2m);
            // CandidateIDAndPercents LuJesse = new CandidateIDAndPercents(Jesse.ID, 0);
            // CandidateIDAndPercents LuTrump = new CandidateIDAndPercents(Trump.ID, 0.4m);
            // List<CandidateIDAndPercents> LukesList = new List<CandidateIDAndPercents> {LuObama, LuBrown, LuLincoln, LuJesse, LuTrump};
            // CandidatePreferences LukePreferences = new CandidatePreferences(Luke, LukesList, 0.1m);

            // List<CandidatePreferences> AllCandidatePreferences = new List<CandidatePreferences> {TrumpPreferences, ObamaPreferences, BrownPreferences, LincolnPreferences, JessePreferences, LukePreferences};
            // VoteGenerator VoteMaker = new VoteGenerator(); 
            

            // Electoral Points, Winner Takes All, Instant Runoff Election
            foreach (State s in states)
            {
                s.calculateCandidateElectors(ElectoralCollegeDistributionType.WinnerTakeAll);
            }
            Federal EPWTAIRE = new Federal();
            ElectionOutcome EPWTAIREWinner = EPWTAIRE.IsThereAWinner(ElectionType.InstantRunoff);
            while (EPWTAIREWinner.outcome == Outcome.Undecided)
            {
                List<int> loser = EPWTAIRE.CandidateToRemove();
                foreach (State s in states)
                {
                    s.removeCandidate(ElectoralCollegeDistributionType.WinnerTakeAll, loser);
                }
                EPWTAIREWinner = EPWTAIRE.IsThereAWinner(ElectionType.InstantRunoff);
            }
            Console.WriteLine("ELECTORAL POINTS / WINNER TAKES ALL / INSTANT RUNOFF ELECTION:");
            Console.WriteLine(EPWTAIREWinner.GetElectionOutcome());



            // Electoral Points, Winner Takes All, First Past The Post Election
            foreach (State s in states)
            {
                s.calculateCandidateElectors(ElectoralCollegeDistributionType.WinnerTakeAll);
            }
            Federal EPWTAFPPE = new Federal();
            ElectionOutcome EPWTAFPPEWinner = EPWTAFPPE.IsThereAWinner(ElectionType.FirstPastThePost);
            Console.WriteLine("ELECTORAL POINTS / WINNER TAKES ALL / FIRST PAST THE POST ELECTION:");
            Console.WriteLine(EPWTAFPPEWinner.GetElectionOutcome());



            // Electoral Points, Proportional Votes, Instant Runoff Election
            foreach (State s in states)
            {
                s.calculateCandidateElectors(ElectoralCollegeDistributionType.ProportionalVote);
            }
            Federal instantRunoffGovernment = new Federal();
            ElectionOutcome thereIsAWinner = instantRunoffGovernment.IsThereAWinner(ElectionType.InstantRunoff);
            while (thereIsAWinner.outcome == Outcome.Undecided)
            {
                List<int> loser = instantRunoffGovernment.CandidateToRemove();
                foreach (State s in states)
                {
                    s.removeCandidate(ElectoralCollegeDistributionType.ProportionalVote, loser);
                }
                thereIsAWinner = instantRunoffGovernment.IsThereAWinner(ElectionType.InstantRunoff);
            }
            Console.WriteLine("ELECTORAL POINTS / PROPORTIONAL VOTE / INSTANT RUNOFF ELECTION:");
            Console.WriteLine(thereIsAWinner.GetElectionOutcome());



            // Electoral Points, Proportional Votes, First Past The Post Election
            foreach (State s in states)
            {
                s.calculateCandidateElectors(ElectoralCollegeDistributionType.ProportionalVote);
            }
            Federal currentGovernment = new Federal();
            ElectionOutcome PostWinner = currentGovernment.IsThereAWinner(ElectionType.FirstPastThePost);
            Console.WriteLine("ELECTORAL POINTS / PROPORTIONAL VOTE / FIRST PAST THE POST ELECTION:");
            Console.WriteLine(PostWinner.GetElectionOutcome());
        }

    }
}