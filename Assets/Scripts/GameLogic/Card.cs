using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour
{
    public int fieldNumber = 0;
    public int cooltime = 0;
    public int mana = 0;
    public int leftCooltime = 0;
    public string cardName;

    protected virtual void Start()
    {
        
    }
    protected virtual void Update()
    {
    }
    protected virtual void OnMouseUp()
    {
    }
    protected virtual void OnMouseUp_Field(RaycastHit2D hit)
    {
    }
    protected virtual void OnMouseUp_Hand(RaycastHit2D hit)
    {
    }
    public void CopyStat(Card card)
    {
        fieldNumber = card.fieldNumber;
        cooltime = card.cooltime;
        mana = card.mana;
        leftCooltime = card.leftCooltime;
        cardName = card.cardName;
    }
}
