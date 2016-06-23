using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameItem;

public class Enemy : MonoBehaviour
{
    public int cardNum;
    public int mana;

    [HideInInspector]
    public List<GameObject> cards = new List<GameObject>();

    public List<GameObject> cards_hand = new List<GameObject>();

    void Awake()
    {
    }
    void Update()
    {
        Vector3 newPosition = new Vector3();
        int blank = 5;
        if (LogicManager.instance.PresentLevel != Level.Init
            && LogicManager.instance.PresentLevel != Level.Init_Wait)
        {
            for (int i = 0; i < cards_hand.Count; i++)
            {
                newPosition.x = ((-((float)cards_hand[i].GetComponentInChildren<SpriteRenderer>().sprite.texture.width * 0.5f + blank * 0.5f) *
                       (cards_hand.Count - 1)) +
                       (((float)cards_hand[i].GetComponentInChildren<SpriteRenderer>().sprite.texture.width + blank) * i))
                       * 1 / 100;
                newPosition.y = 5;
                newPosition.z = 0;

                cards_hand[i].transform.position = newPosition;
            }
        }
    }
    public void CardArrange()
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
    public void CardAdd(CardInfo_send info)
    {
        ObjectPool objpool = GameObject.Find("CardPool").GetComponent<ObjectPool>();
        GameObject card = objpool.GetObject();
        card.GetComponent<Card>().SetInfo(info);
        card.GetComponent<Card>().Init();
        if (info.cardLocation == (int)CardLocation.HAND)
        {
            card.GetComponent<Card>().ChangeState(new CardStates.Hand());
            cards_hand.Add(card);
        }
        else if (info.cardLocation == (int)CardLocation.FIELD)
        {
            card.GetComponent<Card>().ChangeState(new CardStates.Battle());
            cards.Add(card);
        }
        
    }
}
