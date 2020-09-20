using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class DiscountPrice
    {
        [Index(0)] public virtual decimal? CostBeforeDiscount { get; set; }
    }
}