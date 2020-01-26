using ZeroFormatter;


[ZeroFormattable]
public class GameMatherResponse
{
    [Index(0)] public virtual bool PlayerHasJustBeenRegistered{ get; set; }
    [Index(1)] public virtual bool PlayerInQueue{ get; set; }
    [Index(2)] public virtual bool PlayerInBattle{ get; set; }
    [Index(3)] public virtual GameRoomData GameRoomData{ get; set; }
}

[ZeroFormattable]
public class GameRoomData
{
    [Index(0)] public virtual string GameServerIp{ get; set; }
    [Index(1)] public virtual int GameRoomNumber{ get; set; }
    [Index(2)] public virtual PlayerInfoForGameRoom[] Players { get; set; }
    [Index(3)] public virtual int GameServerPort{ get; set; }
}

[ZeroFormattable]
public class PlayerInfoForGameRoom
{
    [Index(0)] public virtual string PlayerLogin { get; set; }
    [Index(1)] public virtual bool IsBot { get; set; }
}