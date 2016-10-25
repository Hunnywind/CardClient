using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Storage : MonoBehaviour {

    [SerializeField]
    private Vector2 m_cardStartPosition;
    [SerializeField]
    private float m_blank;

    private List<GameObject> m_cards = new List<GameObject>();

    void Start()
    {
        StartCoroutine(CardArray());
    }

    public void AddCard(GameObject card)
    {
        if(!m_cards.Contains(card))
        {
            m_cards.Add(card);
        }
    }
    public void RemoveCard(GameObject card)
    {
        if(m_cards.Contains(card))
        {
            m_cards.Remove(card);
        }
    }
    public void SaveDeck()
    {
        foreach(var card in m_cards)
        {
            card.GetComponent<StorageCard>().AddDeck();
        }
    }
    IEnumerator CardArray()
    {
        while(true)
        {
            for(int i = 0; i < m_cards.Count; i++)
            {
                m_cards[i].transform.position = m_cardStartPosition + new Vector2(i * m_blank, 0);
            }
            yield return null;
        }
    }
}
