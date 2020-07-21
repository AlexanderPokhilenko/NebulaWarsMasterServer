﻿﻿﻿﻿﻿﻿﻿﻿using System;
   using System.Collections.Generic;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    /// <summary>
    /// Извлекает из полной модели данных о матче данные для клиента.
    /// </summary>
    public class BattleRoyalePlayerModelFactory
    {
        public BattleRoyalePlayerModel[] Create(BattleRoyaleMatchModel fullModel)
        {
            List<BattleRoyalePlayerModel> result = new List<BattleRoyalePlayerModel>();
            foreach (PlayerModel playerModel in fullModel.GameUnits.Players)
            {
                BattleRoyalePlayerModel battleRoyalePlayerModel = 
                    new BattleRoyalePlayerModel(playerModel.AccountId, playerModel.Nickname,  playerModel.WarshipName , playerModel.WarshipPowerLevel);
                result.Add(battleRoyalePlayerModel);
            }

            foreach (BotModel botModel in fullModel.GameUnits.Bots)
            {
                BattleRoyalePlayerModel battleRoyalePlayerModel = 
                    new BattleRoyalePlayerModel(-botModel.TemporaryId, botModel.BotName,  botModel.WarshipName, botModel.WarshipPowerLevel);
                result.Add(battleRoyalePlayerModel);
            }

            return result.ToArray();
        }
    }
    
    public class GameUnitsFactory
    {
        public List<GameUnitModel> Create(BattleRoyaleMatchModel fullModel)
        {
            List<GameUnitModel> result = new List<GameUnitModel>();
            foreach (PlayerModel playerModel in fullModel.GameUnits.Players)
            {
                GameUnitModel gameUnitModel = new GameUnitModel(playerModel);
                result.Add(gameUnitModel);
            }

            foreach (BotModel botModel in fullModel.GameUnits.Bots)
            {
                GameUnitModel gameUnitModel = new GameUnitModel(botModel);
                result.Add(gameUnitModel);
            }

            return result;
        }
    }
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
        [Index(4)] public virtual BattleRoyalePlayerModel[] PlayerModels { get; set; }

        // Конструктор для ZeroFormatter'а
        public BattleRoyaleClientMatchModel()
        {
        }

        public BattleRoyaleClientMatchModel(BattleRoyaleMatchModel fullModel, string playerServiceId)
        {
            GameServerIp = fullModel.GameServerIp;
            GameServerPort = fullModel.GameServerPort;
            MatchId = fullModel.MatchId;
            PlayerTemporaryId = fullModel.GameUnits.Players.Find(player => player.ServiceId == playerServiceId)
                .TemporaryId;
            PlayerModels = new BattleRoyalePlayerModelFactory().Create(fullModel);
        }
    }
}