using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Storage : MonoBehaviour {

    [SerializeField]
    private Vector2 m_cardStartPosition;
    [SerializeField]
    private float m_blank;
    [SerializeField]
    private bool m_isHorizon;

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
                if(m_isHorizon)
                    m_cards[i].transform.position = m_cardStartPosition + new Vector2(i * m_blank, 0);
                else
                    m_cards[i].transform.position = m_cardStartPosition + new Vector2(0, i * m_blank);
            }
            yield return null;
        }
    }
}
