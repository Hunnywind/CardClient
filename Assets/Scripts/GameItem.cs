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
    public struct CardData
    {
        public int number;
        public string name;
        public int mana;
        public int attack;
        public int speed;
        public int a_type;
        public int effect;
        public int picture;
        public int health;
    }
    public struct CardInfo
    {
        public int mana;
        public int attack;
        public int cooltime;
        public int leftcooltime;
        public int a_type;
        public string cardName;
        public int health;
    }
    
    public struct CardInfo_send
    {
        public int number;
        //public int mana;
        public int cooltime;
        public int leftcooltime;
        public int FieldLocation;
        public int health;
        public bool isReturn;
        public bool isEnemyCard;
    }
}
