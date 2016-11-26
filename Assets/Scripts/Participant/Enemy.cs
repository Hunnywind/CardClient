using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameItem;
using CardPositions;

public class Enemy : Participant
{
    private List<CardInfo_send> cards_wait = new List<CardInfo_send>();
    private List<int> return_wait = new List<int>();

    [SerializeField]
    private Sprite m_cardBackSprite;

    protected override void Awake()
    {
        m_MaxHp = 10;
        m_preHp = 10;
        m_name = "Hanjo";
    }
    protected override void Start()
    {
        base.Start();
        StartCoroutine(CoroutineUpdate());
    }
    protected override void Update()
    {
    }
    public void Damaged(int fieldNum, int dmg)
    {
        foreach(var card in cards)
        {
            if(fieldNum == card.GetComponent<Card>().FieldNumber)
            {
                card.GetComponent<Card>().Attacked(dmg);
            }
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
        if (card == null)
        {
            cards_hand.RemoveAt(cards_hand.Count - 1);
        }
        else if (cards_hand.Contains(card))
        {
            cards_hand.Remove(card);
        }
    }
    public override void CardArrange()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Card card = cards[i].GetComponent<Card>();
            if (card.FieldNumber != 0)
            {
                card.gameObject.transform.position = LogicManager.instance.enemyfields[card.FieldNumber - 1].transform.position;
            }
        }
    }
    public void CardInHandArrange()
    {
        Vector3 newPosition = new Vector3();
        for (int i = 0; i < cards_hand.Count; i++)
        {
            newPosition.x = UIManager.instance.GetCardPosition(cards_hand.Count, i);
            newPosition.y = 5;
            newPosition.z = 0;
            cards_hand[i].GetComponentInChildren<SpriteRenderer>().sprite = m_cardBackSprite;
            cards_hand[i].SendMessage("EnemyCardSetting");
            cards_hand[i].GetComponentInChildren<SpriteRenderer>().sortingOrder = 2 + cards_hand.Count - i;
            cards_hand[i].transform.position = newPosition;
        }
    }
    public void CardInit()
    {
        ObjectPool objpool = GameObject.Find("CardPool").GetComponent<ObjectPool>();
        foreach (var card in cards_wait)
        {
            GameObject newCardObj = objpool.GetObject();
            Card newCard = newCardObj.GetComponent<Card>();
            newCard.SetInfo(card);
            newCard.Init();
            newCard.ChangePosition(new InField());
            cards.Add(newCardObj);
        }
        for (int i = 0; i < m_cardNum; i++)
        {
            GameObject card = objpool.GetObject();
            CardInfo_send info;
            info.number = 0;
            info.cooltime = 0;
            info.FieldLocation = 0;
            info.leftcooltime = 0;
            info.isReturn = false;
            info.isEnemyCard = true;
            info.health = 0;
            card.GetComponent<Card>().SetInfo(info);
            card.GetComponent<Card>().Init();
        }
        CardWaitClear();
        m_cardNum = 0;
    }
    public void ReturnCardSet()
    {
        foreach(int f in return_wait)
        {
            foreach (var card in cards)
            {
                if (card.GetComponent<Card>().FieldNumber == f)
                {
                    card.GetComponent<Card>().m_isReturn = true;
                    LogicManager.instance.AddReturnCard(card.GetComponent<Card>());
                }
            }
        }
        return_wait.Clear();
    }
    public void CardClear()
    {
        for(int i = 0; i < cards.Count; i++)
        {
            cards[i].SetActive(false);
        }
        cards.Clear();

        for (int i = 0; i < cards_hand.Count; i++)
        {
            cards_hand[i].SetActive(false);
        }
        cards_hand.Clear();
    }
    public void AddCardWait(CardInfo_send info)
    {
        cards_wait.Add(info);
    }
    public void AddReturnWait(int fieldNum)
    {
        return_wait.Add(fieldNum);
    }
    private void CardWaitClear()
    {
        cards_wait.Clear();
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
