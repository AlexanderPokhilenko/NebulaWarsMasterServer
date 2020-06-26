using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class ProductMark
    {
        [Index(0)] public virtual MarkType MarkType { get; set; }
        [Index(1)] public virtual int? NumberOfPercent { get; set; }
    }
}