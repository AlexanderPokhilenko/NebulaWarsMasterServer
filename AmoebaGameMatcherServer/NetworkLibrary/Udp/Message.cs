﻿﻿﻿﻿﻿using ZeroFormatter;

    namespace NetworkLibrary.NetworkLibrary.Udp
{
    [ZeroFormattable]
    public struct Message
    {
        [Index(0)] public int MessageType;
        [Index(1)] public int EncryptionKeyId;
        [Index(2)] public byte[] SerializedMessage;
        public Message(int messageType, int encryptionKeyId, byte[] serializedMessage)
        {
            MessageType = messageType;
            EncryptionKeyId = encryptionKeyId;
            SerializedMessage = serializedMessage;
        }
    }
}