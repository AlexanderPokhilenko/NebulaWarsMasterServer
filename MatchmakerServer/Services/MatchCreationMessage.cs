namespace AmoebaGameMatcherServer.Services
{
    public struct MatchCreationMessage
    {
        public bool Success;
        public MatchCreationFailureReason? FailureReason;
        public int? MatchId;
    }
}