﻿﻿using NetworkLibrary.NetworkLibrary.Udp.PlayerToServer.Ping;
using NetworkLibrary.NetworkLibrary.Udp.PlayerToServer.UserInputMessage;
using NetworkLibrary.NetworkLibrary.Udp.ServerToPlayer.PositionMessages;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Udp
{
    //TODO: рефакторинг
    public static class MessageFactory
    {
        public static  Message GetMessage(PlayerInputMessage mes)
        {
            byte[] serializedMessage = ZeroFormatterSerializer.Serialize(mes);
            int messageType = GetMessageType(mes);
            int messageId = MessageIdGenerator.GetMessageId();
            Message message = new Message(messageType, serializedMessage, messageId, false);
            return message;
        }
         public static  Message GetMessage(PlayerPingMessage mes)
         {
             byte[] serializedMessage = ZeroFormatterSerializer.Serialize(mes);
             int messageType = GetMessageType(mes);
             int messageId = MessageIdGenerator.GetMessageId();
             Message message = new Message(messageType, serializedMessage, messageId, false);
             return message;
         }
        
         public static  Message GetMessage(PositionsMessage mes)
        {
            byte[] serializedMessage = ZeroFormatterSerializer.Serialize(mes);
            int messageType = GetMessageType(mes);
            int messageId = MessageIdGenerator.GetMessageId();
            Message message = new Message(messageType, serializedMessage, messageId, false);
            return message;
        }
         

         private static int GetMessageType(PlayerInputMessage message)
        {
            return 3;
        } 
     
        private static int GetMessageType(PositionsMessage message)
        {
            return 5;
        }
        private static int GetMessageType(PlayerPingMessage message)
        {
            return 6;
        }
        public static byte[] GetSerializedMessage(Message message)
        {
            return ZeroFormatterSerializer.Serialize(message);
        }
    }

    public static class MessageIdGenerator
    {
        private static int lastMessageId=1_000_000;
        public static int GetMessageId()
        {
            return lastMessageId++;
        }
    }
}