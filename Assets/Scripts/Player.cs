using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameItem;

public class Player : MonoBehaviour {
    public int cardNum;
    public int mana;

    [HideInInspector]
    public List<GameObject> cards = new List<GameObject>();
    public List<GameObject> cards_hand = new List<GameObject>();

    private List<GameObject> manaObjects = new List<GameObject>();
    private ObjectPool objpool;
    void Awake()
    {
    }
    void Start()
    {
        objpool = GameObject.Find("ManaPool").GetComponent<ObjectPool>();
        objpool.Init();
        AddMana(5);
    }
    void Update()
    {
        Vector3 newPosition = new Vector3();
        int blank = 5;
        for (int i = 0; i < cards_hand.Count; i++)
        {
            newPosition.x = ((-((float)cards_hand[i].GetComponentInChildren<SpriteRenderer>().sprite.texture.width * 0.5f + blank * 0.5f) *
                   (cards_hand.Count - 1)) +
                   (((float)cards_hand[i].GetComponentInChildren<SpriteRenderer>().sprite.texture.width + blank) * i))
                   * 1 / 100;
            newPosition.y = -5;
            newPosition.z = 0;
            
            cards_hand[i].transform.position = newPosition;
        }
        for(int i = 0; i < manaObjects.Count; i++)
        {
            newPosition.x = 8.48f;
            newPosition.y = -3.5f + i * 0.45f;
            newPosition.z = 0;
            manaObjects[i].transform.position = newPosition;
        }
    }
    public void AddMana(int _mana)
    {
        mana += _mana;
        for(int i = 0; i < _mana; i++)
        {
            manaObjects.Add(objpool.GetObject());
        }
    }
    public void SubtractMana(int _mana)
    {
        mana -= _mana;
        for (int i = 0; i < _mana; i++)
        {
            manaObjects[manaObjects.Count - 1].SetActive(false);
            manaObjects.RemoveAt(manaObjects.Count - 1);
        }
    }
    public void CardManaCheck()
    {
        for(int i = 0; i < cards.Count; i++)
        {
            if (cards[i].GetComponent<Card>().Cardinfo.mana > mana
                && cards[i].GetComponent<Card>().FieldNumber == 0)
                cards[i].GetComponentInChildren<SpriteRenderer>().color = Color.red;
            else
                cards[i].GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }
    public void CardArrange()
    {
        for(int i = 0; i < cards.Count; i++)
        {
            Card card = cards[i].GetComponent<Card>();
            if (card.FieldNumber != 0)
            {
                card.gameObject.transform.position = LogicManager.instance.fields[card.FieldNumber - 1].transform.position;
            }
        }
    }
    public void AddCardHand(GameObject card)
    {
        cards_hand.Add(card);
    }
    public void RemoveCardHand(GameObject card)
    {
        cards_hand.Remove(card);
    }
    public void SendInfo()
    {
        for(int i = 0; i < cards.Count; i++)
        {
            cards[i].GetComponent<Card>().InfoSend();
        }
        for(int i = 0; i < cards_hand.Count; i++)
        {
            cards_hand[i].GetComponent<Card>().InfoSend();
        }
    }
}
