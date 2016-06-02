using UnityEngine;
using System.Collections;
using CardStates;
using GameItem;

public class Card : MonoBehaviour
{
    private CardStateMachine<Card> stateMachine = new CardStateMachine<Card>();
    private int fieldNumber = 0;
    private CardInfo cardinfo;
    private bool attackOrder;

    public bool AttackOrder
    {
        get { return attackOrder; }
        set { attackOrder = value; }
    }
    public int FieldNumber
    {
        get { return fieldNumber; }
        set
        {
            if(value < 7)
            fieldNumber = value;
        }
    }
    public CardInfo Cardinfo
    {
        get { return cardinfo; }
    }
    void Start()
    {
        gameObject.GetComponentInParent<ObjectPool>().AddObject(gameObject);
        
        cardinfo.mana = Random.Range(1, 7);
        cardinfo.cooltime = Random.Range(1, 7);
        cardinfo.leftcooltime = cardinfo.cooltime;
        cardinfo.cardName = "Card";
    }
   
    void Update()
    {
        stateMachine.Update();
    }
    public void Init()
    {
        stateMachine.Init(this, new Setting());
    }
    public void ChangeState(CardState<Card> state)
    {
        stateMachine.ChangeState(state);
    }
    public void OnMouseDown()
    {
        stateMachine.MouseDown();
    }
    public void OnMouseDrag()
    {
        stateMachine.MouseDrag();
    }
    public void OnMouseUp()
    {
        stateMachine.MouseUp();
    }

    public void TurnStart()
    {
        if (fieldNumber == 0) return;
        cardinfo.leftcooltime--;
        if (cardinfo.leftcooltime <= 0)
        {
            cardinfo.leftcooltime = cardinfo.cooltime;
            LogicManager.instance.CardAdd(gameObject);
        }
    }
}
