using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public struct BattleRoyalePlayerModel
    {
        [Index(0)] public readonly int AccountId;
        [Index(1)] public readonly string Nickname;
        [Index(2)] public readonly int WarshipPowerLevel;
        
        public BattleRoyalePlayerModel(int accountId, string nickname, int warshipPowerLevel)
        {
            AccountId = accountId;
            Nickname = nickname;
            WarshipPowerLevel = warshipPowerLevel;
        }

        public bool IsBot()
        {
            return AccountId < 0;
        }
    }
}