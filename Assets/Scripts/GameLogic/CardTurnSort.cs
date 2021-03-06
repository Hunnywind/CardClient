﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardTurnSort : IComparer<GameObject>
{
    // -1 = left
    // 1 = right
    private int randomCount = 0;

    public int Compare(GameObject obj1, GameObject obj2)
    {
        Card card1 = obj1.GetComponent<Card>();
        Card card2 = obj2.GetComponent<Card>();

        if (card1.FieldNumber < card2.FieldNumber)
            return -1;
        else if (card1.FieldNumber > card2.FieldNumber)
            return 1;
        return 0;
    }
}
