using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public struct BattleRoyalePlayerInfo
    {
        [Index(0)] public readonly int AccountId;
        [Index(1)] public readonly string Nickname;
        [Index(2)] public readonly int WarshipPowerPoints;
        [IgnoreFormat] public bool IsBot { get; }

        public BattleRoyalePlayerInfo(int accountId, string nickname, int warshipPowerPoints)
        {
            AccountId = accountId;
            Nickname = nickname;
            WarshipPowerPoints = warshipPowerPoints;
            IsBot = AccountId < 0; // У ботов отрицательные Id, а у игроков они генерируются инкрементом
        }
    }
}