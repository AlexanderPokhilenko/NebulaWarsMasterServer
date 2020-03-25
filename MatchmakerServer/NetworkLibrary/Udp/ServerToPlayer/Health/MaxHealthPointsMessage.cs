using NetworkLibrary.NetworkLibrary.Udp;
using ZeroFormatter;

namespace Libraries.NetworkLibrary.Udp.ServerToPlayer
{
    [ZeroFormattable]
    public struct MaxHealthPointsMessage:ITypedMessage
    {
        [Index(0)] public readonly float Value;
        
        public MaxHealthPointsMessage(float value)
        {
            Value = value;
        }

        public MessageType GetMessageType()
        {
            return MessageType.MaxHealthPoints;
        }
    }
}