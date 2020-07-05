using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class WarshipPowerPointsProduct
    {
        [Index(0)] public virtual int WarshipId { get; set; }
        [Index(1)] public virtual int CurrentPowerPointsAmount { get; set; }
        [Index(2)] public virtual int CurrentMaxPowerPointsAmount { get; set; }
        [Index(3)] public virtual int PowerPointsIncrement { get; set; }
    }
}