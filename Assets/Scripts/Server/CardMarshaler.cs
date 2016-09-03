using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameItem;

namespace CardClient
{
    public class CardMarshaler : Nettention.Proud.Marshaler
    {
        public static void Write(Nettention.Proud.Message msg, CardData b)
        {
            msg.Write(b.number);
            msg.Write(b.name);
            msg.Write(b.mana);
            msg.Write(b.attack);
            msg.Write(b.speed);
            msg.Write(b.a_type);
            msg.Write(b.effect);
            msg.Write(b.picture);
        }
        public static void Read(Nettention.Proud.Message msg, out CardData b)
        {
            b = new CardData();
            msg.Read(out b.number);
            msg.Read(out b.name);
            msg.Read(out b.mana);
            msg.Read(out b.attack);
            msg.Read(out b.speed);
            msg.Read(out b.a_type);
            msg.Read(out b.effect);
            msg.Read(out b.picture);
        }
    }
}
