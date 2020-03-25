using NetworkLibrary.NetworkLibrary.Udp;
using NetworkLibrary.NetworkLibrary.Udp.ServerToPlayer.PositionMessages;
using ZeroFormatter;

namespace Libraries.NetworkLibrary.Udp.ServerToPlayer.BattleStatus
{
    [ZeroFormattable]
    public struct PointTrackingMessage:ITypedMessage
    {
        [Index(0)] public readonly Vector2 Point;
        
        public PointTrackingMessage(Vector2 point)
        {
            Point = point;
        }
        
        public MessageType GetMessageType()
        {
            return MessageType.PointTracking;
        }
    }
}