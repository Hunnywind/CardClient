using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour {
    public int number;
    public Card card;
    private bool isEnemyField;

    public bool IsEnemyField
    {
        get { return isEnemyField; }
        set { isEnemyField = value; }
    }

    public void Start()
    {
        gameObject.GetComponentInParent<ObjectPool>().AddObject(gameObject);
        isEnemyField = false;
    }
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
