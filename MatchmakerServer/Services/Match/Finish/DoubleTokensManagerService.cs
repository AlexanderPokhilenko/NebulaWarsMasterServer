namespace AmoebaGameMatcherServer.Services
{
     
    /// <summary>
    /// Решает нужно ли давать двойное кол-во токенов по итогам боя
    /// </summary>
    public class DoubleTokensManagerService
    {
        public bool IsDoubleTokensEnabled(int accountId, int matchId)
        {
            return true;
        }
    }
}