namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    /// <summary>
    /// Нужно для чтения данных аккаунта из БД
    /// </summary>
    public class DapperHelperNotShownReward
    {
        /// <summary>
        /// id транзакции. После показа наград нужно выставить wasShown флаг в true
        /// </summary>
        public int Id { get; set; }
        public int SoftCurrencyDelta { get; set; }
        public int HardCurrencyDelta { get; set; }
        public int LootboxPointsDelta { get; set; }
        public int AccountRatingDelta { get; set; }
    }
}