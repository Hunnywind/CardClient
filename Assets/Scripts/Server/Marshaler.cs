using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameItem;

namespace CardClient
{
    public class Marshaler : Nettention.Proud.Marshaler
    {
        public static void Write(Nettention.Proud.Message msg, CardInfo_send b)
        {
            msg.Write(b.number);
            msg.Write(b.cooltime);
            msg.Write(b.leftcooltime);
            //msg.Write(b.cardLocation);
            msg.Write(b.FieldLocation);
            msg.Write(b.isReturn);
            msg.Write(b.isEnemyCard);
            //msg.Write(b.attackType);
            //msg.Write(b.maxHp);
            //msg.Write(b.presentHp);
        }
        public static void Read(Nettention.Proud.Message msg, out CardInfo_send b)
        {
            b = new CardInfo_send();
            msg.Read(out b.number);
            msg.Read(out b.cooltime);
            msg.Read(out b.leftcooltime);
            //msg.Read(out b.cardLocation);
            msg.Read(out b.FieldLocation);
            msg.Read(out b.isReturn);
            msg.Read(out b.isEnemyCard);
            
            //msg.Read(out b.attackType);
            //msg.Read(out b.maxHp);
            //msg.Read(out b.presentHp);
        }
    }
}
