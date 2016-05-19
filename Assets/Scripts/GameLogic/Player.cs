using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameItem;

public class Player : MonoBehaviour {
    public int cardNum;
    public int mana;
    [HideInInspector]
    public List<GameObject> cards;
    [HideInInspector]
    public List<GameObject> cards_inHand;
    [HideInInspector]
    public Dictionary<int, GameObject> cards_inField;

    void Awake()
    {
        cards = new List<GameObject>();
        cards_inHand = new List<GameObject>();
        cards_inField = new Dictionary<int, GameObject>();
    }
    void Update()
    {
        if (Level.Battle == LogicManager.instance.level)
            Update_battle();
    }
    void Update_battle()
    {
        float odd_even = (cards_inHand.Count % 2 == 0) ? 1 : 0.5f;
        float odd_even2 = (cards_inHand.Count % 2 == 0) ? 0 : 0.5f;
        for (int i = 0; i < cards_inHand.Count; i++)
        {
            Vector3 newPosition = new Vector3(0, 0, 0);
            newPosition.x = (float)((-cards_inHand[i].GetComponent<SpriteRenderer>().sprite.texture.width * odd_even)
                - (cards_inHand[i].GetComponent<SpriteRenderer>().sprite.texture.width
                * (int)(i - cards_inHand.Count * 0.5 + odd_even2)
                ))
                * 0.01f;
            newPosition.y = -4.0f;
            newPosition.z = 0;
            cards_inHand[i].transform.position = newPosition;
        }
    }
    public void SetBattle()
    {
        foreach(GameObject card in cards_inHand)
        {
            card.AddComponent<Card_inhand>();
            card.GetComponent<Card_inhand>().mana = card.GetComponent<Card_pSetting>().mana;
            card.GetComponent<Card_pSetting>().enabled = false;
        }
        for(int i = 0; i < 5; i++)
        {
            if (cards_inField.ContainsKey(i))
            {
                GameObject newFieldCard = GameObject.Find("FieldCardPool").GetComponent<ObjectPool>().GetObject();
                newFieldCard.GetComponent<Card_infield>().CopyStat(cards_inField[i].GetComponent<Card_pSetting>());
                newFieldCard.GetComponent<Card_infield>().field
                    = LogicManager.instance.fields[i];
                newFieldCard.GetComponent<Card_infield>().Init();
                cards_inField[i].SetActive(false);
                cards.Remove(cards_inField[i]);
                cards_inField.Remove(i);
                cards.Add(newFieldCard);
                cards_inField.Add(i, newFieldCard);
            }
        }
    }
}
