using System;
using System.Linq;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    /// <summary>
    /// Нужен для передачи данных о бое между матчером и клиентом.
    /// </summary>
    [ZeroFormattable]
    public class BattleRoyaleClientMatchModel
    {
        [Index(0)] public virtual string GameServerIp { get; set; }
        [Index(1)] public virtual int GameServerPort { get; set; }
        [Index(2)] public virtual int MatchId { get; set; }
        [Index(3)] public virtual ushort PlayerTemporaryId { get; set; }
        [Index(4)] public virtual BattleRoyalePlayerInfo[] PlayerInfos { get; set; }

        // Конструктор для ZeroFormatter'а
        public BattleRoyaleClientMatchModel()
        { }

        public BattleRoyaleClientMatchModel(BattleRoyaleMatchModel fullModel, string playerServiceId)
        {
            GameServerIp = fullModel.GameServerIp;
            GameServerPort = fullModel.GameServerPort;
            MatchId = fullModel.MatchId;
            PlayerTemporaryId = fullModel.GameUnitsForMatch.Players.Find(player => player.ServiceId == playerServiceId)
                .TemporaryId;
            PlayerInfos = fullModel.GameUnitsForMatch.Select(unit => unit is PlayerInfoForMatch player
                    ? new BattleRoyalePlayerInfo(player.AccountId,
                        "Acc_" + player.AccountId, // TODO: Получать ники игроков
                        player.WarshipPowerPoints)
                    : unit is BotInfo bot
                        ? new BattleRoyalePlayerInfo(-bot.TemporaryId, // Обязательно должен быть отрицательным
                            bot.BotName,
                            bot.WarshipPowerPoints)
                        : throw new NotSupportedException("Неизвестный тип игрового объекта."))
                .ToArray();
        }
    }
}