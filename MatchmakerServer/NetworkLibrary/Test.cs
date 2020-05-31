using System;
using System.Collections.Generic;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.NetworkLibrary
{
    [ZeroFormattable]
    public struct TestKeyValuePair 
    {
        [Index(0)] public KeyValuePair<ushort, SevenBytes>[] EntitiesInfo { get; private set; }
        [Index(1)] public KeyValuePair<ushort, ushort>[] __RadiusInfo { get; private set; }

        public TestKeyValuePair(KeyValuePair<ushort, SevenBytes>[] entitiesInfo
            , KeyValuePair<ushort, ushort>[] radiusInfo 
            )
        {
            EntitiesInfo = entitiesInfo;
            __RadiusInfo = radiusInfo;
        }
    }
    
    [ZeroFormattable]
    public struct TestDictionaryStruct 
    {
        [Index(0)] public Dictionary<ushort, SevenBytes> EntitiesInfo { get; private set; }
        [Index(1)] public Dictionary<ushort, ushort> __RadiusInfo { get; private set; }

        public TestDictionaryStruct(Dictionary<ushort, SevenBytes> entitiesInfo
            , Dictionary<ushort, ushort> radiusInfo 
            )
        {
            EntitiesInfo = entitiesInfo;
            __RadiusInfo = radiusInfo;
        }
    }
    
    [ZeroFormattable]
    public struct TestTuple
    {
        [Index(0)] public Tuple<ushort, SevenBytes>[] EntitiesInfo { get; private set; }
        [Index(1)] public Tuple<ushort, ushort>[] __RadiusInfo { get; private set; }

        public TestTuple(Tuple<ushort, SevenBytes>[] entitiesInfo
            , Tuple<ushort, ushort>[] radiusInfo 
        )
        {
            EntitiesInfo = entitiesInfo;
            __RadiusInfo = radiusInfo;
        }
    }

    [ZeroFormattable]
    public struct SevenBytes
    {
        [Index(0)] public byte Sich1;
        [Index(1)] public byte Sich2;
        [Index(2)] public byte Sich3;
        [Index(3)] public byte Sich4;
        [Index(4)] public byte Sich5;
        [Index(5)] public byte Sich6;
        [Index(6)] public byte Sich7;

        public SevenBytes(byte sich1, byte sich2, byte sich3, byte sich4, byte sich5, byte sich6, byte sich7)
        {
            Sich1 = sich1;
            Sich2 = sich2;
            Sich3 = sich3;
            Sich4 = sich4;
            Sich5 = sich5;
            Sich6 = sich6;
            Sich7 = sich7;
        }
    }
}