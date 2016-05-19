using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardTurnSort : IComparer<GameObject> {
    public int Compare(GameObject obj1, GameObject obj2)
    {
        Card card1 = obj1.GetComponent<Card>();
        Card card2 = obj2.GetComponent<Card>();

        if (card1.fieldNumber < card2.fieldNumber)
            return -1;
        else if (card1.fieldNumber > card2.fieldNumber)
            return 1;
        else if (card1.cooltime < card2.cooltime)
            return -1;
        else if (card1.cooltime > card2.cooltime)
            return 1;
        else if (card1.mana > card2.mana)
            return -1;
        else if (card1.mana < card2.mana)
            return 1;
        else
        {
            int returnValue = 0;
            returnValue = (Random.Range(0, 2) == 0) ? -1 : 1;
            return returnValue;
        }
    }
}
