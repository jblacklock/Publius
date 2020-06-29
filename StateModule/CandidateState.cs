    //TODO: LOW PRIORITY/REFACTORING This class probably shouldn't exist, refactor this class out of existance
    //CandidateState represents a presidential candidate from the perspective OF A SINGLE STATE
    //Rather than track the number of electoral college votes which the candidate earns, this class tracks the number of votes a candidate earns in a particular state
    public class CandidateState
    {
        //Name is the Candidate's Name
        public string Name { get; set; }
        //ID is the candidate's candidate ID 
        public int ID { get; set; }
        //numberOfVotes is the number of votes which the candidate has recieved in a particular state
        public int numberOfVotes { get; set; }
    }