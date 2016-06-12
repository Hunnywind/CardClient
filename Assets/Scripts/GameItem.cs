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
        Battle,
        Return,
        Return_Wait,
        Return_End,
        Summon,
        Summon_Wait,
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

    public struct CardInfo
    {
        public int mana;
        public int cooltime;
        public int leftcooltime;
        public string cardName;
    }
}
