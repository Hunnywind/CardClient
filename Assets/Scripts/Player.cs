using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameItem;

public class Player : Participant {

    [SerializeField]
    private float m_cardPosition_y;
    [SerializeField]
    private float m_battleCardPosition_y;

    protected override void Awake()
    {
        m_MaxHp = 10;
        m_preHp = 10;
        m_name = "Genji";
    }
    protected override void Start()
    {
        AddMana(5);
        StartCoroutine(CoroutineUpdate());
    }
    public void Init()
    {
        m_cardNum = CardDatabase.Instance().GetPlayerDeckCount();   
    }
    protected override void Update()
    {
    }
    public void Damaged(int fieldNum, int dmg)
    {
        foreach (var card in cards)
        {
            if (fieldNum == card.GetComponent<Card>().FieldNumber)
            {
                card.GetComponent<Card>().Attacked(dmg);
            }
        }
    }
    public override void CardArrange()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Card card = cards[i].GetComponent<Card>();
            if (card.GetCurrentPositionName().Equals("FIELD"))
            {
                card.gameObject.transform.position = LogicManager.instance.fields[card.FieldNumber - 1].transform.position;
            }
        }
    }

    public void CardInHandArrange()
    {
        Vector3 newPosition = new Vector3();
        if(LogicManager.instance.PresentLevel == Level.Init
            || LogicManager.instance.PresentLevel == Level.Init_Wait)
        {
            for (int i = 0; i < cards_hand.Count; i++)
            {
                int revision = i;
                if (revision > 4) revision -= 5;
                    newPosition.x = m_cardPositions[0].x + revision* m_interval;
                if (i< 5)
                    newPosition.y = m_cardPositions[0].y;
                else
                    newPosition.y = m_cardPositions[1].y;
                    newPosition.z = 0;

                    cards_hand[i].transform.position = newPosition;
                    cards_hand[i].GetComponentInChildren<SpriteRenderer>().sortingOrder = 2 + i;
            }
        }
        else
        {
            for (int i = 0; i < cards_hand.Count; i++)
            {
                newPosition.x = UIManager.instance.GetCardPosition(cards_hand.Count, i);
                newPosition.y = m_cardPositions[2].y;
                newPosition.z = 0;
                cards_hand[i].GetComponentInChildren<SpriteRenderer>().sortingOrder = 2 + cards_hand.Count - i;
                cards_hand[i].transform.position = newPosition;
            }
        }
    }
    public void LockAction()
    {
        foreach (var card in cards)
        {
            if(card.GetComponent<Card>().GetCurrentPositionName().Equals("FIELD"))
            {
                card.GetComponent<Card>().m_lock = true;
            }
        }
    }
    public void AddMana(int _mana)
    {
        m_mana += _mana;
    }
    public void SubtractMana(int _mana)
    {
        m_mana -= _mana;
    }
    public void CardManaCheck()
    {
        for(int i = 0; i < cards.Count; i++)
        {
            if (cards[i].GetComponent<Card>().Cardinfo.mana > m_mana
                && cards[i].GetComponent<Card>().FieldNumber == 0)
                cards[i].GetComponentInChildren<SpriteRenderer>().color = Color.gray;
            else
                cards[i].GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }

    public void AddCardHand(GameObject card)
    {
        if (!cards_hand.Contains(card))
        {
            cards_hand.Add(card);
        }
    }
    public void RemoveCardHand(GameObject card)
    {
        cards_hand.Remove(card);
    }
    public void SendInfo()
    {
        for(int i = 0; i < cards.Count; i++)
        {
            if(cards[i].GetComponent<Card>().FieldNumber != 0)
                cards[i].GetComponent<Card>().InfoSend();
        }
    }
    public void SendReturnInfo()
    {
        for(int i = 0; i < cards.Count; i++)
        {
            if(cards[i].GetComponent<Card>().m_isReturn)
            {
                GameClient.instance.SendReturnCard(cards[i].GetComponent<Card>().FieldNumber);
            }
        }
    }
    public void SendSummonInfo()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (!cards[i].GetComponent<Card>().m_lock
                && cards[i].GetComponent<Card>().GetCurrentPositionName().Equals("FIELD"))
            {
                LogicManager.instance.AddSummonCard(cards[i]);
                cards[i].GetComponent<Card>().InfoSend();
            }
        }
    }

    IEnumerator CoroutineUpdate()
    {
        while (true)
        {
            CardArrange();
            CardInHandArrange();
            yield return null;
        }
    }
}
