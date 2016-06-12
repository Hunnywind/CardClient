using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameItem;

public class Player : MonoBehaviour {
    public int cardNum;
    public int mana;

    [HideInInspector]
    public List<GameObject> cards;
    
    public List<GameObject> cards_hand;

    void Awake()
    {
        cards = new List<GameObject>();
        cards_hand = new List<GameObject>();
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

}
