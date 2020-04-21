using JetBrains.Annotations;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class LobbyData
    {
        [Index(0)] [NotNull] public virtual RelevantAccountData RelevantAccountData { get; set; }
        [Index(1)] [NotNull] public virtual RewardsThatHaveNotBeenShown RewardsThatHaveNotBeenShown { get; set; }
    }
}