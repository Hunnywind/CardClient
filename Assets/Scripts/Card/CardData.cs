using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameItem;
using UnityEngine;

class CardData
{
    static private CardData instance;
    static public CardData Instance()
    {
        if(instance == null)
        {
            instance = new CardData();
        }
        return instance;
    }

    
    public CardInfo GetCardInfo(int num)
    {
        CardInfo info = new CardInfo();
        info.mana = UnityEngine.Random.Range(1, 5);
        info.cooltime = UnityEngine.Random.Range(1, 7);
        info.cardName = "none";
        return info;
    }
}
