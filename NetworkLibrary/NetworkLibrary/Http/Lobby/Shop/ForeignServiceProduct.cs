using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class ForeignServiceProduct
    {
        [Index(0)] public virtual string ProductGoogleId { get; set; }
    }
}