﻿using NetworkLibrary.NetworkLibrary.Udp.PlayerToServer.Ping;
using NetworkLibrary.NetworkLibrary.Udp.PlayerToServer.UserInputMessage;
using NetworkLibrary.NetworkLibrary.Udp.ServerToPlayer.PositionMessages;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Udp
{
    public static class MessageFactory
    {
        public static  Message GetMessage(PlayerJoystickInputMessage mes)
        {
            byte[] serializedMessage = ZeroFormatterSerializer.Serialize(mes);
            int mesType = GetMessageType(mes);
            Message message = new Message(mesType, 0, serializedMessage);
            return message;
        }
         public static  Message GetMessage(PlayerPingMessage mes)
         {
             byte[] serializedMessage = ZeroFormatterSerializer.Serialize(mes);
             int mesType = GetMessageType(mes);
             Message message = new Message(mesType, 0, serializedMessage);
             return message;
         }
        
         public static  Message GetMessage(PlayersPositionsMessage mes)
        {
            byte[] serializedMessage = ZeroFormatterSerializer.Serialize(mes);
            int mesType = GetMessageType(mes);
            Message message = new Message(mesType, 0, serializedMessage);
            return message;
        }

         public static byte[] GetSerializedMessage(Message message)
         {
             return ZeroFormatterSerializer.Serialize(message);
         }
        
      
        private static int GetMessageType(PlayerJoystickInputMessage mes)
        {
            return 3;
        } 
     
        private static int GetMessageType(PlayersPositionsMessage mes)
        {
            return 5;
        }
        private static int GetMessageType(PlayerPingMessage mes)
        {
            return 6;
        } 
    }
}