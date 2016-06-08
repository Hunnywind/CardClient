using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameItem
{
    public enum Level
    {
        Init,
        Battle,
        Return,
        Return_End,
        Summon,
    }

    enum State
    {
        Setting,
        Hand,
        Field,
    }

    public struct CardInfo
    {
        public int mana;
        public int cooltime;
        public int leftcooltime;
        public string cardName;
    }
}
