using ZeroFormatter;


[ZeroFormattable]
public class GameRoomData
{
    [Index(0)] public virtual string GameServerIp{ get; set; }
    [Index(1)] public virtual int GameRoomNumber{ get; set; }
    [Index(2)] public virtual PlayerInfoForGameRoom[] Players { get; set; }
}

[ZeroFormattable]
public class PlayerInfoForGameRoom
{
    [Index(0)] public virtual string PlayerLogin { get; set; }
    [Index(1)] public virtual bool IsBot { get; set; }
}