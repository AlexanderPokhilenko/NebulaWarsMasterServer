namespace AmoebaGameMatcherServer.Services.MatchFinishing
{
    /// <summary>
    /// По результатам боя в батл рояль режиме присуждает награду игроку.
    /// </summary>
    public interface IBattleRoyaleMatchRewardCalculatorService
    {
        MatchReward Calculate(int placeInBattle, int currentWarshipRating);
    }
}