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
    private CardInfo_send info;
    private bool isEnemyCard = false;

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
    public bool IsEnemyCard
    {
        get { return isEnemyCard; }
    }

    void Awake()
    {
        gameObject.GetComponentInParent<ObjectPool>().AddObject(gameObject);
    }

    void Start()
    {
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
    public void InfoSend()
    {
        if (fieldNumber == 0)
        {
            info.cardLocation = (int)CardLocation.HAND;
        }
        else
        {
            info.cardLocation = (int)CardLocation.FIELD;
            info.FieldLocation = fieldNumber;
        }
        info.cooltime = cardinfo.cooltime;
        info.leftcooltime = cardinfo.leftcooltime;
        GameClient.instance.SendCardInfo(info);
    }
    public void SetInfo(CardInfo_send pinfo)
    {
        isEnemyCard = true;
        info = pinfo;
        cardinfo.leftcooltime = info.leftcooltime;
        cardinfo.mana = info.mana;
        cardinfo.cooltime = info.cooltime;
        fieldNumber = info.FieldLocation;
    }
}
