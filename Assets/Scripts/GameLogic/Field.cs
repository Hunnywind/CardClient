using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour {
    public int number;
    public Card card;

    public void AddField(Card _card)
    {
        if (card != null)
            card = _card;
    }
    public void RemoveField()
    {
        card = null;
    }
}
