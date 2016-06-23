using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameItem
{
    public enum Level
    {
        Init,
        Init_Wait,
        Init_Init,
        Battle,
        Return,
        Return_Wait,
        Return_Init,
        Return_End,
        Summon,
        Summon_Wait,
        Summon_Init,
    }

    public enum State
    {
        Stanby,
        Conneting,
        LoggingOn,
        MachingWait,
        MachingComplete,
        Failed,
    }

    public enum CardLocation
    {
        HAND,
        FIELD,
    }

    public enum AttackType
    {
        FRONT,
        SIDE,
        HERO,
        ALL_NOTFRONT,
    }

    public struct CardInfo
    {
        public int mana;
        public int cooltime;
        public int leftcooltime;
        public string cardName;
    }
    
    public struct CardInfo_send
    {
        public int mana;
        public int cooltime;
        public int leftcooltime;
        public string cardName;

        public int cardLocation;
        public int FieldLocation;
        public int attackType;
        public int maxHp;
        public int presentHp;
    }
}
