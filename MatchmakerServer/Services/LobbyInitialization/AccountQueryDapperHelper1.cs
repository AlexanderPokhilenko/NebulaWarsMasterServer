namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    public class AccountQueryDapperHelper1
    {
        public int WarshipRating { get; set; }
        public int WarshipPowerPoints { get; set; }
        public int WarshipLevel { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(WarshipRating)} {WarshipRating} " +
                $" {nameof(WarshipPowerPoints)} {WarshipPowerPoints}";
        }
    }
}