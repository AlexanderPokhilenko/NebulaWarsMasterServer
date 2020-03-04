﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class GameRoomValidationResult
    {
        [Index(0)] public virtual GameRoomValidationResultEnum ResultEnum { get; set; }
        [Index(1)] public virtual string[] ProblemPlayersIds { get; set; }
    }

    public enum GameRoomValidationResultEnum
    {
        Ok,
        AlreadyHaveARoomWithThatNumber,
        ThereIsAtLeastOneSuchPlayer
    }
}