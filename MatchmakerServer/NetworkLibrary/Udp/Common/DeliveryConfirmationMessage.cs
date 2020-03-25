﻿﻿using NetworkLibrary.NetworkLibrary.Udp;
using ZeroFormatter;

namespace Libraries.NetworkLibrary.Udp.Common
{
    [ZeroFormattable]
    public struct DeliveryConfirmationMessage:ITypedMessage
    {
        [Index(0)] public uint MessageNumberThatConfirms;
        public MessageType GetMessageType() => MessageType.DeliveryConfirmation;

        public DeliveryConfirmationMessage(uint messageNumberThatConfirms)
        {
            MessageNumberThatConfirms = messageNumberThatConfirms;
        }
    }
}